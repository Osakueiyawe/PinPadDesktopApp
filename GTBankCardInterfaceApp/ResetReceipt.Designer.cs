namespace GTBankCardInterfaceApp
{
    partial class ResetReceipt
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtAcctno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtmFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTraAmt = new System.Windows.Forms.TextBox();
            this.btngetTransDetails = new System.Windows.Forms.Button();
            this.btnResetTransaction = new System.Windows.Forms.Button();
            this.bdsHistory = new System.Windows.Forms.BindingSource(this.components);
            this.dgvReceipt = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Transtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bdsHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Enter the Customer Account Number:";
            // 
            // txtAcctno
            // 
            this.txtAcctno.Location = new System.Drawing.Point(228, 21);
            this.txtAcctno.Name = "txtAcctno";
            this.txtAcctno.Size = new System.Drawing.Size(204, 20);
            this.txtAcctno.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Transaction Date:";
            // 
            // dtmFrom
            // 
            this.dtmFrom.Location = new System.Drawing.Point(228, 56);
            this.dtmFrom.Name = "dtmFrom";
            this.dtmFrom.Size = new System.Drawing.Size(142, 20);
            this.dtmFrom.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Transaction Amount:";
            // 
            // txtTraAmt
            // 
            this.txtTraAmt.Location = new System.Drawing.Point(228, 90);
            this.txtTraAmt.Name = "txtTraAmt";
            this.txtTraAmt.Size = new System.Drawing.Size(204, 20);
            this.txtTraAmt.TabIndex = 5;
            this.txtTraAmt.TextChanged += new System.EventHandler(this.txtTraAmt_TextChanged);
            // 
            // btngetTransDetails
            // 
            this.btngetTransDetails.Location = new System.Drawing.Point(469, 90);
            this.btngetTransDetails.Name = "btngetTransDetails";
            this.btngetTransDetails.Size = new System.Drawing.Size(169, 23);
            this.btngetTransDetails.TabIndex = 6;
            this.btngetTransDetails.Text = "Get Transaction Details";
            this.btngetTransDetails.UseVisualStyleBackColor = true;
            this.btngetTransDetails.Click += new System.EventHandler(this.btngetTransDetails_Click);
            // 
            // btnResetTransaction
            // 
            this.btnResetTransaction.Location = new System.Drawing.Point(469, 387);
            this.btnResetTransaction.Name = "btnResetTransaction";
            this.btnResetTransaction.Size = new System.Drawing.Size(169, 23);
            this.btnResetTransaction.TabIndex = 13;
            this.btnResetTransaction.Text = "Reset Selected Transaction";
            this.btnResetTransaction.UseVisualStyleBackColor = true;
            this.btnResetTransaction.Click += new System.EventHandler(this.btnResetTransaction_Click);
            // 
            // dgvReceipt
            // 
            this.dgvReceipt.AllowUserToAddRows = false;
            this.dgvReceipt.AllowUserToDeleteRows = false;
            this.dgvReceipt.AllowUserToResizeColumns = false;
            this.dgvReceipt.AllowUserToResizeRows = false;
            this.dgvReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReceipt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvReceipt.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvReceipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceipt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.CustomerNo,
            this.Transtype,
            this.TransAmount,
            this.CustomerName,
            this.TransDate});
            this.dgvReceipt.Location = new System.Drawing.Point(13, 120);
            this.dgvReceipt.Margin = new System.Windows.Forms.Padding(4);
            this.dgvReceipt.Name = "dgvReceipt";
            this.dgvReceipt.Size = new System.Drawing.Size(624, 260);
            this.dgvReceipt.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(439, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "(Old Account Format Only)";
            // 
            // ID
            // 
            this.ID.DataPropertyName = "TransactionID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ID.Width = 43;
            // 
            // CustomerNo
            // 
            this.CustomerNo.DataPropertyName = "CustomerNo";
            this.CustomerNo.HeaderText = "CustomerNo";
            this.CustomerNo.Name = "CustomerNo";
            this.CustomerNo.ReadOnly = true;
            this.CustomerNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CustomerNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CustomerNo.Width = 71;
            // 
            // Transtype
            // 
            this.Transtype.DataPropertyName = "Transtype";
            this.Transtype.HeaderText = "Transtype";
            this.Transtype.Name = "Transtype";
            this.Transtype.ReadOnly = true;
            this.Transtype.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Transtype.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Transtype.Width = 60;
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
            this.TransAmount.Width = 76;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "CustomerName";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CustomerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CustomerName.Width = 85;
            // 
            // TransDate
            // 
            this.TransDate.DataPropertyName = "TransDate";
            this.TransDate.HeaderText = "TransDate";
            this.TransDate.Name = "TransDate";
            this.TransDate.ReadOnly = true;
            this.TransDate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TransDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TransDate.Width = 63;
            // 
            // ResetReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 422);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvReceipt);
            this.Controls.Add(this.btnResetTransaction);
            this.Controls.Add(this.btngetTransDetails);
            this.Controls.Add(this.txtTraAmt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAcctno);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "ResetReceipt";
            this.Text = "ResetReceipt";
            this.Load += new System.EventHandler(this.ResetReceipt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bdsHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceipt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAcctno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtmFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTraAmt;
        private System.Windows.Forms.Button btngetTransDetails;
        private System.Windows.Forms.Button btnResetTransaction;
        private System.Windows.Forms.BindingSource bdsHistory;
        private System.Windows.Forms.DataGridView dgvReceipt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Transtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransDate;
    }
}