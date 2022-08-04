namespace GTBankCardInterfaceApp
{
    partial class Welcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlStrpGeneral = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewCusDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDeposit = new System.Windows.Forms.ToolStripMenuItem();
            this.cardLessDepositsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cardLessWithdrawalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cardLessDepositMobileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cardLessWithDrawalMobileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlStrpApprove = new System.Windows.Forms.ToolStripMenuItem();
            this.approvalScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.depositApprovalScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.poToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetReceiptForReprintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlStrpReport = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printReceiptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlStrpAdminReport = new System.Windows.Forms.ToolStripMenuItem();
            this.logByDateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.logByBranchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.logByAmountToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.manageLogoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlStrpHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadHelpFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblUserName = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.tlStrpGeneral,
            this.tlStrpApprove,
            this.tlStrpReport,
            this.tlStrpAdminReport,
            this.tlStrpHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(765, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(59, 20);
            this.toolStripMenuItem1.Text = "General";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
            // 
            // tlStrpGeneral
            // 
            this.tlStrpGeneral.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewCusDetails,
            this.toolStripDeposit,
            this.cardLessDepositsToolStripMenuItem,
            this.cardLessWithdrawalToolStripMenuItem,
            this.cardLessDepositMobileToolStripMenuItem,
            this.cardLessWithDrawalMobileToolStripMenuItem});
            this.tlStrpGeneral.Name = "tlStrpGeneral";
            this.tlStrpGeneral.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.G)));
            this.tlStrpGeneral.Size = new System.Drawing.Size(124, 20);
            this.tlStrpGeneral.Text = "&Initiate Transactions";
            this.tlStrpGeneral.Click += new System.EventHandler(this.tlStrpGeneral_Click);
            // 
            // mnuViewCusDetails
            // 
            this.mnuViewCusDetails.Name = "mnuViewCusDetails";
            this.mnuViewCusDetails.Size = new System.Drawing.Size(233, 22);
            this.mnuViewCusDetails.Text = "Card Withdrawal Transactions";
            this.mnuViewCusDetails.Click += new System.EventHandler(this.mnuViewCusDetails_Click);
            // 
            // toolStripDeposit
            // 
            this.toolStripDeposit.Name = "toolStripDeposit";
            this.toolStripDeposit.Size = new System.Drawing.Size(233, 22);
            this.toolStripDeposit.Text = "CardHolder Deposits";
            this.toolStripDeposit.Click += new System.EventHandler(this.toolStripDeposit_Click);
            // 
            // cardLessDepositsToolStripMenuItem
            // 
            this.cardLessDepositsToolStripMenuItem.Name = "cardLessDepositsToolStripMenuItem";
            this.cardLessDepositsToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.cardLessDepositsToolStripMenuItem.Text = "CardLess Deposits";
            this.cardLessDepositsToolStripMenuItem.Click += new System.EventHandler(this.cardLessDepositsToolStripMenuItem_Click);
            // 
            // cardLessWithdrawalToolStripMenuItem
            // 
            this.cardLessWithdrawalToolStripMenuItem.Name = "cardLessWithdrawalToolStripMenuItem";
            this.cardLessWithdrawalToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.cardLessWithdrawalToolStripMenuItem.Text = "CardLess Withdrawal";
            this.cardLessWithdrawalToolStripMenuItem.Click += new System.EventHandler(this.cardLessWithdrawalToolStripMenuItem_Click);
            // 
            // cardLessDepositMobileToolStripMenuItem
            // 
            this.cardLessDepositMobileToolStripMenuItem.Name = "cardLessDepositMobileToolStripMenuItem";
            this.cardLessDepositMobileToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.cardLessDepositMobileToolStripMenuItem.Text = "CardLess Deposit - Mobile";
            this.cardLessDepositMobileToolStripMenuItem.Click += new System.EventHandler(this.cardLessDepositMobileToolStripMenuItem_Click);
            // 
            // cardLessWithDrawalMobileToolStripMenuItem
            // 
            this.cardLessWithDrawalMobileToolStripMenuItem.Enabled = false;
            this.cardLessWithDrawalMobileToolStripMenuItem.Name = "cardLessWithDrawalMobileToolStripMenuItem";
            this.cardLessWithDrawalMobileToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.cardLessWithDrawalMobileToolStripMenuItem.Text = "CardLess WithDrawal - Mobile";
            this.cardLessWithDrawalMobileToolStripMenuItem.Click += new System.EventHandler(this.cardLessWithDrawalMobileToolStripMenuItem_Click);
            // 
            // tlStrpApprove
            // 
            this.tlStrpApprove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.approvalScreenToolStripMenuItem,
            this.depositApprovalScreenToolStripMenuItem,
            this.poToolStripMenuItem,
            this.resetReceiptForReprintToolStripMenuItem});
            this.tlStrpApprove.Name = "tlStrpApprove";
            this.tlStrpApprove.Size = new System.Drawing.Size(133, 20);
            this.tlStrpApprove.Text = "Approve Transactions";
            this.tlStrpApprove.Click += new System.EventHandler(this.tlStrpApprove_Click);
            // 
            // approvalScreenToolStripMenuItem
            // 
            this.approvalScreenToolStripMenuItem.Name = "approvalScreenToolStripMenuItem";
            this.approvalScreenToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.approvalScreenToolStripMenuItem.Text = "Withdrawal Approval Screen";
            this.approvalScreenToolStripMenuItem.Click += new System.EventHandler(this.approvalScreenToolStripMenuItem_Click);
            // 
            // depositApprovalScreenToolStripMenuItem
            // 
            this.depositApprovalScreenToolStripMenuItem.Name = "depositApprovalScreenToolStripMenuItem";
            this.depositApprovalScreenToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.depositApprovalScreenToolStripMenuItem.Text = "Deposit Approval Screen";
            this.depositApprovalScreenToolStripMenuItem.Click += new System.EventHandler(this.depositApprovalScreenToolStripMenuItem_Click);
            // 
            // poToolStripMenuItem
            // 
            this.poToolStripMenuItem.Name = "poToolStripMenuItem";
            this.poToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.poToolStripMenuItem.Text = "Post Offline Transactions";
            this.poToolStripMenuItem.Click += new System.EventHandler(this.poToolStripMenuItem_Click);
            // 
            // resetReceiptForReprintToolStripMenuItem
            // 
            this.resetReceiptForReprintToolStripMenuItem.Name = "resetReceiptForReprintToolStripMenuItem";
            this.resetReceiptForReprintToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.resetReceiptForReprintToolStripMenuItem.Text = "Reset Receipt For Reprint";
            this.resetReceiptForReprintToolStripMenuItem.Click += new System.EventHandler(this.resetReceiptForReprintToolStripMenuItem_Click);
            // 
            // tlStrpReport
            // 
            this.tlStrpReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transactionReportToolStripMenuItem,
            this.printReceiptToolStripMenuItem});
            this.tlStrpReport.Name = "tlStrpReport";
            this.tlStrpReport.Size = new System.Drawing.Size(99, 20);
            this.tlStrpReport.Text = "&Branch Reports";
            // 
            // transactionReportToolStripMenuItem
            // 
            this.transactionReportToolStripMenuItem.Name = "transactionReportToolStripMenuItem";
            this.transactionReportToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.transactionReportToolStripMenuItem.Text = "Transaction Report";
            this.transactionReportToolStripMenuItem.Click += new System.EventHandler(this.transactionReportToolStripMenuItem_Click);
            // 
            // printReceiptToolStripMenuItem
            // 
            this.printReceiptToolStripMenuItem.Name = "printReceiptToolStripMenuItem";
            this.printReceiptToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.printReceiptToolStripMenuItem.Text = "Print Receipt";
            this.printReceiptToolStripMenuItem.Click += new System.EventHandler(this.printReceiptToolStripMenuItem_Click);
            // 
            // tlStrpAdminReport
            // 
            this.tlStrpAdminReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logByDateToolStripMenuItem1,
            this.logByBranchToolStripMenuItem1,
            this.logByAmountToolStripMenuItem1,
            this.manageLogoToolStripMenuItem});
            this.tlStrpAdminReport.Name = "tlStrpAdminReport";
            this.tlStrpAdminReport.Size = new System.Drawing.Size(55, 20);
            this.tlStrpAdminReport.Text = "Admin";
            // 
            // logByDateToolStripMenuItem1
            // 
            this.logByDateToolStripMenuItem1.Name = "logByDateToolStripMenuItem1";
            this.logByDateToolStripMenuItem1.Size = new System.Drawing.Size(157, 22);
            this.logByDateToolStripMenuItem1.Text = "Log By Date";
            // 
            // logByBranchToolStripMenuItem1
            // 
            this.logByBranchToolStripMenuItem1.Name = "logByBranchToolStripMenuItem1";
            this.logByBranchToolStripMenuItem1.Size = new System.Drawing.Size(157, 22);
            this.logByBranchToolStripMenuItem1.Text = "Log By Branch";
            // 
            // logByAmountToolStripMenuItem1
            // 
            this.logByAmountToolStripMenuItem1.Name = "logByAmountToolStripMenuItem1";
            this.logByAmountToolStripMenuItem1.Size = new System.Drawing.Size(157, 22);
            this.logByAmountToolStripMenuItem1.Text = "Log By Amount";
            // 
            // manageLogoToolStripMenuItem
            // 
            this.manageLogoToolStripMenuItem.Name = "manageLogoToolStripMenuItem";
            this.manageLogoToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.manageLogoToolStripMenuItem.Text = "Manage Logo";
            this.manageLogoToolStripMenuItem.Click += new System.EventHandler(this.manageLogoToolStripMenuItem_Click);
            // 
            // tlStrpHelp
            // 
            this.tlStrpHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadHelpFileToolStripMenuItem});
            this.tlStrpHelp.Name = "tlStrpHelp";
            this.tlStrpHelp.Size = new System.Drawing.Size(44, 20);
            this.tlStrpHelp.Text = "&Help";
            // 
            // downloadHelpFileToolStripMenuItem
            // 
            this.downloadHelpFileToolStripMenuItem.Name = "downloadHelpFileToolStripMenuItem";
            this.downloadHelpFileToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.downloadHelpFileToolStripMenuItem.Text = "&Download Help File";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(730, 9);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(0, 15);
            this.lblUserName.TabIndex = 2;
            // 
            // Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 602);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Welcome";
            this.Text = "GTBank Card Interface Application - Welcome   (1.8.0)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Welcome_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Welcome_FormClosed);
            this.Load += new System.EventHandler(this.Welcome_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Welcome_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tlStrpGeneral;
        private System.Windows.Forms.ToolStripMenuItem mnuViewCusDetails;
        private System.Windows.Forms.ToolStripMenuItem tlStrpReport;
        private System.Windows.Forms.ToolStripMenuItem tlStrpHelp;
        private System.Windows.Forms.ToolStripMenuItem downloadHelpFileToolStripMenuItem;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.ToolStripMenuItem tlStrpApprove;
        private System.Windows.Forms.ToolStripMenuItem approvalScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transactionReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tlStrpAdminReport;
        private System.Windows.Forms.ToolStripMenuItem logByDateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem logByBranchToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem logByAmountToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem printReceiptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripDeposit;
        private System.Windows.Forms.ToolStripMenuItem manageLogoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem depositApprovalScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem poToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetReceiptForReprintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cardLessDepositsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cardLessWithdrawalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cardLessDepositMobileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cardLessWithDrawalMobileToolStripMenuItem;
    }
}