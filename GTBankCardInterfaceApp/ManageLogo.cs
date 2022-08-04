using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace GTBankCardInterfaceApp
{
    public partial class ManageLogo : Form
    {
        SqlConnection PinPadCon = new SqlConnection(AdminUser.PinPadCon);

        public ManageLogo()
        {
            InitializeComponent();
        }

        private void btnHeader_Click(object sender, EventArgs e)
        {
            openFile1.ShowDialog();

            if (!string.IsNullOrEmpty(openFile1.FileName))
            {
                txtHeader.Text = openFile1.FileName;
                pixHeader.ImageLocation = openFile1.FileName;
                txtHeader.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFile2.ShowDialog();

            if (!string.IsNullOrEmpty(openFile2.FileName))
            {
                txtFooter.Text = openFile2.FileName;
                pixFooter.ImageLocation = openFile2.FileName;
                txtFooter.Enabled = false;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (txtDescription.Text.Equals(""))
            {
                MessageBox.Show("Please Type a brief Description for this Upload");
                return;
            }

            try
            {
                FileStream header = new FileStream(txtHeader.Text, FileMode.Open);
                byte[] HeaderData = new byte[header.Length + 1];
                header.Read(HeaderData, 0, (int)header.Length);

                FileStream footer = new FileStream(txtFooter.Text, FileMode.Open);
                byte[] FooterData = new byte[footer.Length + 1];
                footer.Read(FooterData, 0, (int)footer.Length);

                SqlConnection con = new SqlConnection(AdminUser.PinPadCon);
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_ImagesInsert")
                {
                    Connection = con,
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Header", HeaderData);
                cmd.Parameters.AddWithValue("@Footer", FooterData);
                cmd.Parameters.AddWithValue("@CreatedBy", AdminUser.username);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@IsActive", 1);
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                header.Close();

                MessageBox.Show("Images Uploaded Succesfully.");
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                MessageBox.Show("An Error Occured Uploading Image: " + ex.Message);
            }
        }    
    }
}