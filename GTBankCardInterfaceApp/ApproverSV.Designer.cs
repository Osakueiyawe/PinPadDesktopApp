namespace GTBankCardInterfaceApp
{
    partial class ApproverSV
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
            this.rasterImageList1 = new Leadtools.WinForms.RasterImageList();
            this.lblAcctName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rasterImageList1
            // 
            this.rasterImageList1.ItemImageSize = new System.Drawing.Size(180, 130);
            this.rasterImageList1.ItemSize = new System.Drawing.Size(200, 200);
            this.rasterImageList1.Location = new System.Drawing.Point(47, 48);
            this.rasterImageList1.Margin = new System.Windows.Forms.Padding(4);
            this.rasterImageList1.Name = "rasterImageList1";
            this.rasterImageList1.Size = new System.Drawing.Size(531, 298);
            this.rasterImageList1.TabIndex = 9;
            // 
            // lblAcctName
            // 
            this.lblAcctName.AutoSize = true;
            this.lblAcctName.Location = new System.Drawing.Point(47, 28);
            this.lblAcctName.Name = "lblAcctName";
            this.lblAcctName.Size = new System.Drawing.Size(10, 13);
            this.lblAcctName.TabIndex = 10;
            this.lblAcctName.Text = "-";
            // 
            // ApproverSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 410);
            this.Controls.Add(this.lblAcctName);
            this.Controls.Add(this.rasterImageList1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApproverSV";
            this.Text = "ApproverSV";
            this.Load += new System.EventHandler(this.ApproverSV_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Leadtools.WinForms.RasterImageList rasterImageList1;
        private System.Windows.Forms.Label lblAcctName;

    }
}