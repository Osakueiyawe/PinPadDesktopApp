using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using Leadtools.Codecs;
using System.Threading;

namespace GTBankCardInterfaceApp
{
    public partial class CardLessDeposit : Form
    {
        enum PinPadRes { Success = 0, Error = 1, InProgress = 2 };
        RasterCodecs codec;
        Utilities u = new Utilities();
        CustDetRetVal custdetails = null;
        string remark = "";
        string PostResult = "";
        ulong InsertResult = 0;
        byte[] imagedata = new byte[8056];
        object[] Images = null;
        int noofimages = 0;
        int i = 0;
        public CardLessDeposit()
        {
            InitializeComponent();
        }
        private delegate void AddLogText(string text);
        private delegate void ShowProgressBar(ProgressBar b);
        private delegate void ToggleButton(Button a);
        private delegate void EnableButton(Button a);
        private delegate void DisableList(ListBox a);
        private void TS_AddLogText(string text)
        {
            if (InvokeRequired)
            {
                AddLogText del = new AddLogText(TS_AddLogText);
                Invoke(del, text);
            }
            else
            {
                lblPostProgress.Text = text;
            }
        }

        private void TS_ToggleButton(Button a)
        {
            if (InvokeRequired)
            {
                ToggleButton del = new ToggleButton(TS_ToggleButton);
                Invoke(del, a);
            }
            else
            {
                if (a.Enabled == true)
                {
                    a.Enabled = false;
                }
            }
        }

        private void TS_EnableButton(Button a)
        {
            if (InvokeRequired)
            {
                ToggleButton del = new ToggleButton(TS_EnableButton);
                Invoke(del, a);
            }
            else
            {
                if (a.Enabled == false)
                {
                    a.Enabled = true;
                }
            }
        }
        private void TS_DisableList(ListBox a)
        {
            if (InvokeRequired)
            {
                DisableList del = new DisableList(TS_DisableList);
                Invoke(del, a);
            }
            else
            {
                a.Enabled = false;
            }
        }
        private void TS_ShowProgressBar(ProgressBar a)
        {
            if (InvokeRequired)
            {
                ShowProgressBar del = new ShowProgressBar(TS_ShowProgressBar);
                Invoke(del, a);
            }
            else
            {
                if (a.Visible == false)
                {
                    a.Visible = true;
                }
                else if (a.Visible == true)
                {
                    a.Visible = false;
                }
            }
        }

        //private void btnGetDetails_Click(object sender, EventArgs e)
        //{
        //    AppDevService appserve = new AppDevService();
        //     if (string.IsNullOrEmpty(txtNubanAccount.Text))
        //    {

        //        MessageBox.Show("Please Enter the Account Number", "GTBankCardInterfaceApp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        txtNubanAccount.Focus();
        //        return;

        //    }
        //    if (txtNubanAccount.Text.Length < 10)
        //    {


        //        MessageBox.Show("The Account Number Length should 10", "GTBankCardInterfaceApp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        txtNubanAccount.Focus();
        //        return;
        //    }
        //    if(!u.verifyNubanCheckDigit("058",txtNubanAccount.Text))
        //    {

        //        MessageBox.Show("The account check digit is wrong the account is Incorrect", "GTBankCardInterfaceApp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        txtNubanAccount.Focus();
        //        return;

        //    }


        //    if (Utilities.isServerUp(ConfigurationSettings.AppSettings["ServerIP"].ToString(), Convert.ToInt16(ConfigurationSettings.AppSettings["ServerPort"]), Convert.ToInt16(ConfigurationSettings.AppSettings["PingTimeOut"])))
        //    {

        //      //   t = new Converter(ConfigurationSettings.AppSettings["Oracleconstring"]);
        //        string oldaccountnumber = appserve.ConvertToOldAccountNumber(txtNubanAccount.Text);

        //        if (oldaccountnumber.Equals("-1") || oldaccountnumber.Equals("-2")||oldaccountnumber.ToUpper().Contains("ERROR"))
        //        {
        //            MessageBox.Show("The Account Number is Invalid.");
        //            return;
        //        }
        //        else
        //        {
        //            try
        //            {
        //                AdminUser.CusDepositAccount = oldaccountnumber;//set account to the entered account this is useful for deposit operations only.
        //                string[] accountkey = new string[4];// 
        //                string bra_code = null;
        //                string cus_num = null;
        //                string cur_code = null;
        //                string led_code = null;
        //                string sub_acct_code = null;
        //                accountkey = oldaccountnumber.Split('/');
        //                bra_code = accountkey[0];
        //                cus_num = accountkey[1];
        //                cur_code = accountkey[2];
        //                led_code = accountkey[3];
        //                sub_acct_code = accountkey[4];
        //                txtBraCode.Text = bra_code;
        //                txtCusNum.Text = cus_num;
        //                pnlAcctDetails.Visible = true;

        //                lstAccounts.Items.Clear();

        //                if (rasterImageList1.Items.Count > 0)
        //                {
        //                    rasterImageList1.Items.Clear();
        //                }



        //                if (!getCustomerDetails(Convert.ToInt16(txtBraCode.Text), Convert.ToInt32(txtCusNum.Text)))
        //                {
        //                    MessageBox.Show("No Details For the customer", "GTBankCardInterfaceApp", MessageBoxButtons.OK, MessageBoxIcon.Stop);

        //                    return;
        //                }
        //                else
        //                {
        //                    btnPinPadDetails.Enabled = false;
        //                    btnGetDetails.Enabled = false;
        //                    txtBraCode.Enabled = false;
        //                    txtCusNum.Enabled = false;
        //                    txtTransAmount.Enabled = true;

        //                    txtNubanAccount.Enabled = false;

        //                }

        //                if (lstAccounts.Items.Count > 0)
        //                {
        //                    pnlTransaction.Visible = true;

        //                }
        //                else { pnlTransaction.Visible = false; }
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("An Error Ocurred" + ex.Message);


        //            }
        //        }
        //    }
        //    else
        //    {//Basis is not accessible Deposit Transaction Only
        //        pnlTransaction.Visible = true;

        //        pnlAcctDetails.Visible = false;
        //        AdminUser.LoginMode = "Offline";
        //        AdminUser.CusDepositAccount = txtNubanAccount.Text;
        //        txtNubanAccount.Enabled = false;
        //        MessageBox.Show("Basis is not accessible Deposit Transaction Only");


        //    }


        //}

        public bool getCustomerDetails(int bracode, int cusnum)
        {
            int appid = Convert.ToInt16(AdminUser.ApplicationID);
            AppDevService appserve = new AppDevService();
            string[] accounts = null;
            string[] mandates = null;
            Images = null;
            noofimages = 0;
            XmlDocument document = null;
            XPathNavigator navigator = null;
            XPathNodeIterator snodes = null;
            custdetails = appserve.GetBasisCustomerDetails(bracode, cusnum);
            document = new XmlDocument();
            document.LoadXml(custdetails.custdet.Replace("&", " and "));
            navigator = document.CreateNavigator();
            snodes = navigator.Select("/Response/CODE");
            snodes.MoveNext();
            string retcode = snodes.Current.Value;
            if (retcode != "1000")
            {
                snodes = navigator.Select("/Response/ERROR");
                snodes.MoveNext();
                return false;
            }
            accounts = custdetails.Accounts;
            Images = custdetails.picture;
            mandates = custdetails.Mandates;
            noofimages = Images.Length;
            snodes = navigator.Select("/Response/CUSTOMERS/CUSTOMER/SIGNATORY_NAME");
            snodes.MoveNext();
            txtCusName.Text = snodes.Current.Value;
            snodes = navigator.Select("/Response/CUSTOMERS/CUSTOMER/CUST_TYPE");
            snodes.MoveNext();
            lblCustomerLevel.Text = "LEVEL " + snodes.Current.Value;
            AdminUser.CustomerLevel = snodes.Current.Value;

            if (AdminUser.roleid.Equals(AdminUser.TellerRoleId))
            {
                txtTellerTillDr.Text = AdminUser.tillaccount;
            }
            else
            {
                txtTellerTillDr.Text = "1/1/1/1/0";
            }

            AdminUser.CusName = snodes.Current.Value;

            return true;
        }

        private void btnWithdrawal_Click(object sender, EventArgs e)
        {
            AdminUser.CusWithdrawalAccount = txtCusAcctCredit.Text;
        }

        private void btnClearScreen_Click(object sender, EventArgs e)
        {
            lblProgress.Text = "-";
            lblTransProgress.Text = "-";
            btnPinPadDetails.Enabled = true;
            txtBraCode.Text = "";
            txtCusNum.Text = "";
            txtCurCode.Text = "";
            txtLedCode.Text = "";
            txtSubAcctCode.Text = "";
            txtNubanAcct.Text = "";
            pnlTransaction.Visible = false;
            btnSubmitDr.Enabled = true;
            btnCancel.Enabled = true;
            btnCashAnalysis.Enabled = true;
            txtCusName.Text = "";
            txtTellerTillDr.Text = "";
            txtCusAcctCredit.Text = "";
            txtTransAmount.Text = "";
            AdminUser.AnalyisAmount = "0";
            cmbAccountType.SelectedIndex = 0;
            pnlAcctDetails.Enabled = false;
            pnlNuban.Enabled = false;
            pnlaccts.Visible = false;
            cmbAccountType.Enabled = true;
            lblPostProgress.Text = "";
        }

        private void CustomerDetail_Load(object sender, EventArgs e)
        {
            AdminUser.LoginMode = "Online";
            AdminUser.AnalyisAmount = "0";
        }

        private void btnSubmitDr_Click(object sender, EventArgs e)
        {
            string curcode = "";
            curcode = u.getcurcode(txtCusAcctCredit.Text);

            if (txtTransAmount.Text.Equals(""))
            {
                MessageBox.Show("Please enter the transaction Amount");
                return;
            }
            if (!u.checkifnumeric(txtTransAmount.Text))
            {
                MessageBox.Show("Please Enter a Valid Transaction Amount");
                return;
            }

            if (Convert.ToDouble(txtTransAmount.Text) == 0)
            {
                MessageBox.Show("Transaction Amount Cannot be zero");
                return;
            }

            if (Convert.ToDouble(AdminUser.AnalysedAmount) != (Convert.ToDouble(txtTransAmount.Text)))
            {

                MessageBox.Show("The analysed amount is different from the tendered cash. Please analyse again");
                return;
            }

            if (AdminUser.CustomerLevel.Equals("11"))
            {
                if (Convert.ToDouble(txtTransAmount.Text) > Convert.ToDouble(AdminUser.Level1TransactionLimit))
                {
                    MessageBox.Show("The transaction amount is greater than the maximum single transaction amount allowed for this customer's Level.");
                    return;
                }

                if ((Convert.ToDouble(txtTransAmount.Text) + Convert.ToDouble(AdminUser.AvailableBalance)) > Convert.ToDouble(AdminUser.Level1BalanceLimit))
                {
                    MessageBox.Show("The transaction will make the customer have more than the allowed Balance Limit for this customer's Level.");
                    return;
                }
            }
            if (AdminUser.CustomerLevel.Equals("12"))
            {
                if (Convert.ToDouble(txtTransAmount.Text) > Convert.ToDouble(AdminUser.Level2TransactionLimit))
                {
                    MessageBox.Show("The transaction amount is greater than the maximum single transaction amount allowed for this customer's Level.");
                    return;
                }
                if ((Convert.ToDouble(txtTransAmount.Text) + Convert.ToDouble(AdminUser.AvailableBalance)) > Convert.ToDouble(AdminUser.Level2BalanceLimit))
                {
                    MessageBox.Show("The transaction will make the customer have more than the allowed Balance Limit for this customer's Level.");
                    return;
                }
            }
            if (string.IsNullOrEmpty(txtCusName.Text))
            {
                MessageBox.Show("Please Enter the Depositors Name");
                return;
            }

            Thread thread = new Thread(new ThreadStart(Post));
            thread.IsBackground = true;
            thread.Name = "CardHolderDeposit";
            thread.Start();
        }

        private void Post()
        {
            try
            {
                remark = "";
                PostResult = "";
                InsertResult = 0;
                string restriction = "";
                string curcode = "";
                AppDevService appserve = new AppDevService();
                Utilities util = new Utilities();
                Transaction Trans = new Transaction();

                TS_ToggleButton(btnSubmitDr);
                TS_ToggleButton(btnCancel);
                TS_ToggleButton(btnCashAnalysis);
                TS_ShowProgressBar(PostingprogressBar);
                TS_AddLogText("Processing Transaction ......");
                AdminUser.SVAccountNumber = txtCusAcctCredit.Text;

                InsertResult = Trans.InitiateTransaction(AdminUser.branchcode, AdminUser.TellerId, txtCusAcctCredit.Text, Convert.ToDecimal(txtTransAmount.Text), AdminUser.LoginMode, "DEPOSIT", lblAccountName.Text.Trim(), txtTellerTillDr.Text, txtCusName.Text, "CardLess", CardAuth.Stan, "CardLess Teller-" + AdminUser.TellerId, CardAuth.AuthCode, CardAuth.AuthAmount, lblAnalysis.Text);
                ulong update = 0;
                if (InsertResult != 0)
                {
                    remark = "PINPAD-" + InsertResult + "-" + AdminUser.branchcode + "/" + AdminUser.TellerId + " CASH DEPOSIT BY " + txtCusName.Text.ToUpper();
                    PostResult = Trans.PostToBasis(txtCusAcctCredit.Text, txtTellerTillDr.Text, Convert.ToDouble(txtTransAmount.Text), Convert.ToUInt16(AdminUser.DepositExpl_Code), remark, "DEPOSIT", AdminUser.TellerId, AdminUser.branchcode);
                    curcode = util.getcurcode(txtCusAcctCredit.Text);

                    if (PostResult.Equals("0"))
                    {
                        update = Trans.UpdateTransaction(AdminUser.TellerId, InsertResult, AdminUser.branchcode, txtTellerTillDr.Text, remark, "APPROVED", "SUCCESS", AdminUser.LoginMode, AdminUser.tillaccount, AdminUser.DepositExpl_Code);

                        if (Trans.PrintReceipt(update.ToString(), "DEPOSIT", AdminUser.TellerId + "/" + InsertResult.ToString(), txtNubanAcct.Text, txtCusName.Text, Convert.ToDouble(txtTransAmount.Text), txtCusName.Text, AdminUser.branchname, curcode))
                        {
                            Trans.UpdatePrintStatus(InsertResult, 1);

                            MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence:" + update.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Transaction Succesful. Receipt Failed to Print Check the printer and try Printing again from the Printing Menu:" + update.ToString());
                        }
                    }

                    else if (PostResult.Equals("1"))
                    {
                        AdminUser.IsOverAuthlimit = true;

                        Trans.UpdateTransactionForApproval(InsertResult);

                        MessageBox.Show("Transaction Awaiting Approval.");
                    }
                    else
                    {
                        Trans.UpdateTransaction(AdminUser.TellerId, InsertResult, AdminUser.branchcode, txtTellerTillDr.Text, remark, "FAILED", PostResult, AdminUser.LoginMode, AdminUser.tillaccount, AdminUser.DepositExpl_Code);

                        MessageBox.Show("Transaction Failed. Please Reinitiate. Error is::" + PostResult);
                    }
                }
                else
                {
                    MessageBox.Show("An Error Occured Initiating Transaction Please try Again..");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occured Initiating Transaction Please try Again.. " + ex.Message);
            }

            finally
            {
                TS_AddLogText("Transaction Completed...");
                TS_EnableButton(btnCancel);
                TS_ShowProgressBar(PostingprogressBar);
                AdminUser.SVAccountNumber = "";
                AdminUser.CashAnalysis = "";
                Thread.CurrentThread.Abort();
                cmbAccountType.SelectedIndex = 0;
                pnlNuban.Enabled = false;
            }
        }

        private void btnPinPadDetails_Click(object sender, EventArgs e)
        {
            btnPinPadDetails.Enabled = false;
            lblProgress.Text = "";
            string[] cusdetails = null;
            string accountnumber = string.Empty;

            try
            {
                AppDevService appserve = new AppDevService();

                if (cmbAccountType.SelectedIndex == 1)
                {
                    if (string.IsNullOrEmpty(txtNubanAcct.Text))
                    {
                        MessageBox.Show("Please Enter the NUBAN");
                        return;
                    }

                    if (txtNubanAcct.Text.Length != 10)
                    {
                        MessageBox.Show("Please Enter a 10 digit NUBAN");
                        return;
                    }
                    accountnumber = txtNubanAcct.Text;
                    cusdetails = appserve.getcusDepositDetails(accountnumber);

                }
                else if (cmbAccountType.SelectedIndex == 2)
                {
                    if (string.IsNullOrEmpty(txtBraCode.Text))
                    {
                        MessageBox.Show("Please Enter the branch code");
                        return;

                    }
                    if (string.IsNullOrEmpty(txtCusNum.Text))
                    {
                        MessageBox.Show("Please Enter the Customer number");
                        return;

                    }
                    if (string.IsNullOrEmpty(txtCurCode.Text))
                    {
                        MessageBox.Show("Please Enter the currency code");
                        return;

                    }
                    if (string.IsNullOrEmpty(txtLedCode.Text))
                    {
                        MessageBox.Show("Please Enter the ledger code");
                        return;

                    }
                    if (string.IsNullOrEmpty(txtSubAcctCode.Text))
                    {
                        MessageBox.Show("Please Enter the sub account code");
                        return;

                    }

                    accountnumber = txtBraCode.Text + "/" + txtCusNum.Text + "/" + txtCurCode.Text + "/" + txtLedCode.Text + "/" + txtSubAcctCode.Text;

                    cusdetails = appserve.getcusDepositDetailsOldAccount(accountnumber);
                }

                if ((cusdetails != null) && (!string.IsNullOrEmpty(cusdetails[0].ToString())))
                {
                    if (AdminUser.roleid.Equals(AdminUser.TellerRoleId))
                    {
                        txtTellerTillDr.Text = AdminUser.tillaccount;
                    }
                    else
                    {
                        txtTellerTillDr.Text = "1/1/1/1/0";
                    }

                    lblProgress.Text = "Account Details Successfully Validated.";
                    lblProgress.ForeColor = Color.Green;
                    btnPinPadDetails.Enabled = false;
                    pnlaccts.Visible = true;
                    pnlTransaction.Visible = true;

                    lblCustomerLevel.Text = "LEVEL " + cusdetails[2].ToString().Trim();
                    AdminUser.CustomerLevel = cusdetails[2].ToString().Trim();
                    AdminUser.AvailableBalance = cusdetails[4].ToString().Trim();
                    txtCusAcctCredit.Text = cusdetails[5].ToString().Trim();
                    lblAccountName.Text = cusdetails[3].ToString().Trim();

                    string restriction = appserve.checkrestriction(txtCusAcctCredit.Text);
                    lstRestrictions.Items.Clear();
                    if (!restriction.Equals("0"))
                    {
                        string[] restrictionarray = restriction.Split(',');

                        foreach (string s in restrictionarray)
                        {
                            lstRestrictions.Items.Add(s);
                        }
                    }
                    else
                    {
                        lstRestrictions.Items.Add("NONE");
                    }

                    btnSubmitDr.Enabled = true;
                }
                else
                {
                    btnSubmitDr.Enabled = false;
                    MessageBox.Show("Error retrieving the account details cannot continue");
                    return;
                }
            }
            catch (Exception ex)
            {
                lblProgress.Text = "An error occured: " + ex.Message;
            }
        }
        private void lstAccounts_SelectedValueChanged(object sender, EventArgs e)
        {
            //populate the account fields;
            //string availbalance = "";
            //string[] accounts = new string[2];//
            //accounts =  lstAccounts.SelectedItem.ToString().Split('-');
            //txtNuban.Text = accounts[0].Replace("(","").Replace(")","");
            //txtCusAcctCredit.Text = accounts[1];
            //availbalance = accounts[2].Replace("(", "").Replace(")", "");
            //AdminUser.AvailableBalance = accounts[2].Replace("(", "").Replace(")", "");
            ////txtNubanAccount.Text = accounts[0].Replace("(", "").Replace(")", "");
            ////Verify the account to debit and ensure that the currency matches.
            //string[] telleraccts = new string[5];
            //string[] custaccts = new string[5];
            //telleraccts = txtTellerTillDr.Text.Split('/');
            //custaccts = txtCusAcctCredit.Text.Split('/');
            //telleraccts[2] = custaccts[2];//assign the currency code of the cus account to the teller account.
            //txtTellerTillDr.Text = telleraccts[0].ToString() + "/" + telleraccts[1].ToString() + "/" + telleraccts[2].ToString() + "/" + telleraccts[3].ToString() + "/" + telleraccts[4].ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AdminUser.SVAccountNumber = "";
            AdminUser.CashAnalysis = "";
            Close();
        }

        private void txtTransAmount_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                double val = double.Parse((sender as TextBox).Text);
                txtTransAmount.Text = val.ToString("0.00");
            }
            catch(Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                MessageBox.Show("Amount must be numeric and Only Two Decimal Place Allowed");
            }
        }
        private void txtTransAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !(e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }

        private void txtTransAmount_KeyUp(object sender, KeyEventArgs e)
        {
            txtTransAmount.Text = u.FormatString(txtTransAmount.Text, 2);
            txtTransAmount.Select(txtTransAmount.TextLength, 0);
        }

        private void txtTransAmount_TextChanged(object sender, EventArgs e)
        {
            txtTransAmount.Text = u.FormatString(txtTransAmount.Text, 2);
            txtTransAmount.Select(txtTransAmount.TextLength, 0);
        }

        private void btnCashAnalysis_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTransAmount.Text))
            {
                AdminUser.AnalyisAmount = txtTransAmount.Text;
                AdminUser.CusDepositAccount = txtCusAcctCredit.Text;
                CashAnalysis cashAnalysis = new CashAnalysis();
                cashAnalysis.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please supply the transaction amount...");
                return;
            }
        }

        private void btnSubmitDr_MouseEnter(object sender, EventArgs e)
        {
            lblAnalysis.Text = AdminUser.CashAnalysis;
        }

        private void cmbAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAccountType.SelectedIndex == 1)
            {
                pnlaccts.Visible = true;
                pnlNuban.Enabled = true;
                pnlAcctDetails.Enabled = false;
            }
            else if (cmbAccountType.SelectedIndex == 2)
            {
                pnlaccts.Visible = true;
                pnlNuban.Enabled = false;
                pnlAcctDetails.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please select a valid account type to continue");
                return;
            }
        }
    }
}