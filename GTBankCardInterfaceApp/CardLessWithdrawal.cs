using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using Leadtools.Codecs;
using Leadtools.WinForms;
using System.Threading;

namespace GTBankCardInterfaceApp
{
    public partial class CardLessWithdrawal : Form
    {
        enum PinPadRes { Success = 0, Error = 1, InProgress = 2 };
        RasterCodecs codec;
        Utilities u = new Utilities();
        string remark = "";
        string PostResult = "";
        ulong InsertResult = 0;
        CustDetRetVal custdetails = null;
        byte[] imagedata = new byte[8056];
        object[] Images = null;
        int noofimages = 0;
        int i = 0;
        public CardLessWithdrawal()
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

        public bool getCustomerDetails(int bracode, int cusnum)
        {
            int appid = Convert.ToInt16(AdminUser.ApplicationID);
            AppDevService appserve = new AppDevService();
            string[] accounts = null;
            string[] mandates = null;
            string cusmandate = "";
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

            if (!(accounts == null))
            {
                foreach (string account in accounts)
                {
                    lstAccounts.Items.Add("(" + account + ")");
                }
            }
            if (!(mandates == null))
            {
                foreach (string mandate in mandates)
                {
                    cusmandate = cusmandate + " " + mandate;
                }
            }
            else
            {
                lstAccounts.Items.Add("No Suitable Account For Transactions");
            }
            if (!(Images == null))
            {
                int i = 0;
                foreach (object pix in Images)
                {
                    try
                    {
                        imagedata = (byte[])pix;
                        MemoryStream myImage = new MemoryStream(imagedata, true);
                        codec = new RasterCodecs();
                        RasterImageListItem a = new RasterImageListItem();
                        a.Image = codec.Load(myImage);
                        a.Text = mandates[i];
                        rasterImageList1.Items.Add(a);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        ErrHandler.WriteError(ex.ToString());
                        continue;
                    }
                }
            }
            else
            {
                MessageBox.Show("No Image For the customer");
            }
            snodes = navigator.Select("/Response/CUSTOMERS/CUSTOMER/SIGNATORY_NAME");
            snodes.MoveNext();
            txtCusName.Text = snodes.Current.Value;
            txtTellerTillDr.Text = AdminUser.tillaccount;
            snodes = navigator.Select("/Response/CUSTOMERS/CUSTOMER/CUST_TYPE");
            snodes.MoveNext();
            txtCusName.Text = "LEVEL " + snodes.Current.Value;
            AdminUser.CustomerLevel = snodes.Current.Value;
            AdminUser.CusName = snodes.Current.Value;

            return true;
        }

        private void btnWithdrawal_Click(object sender, EventArgs e)
        {
            if (lstAccounts.SelectedItem == null)
            {

                MessageBox.Show("Please Select an account for the transaction");
                return;
            }

            AdminUser.CusWithdrawalAccount = lstAccounts.SelectedItem.ToString();
        }

        private void btnClearScreen_Click(object sender, EventArgs e)
        {
            lblProgress.Text = "";
            lblTransProgress.Text = "";
            btnGetDetails.Enabled = true;
            panel1.Visible = false;
            txtBraCode.Text = "";
            txtCusNum.Text = "";
            lstAccounts.Items.Clear();
            pnlTransaction.Visible = false;
            lstAccounts.Enabled = true;
            txtCusName.Text = "";
            txtNuban.Text = "";
            txtTellerTillDr.Text = "";
            txtCusAcctDebit.Text = "";
            txtTransAmount.Text = "";
            if (rasterImageList1.Items.Count > 0)
            {
                rasterImageList1.Items.Clear();
            }
        }

        private void CustomerDetail_Load(object sender, EventArgs e)
        {
            AdminUser.LoginMode = "Online";
        }

        private void btnSubmitDr_Click(object sender, EventArgs e)
        {
            string curcode = "";
            curcode = u.getcurcode(txtCusAcctDebit.Text);
            if (lstAccounts.SelectedItem == null)
            {
                MessageBox.Show("Please Select an account for the transaction");
                return;
            }

            if (txtTransAmount.Text.Equals(""))
            {
                MessageBox.Show("Please enter the transaction Ammount");
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

            lstAccounts.Enabled = false;
            Thread thread = new Thread(new ThreadStart(Post));
            thread.IsBackground = true;
            thread.Name = "CardLessWithDrawal";
            thread.Start();
        }

        private void Post()
        {
            remark = "";
            PostResult = "";
            InsertResult = 0;

            try
            {
                AppDevService appserve = new AppDevService();
                string restriction = "";
                string curcode = "";
                Utilities util = new Utilities();
                Transaction Trans = new Transaction();
                TS_ToggleButton(btnSubmitDr);
                TS_ToggleButton(btnCancel);
                TS_ShowProgressBar(PostingprogressBar);

                TS_AddLogText("Processing Transaction ......");

                AdminUser.SVAccountNumber = txtCusAcctDebit.Text;

                InsertResult = Trans.InitiateTransaction(AdminUser.branchcode, AdminUser.TellerId, txtCusAcctDebit.Text, Convert.ToDecimal(txtTransAmount.Text), AdminUser.LoginMode, "WITHDRAWAL", txtCusName.Text.Trim(), txtTellerTillDr.Text, txtCusName.Text, CardAuth.CardNumber, CardAuth.Stan, "CardLess Teller-" + AdminUser.TellerId, CardAuth.AuthCode, CardAuth.AuthAmount, "None Required");
                curcode = util.getcurcode(txtCusAcctDebit.Text);
                if (InsertResult != 0)
                {
                    remark = "PINPAD-" + InsertResult + "-" + AdminUser.branchcode + "/" + AdminUser.TellerId + " CASH WITHDRAWAL BY " + txtCusName.Text.ToUpper();// THIS ALLOWS FOR UNIQUE RETRIEVAL OF transaction sequence from Basis.
                    TS_AddLogText("Processing Transaction ......");
                    PostResult = Trans.PostToBasis(txtCusAcctDebit.Text, txtTellerTillDr.Text, Convert.ToDouble(txtTransAmount.Text), Convert.ToUInt16(AdminUser.WithdrwalExpl_Code), remark, "WITHDRAWAL", AdminUser.TellerId, AdminUser.branchcode);

                    if (PostResult.Equals("0")) // successful Transaction
                    {
                        ulong update = Trans.UpdateTransaction(AdminUser.TellerId, InsertResult, AdminUser.branchcode, txtTellerTillDr.Text, remark, "APPROVED", "SUCCESS", AdminUser.LoginMode, AdminUser.tillaccount, AdminUser.WithdrwalExpl_Code);

                        if (update == 0)// succesful but couldnt update local DB with success status. Log for the service to update. But allow transaction to go through.
                        {
                            if (Trans.PrintReceipt(update.ToString(), "WITHDRAWAL", AdminUser.TellerId + "/" + InsertResult.ToString(), txtNuban.Text, txtCusName.Text, Convert.ToDouble(txtTransAmount.Text), txtCusName.Text, AdminUser.branchname, curcode))
                            {
                                MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence could not be retrieved:");
                            }
                            else
                            {
                                MessageBox.Show("Transaction Succesful. Receipt Failed to send to Printer");
                            }
                        }
                        else
                        {
                            if (Trans.PrintReceipt(update.ToString(), "WITHDRAWAL", AdminUser.TellerId + "/" + InsertResult.ToString(), txtNuban.Text, txtCusName.Text, Convert.ToDouble(txtTransAmount.Text), txtCusName.Text, AdminUser.branchname, curcode))
                            {
                                Trans.UpdatePrintStatus(InsertResult, 1);

                                MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence:" + update.ToString());
                            }
                            else
                            {
                                MessageBox.Show("Transaction Succesful. Receipt Failed to Print Check the printer and try Printing again from the Printing Menu:" + update.ToString());
                            }
                        }
                    }
                    else if (PostResult.Equals("1"))
                    {
                        AdminUser.IsOverAuthlimit = true;

                        Trans.UpdateTransactionForApproval(InsertResult);

                        MessageBox.Show("Transaction Awaiting Ops Head Approval.");
                    }
                    else
                    {
                        Trans.UpdateTransaction(AdminUser.TellerId, InsertResult, AdminUser.branchcode, txtTellerTillDr.Text, remark, "FAILED", PostResult, AdminUser.LoginMode, AdminUser.tillaccount, AdminUser.WithdrwalExpl_Code);

                        MessageBox.Show("Transaction Failed. Please Try Again. Error is::" + PostResult);
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
                return;
            }
            finally
            {
                TS_AddLogText("Transaction Completed...");
                TS_EnableButton(btnCancel);
                TS_ShowProgressBar(PostingprogressBar);
                AdminUser.SVAccountNumber = "";
                TS_DisableList(lstAccounts);
                Thread.CurrentThread.Abort();
            }
        }

        private void lstAccounts_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbAccountType.SelectedIndex == 3)
                {
                    var appserve = new AppDevService();
                    string[] accountdetails = lstAccounts.SelectedItem.ToString().Split('-');
                    txtCusName.Text = accountdetails[2];
                    txtCusAcctDebit.Text = accountdetails[1];
                    txtNuban.Text = accountdetails[0];
                    txtTellerTillDr.Text = AdminUser.tillaccount;
                    string restriction = appserve.checkrestriction(txtCusAcctDebit.Text);
                    string[] cusdetails = txtCusAcctDebit.Text.Split('/');
                    getCustomerDetailsfrommobile(Convert.ToInt32(cusdetails[0]), Convert.ToInt32(cusdetails[1]), Convert.ToInt32(cusdetails[2]), Convert.ToInt32(cusdetails[3]), Convert.ToInt32(cusdetails[4]));
                    if (!restriction.Equals("0"))
                    {
                        lstRestrictions.Items.Clear();
                        string[] restrictionarray = restriction.Split(',');

                        foreach (string s in restrictionarray)
                        {
                            lstRestrictions.Items.Add(s);
                        }
                    }
                    else
                    {
                        lstRestrictions.Items.Clear();
                        lstRestrictions.Items.Add("NONE");
                    }

                    DataTable opportunites = new DataTable();
                    opportunites = appserve.getOIOpportunities(cusdetails[0], cusdetails[1]);
                    lstOpportunities.Text = string.Empty;

                    if (opportunites.Rows.Count > 0)
                    {
                        foreach (DataRow row in opportunites.Rows)
                        {
                            string message = row["MSG"].ToString();
                            lstOpportunities.Text = lstOpportunities.Text + Environment.NewLine + message;                                                                                                   //   lstOpportunities.DrawItem += lst_DrawItem;
                        }

                        btnSubmit.Enabled = true;
                    }
                    return;
                }
                string availbalance = "";
                string[] accounts = new string[3];//
                accounts = lstAccounts.SelectedItem.ToString().Split('-');
                txtNuban.Text = accounts[0].Replace("(", "").Replace(")", "");
                txtCusAcctDebit.Text = accounts[1];
                availbalance = accounts[2].Replace("(", "").Replace(")", "");
                AdminUser.AvailableBalance = accounts[2].Replace("(", "").Replace(")", "");
                string[] telleraccts = new string[5];
                string[] custaccts = new string[5];
                telleraccts = txtTellerTillDr.Text.Split('/');
                custaccts = txtCusAcctDebit.Text.Split('/');

                txtTellerTillDr.Text = telleraccts[0].ToString() + "/" + telleraccts[1].ToString() + "/" + telleraccts[2].ToString() + "/" + telleraccts[3].ToString() + "/" + telleraccts[4].ToString();
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AdminUser.SVAccountNumber = "";
            Close();
        }

        private void txtTransAmount_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                double val = double.Parse((sender as TextBox).Text);
                txtTransAmount.Text = val.ToString("0.00");
            }
            catch
            {
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

        private void btnTransHistory_Click(object sender, EventArgs e)
        {
            AdminUser.SVAccountNumber = txtCusAcctDebit.Text;
            CustomerTransactionHistory Historyscreen = new CustomerTransactionHistory();
            Historyscreen.ShowDialog();
        }

        private void btnGetDetails_Click(object sender, EventArgs e)
        {
            btnGetDetails.Enabled = false;
            lblProgress.Text = "";
            string[] cusdetails = null;
            string accountnumber = string.Empty;

            try
            {
                AppDevService appserve = new AppDevService();

                string[] accounts = null;

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
                    accountnumber = appserve.ConvertToOldAccountNumber(txtNubanAcct.Text);

                    if (!accountnumber.Contains("/"))
                    {
                        MessageBox.Show("Invalid NUBAN Account Number");
                        return;
                    }
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
                }
                else if (cmbAccountType.SelectedIndex == 3)
                {
                    if (string.IsNullOrEmpty(txtNubanAcct.Text))
                    {
                        MessageBox.Show("Please Enter the Phone Number");
                        return;
                    }
                    if (txtNubanAcct.Text.Length != 11)
                    {
                        MessageBox.Show("Please Enter an 11 digit Phone number");
                        return;
                    }
                    DataTable dt = appserve.GetWithdrawaldetailsfromMobile(txtNubanAcct.Text);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No Account matches this Phone Number");
                        return;
                    }
                    for (int i = 0; i<dt.Rows.Count; i++)
                    {
                        accountnumber = dt.Rows[i]["OldAccountNo"].ToString();
                        lstAccounts.Items.Add(dt.Rows[i]["BenAcctNo"].ToString() + "-" + dt.Rows[i]["OldAccountNo"].ToString() + "-" + dt.Rows[i]["CustomerFullName"].ToString() + "-" + dt.Rows[i]["Account Balance"].ToString());
                        pnlTransaction.Visible = true;
                        
                        //cusdetails = accountnumber.Split('/');
                        //bool accountretrieval = getCustomerDetailsfrommobile(Convert.ToInt16(cusdetails[0]), Convert.ToInt32(cusdetails[1]), Convert.ToInt16(cusdetails[2]), Convert.ToInt16(cusdetails[3]), Convert.ToInt16(cusdetails[4]));
                        //if (!accountretrieval)
                        //{
                        //    MessageBox.Show("Error getting Account Numbers");
                        //}

                    }
                    return;
                }
                

                cusdetails = accountnumber.Split('/');

                if (getCustomerDetails(Convert.ToInt16(cusdetails[0]), Convert.ToInt32(cusdetails[1]), Convert.ToInt16(cusdetails[2]), Convert.ToInt16(cusdetails[3]), Convert.ToInt16(cusdetails[4])))
                {
                    lblProgress.Text = "Account Successfully Validated.";
                    lblProgress.ForeColor = Color.Green;
                    btnGetDetails.Enabled = false;
                    panel1.Visible = true;
                    pnlTransaction.Visible = true;
                    txtCusAcctDebit.Text = accountnumber;
                    string restriction = appserve.checkrestriction(txtCusAcctDebit.Text);
                    if (!restriction.Equals("0"))
                    {
                        lstRestrictions.Items.Clear();
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

                    DataTable opportunites = new DataTable();
                    opportunites = appserve.getOIOpportunities(cusdetails[0], cusdetails[1]);
                    lstOpportunities.Text = string.Empty;

                    if (opportunites.Rows.Count > 0)
                    {
                        foreach (DataRow row in opportunites.Rows)
                        {
                            string message = row["MSG"].ToString();
                            lstOpportunities.Text = lstOpportunities.Text + Environment.NewLine + message;                                                                                                   //   lstOpportunities.DrawItem += lst_DrawItem;
                        }

                        btnSubmit.Enabled = true;
                    }
                }
                else
                {
                    pnlTransaction.Visible = false;
                    txtTransAmount.Text = "";
                    txtCusName.Text = "";
                    txtTransAmount.Enabled = true;
                    btnGetDetails.Enabled = true;
                    MessageBox.Show("Unable to retrieve account details ");
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                lblProgress.Text = "An Error occured: " + ex.Message;
            }
        }

        public bool getCustomerDetails(int bracode, int cusnum, int curcode, int ledcode, int subacctcode)
        {
            int appid = Convert.ToInt16(AdminUser.ApplicationID);
            AppDevService appserve = new AppDevService();
            string[] accounts = null;
            string[] mandates = null;
            string cusmandate = "";
            Images = null;
            noofimages = 0;
            XmlDocument document = null;
            XPathNavigator navigator = null;
            XPathNodeIterator snodes = null;
            custdetails = appserve.GetBasisCustomerDetailsFullKey(bracode, cusnum, curcode, ledcode, subacctcode);
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

            if (!(accounts == null))
            {
                foreach (string account in accounts)
                {
                    lstAccounts.Items.Add("(" + account + ")");


                }
            }
            if (!(mandates == null))
            {
                foreach (string mandate in mandates)
                {
                    cusmandate = cusmandate + " " + mandate;
                }
            }
            else
            {
                lstAccounts.Items.Add("No Suitable Account For Transactions");
            }
            if (!(Images == null))
            {
                int i = 0;
                foreach (object pix in Images)
                {
                    try
                    {
                        imagedata = (byte[])pix;
                        MemoryStream myImage = new MemoryStream(imagedata, true);
                        codec = new RasterCodecs();
                        RasterImageListItem a = new RasterImageListItem();
                        a.Image = codec.Load(myImage);
                        a.Text = mandates[i];
                        rasterImageList1.Items.Add(a);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        ErrHandler.WriteError(ex.ToString());
                        continue;
                    }
                }
            }
            else
            {
                MessageBox.Show("No Image For the customer");
            }
            snodes = navigator.Select("/Response/CUSTOMERS/CUSTOMER/SIGNATORY_NAME");
            snodes.MoveNext();
            txtCusName.Text = snodes.Current.Value;
            AdminUser.CusName = snodes.Current.Value;
            txtTellerTillDr.Text = AdminUser.tillaccount;
            snodes = navigator.Select("/Response/CUSTOMERS/CUSTOMER/CUST_TYPE");
            snodes.MoveNext();
            lblCustomerLevel.Text = "LEVEL " + snodes.Current.Value;
            AdminUser.CustomerLevel = snodes.Current.Value;

            return true;
        }

        public bool getCustomerDetailsfrommobile(int bracode, int cusnum, int curcode, int ledcode, int subacctcode)
        {
            int appid = Convert.ToInt16(AdminUser.ApplicationID);
            AppDevService appserve = new AppDevService();
            string[] accounts = null;
            string[] mandates = null;
            string cusmandate = "";
            Images = null;
            noofimages = 0;
            XmlDocument document = null;
            XPathNavigator navigator = null;
            XPathNodeIterator snodes = null;
            custdetails = appserve.GetBasisCustomerDetailsFullKey(bracode, cusnum, curcode, ledcode, subacctcode);
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
            Images = custdetails.picture;
            mandates = custdetails.Mandates;
            noofimages = Images.Length;            
            if (!(mandates == null))
            {
                foreach (string mandate in mandates)
                {
                    cusmandate = cusmandate + " " + mandate;
                }
            }
            else
            {
                lstAccounts.Items.Add("No Suitable Account For Transactions");
            }
            if (!(Images == null))
            {
                int i = 0;
                foreach (object pix in Images)
                {
                    try
                    {
                        imagedata = (byte[])pix;
                        MemoryStream myImage = new MemoryStream(imagedata, true);
                        codec = new RasterCodecs();
                        RasterImageListItem a = new RasterImageListItem();
                        a.Image = codec.Load(myImage);
                        a.Text = mandates[i];
                        rasterImageList1.Items.Clear();
                        rasterImageList1.Items.Add(a);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        ErrHandler.WriteError(ex.ToString());
                        continue;
                    }
                }
            }
            else
            {
                MessageBox.Show("No Image For the customer");
            }            
            snodes = navigator.Select("/Response/CUSTOMERS/CUSTOMER/CUST_TYPE");
            snodes.MoveNext();
            lblCustomerLevel.Text = "LEVEL " + snodes.Current.Value;
            AdminUser.CustomerLevel = snodes.Current.Value;
            return true;
        }

        private void cmbAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAccountType.SelectedIndex == 1)
            {
                pnlaccts.Visible = true;
                pnlNuban.Enabled = true;
                pnloldacct.Enabled = false;
                label11.Text = "NUBAN Account Number :";
                txtNubanAcct.MaxLength = 10;
            }

            else if (cmbAccountType.SelectedIndex == 2)
            {
                pnlaccts.Visible = true;
                pnlNuban.Enabled = false;
                pnloldacct.Enabled = true;
            }
            else if (cmbAccountType.SelectedIndex == 3)
            {
                pnlaccts.Visible = true;
                pnlNuban.Enabled = true;
                pnloldacct.Enabled = false;
                label11.Text = "Phone Number (e.g. 080xxx) :";
                txtNubanAcct.MaxLength = 11;
            }
            else
            {
                MessageBox.Show("Please select a valid account type to continue");
                return;
            }
        }

        private void btnClearScreen_Click_1(object sender, EventArgs e)
        {
            pnlaccts.Visible = false;
            lblProgress.Text = "";
            lblTransProgress.Text = "";
            btnGetDetails.Enabled = true;
            pnlTransaction.Visible = false;
            txtBraCode.Text = "";
            txtCusNum.Text = "";
            lstAccounts.Items.Clear();
            cmbAccountType.SelectedIndex = 0;
            lblCustomerLevel.Text = "";
            txtNubanAcct.Text = "";
            lstAccounts.Enabled = true;
            txtCusName.Text = "";
            txtNuban.Text = "";
            txtTellerTillDr.Text = "";
            txtCusAcctDebit.Text = "";
            txtTransAmount.Text = "";
            txtBraCode.Text = "";
            txtCusNum.Text = "";
            txtCurCode.Text = "";
            txtLedCode.Text = "";
            txtSubAcctCode.Text = "";
            btnSubmitDr.Enabled = true;
            if (rasterImageList1.Items.Count > 0)
            {
                rasterImageList1.Items.Clear();
            }

            lstRestrictions.Items.Clear();
            lstOpportunities.Text = string.Empty;
            txtFeedBack.Text = string.Empty;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            AppDevService appserve = new AppDevService();
            string result = appserve.UpdateOIOpportunities(txtBraCode.Text, txtCusNum.Text, txtFeedBack.Text, AdminUser.TellerId);
            MessageBox.Show("Opportunites update submitted succesfully");
            btnSubmit.Enabled = false;
        }

        private void txtNubanAcct_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}