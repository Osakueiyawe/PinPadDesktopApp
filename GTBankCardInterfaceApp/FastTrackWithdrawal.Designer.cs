namespace GTBankCardInterfaceApp
{
    partial class FastTrackWithdrawal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FastTrackWithdrawal));
            this.panel1 = new System.Windows.Forms.Panel();
            this.FastTrackDataGrid = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewLinkColumn();
            this.TransType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MobileNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BenAcctNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Originator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Transtatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoOfHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCustomerLevel = new System.Windows.Forms.Label();
            this.btnClearScreen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lstRestrictions = new System.Windows.Forms.ListBox();
            this.label16 = new System.Windows.Forms.Label();
            this.rasterImageList1 = new Leadtools.WinForms.RasterImageList();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lstAccounts = new System.Windows.Forms.ListBox();
            this.btnGetDetails = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.txtFeedBack = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlTransaction = new System.Windows.Forms.Panel();
            this.lblId = new System.Windows.Forms.Label();
            this.btnTransHistory = new System.Windows.Forms.Button();
            this.txtCharge = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblTransProgress = new System.Windows.Forms.Label();
            this.txtNuban = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtTransAmount = new System.Windows.Forms.TextBox();
            this.btnSubmitDr = new System.Windows.Forms.Button();
            this.txtCusName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTellerTillDr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCusAcctDebit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.PostingprogressBar = new System.Windows.Forms.ProgressBar();
            this.lblPostProgress = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstOpportunities = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FastTrackDataGrid)).BeginInit();
            this.pnlTransaction.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FastTrackDataGrid);
            this.panel1.Controls.Add(this.lblCustomerLevel);
            this.panel1.Controls.Add(this.btnClearScreen);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lstRestrictions);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.rasterImageList1);
            this.panel1.Controls.Add(this.lblProgress);
            this.panel1.Controls.Add(this.lstAccounts);
            this.panel1.Location = new System.Drawing.Point(13, 98);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(844, 525);
            this.panel1.TabIndex = 0;
            // 
            // FastTrackDataGrid
            // 
            this.FastTrackDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FastTrackDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.TransType,
            this.RequestDate,
            this.MobileNumber,
            this.BranchCode,
            this.CustomerNo,
            this.BenAcctNo,
            this.Amount,
            this.Originator,
            this.Status,
            this.Transtatus,
            this.NoOfHours});
            this.FastTrackDataGrid.Location = new System.Drawing.Point(3, 46);
            this.FastTrackDataGrid.Name = "FastTrackDataGrid";
            this.FastTrackDataGrid.Size = new System.Drawing.Size(838, 122);
            this.FastTrackDataGrid.TabIndex = 52;
            this.FastTrackDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FastTrackDataGrid_CellContentClick);
            // 
            // Select
            // 
            this.Select.DataPropertyName = "Select";
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.Text = "Select";
            // 
            // TransType
            // 
            this.TransType.DataPropertyName = "TransType";
            this.TransType.HeaderText = "TransType";
            this.TransType.Name = "TransType";
            // 
            // RequestDate
            // 
            this.RequestDate.DataPropertyName = "RequestDate";
            this.RequestDate.HeaderText = "RequestDate";
            this.RequestDate.Name = "RequestDate";
            // 
            // MobileNumber
            // 
            this.MobileNumber.DataPropertyName = "MobileNumber";
            this.MobileNumber.HeaderText = "MobileNo";
            this.MobileNumber.Name = "MobileNumber";
            // 
            // BranchCode
            // 
            this.BranchCode.DataPropertyName = "BranchCode";
            this.BranchCode.HeaderText = "BraCode";
            this.BranchCode.Name = "BranchCode";
            // 
            // CustomerNo
            // 
            this.CustomerNo.DataPropertyName = "CustomerNo";
            this.CustomerNo.HeaderText = "CusNo";
            this.CustomerNo.Name = "CustomerNo";
            // 
            // BenAcctNo
            // 
            this.BenAcctNo.DataPropertyName = "BenAcctNo";
            this.BenAcctNo.HeaderText = "BenAcctNo";
            this.BenAcctNo.Name = "BenAcctNo";
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            // 
            // Originator
            // 
            this.Originator.DataPropertyName = "Originator";
            this.Originator.HeaderText = "Originator";
            this.Originator.Name = "Originator";
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            // 
            // Transtatus
            // 
            this.Transtatus.DataPropertyName = "Transtatus";
            this.Transtatus.HeaderText = "Transtatus";
            this.Transtatus.Name = "Transtatus";
            // 
            // NoOfHours
            // 
            this.NoOfHours.DataPropertyName = "NoOfHours";
            this.NoOfHours.HeaderText = "NoOfHours";
            this.NoOfHours.Name = "NoOfHours";
            // 
            // lblCustomerLevel
            // 
            this.lblCustomerLevel.AutoSize = true;
            this.lblCustomerLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerLevel.Location = new System.Drawing.Point(169, 8);
            this.lblCustomerLevel.Name = "lblCustomerLevel";
            this.lblCustomerLevel.Size = new System.Drawing.Size(16, 24);
            this.lblCustomerLevel.TabIndex = 11;
            this.lblCustomerLevel.Text = ".";
            // 
            // btnClearScreen
            // 
            this.btnClearScreen.Location = new System.Drawing.Point(619, 6);
            this.btnClearScreen.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearScreen.Name = "btnClearScreen";
            this.btnClearScreen.Size = new System.Drawing.Size(142, 31);
            this.btnClearScreen.TabIndex = 9;
            this.btnClearScreen.Text = "Clear Screen";
            this.btnClearScreen.UseVisualStyleBackColor = true;
            this.btnClearScreen.Click += new System.EventHandler(this.btnClearScreen_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Customer Level:";
            // 
            // lstRestrictions
            // 
            this.lstRestrictions.FormattingEnabled = true;
            this.lstRestrictions.ItemHeight = 18;
            this.lstRestrictions.Location = new System.Drawing.Point(128, 416);
            this.lstRestrictions.Name = "lstRestrictions";
            this.lstRestrictions.Size = new System.Drawing.Size(698, 76);
            this.lstRestrictions.TabIndex = 51;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 445);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(104, 18);
            this.label16.TabIndex = 50;
            this.label16.Text = "Restrictions:";
            // 
            // rasterImageList1
            // 
            this.rasterImageList1.ItemImageSize = new System.Drawing.Size(180, 130);
            this.rasterImageList1.ItemSize = new System.Drawing.Size(200, 200);
            this.rasterImageList1.Location = new System.Drawing.Point(345, 189);
            this.rasterImageList1.Margin = new System.Windows.Forms.Padding(4);
            this.rasterImageList1.Name = "rasterImageList1";
            this.rasterImageList1.Size = new System.Drawing.Size(464, 220);
            this.rasterImageList1.TabIndex = 36;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.ForeColor = System.Drawing.Color.Red;
            this.lblProgress.Location = new System.Drawing.Point(268, 14);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(17, 24);
            this.lblProgress.TabIndex = 20;
            this.lblProgress.Text = "-";
            // 
            // lstAccounts
            // 
            this.lstAccounts.FormattingEnabled = true;
            this.lstAccounts.ItemHeight = 18;
            this.lstAccounts.Location = new System.Drawing.Point(4, 189);
            this.lstAccounts.Margin = new System.Windows.Forms.Padding(4);
            this.lstAccounts.Name = "lstAccounts";
            this.lstAccounts.Size = new System.Drawing.Size(333, 220);
            this.lstAccounts.TabIndex = 5;
            this.lstAccounts.SelectedValueChanged += new System.EventHandler(this.lstAccounts_SelectedValueChanged);
            // 
            // btnGetDetails
            // 
            this.btnGetDetails.Location = new System.Drawing.Point(467, 58);
            this.btnGetDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetDetails.Name = "btnGetDetails";
            this.btnGetDetails.Size = new System.Drawing.Size(201, 27);
            this.btnGetDetails.TabIndex = 8;
            this.btnGetDetails.Text = "Get Account Details";
            this.btnGetDetails.UseVisualStyleBackColor = true;
            this.btnGetDetails.Click += new System.EventHandler(this.btnGetDetails_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(1105, 587);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(167, 36);
            this.btnSubmit.TabIndex = 56;
            this.btnSubmit.Text = "Submit Feedback";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(999, 468);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(202, 18);
            this.label18.TabIndex = 55;
            this.label18.Text = "Feedback from customer:";
            // 
            // txtFeedBack
            // 
            this.txtFeedBack.Location = new System.Drawing.Point(933, 489);
            this.txtFeedBack.Multiline = true;
            this.txtFeedBack.Name = "txtFeedBack";
            this.txtFeedBack.Size = new System.Drawing.Size(338, 92);
            this.txtFeedBack.TabIndex = 54;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label17.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(910, 185);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(361, 23);
            this.label17.TabIndex = 52;
            this.label17.Text = "Opportunities: (Please share with customer)";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(135, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(609, 39);
            this.label8.TabIndex = 35;
            this.label8.Text = "FASTRACK WITHDRAWAL SCREEN.";
            // 
            // pnlTransaction
            // 
            this.pnlTransaction.Controls.Add(this.lblId);
            this.pnlTransaction.Controls.Add(this.btnTransHistory);
            this.pnlTransaction.Controls.Add(this.txtCharge);
            this.pnlTransaction.Controls.Add(this.label9);
            this.pnlTransaction.Controls.Add(this.lblTransProgress);
            this.pnlTransaction.Controls.Add(this.txtNuban);
            this.pnlTransaction.Controls.Add(this.label7);
            this.pnlTransaction.Controls.Add(this.btnCancel);
            this.pnlTransaction.Controls.Add(this.txtTransAmount);
            this.pnlTransaction.Controls.Add(this.btnSubmitDr);
            this.pnlTransaction.Controls.Add(this.txtCusName);
            this.pnlTransaction.Controls.Add(this.label4);
            this.pnlTransaction.Controls.Add(this.label1);
            this.pnlTransaction.Controls.Add(this.txtTellerTillDr);
            this.pnlTransaction.Controls.Add(this.label5);
            this.pnlTransaction.Controls.Add(this.txtCusAcctDebit);
            this.pnlTransaction.Controls.Add(this.label6);
            this.pnlTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTransaction.Location = new System.Drawing.Point(54, 622);
            this.pnlTransaction.Margin = new System.Windows.Forms.Padding(4);
            this.pnlTransaction.Name = "pnlTransaction";
            this.pnlTransaction.Size = new System.Drawing.Size(928, 230);
            this.pnlTransaction.TabIndex = 0;
            this.pnlTransaction.Visible = false;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(26, 179);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(14, 18);
            this.lblId.TabIndex = 49;
            this.lblId.Text = "-";
            // 
            // btnTransHistory
            // 
            this.btnTransHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTransHistory.Location = new System.Drawing.Point(732, 87);
            this.btnTransHistory.Name = "btnTransHistory";
            this.btnTransHistory.Size = new System.Drawing.Size(181, 27);
            this.btnTransHistory.TabIndex = 39;
            this.btnTransHistory.Text = "Transaction History";
            this.btnTransHistory.UseVisualStyleBackColor = true;
            this.btnTransHistory.Click += new System.EventHandler(this.btnTransHistory_Click);
            // 
            // txtCharge
            // 
            this.txtCharge.ForeColor = System.Drawing.Color.DarkGreen;
            this.txtCharge.Location = new System.Drawing.Point(205, 5);
            this.txtCharge.Margin = new System.Windows.Forms.Padding(4);
            this.txtCharge.Name = "txtCharge";
            this.txtCharge.ReadOnly = true;
            this.txtCharge.Size = new System.Drawing.Size(377, 24);
            this.txtCharge.TabIndex = 38;
            this.txtCharge.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(26, 12);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(159, 18);
            this.label9.TabIndex = 37;
            this.label9.Text = "Associated Charges";
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
            // txtNuban
            // 
            this.txtNuban.Location = new System.Drawing.Point(205, 117);
            this.txtNuban.Margin = new System.Windows.Forms.Padding(4);
            this.txtNuban.Name = "txtNuban";
            this.txtNuban.ReadOnly = true;
            this.txtNuban.Size = new System.Drawing.Size(377, 24);
            this.txtNuban.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 118);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(176, 18);
            this.label7.TabIndex = 33;
            this.label7.Text = "Customers Nuban No:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(205, 178);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(179, 41);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtTransAmount
            // 
            this.txtTransAmount.Location = new System.Drawing.Point(205, 146);
            this.txtTransAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtTransAmount.Name = "txtTransAmount";
            this.txtTransAmount.Size = new System.Drawing.Size(377, 24);
            this.txtTransAmount.TabIndex = 0;
            this.txtTransAmount.TextChanged += new System.EventHandler(this.txtTransAmount_TextChanged);
            this.txtTransAmount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTransAmount_KeyUp);
            this.txtTransAmount.Validating += new System.ComponentModel.CancelEventHandler(this.txtTransAmount_Validating);
            // 
            // btnSubmitDr
            // 
            this.btnSubmitDr.Location = new System.Drawing.Point(392, 179);
            this.btnSubmitDr.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmitDr.Name = "btnSubmitDr";
            this.btnSubmitDr.Size = new System.Drawing.Size(191, 40);
            this.btnSubmitDr.TabIndex = 1;
            this.btnSubmitDr.Text = "Submit";
            this.btnSubmitDr.UseVisualStyleBackColor = true;
            this.btnSubmitDr.Click += new System.EventHandler(this.btnSubmitDr_Click);
            // 
            // txtCusName
            // 
            this.txtCusName.Location = new System.Drawing.Point(205, 32);
            this.txtCusName.Margin = new System.Windows.Forms.Padding(4);
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.ReadOnly = true;
            this.txtCusName.Size = new System.Drawing.Size(441, 24);
            this.txtCusName.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 18);
            this.label4.TabIndex = 28;
            this.label4.Text = "Customers Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 143);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 18);
            this.label1.TabIndex = 27;
            this.label1.Text = "Amount To Debit:";
            // 
            // txtTellerTillDr
            // 
            this.txtTellerTillDr.ForeColor = System.Drawing.Color.DarkGreen;
            this.txtTellerTillDr.Location = new System.Drawing.Point(205, 60);
            this.txtTellerTillDr.Margin = new System.Windows.Forms.Padding(4);
            this.txtTellerTillDr.Name = "txtTellerTillDr";
            this.txtTellerTillDr.ReadOnly = true;
            this.txtTellerTillDr.Size = new System.Drawing.Size(377, 24);
            this.txtTellerTillDr.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkGreen;
            this.label5.Location = new System.Drawing.Point(26, 67);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 18);
            this.label5.TabIndex = 25;
            this.label5.Text = "Tellers Till Account:";
            // 
            // txtCusAcctDebit
            // 
            this.txtCusAcctDebit.ForeColor = System.Drawing.Color.Red;
            this.txtCusAcctDebit.Location = new System.Drawing.Point(204, 88);
            this.txtCusAcctDebit.Margin = new System.Windows.Forms.Padding(4);
            this.txtCusAcctDebit.Name = "txtCusAcctDebit";
            this.txtCusAcctDebit.ReadOnly = true;
            this.txtCusAcctDebit.Size = new System.Drawing.Size(377, 24);
            this.txtCusAcctDebit.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(26, 94);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(162, 18);
            this.label6.TabIndex = 23;
            this.label6.Text = "Customers Account:";
            // 
            // PostingprogressBar
            // 
            this.PostingprogressBar.Location = new System.Drawing.Point(613, 36);
            this.PostingprogressBar.MarqueeAnimationSpeed = 30;
            this.PostingprogressBar.Name = "PostingprogressBar";
            this.PostingprogressBar.Size = new System.Drawing.Size(244, 23);
            this.PostingprogressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.PostingprogressBar.TabIndex = 48;
            this.PostingprogressBar.Visible = false;
            // 
            // lblPostProgress
            // 
            this.lblPostProgress.AutoSize = true;
            this.lblPostProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPostProgress.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblPostProgress.Location = new System.Drawing.Point(389, 35);
            this.lblPostProgress.Name = "lblPostProgress";
            this.lblPostProgress.Size = new System.Drawing.Size(17, 24);
            this.lblPostProgress.TabIndex = 47;
            this.lblPostProgress.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 18);
            this.label2.TabIndex = 49;
            this.label2.Text = "Phone Number";
            // 
            // lstOpportunities
            // 
            this.lstOpportunities.AutoSize = true;
            this.lstOpportunities.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstOpportunities.ForeColor = System.Drawing.Color.Green;
            this.lstOpportunities.Location = new System.Drawing.Point(915, 204);
            this.lstOpportunities.MaximumSize = new System.Drawing.Size(400, 0);
            this.lstOpportunities.Name = "lstOpportunities";
            this.lstOpportunities.Size = new System.Drawing.Size(67, 29);
            this.lstOpportunities.TabIndex = 57;
            this.lstOpportunities.Text = "None";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(186, 61);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(220, 24);
            this.txtPhoneNumber.TabIndex = 58;
            // 
            // FastTrackWithdrawal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1275, 873);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.lstOpportunities);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btnGetDetails);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtFeedBack);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PostingprogressBar);
            this.Controls.Add(this.lblPostProgress);
            this.Controls.Add(this.pnlTransaction);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FastTrackWithdrawal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CardHolder Withdrawal Transactions";
            this.Load += new System.EventHandler(this.CustomerDetail_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FastTrackDataGrid)).EndInit();
            this.pnlTransaction.ResumeLayout(false);
            this.pnlTransaction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void FastTrackDataGrid_CellContentClick1(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            throw new System.NotImplementedException();
        }


        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox lstAccounts;
        private System.Windows.Forms.Panel pnlTransaction;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtTransAmount;
        private System.Windows.Forms.Button btnSubmitDr;
        private System.Windows.Forms.TextBox txtCusName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTellerTillDr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCusAcctDebit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTransProgress;
        private System.Windows.Forms.TextBox txtCharge;
        private System.Windows.Forms.Label label9;
        private Leadtools.WinForms.RasterImageList rasterImageList1;
        private System.Windows.Forms.ProgressBar PostingprogressBar;
        private System.Windows.Forms.Label lblPostProgress;
        private System.Windows.Forms.Button btnTransHistory;
        private System.Windows.Forms.Button btnClearScreen;
        private System.Windows.Forms.Button btnGetDetails;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCustomerLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNuban;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lstRestrictions;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtFeedBack;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lstOpportunities;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.DataGridView FastTrackDataGrid;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.DataGridViewLinkColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransType;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn MobileNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BenAcctNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Originator;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Transtatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoOfHours;
    }
}