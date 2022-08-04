namespace GTBankCardInterfaceApp
{
    partial class ManageLogo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageLogo));
            this.pixHeader = new System.Windows.Forms.PictureBox();
            this.pixFooter = new System.Windows.Forms.PictureBox();
            this.openFile1 = new System.Windows.Forms.OpenFileDialog();
            this.openFile2 = new System.Windows.Forms.OpenFileDialog();
            this.lblHeader = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnHeader = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.txtFooter = new System.Windows.Forms.TextBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pixHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixFooter)).BeginInit();
            this.SuspendLayout();
            // 
            // pixHeader
            // 
            this.pixHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pixHeader.Location = new System.Drawing.Point(38, 23);
            this.pixHeader.Name = "pixHeader";
            this.pixHeader.Size = new System.Drawing.Size(600, 240);
            this.pixHeader.TabIndex = 2;
            this.pixHeader.TabStop = false;
            // 
            // pixFooter
            // 
            this.pixFooter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pixFooter.Location = new System.Drawing.Point(38, 298);
            this.pixFooter.Name = "pixFooter";
            this.pixFooter.Size = new System.Drawing.Size(600, 240);
            this.pixFooter.TabIndex = 3;
            this.pixFooter.TabStop = false;
            // 
            // openFile1
            // 
            this.openFile1.FileName = "openFileDialog1";
            // 
            // openFile2
            // 
            this.openFile2.FileName = "openFileDialog1";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(588, 5);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(50, 15);
            this.lblHeader.TabIndex = 6;
            this.lblHeader.Text = "Header";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(595, 541);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Footer";
            // 
            // btnHeader
            // 
            this.btnHeader.Location = new System.Drawing.Point(38, 269);
            this.btnHeader.Name = "btnHeader";
            this.btnHeader.Size = new System.Drawing.Size(117, 23);
            this.btnHeader.TabIndex = 0;
            this.btnHeader.Text = "Select Header File...";
            this.btnHeader.UseVisualStyleBackColor = true;
            this.btnHeader.Click += new System.EventHandler(this.btnHeader_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(38, 555);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Select Footer File...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtHeader
            // 
            this.txtHeader.Enabled = false;
            this.txtHeader.Location = new System.Drawing.Point(162, 269);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(193, 20);
            this.txtHeader.TabIndex = 1;
            // 
            // txtFooter
            // 
            this.txtFooter.Enabled = false;
            this.txtFooter.Location = new System.Drawing.Point(162, 558);
            this.txtFooter.Name = "txtFooter";
            this.txtFooter.Size = new System.Drawing.Size(193, 20);
            this.txtFooter.TabIndex = 3;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(512, 641);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(126, 23);
            this.btnUpload.TabIndex = 6;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(135, 584);
            this.txtDescription.MaxLength = 150;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(503, 51);
            this.txtDescription.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(41, 587);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 36);
            this.label4.TabIndex = 4;
            this.label4.Text = "Type a brief description";
            // 
            // ManageLogo
            // 
            this.AcceptButton = this.btnUpload;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 689);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.txtFooter);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnHeader);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.pixFooter);
            this.Controls.Add(this.pixHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ManageLogo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ManageLogo";
            ((System.ComponentModel.ISupportInitialize)(this.pixHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixFooter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pixHeader;
        private System.Windows.Forms.PictureBox pixFooter;
        private System.Windows.Forms.OpenFileDialog openFile1;
        private System.Windows.Forms.OpenFileDialog openFile2;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnHeader;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.TextBox txtFooter;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label4;
    }
}