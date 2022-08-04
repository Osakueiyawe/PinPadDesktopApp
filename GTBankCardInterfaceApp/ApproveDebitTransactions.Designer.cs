namespace GTBankCardInterfaceApp
{
    partial class ApproveDebitTransactions
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApproveDebitTransactions));
            this.dgvApproval = new System.Windows.Forms.DataGridView();
            this.TransactionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TellerTillAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Transtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OriginatingTellerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AuthenticationMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OriginatingBraCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransactionStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepositorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnApprove = new System.Windows.Forms.Button();
            this.bdsApproval = new System.Windows.Forms.BindingSource(this.components);
            this.btnReject = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbAuthMode = new System.Windows.Forms.ComboBox();
            this.btnGetPendingTrans = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApproval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsApproval)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvApproval
            // 
            this.dgvApproval.AllowUserToAddRows = false;
            this.dgvApproval.AllowUserToDeleteRows = false;
            this.dgvApproval.AllowUserToResizeColumns = false;
            this.dgvApproval.AllowUserToResizeRows = false;
            this.dgvApproval.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvApproval.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvApproval.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvApproval.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvApproval.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TransactionID,
            this.TellerTillAccount,
            this.CustomerNo,
            this.Transtype,
            this.OriginatingTellerId,
            this.AuthenticationMode,
            this.TransAmount,
            this.OriginatingBraCode,
            this.TransactionStatus,
            this.CustomerName,
            this.DepositorName,
            this.TransDate});
            this.dgvApproval.Location = new System.Drawing.Point(13, 42);
            this.dgvApproval.Margin = new System.Windows.Forms.Padding(4);
            this.dgvApproval.Name = "dgvApproval";
            this.dgvApproval.Size = new System.Drawing.Size(770, 494);
            this.dgvApproval.TabIndex = 3;
            // 
            // TransactionID
            // 
            this.TransactionID.DataPropertyName = "TransactionId";
            this.TransactionID.HeaderText = "ID";
            this.TransactionID.Name = "TransactionID";
            this.TransactionID.ReadOnly = true;
            this.TransactionID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TransactionID.Width = 48;
            // 
            // TellerTillAccount
            // 
            this.TellerTillAccount.DataPropertyName = "TellerTillAccount";
            this.TellerTillAccount.HeaderText = "TellerTill";
            this.TellerTillAccount.Name = "TellerTillAccount";
            this.TellerTillAccount.ReadOnly = true;
            this.TellerTillAccount.Width = 97;
            // 
            // CustomerNo
            // 
            this.CustomerNo.DataPropertyName = "CustomerNo";
            this.CustomerNo.HeaderText = "CustomerNo";
            this.CustomerNo.Name = "CustomerNo";
            this.CustomerNo.ReadOnly = true;
            this.CustomerNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CustomerNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CustomerNo.Width = 102;
            // 
            // Transtype
            // 
            this.Transtype.DataPropertyName = "Transtype";
            this.Transtype.HeaderText = "Transtype";
            this.Transtype.Name = "Transtype";
            this.Transtype.ReadOnly = true;
            this.Transtype.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Transtype.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Transtype.Width = 87;
            // 
            // OriginatingTellerId
            // 
            this.OriginatingTellerId.DataPropertyName = "OriginatingTellerId";
            this.OriginatingTellerId.HeaderText = "TellerId";
            this.OriginatingTellerId.Name = "OriginatingTellerId";
            this.OriginatingTellerId.ReadOnly = true;
            this.OriginatingTellerId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.OriginatingTellerId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OriginatingTellerId.Width = 69;
            // 
            // AuthenticationMode
            // 
            this.AuthenticationMode.DataPropertyName = "AuthenticationMode";
            this.AuthenticationMode.HeaderText = "AuthMode";
            this.AuthenticationMode.Name = "AuthenticationMode";
            this.AuthenticationMode.ReadOnly = true;
            this.AuthenticationMode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AuthenticationMode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AuthenticationMode.Width = 86;
            // 
            // TransAmount
            // 
            this.TransAmount.DataPropertyName = "TransAmount";
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.TransAmount.DefaultCellStyle = dataGridViewCellStyle1;
            this.TransAmount.HeaderText = "TransAmount";
            this.TransAmount.Name = "TransAmount";
            this.TransAmount.ReadOnly = true;
            this.TransAmount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TransAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TransAmount.Width = 110;
            // 
            // OriginatingBraCode
            // 
            this.OriginatingBraCode.DataPropertyName = "OriginatingBraCode";
            this.OriginatingBraCode.HeaderText = "BraCode";
            this.OriginatingBraCode.Name = "OriginatingBraCode";
            this.OriginatingBraCode.ReadOnly = true;
            this.OriginatingBraCode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.OriginatingBraCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OriginatingBraCode.Width = 76;
            // 
            // TransactionStatus
            // 
            this.TransactionStatus.DataPropertyName = "TransactionStatus";
            this.TransactionStatus.HeaderText = "Status";
            this.TransactionStatus.Name = "TransactionStatus";
            this.TransactionStatus.ReadOnly = true;
            this.TransactionStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TransactionStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TransactionStatus.Width = 60;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "CustomerName";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CustomerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CustomerName.Width = 123;
            // 
            // DepositorName
            // 
            this.DepositorName.DataPropertyName = "DepositorName";
            this.DepositorName.HeaderText = "DepositorName";
            this.DepositorName.Name = "DepositorName";
            this.DepositorName.ReadOnly = true;
            this.DepositorName.Width = 144;
            // 
            // TransDate
            // 
            this.TransDate.DataPropertyName = "TransDate";
            this.TransDate.HeaderText = "TransDate";
            this.TransDate.Name = "TransDate";
            this.TransDate.ReadOnly = true;
            this.TransDate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TransDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TransDate.Width = 90;
            // 
            // btnApprove
            // 
            this.btnApprove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApprove.Enabled = false;
            this.btnApprove.Location = new System.Drawing.Point(13, 544);
            this.btnApprove.Margin = new System.Windows.Forms.Padding(4);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(285, 28);
            this.btnApprove.TabIndex = 4;
            this.btnApprove.Text = "Approve Selected Transactions";
            this.btnApprove.UseVisualStyleBackColor = true;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnReject
            // 
            this.btnReject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReject.Enabled = false;
            this.btnReject.Location = new System.Drawing.Point(306, 544);
            this.btnReject.Margin = new System.Windows.Forms.Padding(4);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(285, 28);
            this.btnReject.TabIndex = 5;
            this.btnReject.Text = "Reject Selected Transactions";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Select Transaction Mode:";
            // 
            // cmbAuthMode
            // 
            this.cmbAuthMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAuthMode.FormattingEnabled = true;
            this.cmbAuthMode.Items.AddRange(new object[] {
            "Online"});
            this.cmbAuthMode.Location = new System.Drawing.Point(274, 6);
            this.cmbAuthMode.Margin = new System.Windows.Forms.Padding(4);
            this.cmbAuthMode.Name = "cmbAuthMode";
            this.cmbAuthMode.Size = new System.Drawing.Size(124, 24);
            this.cmbAuthMode.TabIndex = 1;
            // 
            // btnGetPendingTrans
            // 
            this.btnGetPendingTrans.Location = new System.Drawing.Point(406, 6);
            this.btnGetPendingTrans.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetPendingTrans.Name = "btnGetPendingTrans";
            this.btnGetPendingTrans.Size = new System.Drawing.Size(209, 28);
            this.btnGetPendingTrans.TabIndex = 2;
            this.btnGetPendingTrans.Text = "Get Pending Transactions";
            this.btnGetPendingTrans.UseVisualStyleBackColor = true;
            this.btnGetPendingTrans.Click += new System.EventHandler(this.btnGetPendingTrans_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(623, 6);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(135, 28);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear Screen";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ApproveDebitTransactions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(792, 602);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnGetPendingTrans);
            this.Controls.Add(this.cmbAuthMode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.dgvApproval);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ApproveDebitTransactions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Approve Deposit Transactions";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ApproveTransactions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvApproval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsApproval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvApproval;
        private System.Windows.Forms.BindingSource bdsApproval;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TellerTillAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Transtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn OriginatingTellerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuthenticationMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OriginatingBraCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepositorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbAuthMode;
        private System.Windows.Forms.Button btnGetPendingTrans;
        private System.Windows.Forms.Button btnClear;
    }
}