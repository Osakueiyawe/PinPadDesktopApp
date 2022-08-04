namespace GTBankCardInterfaceApp
{
    partial class FastTrackDeposit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FastTrackDeposit));
            this.pnlaccts = new System.Windows.Forms.Panel();
            this.FastTrackDataGrid = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewLinkColumn();
            this.BenAcctNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MobileNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Originator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Transtatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoOfHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClearScreen = new System.Windows.Forms.Button();
            this.btnPinPadDetails = new System.Windows.Forms.Button();
            this.lblCustomerLevel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pnlTransaction = new System.Windows.Forms.Panel();
            this.lblId = new System.Windows.Forms.Label();
            this.lstRestrictions = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
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
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.pnlaccts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FastTrackDataGrid)).BeginInit();
            this.pnlTransaction.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlaccts
            // 
            this.pnlaccts.Controls.Add(this.FastTrackDataGrid);
            this.pnlaccts.Location = new System.Drawing.Point(17, 132);
            this.pnlaccts.Margin = new System.Windows.Forms.Padding(4);
            this.pnlaccts.Name = "pnlaccts";
            this.pnlaccts.Size = new System.Drawing.Size(828, 173);
            this.pnlaccts.TabIndex = 3;
            this.pnlaccts.Visible = false;
            // 
            // FastTrackDataGrid
            // 
            this.FastTrackDataGrid.AllowUserToAddRows = false;
            this.FastTrackDataGrid.AllowUserToDeleteRows = false;
            this.FastTrackDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FastTrackDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.BenAcctNo,
            this.TransType,
            this.RequestDate,
            this.MobileNumber,
            this.BranchCode,
            this.CustomerNo,
            this.Amount,
            this.Originator,
            this.Status,
            this.Transtatus,
            this.NoOfHours,
            this.Id});
            this.FastTrackDataGrid.Location = new System.Drawing.Point(-5, -1);
            this.FastTrackDataGrid.Name = "FastTrackDataGrid";
            this.FastTrackDataGrid.ReadOnly = true;
            this.FastTrackDataGrid.RowHeadersWidth = 51;
            this.FastTrackDataGrid.Size = new System.Drawing.Size(869, 122);
            this.FastTrackDataGrid.TabIndex = 53;
            this.FastTrackDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FastTrackDataGrid_CellContentClick);
            // 
            // Select
            // 
            this.Select.DataPropertyName = "Select";
            this.Select.HeaderText = "Select";
            this.Select.MinimumWidth = 6;
            this.Select.Name = "Select";
            this.Select.ReadOnly = true;
            this.Select.Text = "Select";
            this.Select.Width = 125;
            // 
            // BenAcctNo
            // 
            this.BenAcctNo.DataPropertyName = "BenAcctNo";
            this.BenAcctNo.HeaderText = "BenAcctNo";
            this.BenAcctNo.MinimumWidth = 6;
            this.BenAcctNo.Name = "BenAcctNo";
            this.BenAcctNo.ReadOnly = true;
            this.BenAcctNo.Width = 125;
            // 
            // TransType
            // 
            this.TransType.DataPropertyName = "TransType";
            this.TransType.HeaderText = "TransType";
            this.TransType.MinimumWidth = 6;
            this.TransType.Name = "TransType";
            this.TransType.ReadOnly = true;
            this.TransType.Width = 125;
            // 
            // RequestDate
            // 
            this.RequestDate.DataPropertyName = "RequestDate";
            this.RequestDate.HeaderText = "RequestDate";
            this.RequestDate.MinimumWidth = 6;
            this.RequestDate.Name = "RequestDate";
            this.RequestDate.ReadOnly = true;
            this.RequestDate.Width = 125;
            // 
            // MobileNumber
            // 
            this.MobileNumber.DataPropertyName = "MobileNumber";
            this.MobileNumber.HeaderText = "MobileNo";
            this.MobileNumber.MinimumWidth = 6;
            this.MobileNumber.Name = "MobileNumber";
            this.MobileNumber.ReadOnly = true;
            this.MobileNumber.Width = 125;
            // 
            // BranchCode
            // 
            this.BranchCode.DataPropertyName = "BranchCode";
            this.BranchCode.HeaderText = "BraCode";
            this.BranchCode.MinimumWidth = 6;
            this.BranchCode.Name = "BranchCode";
            this.BranchCode.ReadOnly = true;
            this.BranchCode.Width = 125;
            // 
            // CustomerNo
            // 
            this.CustomerNo.DataPropertyName = "CustomerNo";
            this.CustomerNo.HeaderText = "CusNo";
            this.CustomerNo.MinimumWidth = 6;
            this.CustomerNo.Name = "CustomerNo";
            this.CustomerNo.ReadOnly = true;
            this.CustomerNo.Width = 125;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            this.Amount.HeaderText = "Amount";
            this.Amount.MinimumWidth = 6;
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 125;
            // 
            // Originator
            // 
            this.Originator.DataPropertyName = "Originator";
            this.Originator.HeaderText = "Originator";
            this.Originator.MinimumWidth = 6;
            this.Originator.Name = "Originator";
            this.Originator.ReadOnly = true;
            this.Originator.Width = 125;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 125;
            // 
            // Transtatus
            // 
            this.Transtatus.DataPropertyName = "Transtatus";
            this.Transtatus.HeaderText = "Transtatus";
            this.Transtatus.MinimumWidth = 6;
            this.Transtatus.Name = "Transtatus";
            this.Transtatus.ReadOnly = true;
            this.Transtatus.Width = 125;
            // 
            // NoOfHours
            // 
            this.NoOfHours.DataPropertyName = "NoOfHours";
            this.NoOfHours.HeaderText = "NoOfHours";
            this.NoOfHours.MinimumWidth = 6;
            this.NoOfHours.Name = "NoOfHours";
            this.NoOfHours.ReadOnly = true;
            this.NoOfHours.Width = 125;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 125;
            // 
            // btnClearScreen
            // 
            this.btnClearScreen.Location = new System.Drawing.Point(584, 104);
            this.btnClearScreen.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearScreen.Name = "btnClearScreen";
            this.btnClearScreen.Size = new System.Drawing.Size(125, 31);
            this.btnClearScreen.TabIndex = 9;
            this.btnClearScreen.Text = "Clear Screen";
            this.btnClearScreen.UseVisualStyleBackColor = true;
            this.btnClearScreen.Click += new System.EventHandler(this.btnClearScreen_Click);
            // 
            // btnPinPadDetails
            // 
            this.btnPinPadDetails.Location = new System.Drawing.Point(395, 104);
            this.btnPinPadDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnPinPadDetails.Name = "btnPinPadDetails";
            this.btnPinPadDetails.Size = new System.Drawing.Size(181, 31);
            this.btnPinPadDetails.TabIndex = 8;
            this.btnPinPadDetails.Text = "Get Account Details";
            this.btnPinPadDetails.UseVisualStyleBackColor = true;
            this.btnPinPadDetails.Click += new System.EventHandler(this.btnPinPadDetails_Click);
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(133, 36);
            this.label9.TabIndex = 0;
            this.label9.Text = "Phone Number/\r\nAccount Number";
            this.label9.Click += new System.EventHandler(this.label9_Click);
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
            this.pnlTransaction.Controls.Add(this.lblId);
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
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(37, 255);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(14, 18);
            this.lblId.TabIndex = 48;
            this.lblId.Text = "-";
            // 
            // lstRestrictions
            // 
            this.lstRestrictions.FormattingEnabled = true;
            this.lstRestrictions.ItemHeight = 18;
            this.lstRestrictions.Location = new System.Drawing.Point(147, 283);
            this.lstRestrictions.Name = "lstRestrictions";
            this.lstRestrictions.Size = new System.Drawing.Size(646, 58);
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
            // txtCusName
            // 
            this.txtCusName.Location = new System.Drawing.Point(209, 84);
            this.txtCusName.MaxLength = 50;
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
            this.txtTransAmount.MaxLength = 50;
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
            this.label8.Text = "MOBILE CARDLESS DEPOSIT SCREEN.";
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
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(144, 106);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(220, 24);
            this.txtPhoneNumber.TabIndex = 59;
            // 
            // FastTrackDeposit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(984, 709);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.btnClearScreen);
            this.Controls.Add(this.PostingprogressBar);
            this.Controls.Add(this.btnPinPadDetails);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblPostProgress);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.pnlTransaction);
            this.Controls.Add(this.pnlaccts);
            this.Controls.Add(this.label9);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FastTrackDeposit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mobile CARDLESS Deposit Transaction";
            this.Load += new System.EventHandler(this.CustomerDetail_Load);
            this.pnlaccts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FastTrackDataGrid)).EndInit();
            this.pnlTransaction.ResumeLayout(false);
            this.pnlTransaction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlaccts;
        private System.Windows.Forms.Panel pnlTransaction;
        private System.Windows.Forms.Button btnClearScreen;
        private System.Windows.Forms.Button btnPinPadDetails;
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
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblAccountName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCusName;
        private System.Windows.Forms.ListBox lstRestrictions;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView FastTrackDataGrid;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.DataGridViewLinkColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn BenAcctNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransType;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn MobileNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Originator;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Transtatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoOfHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
    }
}