namespace GTBankCardInterfaceApp
{
    partial class CustomerTransactionHistory
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
            this.dtmTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtmFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.grdTransHistory = new System.Windows.Forms.DataGridView();
            this.btnHistory = new System.Windows.Forms.Button();
            this.bdsApproval = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdTransHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsApproval)).BeginInit();
            this.SuspendLayout();
            // 
            // dtmTo
            // 
            this.dtmTo.Enabled = false;
            this.dtmTo.Location = new System.Drawing.Point(384, 12);
            this.dtmTo.Name = "dtmTo";
            this.dtmTo.Size = new System.Drawing.Size(187, 20);
            this.dtmTo.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "From::";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(355, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "To:";
            // 
            // dtmFrom
            // 
            this.dtmFrom.Location = new System.Drawing.Point(165, 12);
            this.dtmFrom.Name = "dtmFrom";
            this.dtmFrom.Size = new System.Drawing.Size(184, 20);
            this.dtmFrom.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Transaction Date:";
            // 
            // grdTransHistory
            // 
            this.grdTransHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTransHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTransHistory.Location = new System.Drawing.Point(12, 38);
            this.grdTransHistory.Name = "grdTransHistory";
            this.grdTransHistory.Size = new System.Drawing.Size(851, 564);
            this.grdTransHistory.TabIndex = 10;
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(577, 11);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(158, 23);
            this.btnHistory.TabIndex = 11;
            this.btnHistory.Text = "Get Transaction History";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // CustomerTransactionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 614);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.grdTransHistory);
            this.Controls.Add(this.dtmTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtmFrom);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerTransactionHistory";
            this.Text = "CustomerTransactionHistory";
            this.Load += new System.EventHandler(this.CustomerTransactionHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTransHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsApproval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtmTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtmFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grdTransHistory;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.BindingSource bdsApproval;
    }
}