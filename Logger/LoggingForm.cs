using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using Logger.Properties;

namespace Logger
{
    public partial class LoggingForm : Form
    {
        private readonly IManageLogFiles _logManager;

        public LoggingForm(IManageLogFiles logManager)
        {
            _logManager = logManager;

            InitializeComponent();

            Icon = Resources.Notepad;
            Opacity = Program.Configuration.WindowOpacity;
            
            NotifyIcon.Icon = Resources.Notepad;
            NotifyIcon.BalloonTipText = Resources.LoggerTimerElapsed;
        }

        private void LoggingFormLoad(object sender, EventArgs e)
        {
            NotifyIcon.Text = Text = string.Format("Logger - {0}", Program.Configuration.LogFileName);
            IntervalTextBox.Text = Program.Configuration.DefaultInterval.ToString(CultureInfo.InvariantCulture);

            Timer.SetIntervalInMinutes(Program.Configuration.DefaultInterval);
            Timer.Enabled = false;
        }

        private void LoggingButtonClick(object sender, EventArgs e)
        {
            WriteLogLine();
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
            OpenLogFile();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch(keyData)
            {
                case Keys.Escape:
                    WindowState = FormWindowState.Minimized;
                    return true;
                case (Keys.L | Keys.Control):
                    WriteLogLine();
                    return true;
                case (Keys.O | Keys.Control | Keys.Shift):
                    OpenLogFile();
                    return true;
                case (Keys.F | Keys.Control | Keys.Shift):
                    OpenLogFolder();
                    return true;
                case (Keys.D | Keys.Control | Keys.Shift):
                    DeleteLogFile();
                    return true;
                case (Keys.A | Keys.Control | Keys.Shift):
                    ArchiveLogFile();
                    return true;
                case (Keys.LWin | Keys.Control):
                case (Keys.RWin | Keys.Control):
                    WindowState = FormWindowState.Normal;
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void WriteLogLine()
        {
            if (string.IsNullOrWhiteSpace(LoggingTextBox.Text))
                return;

            _logManager.WriteLine(LoggingTextBox.Text);

            LoggingTextBox.Text = string.Empty;
            LoggingTextBox.Focus();
        }

        private void OpenLogFile()
        {
            if (_logManager.Exists())
                Process.Start(Program.Configuration.LogFileName);
        }

        private void OpenLogFolder()
        {
            if (string.IsNullOrEmpty(Environment.GetCommandLineArgs()[0]))
                return;

            Process.Start(Environment.GetCommandLineArgs()[0]);
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
            Timer.SetIntervalInMinutes(int.Parse(IntervalTextBox.Text));
        }

        private void TimerTick(object sender, EventArgs e)
        {
            ToggleTimer();

            NotifyIcon.ShowBalloonTip(30*1000);

            NotifyIcon.Text = Text = string.Format("Logger - {0}", Program.Configuration.LogFileName);
        }

        private void ToggleTimer()
        {
            Timer.ToggleEnabled();
            TimerButton.Text = Timer.Enabled ? "&Stop" : "&Start";

            if (Timer.Enabled)
                WindowState = FormWindowState.Minimized;

            NotifyIcon.Text = Text = string.Format("Logger - {0}{1}", Program.Configuration.LogFileName, Timer.Enabled ? " - Timer Running" : string.Empty);
        }

        private void FolderButtonClick(object sender, EventArgs e)
        {
            OpenLogFolder();
        }

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }
    }
}