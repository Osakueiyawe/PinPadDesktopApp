using System;
using System.Windows.Forms;

namespace GTBankCardInterfaceApp
{
    public partial class CashAnalysis : Form
    {
        Utilities util = new Utilities();
        public CashAnalysis()
        {
            InitializeComponent();
        }
        private void CashAnalysis_Load(object sender, EventArgs e)
        {
            lblCusNo.Text = AdminUser.CusDepositAccount;

            txtCusTotal.Text = AdminUser.AnalyisAmount;
        }

        public void refreshTotal()
        {
            try
            {
                string coin = "";
                string five = "";
                string ten = "";
                string twenty = "";
                string fifty = "";
                string hundred = "";
                string twohundred = "";
                string fivehundred = "";
                string onethousand = "";

                if (string.IsNullOrEmpty(txtCoin.Text))
                {
                    coin = "0";
                }
                else
                {
                    coin = txtCoin.Text;
                }

                if (string.IsNullOrEmpty(txt5.Text))
                {
                    five = "0";
                }
                else
                {
                    five = txt5.Text;
                }
                if (string.IsNullOrEmpty(txt10.Text))
                {
                    ten = "0";
                }
                else
                {
                    ten = txt10.Text;
                }
                if (string.IsNullOrEmpty(txt20.Text))
                {
                    twenty = "0";
                }
                else
                {
                    twenty = txt20.Text;
                }
                if (string.IsNullOrEmpty(txt50.Text))
                {
                    fifty = "0";
                }
                else
                {
                    fifty = txt50.Text;
                }
                if (string.IsNullOrEmpty(txt100.Text))
                {
                    hundred = "0";
                }
                else
                {
                    hundred = txt100.Text;
                }
                if (string.IsNullOrEmpty(txt200.Text))
                {
                    twohundred = "0";
                }
                else
                {
                    twohundred = txt200.Text;
                }
                if (string.IsNullOrEmpty(txt500.Text))
                {
                    fivehundred = "0";
                }
                else
                {
                    fivehundred = txt500.Text;
                }
                if (string.IsNullOrEmpty(txt1000.Text))
                {
                    onethousand = "0";
                }
                else
                {
                    onethousand = txt1000.Text;
                }
                txtTotal.Text = (Convert.ToDouble(coin) + Convert.ToUInt32(lbl5.Text) * Convert.ToUInt32(five) + Convert.ToUInt32(lbl10.Text) * Convert.ToUInt32(ten) + Convert.ToUInt32(lbl20.Text) * Convert.ToUInt32(twenty) + Convert.ToUInt32(lbl50.Text) * Convert.ToUInt32(fifty) + Convert.ToInt32(lbl100.Text) * Convert.ToUInt32(hundred) + Convert.ToInt32(lbl200.Text) * Convert.ToUInt32(twohundred) + Convert.ToInt32(lbl500.Text) * Convert.ToUInt32(fivehundred) + Convert.ToInt32(lbl1000.Text) * Convert.ToUInt32(onethousand)).ToString();

            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                MessageBox.Show("Invalid value");
                return;
            }
        }

        public void refreshAnalysis()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCoin.Text))
                {
                    if (Convert.ToDouble(txtCoin.Text) != 0)
                    {
                        lblAnalysed.Text = lblAnalysed.Text + lblcoin.Text + "x" + (Convert.ToDouble(txtCoin.Text)).ToString() + ",";
                    }
                }

                if (!string.IsNullOrEmpty(txt5.Text))
                {
                    if (Convert.ToInt32(txt5.Text) != 0)
                    {
                        lblAnalysed.Text = lblAnalysed.Text + "N" + lbl5.Text + "x" + (Convert.ToInt32(txt5.Text)).ToString() + ",";
                    }
                }

                if (!string.IsNullOrEmpty(txt10.Text))
                {
                    if (Convert.ToInt32(txt10.Text) != 0)
                    {
                        lblAnalysed.Text = lblAnalysed.Text + "N" + lbl10.Text + "x" + (Convert.ToInt32(txt10.Text)).ToString() + ",";
                    }
                }

                if (!string.IsNullOrEmpty(txt20.Text))
                {
                    if (Convert.ToInt32(txt20.Text) != 0)
                    {
                        lblAnalysed.Text = lblAnalysed.Text + "N" + lbl20.Text + "x" + (Convert.ToInt32(txt20.Text)).ToString() + ",";
                    }
                }

                if (!string.IsNullOrEmpty(txt50.Text))
                {
                    if (Convert.ToInt32(txt50.Text) != 0)
                    {
                        lblAnalysed.Text = lblAnalysed.Text + "N" + lbl50.Text + "x" + (Convert.ToInt32(txt50.Text)).ToString() + ",";
                    }
                }
                if (!string.IsNullOrEmpty(txt100.Text))
                {
                    if (Convert.ToInt32(txt100.Text) != 0)
                    {
                        lblAnalysed.Text = lblAnalysed.Text + "N" + lbl100.Text + "x" + (Convert.ToInt32(txt100.Text)).ToString() + ",";
                    }
                }
                if (!string.IsNullOrEmpty(txt200.Text))
                {
                    if (Convert.ToInt32(txt200.Text) != 0)
                    {
                        lblAnalysed.Text = lblAnalysed.Text + "N" + lbl200.Text + "x" + (Convert.ToInt32(txt200.Text)).ToString() + ",";
                    }
                }
                if (!string.IsNullOrEmpty(txt500.Text))
                {
                    if (Convert.ToInt32(txt500.Text) != 0)
                    {
                        lblAnalysed.Text = lblAnalysed.Text + "N" + lbl500.Text + "x" + (Convert.ToInt32(txt500.Text)).ToString() + ",";
                    }
                }
                if (!string.IsNullOrEmpty(txt1000.Text))
                {
                    if (Convert.ToInt32(txt1000.Text) != 0)
                    {
                        lblAnalysed.Text = lblAnalysed.Text + "N" + lbl1000.Text + "x" + (Convert.ToInt32(txt1000.Text)).ToString() + ",";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                MessageBox.Show("Invalid value");
            }
        }

        private void txt5_TextChanged(object sender, EventArgs e)
        {
            refreshTotal();

            if (Convert.ToDouble(txtTotal.Text) == Convert.ToDouble(txtCusTotal.Text))
            {
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }

        private void txt10_TextChanged(object sender, EventArgs e)
        {
            refreshTotal();

            if (Convert.ToDouble(txtTotal.Text) == Convert.ToDouble(txtCusTotal.Text))
            {
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }

        private void txt20_TextChanged(object sender, EventArgs e)
        {
            refreshTotal();

            if (Convert.ToDouble(txtTotal.Text) == Convert.ToDouble(txtCusTotal.Text))
            {
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }

        private void txt50_TextChanged(object sender, EventArgs e)
        {
            refreshTotal();

            if (Convert.ToDouble(txtTotal.Text) == Convert.ToDouble(txtCusTotal.Text))
            {
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }

        private void txt100_TextChanged(object sender, EventArgs e)
        {
            refreshTotal();

            if (Convert.ToDouble(txtTotal.Text) == Convert.ToDouble(txtCusTotal.Text))
            {
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }

        private void txt200_TextChanged(object sender, EventArgs e)
        {
            refreshTotal();

            if (Convert.ToDouble(txtTotal.Text) == Convert.ToDouble(txtCusTotal.Text))
            {
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }

        private void txt500_TextChanged(object sender, EventArgs e)
        {
            refreshTotal();

            if (Convert.ToDouble(txtTotal.Text) == Convert.ToDouble(txtCusTotal.Text))
            {
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }

        private void txt1000_TextChanged(object sender, EventArgs e)
        {
            refreshTotal();

            if (Convert.ToDouble(txtTotal.Text) == Convert.ToDouble(txtCusTotal.Text))
            {
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }

        private void txt5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            lblAnalysed.Text = "";
            refreshAnalysis();
            AdminUser.CashAnalysis = lblAnalysed.Text;
            AdminUser.AnalysedAmount = txtTotal.Text;
            Close();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            txtTotal.Text = util.FormatString(txtTotal.Text, 2);
            txtTotal.Select(txtTotal.TextLength, 0);
        }

        private void txtCusTotal_TextChanged(object sender, EventArgs e)
        {
            txtTotal.Text = util.FormatString(txtTotal.Text, 2);
        }

        private void txtCoin_TextChanged(object sender, EventArgs e)
        {
            refreshTotal();

            if (Convert.ToDouble(txtTotal.Text) == Convert.ToDouble(txtCusTotal.Text))
            {
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }
    }
}
