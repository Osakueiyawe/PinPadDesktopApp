namespace GTBankCardInterfaceApp
{
    partial class PrintPendingReceipt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintPendingReceipt));
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
            this.btnPrintSelected = new System.Windows.Forms.Button();
            this.bdsApproval = new System.Windows.Forms.BindingSource(this.components);
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
            this.dgvApproval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
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
            this.dgvApproval.Location = new System.Drawing.Point(13, 13);
            this.dgvApproval.Margin = new System.Windows.Forms.Padding(4);
            this.dgvApproval.Name = "dgvApproval";
            this.dgvApproval.Size = new System.Drawing.Size(1258, 640);
            this.dgvApproval.TabIndex = 0;
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
            // btnPrintSelected
            // 
            this.btnPrintSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrintSelected.Location = new System.Drawing.Point(13, 661);
            this.btnPrintSelected.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrintSelected.Name = "btnPrintSelected";
            this.btnPrintSelected.Size = new System.Drawing.Size(248, 28);
            this.btnPrintSelected.TabIndex = 1;
            this.btnPrintSelected.Text = "Print";
            this.btnPrintSelected.UseVisualStyleBackColor = true;
            this.btnPrintSelected.Click += new System.EventHandler(this.btnPrintSelected_Click);
            // 
            // PrintPendingReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 704);
            this.Controls.Add(this.btnPrintSelected);
            this.Controls.Add(this.dgvApproval);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PrintPendingReceipt";
            this.Text = "PrintPendingReceipt";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PrintPendingReceipt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvApproval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsApproval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvApproval;
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
        private System.Windows.Forms.BindingSource bdsApproval;
        private System.Windows.Forms.Button btnPrintSelected;
    }
}