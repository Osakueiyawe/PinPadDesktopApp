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
    public partial class FastTrackWithdrawal : Form
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

        public FastTrackWithdrawal()
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
            String retcode = null;
            custdetails = appserve.GetBasisCustomerDetails(bracode, cusnum);
            document = new XmlDocument();
            document.LoadXml(custdetails.custdet.Replace("&", " and "));
            navigator = document.CreateNavigator();
            snodes = navigator.Select("/Response/CODE");
            snodes.MoveNext();
            retcode = snodes.Current.Value;

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

            lstAccounts.Items.Clear();

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
                    PostResult = Trans.PostToBasis(txtCusAcctDebit.Text, txtTellerTillDr.Text, Convert.ToDouble(txtTransAmount.Text), Convert.ToUInt16(AdminUser.WithdrwalExpl_Code), remark, "WITHDRAWAL", AdminUser.TellerId, AdminUser.branchcode, AdminUser.Transid);

                    if (PostResult.Equals("0"))
                    {
                        ulong update = Trans.UpdateTransaction(AdminUser.TellerId, InsertResult, AdminUser.branchcode, txtTellerTillDr.Text, remark, "APPROVED", "SUCCESS", AdminUser.LoginMode, AdminUser.tillaccount, AdminUser.WithdrwalExpl_Code);

                        appserve.FastTrackUpdateTeller(AdminUser.Transid, "Posted", AdminUser.TellerId, (long)InsertResult);

                        if (update == 0)// succesful but couldnt update local DB with success status. Log for the service to update. But allow transaction to go through.
                        {
                            if (Trans.PrintReceipt(update.ToString(), "WITHDRAWAL", AdminUser.TellerId + "/" + InsertResult.ToString(), txtNuban.Text, txtCusName.Text, Convert.ToDouble(txtTransAmount.Text), txtCusName.Text, AdminUser.branchname, curcode))
                            {
                                MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence could not be retrieved:");
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

                        appserve.FastTrackUpdateTeller(AdminUser.Transid, "Pending-Approval", AdminUser.TellerId, (long)InsertResult);

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
                ErrHandler.WriteError(ex.ToString());
                MessageBox.Show("An Error Occured Initiating Transaction Please try Again.." + ex.Message);
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
            catch (Exception ex)
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

                accountnumber = "";

                string phoneNumber = txtPhoneNumber.Text;

                var dtPending = appserve.FastTrackGetPending(phoneNumber, "WITHDRAW");

                FastTrackDataGrid.DataSource = dtPending;

                if (dtPending.Rows.Count == 0)
                {
                    MessageBox.Show("No Valid Request found for customer with stated mobile number", "Alert");
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
            string retcode = null;
            custdetails = appserve.GetBasisCustomerDetailsFullKey(bracode, cusnum, curcode, ledcode, subacctcode);
            document = new XmlDocument();
            document.LoadXml(custdetails.custdet.Replace("&", " and "));
            navigator = document.CreateNavigator();
            snodes = navigator.Select("/Response/CODE");
            snodes.MoveNext();
            retcode = snodes.Current.Value;

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

        private void btnClearScreen_Click_1(object sender, EventArgs e)
        {
            lblProgress.Text = "";
            lblTransProgress.Text = "";
            btnGetDetails.Enabled = true;
            pnlTransaction.Visible = false;

            lstAccounts.Items.Clear();

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

            lstRestrictions.Items.Clear();
            lstOpportunities.Text = string.Empty;
            txtFeedBack.Text = string.Empty;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            AppDevService appserve = new AppDevService();
            MessageBox.Show("Opportunites update submitted succesfully");
            btnSubmit.Enabled = false;
        }

        private void FastTrackDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var clickedCell = FastTrackDataGrid.Rows[e.RowIndex].Cells[0];

            if (clickedCell.Value.ToString() == "Select")
            {
                AppDevService appserve = new AppDevService();
                string accountnumber = string.Empty;
                string[] cusdetails;
                int TransTypeColumnIndex = 2;
                int RequestDateColumnIndex = 3;
                int MobileNoColumnIndex = 4;
                int BraCodeColumnIndex = 5;
                int CusNoColumnIndex = 6;
                int BenAcctNoColumnIndex = 7;
                int AmountColumnIndex = 8;
                int OriginatorColumnIndex = 9;
                int StatusColumnIndex = 10;
                int TranstatusColumnIndex = 11;
                int NoOfhoursColumnIndex = 12;
                int IdColumnIndex = 1;

                var RequestDate =
                     FastTrackDataGrid.Rows[e.RowIndex].Cells[RequestDateColumnIndex];
                var MobileNocell =
                     FastTrackDataGrid.Rows[e.RowIndex].Cells[MobileNoColumnIndex];
                var BraCodecell =
                     FastTrackDataGrid.Rows[e.RowIndex].Cells[BraCodeColumnIndex];
                var CusNocell =
                     FastTrackDataGrid.Rows[e.RowIndex].Cells[CusNoColumnIndex];
                var BenAcctNocell =
                     FastTrackDataGrid.Rows[e.RowIndex].Cells[BenAcctNoColumnIndex];
                var AmountNocell =
                     FastTrackDataGrid.Rows[e.RowIndex].Cells[AmountColumnIndex];
                var Originatorcell =
                     FastTrackDataGrid.Rows[e.RowIndex].Cells[OriginatorColumnIndex];
                var Statuscell =
                     FastTrackDataGrid.Rows[e.RowIndex].Cells[StatusColumnIndex];
                var Transtatuscell =
                     FastTrackDataGrid.Rows[e.RowIndex].Cells[TranstatusColumnIndex];
                var NoOfhourscell =
                     FastTrackDataGrid.Rows[e.RowIndex].Cells[NoOfhoursColumnIndex];
                var idCell =
                     FastTrackDataGrid.Rows[e.RowIndex].Cells[IdColumnIndex];

                int branchCode = Convert.ToInt32(BraCodecell.Value.ToString());
                int customerNo = Convert.ToInt32(CusNocell.Value.ToString());

                lblId.Text = idCell.Value.ToString();

                AdminUser.Transid = lblId.Text;

                cusdetails = accountnumber.Split('/');

                if (getCustomerDetails(branchCode, customerNo))
                {
                    lblProgress.Text = "Account Successfully Validated.";
                    lblProgress.ForeColor = Color.Green;
                    btnGetDetails.Enabled = false;
                    panel1.Visible = true;
                    pnlTransaction.Visible = true;
                    txtCusAcctDebit.Text = accountnumber;
                    txtTransAmount.Text = AmountNocell.Value.ToString();
                    string restriction = appserve.checkrestrictionBraCus(branchCode.ToString(), customerNo.ToString());

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
                    opportunites = appserve.getOIOpportunities(branchCode.ToString(), customerNo.ToString());
                    lstOpportunities.Text = string.Empty;

                    if (opportunites.Rows.Count > 0)
                    {
                        foreach (DataRow row in opportunites.Rows)
                        {
                            string message = row["MSG"].ToString();
                            lstOpportunities.Text = lstOpportunities.Text + Environment.NewLine + message;
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
        }
    }
}