namespace GTBankCardInterfaceApp
{
    partial class CardLessDeposit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardLessDeposit));
            this.pnlaccts = new System.Windows.Forms.Panel();
            this.pnlNuban = new System.Windows.Forms.Panel();
            this.txtNubanAcct = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnClearScreen = new System.Windows.Forms.Button();
            this.btnPinPadDetails = new System.Windows.Forms.Button();
            this.pnlAcctDetails = new System.Windows.Forms.Panel();
            this.txtSubAcctCode = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLedCode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCurCode = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblBraCode = new System.Windows.Forms.Label();
            this.txtBraCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCusNum = new System.Windows.Forms.TextBox();
            this.lblCustomerLevel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbAccountType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pnlTransaction = new System.Windows.Forms.Panel();
            this.txtCusName = new System.Windows.Forms.TextBox();
            this.lblAccountName = new System.Windows.Forms.Label();
            this.btnCashAnalysis = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblAnalysis = new System.Windows.Forms.Label();
            this.lblTransProgress = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtTransAmount = new System.Windows.Forms.TextBox();
            this.btnSubmitDr = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTellerTillDr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCusAcctCredit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPostProgress = new System.Windows.Forms.Label();
            this.PostingprogressBar = new System.Windows.Forms.ProgressBar();
            this.lstRestrictions = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlaccts.SuspendLayout();
            this.pnlNuban.SuspendLayout();
            this.pnlAcctDetails.SuspendLayout();
            this.pnlTransaction.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlaccts
            // 
            this.pnlaccts.Controls.Add(this.pnlNuban);
            this.pnlaccts.Controls.Add(this.btnClearScreen);
            this.pnlaccts.Controls.Add(this.btnPinPadDetails);
            this.pnlaccts.Controls.Add(this.pnlAcctDetails);
            this.pnlaccts.Location = new System.Drawing.Point(17, 133);
            this.pnlaccts.Margin = new System.Windows.Forms.Padding(4);
            this.pnlaccts.Name = "pnlaccts";
            this.pnlaccts.Size = new System.Drawing.Size(828, 173);
            this.pnlaccts.TabIndex = 3;
            this.pnlaccts.Visible = false;
            // 
            // pnlNuban
            // 
            this.pnlNuban.Controls.Add(this.txtNubanAcct);
            this.pnlNuban.Controls.Add(this.label11);
            this.pnlNuban.Location = new System.Drawing.Point(4, 61);
            this.pnlNuban.Name = "pnlNuban";
            this.pnlNuban.Size = new System.Drawing.Size(811, 35);
            this.pnlNuban.TabIndex = 3;
            // 
            // txtNubanAcct
            // 
            this.txtNubanAcct.Location = new System.Drawing.Point(219, 5);
            this.txtNubanAcct.MaxLength = 10;
            this.txtNubanAcct.Name = "txtNubanAcct";
            this.txtNubanAcct.Size = new System.Drawing.Size(232, 24);
            this.txtNubanAcct.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1, 8);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(205, 18);
            this.label11.TabIndex = 0;
            this.label11.Text = "NUBAN Account Number :";
            // 
            // btnClearScreen
            // 
            this.btnClearScreen.Location = new System.Drawing.Point(670, 136);
            this.btnClearScreen.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearScreen.Name = "btnClearScreen";
            this.btnClearScreen.Size = new System.Drawing.Size(142, 31);
            this.btnClearScreen.TabIndex = 9;
            this.btnClearScreen.Text = "Clear Screen";
            this.btnClearScreen.UseVisualStyleBackColor = true;
            this.btnClearScreen.Click += new System.EventHandler(this.btnClearScreen_Click);
            // 
            // btnPinPadDetails
            // 
            this.btnPinPadDetails.Location = new System.Drawing.Point(603, 102);
            this.btnPinPadDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnPinPadDetails.Name = "btnPinPadDetails";
            this.btnPinPadDetails.Size = new System.Drawing.Size(209, 31);
            this.btnPinPadDetails.TabIndex = 8;
            this.btnPinPadDetails.Text = "Get Account Details";
            this.btnPinPadDetails.UseVisualStyleBackColor = true;
            this.btnPinPadDetails.Click += new System.EventHandler(this.btnPinPadDetails_Click);
            // 
            // pnlAcctDetails
            // 
            this.pnlAcctDetails.Controls.Add(this.txtSubAcctCode);
            this.pnlAcctDetails.Controls.Add(this.label14);
            this.pnlAcctDetails.Controls.Add(this.txtLedCode);
            this.pnlAcctDetails.Controls.Add(this.label13);
            this.pnlAcctDetails.Controls.Add(this.txtCurCode);
            this.pnlAcctDetails.Controls.Add(this.label12);
            this.pnlAcctDetails.Controls.Add(this.lblBraCode);
            this.pnlAcctDetails.Controls.Add(this.txtBraCode);
            this.pnlAcctDetails.Controls.Add(this.label2);
            this.pnlAcctDetails.Controls.Add(this.txtCusNum);
            this.pnlAcctDetails.Location = new System.Drawing.Point(4, 5);
            this.pnlAcctDetails.Margin = new System.Windows.Forms.Padding(4);
            this.pnlAcctDetails.Name = "pnlAcctDetails";
            this.pnlAcctDetails.Size = new System.Drawing.Size(811, 49);
            this.pnlAcctDetails.TabIndex = 2;
            // 
            // txtSubAcctCode
            // 
            this.txtSubAcctCode.Location = new System.Drawing.Point(730, 11);
            this.txtSubAcctCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubAcctCode.MaxLength = 6;
            this.txtSubAcctCode.Name = "txtSubAcctCode";
            this.txtSubAcctCode.Size = new System.Drawing.Size(59, 24);
            this.txtSubAcctCode.TabIndex = 9;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(610, 15);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(115, 18);
            this.label14.TabIndex = 8;
            this.label14.Text = "SubAcctCode:";
            // 
            // txtLedCode
            // 
            this.txtLedCode.Location = new System.Drawing.Point(558, 12);
            this.txtLedCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtLedCode.MaxLength = 6;
            this.txtLedCode.Name = "txtLedCode";
            this.txtLedCode.Size = new System.Drawing.Size(52, 24);
            this.txtLedCode.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(474, 15);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 18);
            this.label13.TabIndex = 6;
            this.label13.Text = "LedCode:";
            // 
            // txtCurCode
            // 
            this.txtCurCode.Location = new System.Drawing.Point(426, 12);
            this.txtCurCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtCurCode.MaxLength = 3;
            this.txtCurCode.Name = "txtCurCode";
            this.txtCurCode.Size = new System.Drawing.Size(49, 24);
            this.txtCurCode.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(345, 15);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 18);
            this.label12.TabIndex = 4;
            this.label12.Text = "CurCode:";
            // 
            // lblBraCode
            // 
            this.lblBraCode.AutoSize = true;
            this.lblBraCode.Location = new System.Drawing.Point(7, 15);
            this.lblBraCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBraCode.Name = "lblBraCode";
            this.lblBraCode.Size = new System.Drawing.Size(79, 18);
            this.lblBraCode.TabIndex = 1;
            this.lblBraCode.Text = "BraCode:";
            // 
            // txtBraCode
            // 
            this.txtBraCode.Location = new System.Drawing.Point(94, 12);
            this.txtBraCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtBraCode.MaxLength = 3;
            this.txtBraCode.Name = "txtBraCode";
            this.txtBraCode.Size = new System.Drawing.Size(59, 24);
            this.txtBraCode.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "CusNum:";
            // 
            // txtCusNum
            // 
            this.txtCusNum.Location = new System.Drawing.Point(233, 12);
            this.txtCusNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtCusNum.MaxLength = 6;
            this.txtCusNum.Name = "txtCusNum";
            this.txtCusNum.Size = new System.Drawing.Size(111, 24);
            this.txtCusNum.TabIndex = 4;
            // 
            // lblCustomerLevel
            // 
            this.lblCustomerLevel.AutoSize = true;
            this.lblCustomerLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerLevel.Location = new System.Drawing.Point(210, 41);
            this.lblCustomerLevel.Name = "lblCustomerLevel";
            this.lblCustomerLevel.Size = new System.Drawing.Size(16, 24);
            this.lblCustomerLevel.TabIndex = 7;
            this.lblCustomerLevel.Text = ".";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Customer Level:";
            // 
            // cmbAccountType
            // 
            this.cmbAccountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccountType.FormattingEnabled = true;
            this.cmbAccountType.Items.AddRange(new object[] {
            "--Please Select--",
            "NUBAN",
            "OLD ACCOUNT NUMBER"});
            this.cmbAccountType.Location = new System.Drawing.Point(228, 102);
            this.cmbAccountType.Name = "cmbAccountType";
            this.cmbAccountType.Size = new System.Drawing.Size(268, 26);
            this.cmbAccountType.TabIndex = 1;
            this.cmbAccountType.SelectedIndexChanged += new System.EventHandler(this.cmbAccountType_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(204, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "Account Number System: ";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.ForeColor = System.Drawing.Color.Red;
            this.lblProgress.Location = new System.Drawing.Point(13, 0);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(17, 24);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "-";
            // 
            // pnlTransaction
            // 
            this.pnlTransaction.Controls.Add(this.lstRestrictions);
            this.pnlTransaction.Controls.Add(this.label7);
            this.pnlTransaction.Controls.Add(this.txtCusName);
            this.pnlTransaction.Controls.Add(this.lblAccountName);
            this.pnlTransaction.Controls.Add(this.btnCashAnalysis);
            this.pnlTransaction.Controls.Add(this.label15);
            this.pnlTransaction.Controls.Add(this.label10);
            this.pnlTransaction.Controls.Add(this.lblCustomerLevel);
            this.pnlTransaction.Controls.Add(this.lblAnalysis);
            this.pnlTransaction.Controls.Add(this.lblTransProgress);
            this.pnlTransaction.Controls.Add(this.label3);
            this.pnlTransaction.Controls.Add(this.btnCancel);
            this.pnlTransaction.Controls.Add(this.txtTransAmount);
            this.pnlTransaction.Controls.Add(this.btnSubmitDr);
            this.pnlTransaction.Controls.Add(this.label4);
            this.pnlTransaction.Controls.Add(this.label1);
            this.pnlTransaction.Controls.Add(this.txtTellerTillDr);
            this.pnlTransaction.Controls.Add(this.label5);
            this.pnlTransaction.Controls.Add(this.txtCusAcctCredit);
            this.pnlTransaction.Controls.Add(this.label6);
            this.pnlTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTransaction.Location = new System.Drawing.Point(17, 320);
            this.pnlTransaction.Margin = new System.Windows.Forms.Padding(4);
            this.pnlTransaction.Name = "pnlTransaction";
            this.pnlTransaction.Size = new System.Drawing.Size(828, 376);
            this.pnlTransaction.TabIndex = 3;
            this.pnlTransaction.Visible = false;
            // 
            // txtCusName
            // 
            this.txtCusName.Location = new System.Drawing.Point(209, 84);
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.Size = new System.Drawing.Size(377, 24);
            this.txtCusName.TabIndex = 45;
            // 
            // lblAccountName
            // 
            this.lblAccountName.AutoSize = true;
            this.lblAccountName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountName.Location = new System.Drawing.Point(211, 16);
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Size = new System.Drawing.Size(14, 20);
            this.lblAccountName.TabIndex = 5;
            this.lblAccountName.Text = ":";
            // 
            // btnCashAnalysis
            // 
            this.btnCashAnalysis.Location = new System.Drawing.Point(648, 238);
            this.btnCashAnalysis.Name = "btnCashAnalysis";
            this.btnCashAnalysis.Size = new System.Drawing.Size(143, 35);
            this.btnCashAnalysis.TabIndex = 13;
            this.btnCashAnalysis.Text = "Cash Analysis";
            this.btnCashAnalysis.UseVisualStyleBackColor = true;
            this.btnCashAnalysis.Click += new System.EventHandler(this.btnCashAnalysis_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(36, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(123, 18);
            this.label15.TabIndex = 4;
            this.label15.Text = "Account Name:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(36, 210);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 18);
            this.label10.TabIndex = 10;
            this.label10.Text = "Cash Analysis:";
            // 
            // lblAnalysis
            // 
            this.lblAnalysis.AutoSize = true;
            this.lblAnalysis.Location = new System.Drawing.Point(206, 210);
            this.lblAnalysis.Name = "lblAnalysis";
            this.lblAnalysis.Size = new System.Drawing.Size(14, 18);
            this.lblAnalysis.TabIndex = 44;
            this.lblAnalysis.Text = "-";
            // 
            // lblTransProgress
            // 
            this.lblTransProgress.AutoSize = true;
            this.lblTransProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTransProgress.Location = new System.Drawing.Point(1048, 254);
            this.lblTransProgress.Name = "lblTransProgress";
            this.lblTransProgress.Size = new System.Drawing.Size(0, 18);
            this.lblTransProgress.TabIndex = 36;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(209, 235);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(179, 41);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtTransAmount
            // 
            this.txtTransAmount.Location = new System.Drawing.Point(210, 173);
            this.txtTransAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtTransAmount.Name = "txtTransAmount";
            this.txtTransAmount.Size = new System.Drawing.Size(376, 24);
            this.txtTransAmount.TabIndex = 9;
            this.txtTransAmount.TextChanged += new System.EventHandler(this.txtTransAmount_TextChanged);
            this.txtTransAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTransAmount_KeyUp);
            this.txtTransAmount.Validating += new System.ComponentModel.CancelEventHandler(this.txtTransAmount_Validating);
            // 
            // btnSubmitDr
            // 
            this.btnSubmitDr.Location = new System.Drawing.Point(396, 235);
            this.btnSubmitDr.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmitDr.Name = "btnSubmitDr";
            this.btnSubmitDr.Size = new System.Drawing.Size(191, 40);
            this.btnSubmitDr.TabIndex = 12;
            this.btnSubmitDr.Text = "Submit";
            this.btnSubmitDr.UseVisualStyleBackColor = true;
            this.btnSubmitDr.Click += new System.EventHandler(this.btnSubmitDr_Click);
            this.btnSubmitDr.MouseEnter += new System.EventHandler(this.btnSubmitDr_MouseEnter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "Depositors Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 176);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "Amount To Credit:";
            // 
            // txtTellerTillDr
            // 
            this.txtTellerTillDr.ForeColor = System.Drawing.Color.DarkGreen;
            this.txtTellerTillDr.Location = new System.Drawing.Point(210, 115);
            this.txtTellerTillDr.Margin = new System.Windows.Forms.Padding(4);
            this.txtTellerTillDr.Name = "txtTellerTillDr";
            this.txtTellerTillDr.ReadOnly = true;
            this.txtTellerTillDr.Size = new System.Drawing.Size(377, 24);
            this.txtTellerTillDr.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkGreen;
            this.label5.Location = new System.Drawing.Point(31, 121);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "Tellers Till Account:";
            // 
            // txtCusAcctCredit
            // 
            this.txtCusAcctCredit.ForeColor = System.Drawing.Color.Red;
            this.txtCusAcctCredit.Location = new System.Drawing.Point(209, 143);
            this.txtCusAcctCredit.Margin = new System.Windows.Forms.Padding(4);
            this.txtCusAcctCredit.Name = "txtCusAcctCredit";
            this.txtCusAcctCredit.ReadOnly = true;
            this.txtCusAcctCredit.Size = new System.Drawing.Size(377, 24);
            this.txtCusAcctCredit.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(31, 149);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(162, 18);
            this.label6.TabIndex = 4;
            this.label6.Text = "Customers Account:";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(127, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(701, 44);
            this.label8.TabIndex = 0;
            this.label8.Text = "CARDLESS DEPOSIT SCREEN.";
            // 
            // lblPostProgress
            // 
            this.lblPostProgress.AutoSize = true;
            this.lblPostProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPostProgress.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblPostProgress.Location = new System.Drawing.Point(360, 71);
            this.lblPostProgress.Name = "lblPostProgress";
            this.lblPostProgress.Size = new System.Drawing.Size(17, 24);
            this.lblPostProgress.TabIndex = 1;
            this.lblPostProgress.Text = "-";
            // 
            // PostingprogressBar
            // 
            this.PostingprogressBar.Location = new System.Drawing.Point(584, 72);
            this.PostingprogressBar.MarqueeAnimationSpeed = 30;
            this.PostingprogressBar.Name = "PostingprogressBar";
            this.PostingprogressBar.Size = new System.Drawing.Size(244, 23);
            this.PostingprogressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.PostingprogressBar.TabIndex = 2;
            this.PostingprogressBar.Visible = false;
            // 
            // lstRestrictions
            // 
            this.lstRestrictions.FormattingEnabled = true;
            this.lstRestrictions.ItemHeight = 18;
            this.lstRestrictions.Location = new System.Drawing.Point(147, 283);
            this.lstRestrictions.Name = "lstRestrictions";
            this.lstRestrictions.Size = new System.Drawing.Size(646, 76);
            this.lstRestrictions.TabIndex = 47;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 313);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(141, 18);
            this.label7.TabIndex = 46;
            this.label7.Text = "Text Restrictions:";
            // 
            // CardLessDeposit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(858, 709);
            this.Controls.Add(this.PostingprogressBar);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblPostProgress);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.pnlTransaction);
            this.Controls.Add(this.pnlaccts);
            this.Controls.Add(this.cmbAccountType);
            this.Controls.Add(this.label9);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "CardLessDeposit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CARDLESS Deposit Transaction";
            this.Load += new System.EventHandler(this.CustomerDetail_Load);
            this.pnlaccts.ResumeLayout(false);
            this.pnlNuban.ResumeLayout(false);
            this.pnlNuban.PerformLayout();
            this.pnlAcctDetails.ResumeLayout(false);
            this.pnlAcctDetails.PerformLayout();
            this.pnlTransaction.ResumeLayout(false);
            this.pnlTransaction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlaccts;
        private System.Windows.Forms.Label lblBraCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBraCode;
        private System.Windows.Forms.TextBox txtCusNum;
        private System.Windows.Forms.Panel pnlTransaction;
        private System.Windows.Forms.Button btnClearScreen;
        private System.Windows.Forms.Button btnPinPadDetails;
        private System.Windows.Forms.Panel pnlAcctDetails;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtTransAmount;
        private System.Windows.Forms.Button btnSubmitDr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTellerTillDr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCusAcctCredit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTransProgress;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblAnalysis;
        private System.Windows.Forms.Button btnCashAnalysis;
        private System.Windows.Forms.Label lblPostProgress;
        private System.Windows.Forms.ProgressBar PostingprogressBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCustomerLevel;
        private System.Windows.Forms.ComboBox cmbAccountType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel pnlNuban;
        private System.Windows.Forms.TextBox txtNubanAcct;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCurCode;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSubAcctCode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtLedCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblAccountName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCusName;
        private System.Windows.Forms.ListBox lstRestrictions;
        private System.Windows.Forms.Label label7;
    }
}