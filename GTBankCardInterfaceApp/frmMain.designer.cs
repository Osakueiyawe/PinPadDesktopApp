namespace GTBankCardInterfaceApp
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txtActivity = new System.Windows.Forms.TextBox();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.lblToMonitor = new System.Windows.Forms.Label();
            this.lblActivity = new System.Windows.Forms.Label();
            this.PinPadNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.CDCSMonitorMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CDCSMonitorMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtActivity
            // 
            this.txtActivity.Location = new System.Drawing.Point(12, 51);
            this.txtActivity.Multiline = true;
            this.txtActivity.Name = "txtActivity";
            this.txtActivity.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtActivity.Size = new System.Drawing.Size(717, 425);
            this.txtActivity.TabIndex = 2;
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(106, 6);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.ReadOnly = true;
            this.txtFolderPath.Size = new System.Drawing.Size(623, 20);
            this.txtFolderPath.TabIndex = 3;
            // 
            // lblToMonitor
            // 
            this.lblToMonitor.AutoSize = true;
            this.lblToMonitor.Location = new System.Drawing.Point(12, 9);
            this.lblToMonitor.Name = "lblToMonitor";
            this.lblToMonitor.Size = new System.Drawing.Size(88, 13);
            this.lblToMonitor.TabIndex = 5;
            this.lblToMonitor.Text = "Folder to monitor:";
            // 
            // lblActivity
            // 
            this.lblActivity.AutoSize = true;
            this.lblActivity.Location = new System.Drawing.Point(12, 35);
            this.lblActivity.Name = "lblActivity";
            this.lblActivity.Size = new System.Drawing.Size(41, 13);
            this.lblActivity.TabIndex = 6;
            this.lblActivity.Text = "Activity";
            // 
            // PinPadNotifyIcon
            // 
            this.PinPadNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("PinPadNotifyIcon.Icon")));
            this.PinPadNotifyIcon.Text = "PinPadNotifier";
            this.PinPadNotifyIcon.Visible = true;
            this.PinPadNotifyIcon.BalloonTipClicked += new System.EventHandler(this.PinPadNotifyIcon_BalloonTipClicked);
            // 
            // CDCSMonitorMenu
            // 
            this.CDCSMonitorMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.CDCSMonitorMenu.Name = "CDCSMonitorMenu";
            this.CDCSMonitorMenu.Size = new System.Drawing.Size(110, 48);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 481);
            this.Controls.Add(this.lblToMonitor);
            this.Controls.Add(this.txtActivity);
            this.Controls.Add(this.lblActivity);
            this.Controls.Add(this.txtFolderPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "PinPadOverideRequest";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.CDCSMonitorMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtActivity;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Label lblToMonitor;
        private System.Windows.Forms.Label lblActivity;
        private System.Windows.Forms.NotifyIcon PinPadNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip CDCSMonitorMenu;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
    }
}

