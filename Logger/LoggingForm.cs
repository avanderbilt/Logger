using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using Logger.Properties;
using LoggerLibrary;

namespace Logger
{
    public partial class LoggingForm : Form
    {
        private readonly IManageLogFiles _logManager;
        private readonly IManageConfiguration _configurationManager;
        private readonly IManageFiles _fileManager;

        public LoggingForm(IManageLogFiles logManager, IManageConfiguration configurationManager, IManageFiles fileManager)
        {
            _logManager = logManager;
            _configurationManager = configurationManager;
            _fileManager = fileManager;

            InitializeComponent();

            Icon = Resources.MainIcon;
            Opacity = _configurationManager.WindowOpacity;
            NotifyIcon.Icon = Resources.MainIcon;
        }

        private void LoggingFormLoad(object sender, EventArgs e)
        {
            NotifyIcon.Text = Text = string.Format("Logger - {0}", _configurationManager.FileName);
            IntervalTextBox.Text = _configurationManager.DefaultInterval.ToString(CultureInfo.InvariantCulture);

            Timer.SetIntevalInMinutes(_configurationManager.DefaultInterval);
            Timer.Enabled = false;
        }

        private void LoggingButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoggingTextBox.Text))
                return;

            _logManager.WriteLine(LoggingTextBox.Text);

            LoggingTextBox.Text = string.Empty;
            LoggingTextBox.Focus();
        }

        private void LoggingFormResize(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Minimized:
                    NotifyIcon.Visible = true;
                    ShowInTaskbar = false;
                    break;
                case FormWindowState.Normal:
                    NotifyIcon.Visible = false;
                    ShowInTaskbar = true;
                    BringToFront();
                    break;
            }
        }

        private void NotifyIconDoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void OpenButtonClick(object sender, EventArgs e)
        {
            if (_logManager.Exists())
                Process.Start(_configurationManager.FileName);                
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch(keyData)
            {
                case Keys.Escape:
                    WindowState = FormWindowState.Minimized;
                    return true;
                case (Keys.D | Keys.Control | Keys.Shift):
                    DeleteLogFile();
                    return true;
                case (Keys.A | Keys.Control | Keys.Shift):
                    ArchiveLogFile();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void DeleteLogFile()
        {
            if (!_logManager.Exists())
                return;

            var result = MessageBox.Show(this, Resources.DeleteLogFileQuestion, Resources.DeleteLogFileCaption,
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
                _logManager.Delete();
        }

        private void ArchiveLogFile()
        {
            if (!_logManager.Exists())
                return;

            var result = MessageBox.Show(this, Resources.ArchiveLogFileQuestion, Resources.ArchiveLogFileCaption,
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
                _logManager.Archive();
        }

        private void TimerButtonClick(object sender, EventArgs e)
        {
            ToggleTimer();
            Timer.SetIntevalInMinutes(int.Parse(IntervalTextBox.Text));
        }

        private void TimerTick(object sender, EventArgs e)
        {
            ToggleTimer();
            WindowState = FormWindowState.Normal;
        }

        private void ToggleTimer()
        {
            Timer.ToggleEnabled();
            TimerButton.Text = Timer.Enabled ? "&Stop" : "&Start";

            if (Timer.Enabled)
                WindowState = FormWindowState.Minimized;
        }

        private void FolderButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_fileManager.ProgramDirectory))
                return;

            Process.Start(_fileManager.ProgramDirectory);
        }
    }
}