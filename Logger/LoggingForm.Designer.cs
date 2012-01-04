namespace Logger
{
    partial class LoggingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LoggingButton = new System.Windows.Forms.Button();
            this.LoggingTextBox = new System.Windows.Forms.TextBox();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.OpenButton = new System.Windows.Forms.Button();
            this.TimerButton = new System.Windows.Forms.Button();
            this.IntervalTextBox = new System.Windows.Forms.TextBox();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.FolderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LoggingButton
            // 
            this.LoggingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoggingButton.Location = new System.Drawing.Point(345, 151);
            this.LoggingButton.Name = "LoggingButton";
            this.LoggingButton.Size = new System.Drawing.Size(75, 31);
            this.LoggingButton.TabIndex = 1;
            this.LoggingButton.Text = "&Log";
            this.LoggingButton.UseVisualStyleBackColor = true;
            this.LoggingButton.Click += new System.EventHandler(this.LoggingButtonClick);
            // 
            // LoggingTextBox
            // 
            this.LoggingTextBox.AcceptsReturn = true;
            this.LoggingTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoggingTextBox.Location = new System.Drawing.Point(12, 12);
            this.LoggingTextBox.Multiline = true;
            this.LoggingTextBox.Name = "LoggingTextBox";
            this.LoggingTextBox.Size = new System.Drawing.Size(415, 132);
            this.LoggingTextBox.TabIndex = 0;
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.DoubleClick += new System.EventHandler(this.NotifyIconDoubleClick);
            // 
            // OpenButton
            // 
            this.OpenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenButton.Location = new System.Drawing.Point(264, 151);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 31);
            this.OpenButton.TabIndex = 2;
            this.OpenButton.Text = "&Open";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButtonClick);
            // 
            // TimerButton
            // 
            this.TimerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TimerButton.Location = new System.Drawing.Point(12, 151);
            this.TimerButton.Name = "TimerButton";
            this.TimerButton.Size = new System.Drawing.Size(75, 31);
            this.TimerButton.TabIndex = 3;
            this.TimerButton.Text = "&Start";
            this.TimerButton.UseVisualStyleBackColor = true;
            this.TimerButton.Click += new System.EventHandler(this.TimerButtonClick);
            // 
            // IntervalTextBox
            // 
            this.IntervalTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.IntervalTextBox.Location = new System.Drawing.Point(93, 155);
            this.IntervalTextBox.Name = "IntervalTextBox";
            this.IntervalTextBox.Size = new System.Drawing.Size(56, 22);
            this.IntervalTextBox.TabIndex = 4;
            // 
            // Timer
            // 
            this.Timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // FolderButton
            // 
            this.FolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FolderButton.Location = new System.Drawing.Point(157, 151);
            this.FolderButton.Name = "FolderButton";
            this.FolderButton.Size = new System.Drawing.Size(75, 31);
            this.FolderButton.TabIndex = 5;
            this.FolderButton.Text = "&Folder";
            this.FolderButton.UseVisualStyleBackColor = true;
            this.FolderButton.Click += new System.EventHandler(this.FolderButtonClick);
            // 
            // LoggingForm
            // 
            this.AcceptButton = this.LoggingButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 193);
            this.Controls.Add(this.FolderButton);
            this.Controls.Add(this.IntervalTextBox);
            this.Controls.Add(this.TimerButton);
            this.Controls.Add(this.OpenButton);
            this.Controls.Add(this.LoggingTextBox);
            this.Controls.Add(this.LoggingButton);
            this.MinimumSize = new System.Drawing.Size(435, 175);
            this.Name = "LoggingForm";
            this.Opacity = 0.5D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoggingForm";
            this.Load += new System.EventHandler(this.LoggingFormLoad);
            this.Resize += new System.EventHandler(this.LoggingFormResize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoggingButton;
        private System.Windows.Forms.TextBox LoggingTextBox;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.Button TimerButton;
        private System.Windows.Forms.TextBox IntervalTextBox;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.Button FolderButton;
    }
}