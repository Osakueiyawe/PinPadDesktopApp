using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using Leadtools.Codecs;
using System.Threading;
using GTBankCardInterfaceApp.DatabaseConnection;

namespace GTBankCardInterfaceApp
{
    public partial class FastTrackDeposit : Form
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
        string phonenumberonly = "";
        public FastTrackDeposit()
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
            if (this.InvokeRequired)
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
            if (this.InvokeRequired)
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
            if (this.InvokeRequired)
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
            if (this.InvokeRequired)
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
            if (this.InvokeRequired)
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
        public bool getCustomerDetailsforphonenoonly (int bracode, int cusnum)
        {
            AppDevService appserve = new AppDevService();
            ErrHandler.WriteError("Call to Webservice for details");
            custdetails = appserve.GetBasisCustomerDetails(bracode, cusnum);
            var document = new XmlDocument();
            document.LoadXml(custdetails.custdet.Replace("&", " and "));
            var navigator = document.CreateNavigator();
            var snode = navigator.Select("/Response/CUSTOMERS/CUSTOMER/SIGNATORY_NAME");
            snode.MoveNext();
            var beneficiaryname = snode.Current.Value;
            if(beneficiaryname.Length >50)
            {
                beneficiaryname = beneficiaryname.Substring(0, 50);
            }
            lblAccountName.Text = beneficiaryname;
            var snodes = navigator.Select("/Response/CUSTOMERS/CUSTOMER/CUST_TYPE");
            snodes.MoveNext();
            lblCustomerLevel.Text = "LEVEL" + snodes.Current.Value;
            return true;
        }
        public bool getCustomerDetails(int bracode, int cusnum, int bracode2 = 0, int cusnum2 = 0)
        {
            int appid = Convert.ToInt16(AdminUser.ApplicationID);
            AppDevService appserve = new AppDevService();
            ErrHandler.WriteError("Call to Webservice for details");
            string[] accounts = null;
            string[] mandates = null;
            Images = null;
            noofimages = 0;
            XmlDocument document = null;
            XPathNavigator navigator = null;
            XPathNodeIterator snodes = null;
            string retcode = null;
            ErrHandler.WriteError(bracode+" "+cusnum);
            custdetails = appserve.GetBasisCustomerDetails(bracode, cusnum);
            ErrHandler.WriteError(custdetails.custdet);
            document = new XmlDocument();
            document.LoadXml(custdetails.custdet.Replace("&", " and "));
            navigator = document.CreateNavigator();
            snodes = navigator.Select("/Response/CODE");
            snodes.MoveNext();
            retcode = snodes.Current.Value;
            ErrHandler.WriteError(retcode);

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
            
            if (bracode2 != 0 && cusnum2 != 0)
            {
                custdetails = appserve.GetBasisCustomerDetails(bracode2, cusnum2);
                ErrHandler.WriteError(custdetails.custdet);
                document = new XmlDocument();
                document.LoadXml(custdetails.custdet.Replace("&", " and "));
                navigator = document.CreateNavigator();
                snodes = navigator.Select("/Response/CODE");
                snodes.MoveNext();
                retcode = snodes.Current.Value;
                ErrHandler.WriteError(retcode);

                if (retcode != "1000")
                {
                    snodes = navigator.Select("/Response/ERROR");
                    snodes.MoveNext();
                    return false;
                }

                snodes = navigator.Select("/Response/CUSTOMERS/CUSTOMER/SIGNATORY_NAME");
                snodes.MoveNext();                
                lblAccountName.Text = snodes.Current.Value;
            }
            else
            {
                
                txtCusName.Text = snodes.Current.Value;                
                lblAccountName.Text = txtCusName.Text;
                
                
            }

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
            txtPhoneNumber.Text = "";
            pnlTransaction.Visible = false;
            btnSubmitDr.Enabled = true;
            btnCancel.Enabled = true;
            btnCashAnalysis.Enabled = true;
            txtCusName.Text = "";
            txtTellerTillDr.Text = "";
            txtCusAcctCredit.Text = "";
            txtTransAmount.Text = "";
            phonenumberonly = null;
            AdminUser.AnalyisAmount = "0";

            pnlaccts.Visible = false;

            lblPostProgress.Text = "";
            FastTrackDataGrid.Columns[8].DataPropertyName = "Originator";
            FastTrackDataGrid.Columns[8].HeaderText = "Originator";
            FastTrackDataGrid.Columns[7].DataPropertyName = "Amount";
            FastTrackDataGrid.Columns[7].HeaderText = "Amount";
            FastTrackDataGrid.Columns[9].HeaderText = "Status";
            FastTrackDataGrid.Columns[9].DataPropertyName = "Status";
        }

        private void CustomerDetail_Load(object sender, EventArgs e)
        {
            AdminUser.LoginMode = "Online";
            AdminUser.AnalyisAmount = "0";
            txtPhoneNumber.Text = "e.g. 080xxxxxxxx";
            txtPhoneNumber.GotFocus += RemoveText;
            txtPhoneNumber.LostFocus += AddText;
        }

        public void RemoveText(object sender, EventArgs e)
        {
            if (txtPhoneNumber.Text == "e.g. 080xxxxxxxx")
            {
                txtPhoneNumber.Text = "";
            }
        }

        public void AddText(object sender, EventArgs e)
        {
            if (txtPhoneNumber.Text == "")
            {
                txtPhoneNumber.Text = "e.g. 080xxxxxxxx";
            }
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
                var nuban = appserve.ConvertToNuban(txtCusAcctCredit.Text);

                InsertResult = Trans.InitiateTransaction(AdminUser.branchcode, AdminUser.TellerId, txtCusAcctCredit.Text, Convert.ToDecimal(txtTransAmount.Text), AdminUser.LoginMode, "DEPOSIT", lblAccountName.Text.Trim(), txtTellerTillDr.Text, txtCusName.Text, "CardLess", CardAuth.Stan, "CardLess Teller-" + AdminUser.TellerId, CardAuth.AuthCode, CardAuth.AuthAmount, lblAnalysis.Text);
                ulong update = 0;

                if (InsertResult != 0)
                {
                    remark = "PINPAD-" + InsertResult + "-" + AdminUser.branchcode + "/" + AdminUser.TellerId + " CASH DEPOSIT BY " + txtCusName.Text.ToUpper();// THIS ALLOWS FOR UNIQUE RETRIEVAL OF transaction sequence from Basis.
                    PostResult = Trans.PostToBasis(txtCusAcctCredit.Text, txtTellerTillDr.Text, Convert.ToDouble(txtTransAmount.Text), Convert.ToUInt16(AdminUser.DepositExpl_Code), remark, "DEPOSIT", AdminUser.TellerId, AdminUser.branchcode, AdminUser.Transid);//deposit operations

                    curcode = util.getcurcode(txtCusAcctCredit.Text);
                    if (PostResult.Equals("0"))
                    {
                        update = Trans.UpdateTransaction(AdminUser.TellerId, InsertResult, AdminUser.branchcode, txtTellerTillDr.Text, remark, "APPROVED", "SUCCESS", AdminUser.LoginMode, AdminUser.tillaccount, AdminUser.DepositExpl_Code);

                        appserve.FastTrackUpdateTeller(AdminUser.Transid, "Posted", AdminUser.TellerId, (long)InsertResult);

                        if (Trans.PrintReceipt(update.ToString(), "DEPOSIT", AdminUser.TellerId + "/" + InsertResult.ToString(), nuban, lblAccountName.Text, Convert.ToDouble(txtTransAmount.Text), txtCusName.Text, AdminUser.branchname, curcode))
                        {
                            Trans.UpdatePrintStatus(InsertResult, 1);
                            MessageBox.Show("Transaction Successful. Receipt Sent To Printer. Tran Sequence:" + update.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Transaction Successful. Receipt Failed to Print Check the printer and try Printing again from the Printing Menu:" + update.ToString());
                        }                    
                    }

                    else if (PostResult.Equals("1"))
                    {                      
                        appserve.FastTrackUpdateTeller(AdminUser.Transid, "Pending-Approval", AdminUser.TellerId, (long)InsertResult);

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
                ErrHandler.WriteError(ex.ToString());
                MessageBox.Show("An Error Occured Initiating Transaction Please try Again.." + ex.Message);
            }
            finally
            {

                TS_AddLogText("Transaction Completed...");
                TS_EnableButton(btnCancel);
                TS_ShowProgressBar(PostingprogressBar);
                AdminUser.SVAccountNumber = "";
                AdminUser.CashAnalysis = "";
                Thread.CurrentThread.Abort();
            }
        }

        
        private void btnPinPadDetails_Click(object sender, EventArgs e)
        {
            btnPinPadDetails.Enabled = false;
            lblProgress.Text = "";
            string[] cusdetails = null;
            string accountnumber = string.Empty;
            DataTable dtPending = null;
            try
            {
               
                AppDevService appserve = new AppDevService();
               // ErrHandler.WriteError("Arrive From Webservice");

                string[] accounts = null;
                //accountnumber = appserve.ConvertToOldAccountNumber(txtNubanAcct.Text);
                //TODO Get Pending Requests

                accountnumber = "";//  txtBraCode.Text + "/" + txtCusNum.Text + "/" + txtCurCode.Text + "/" + txtLedCode.Text + "/" + txtSubAcctCode.Text;

                //   cusdetails = appserve.getcusDepositDetailsOldAccount(accountnumber);
                string phoneNumber = txtPhoneNumber.Text;
                //ErrHandler.WriteError("Go To FastTrack to Get Pending");
                dtPending = appserve.FastTrackGetPending(phoneNumber, "DEPOSIT");
                
                if (dtPending.Rows.Count == 0)
                {
                    phonenumberonly = "YES";
                    dtPending = appserve.GetAccountdetailsfromPhone(phoneNumber);
                    FastTrackDataGrid.Columns[7].DataPropertyName = "CustomerFullName";
                    FastTrackDataGrid.Columns[7].HeaderText = "CustomerFullName";
                    FastTrackDataGrid.Columns[8].HeaderText = "OldAccountNo";
                    FastTrackDataGrid.Columns[8].DataPropertyName = "OldAccountNo";
                    FastTrackDataGrid.Columns[9].HeaderText = "AccountType";
                    FastTrackDataGrid.Columns[9].DataPropertyName = "AccountType";
                }
                FastTrackDataGrid.AutoGenerateColumns = false;
                FastTrackDataGrid.DataSource = dtPending;                

                if (dtPending != null)
                {
                    if (dtPending.Rows.Count == 0)
                    {
                        MessageBox.Show("No Valid Request found for customer with stated mobile number", "Alert");
                        return;
                    }
                }
                
                pnlaccts.Visible = true;

                btnSubmitDr.Enabled = true;


            }

            catch (Exception ex)
            {
                lblProgress.Text = "An Error occured: " + ex.Message;

            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AdminUser.SVAccountNumber = "";
            AdminUser.CashAnalysis = "";
            this.Close();
        }

        private void txtTransAmount_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Double val = Double.Parse((sender as TextBox).Text);

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

        }

        private void FastTrackDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (phonenumberonly == "YES")
            {
                var clickedCell1 = FastTrackDataGrid.Rows[e.RowIndex].Cells[0];
                if (clickedCell1.Value.ToString() == "Select")
                {
                    AppDevService appserve = new AppDevService();

                    var benacctnocolumnindex = 1;
                    var transtypecolumnindex = 2;
                    var requestdatecolumnindex = 3;
                    var mobilenocolumnindex = 4;
                    var bracodecolumnindex = 5;
                    var cusnocolumnindex = 6;




                    var benacctno = FastTrackDataGrid.Rows[e.RowIndex].Cells[benacctnocolumnindex];
                    var cusno = FastTrackDataGrid.Rows[e.RowIndex].Cells[cusnocolumnindex];
                    var bracode = FastTrackDataGrid.Rows[e.RowIndex].Cells[bracodecolumnindex];
                    var mobileno = FastTrackDataGrid.Rows[e.RowIndex].Cells[mobilenocolumnindex];
                    var transtype = FastTrackDataGrid.Rows[e.RowIndex].Cells[transtypecolumnindex];
                    var requestdate = FastTrackDataGrid.Rows[e.RowIndex].Cells[requestdatecolumnindex];
                    this.getCustomerDetailsforphonenoonly(Convert.ToInt32(bracode.Value.ToString()), Convert.ToInt32(cusno.Value.ToString()));
                    pnlTransaction.Visible = true;
                    txtCusAcctCredit.Text = appserve.ConvertToOldAccountNumber(benacctno.Value.ToString());
                    txtTellerTillDr.Text = AdminUser.tillaccount;

                    string restriction = appserve.checkrestrictionBraCus(bracode.Value.ToString(), cusno.Value.ToString());

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

                    return;
                }
                
            }
            
            var clickedCell = FastTrackDataGrid.Rows[e.RowIndex].Cells[0];
            try
            {
                ErrHandler.WriteError("cell Selected"+ clickedCell.Value.ToString());
                if (clickedCell.Value.ToString() == "Select")
                {

                    ErrHandler.WriteError("INSTANTIATE WEBSERVICE");
                    AppDevService appserve = new AppDevService();
                    ErrHandler.WriteError("INSTANTIATED WEBSERVICE");
                    string accountnumber = string.Empty;
                    string[] cusdetails;
                    // here you can have column reference by using e.ColumnIndex
                    //Mapping on TEST
                    //int TransTypeColumnIndex = 2;
                    //int RequestDateColumnIndex = 3;
                    //int MobileNoColumnIndex = 4;
                    //int BraCodeColumnIndex = 5;
                    //int CusNoColumnIndex = 6;
                    //int BenAcctNoColumnIndex = 7;
                    //int AmountColumnIndex = 8;
                    //int OriginatorColumnIndex = 9;
                    //int StatusColumnIndex = 10;
                    //int TranstatusColumnIndex = 11;
                    //int NoOfhoursColumnIndex = 12;
                    //int IdColumnIndex = 13;

                    //Mapping On LIVE                    
                    int BenAcctNoColumnIndex = 1;
                    int TransTypeColumnIndex = 2;
                    int RequestDateColumnIndex = 3;
                    int MobileNoColumnIndex = 4;
                    int BraCodeColumnIndex = 5;
                    int CusNoColumnIndex = 6;                    
                    int AmountColumnIndex = 7;
                    int OriginatorColumnIndex = 8;
                    int StatusColumnIndex = 9;
                    int TranstatusColumnIndex = 10;
                    int NoOfhoursColumnIndex = 11;
                    int IdColumnIndex = 12;

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


                    
                    // ... do something ...
                    ErrHandler.WriteError(BraCodecell.Value.ToString()+" "+ CusNocell.Value.ToString());


                    //DataRow[] result = dtPending.Select("Id==75");
                    //int branchCode = 0;
                    //int customerNo = 0;
                    //string customerAccount = null;
                    //foreach (DataRow row in result)
                    //{
                         
                    //    branchCode = Convert.ToInt32(row[5]);
                    //    customerNo = Convert.ToInt32(row[6]);
                    //    customerAccount = row[7].ToString();
                    //}


                   int branchCode = Convert.ToInt32(BraCodecell.Value.ToString());
                   int customerNo = Convert.ToInt32(CusNocell.Value.ToString());
                   string customerAccount = BenAcctNocell.Value.ToString();


                    //All this is to show the beneficiary name on the system when printing out.
                    var beneficialoldaccount = appserve.ConvertToOldAccountNumber(customerAccount);
                    var brokendownbeneficiary = cusdetails = beneficialoldaccount.Split('/');

                    bool ConditionToSend;
                    if (branchCode == Convert.ToInt32(brokendownbeneficiary[0]) && customerNo == Convert.ToInt32(brokendownbeneficiary[1]))
                    {
                        ConditionToSend = getCustomerDetails(branchCode, customerNo);
                        txtCusName.Text = Originatorcell.Value.ToString();
                    }
                    else
                    {
                        ConditionToSend = getCustomerDetails(branchCode, customerNo, Convert.ToInt32(brokendownbeneficiary[0]), Convert.ToInt32(brokendownbeneficiary[1]));
                        txtCusName.Text = Originatorcell.Value.ToString();
                    }

                    // string name = appserve.GetCustomerName(branchCode.ToString(), customerNo.ToString());

                    lblId.Text = idCell.Value.ToString();
                    AdminUser.Transid = lblId.Text;

                    cusdetails = accountnumber.Split('/');

                    if (ConditionToSend)
                    {
                        //  lblAccountName.Text = name;

                        lblProgress.Text = "Account Successfully Validated.";
                        lblProgress.ForeColor = Color.Green;
                        btnPinPadDetails.Enabled = false;

                        pnlTransaction.Visible = true;
                        txtCusAcctCredit.Text = appserve.ConvertToOldAccountNumber(customerAccount);
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
                            lstRestrictions.Items.Clear();
                            lstRestrictions.Items.Add("NONE");
                        }
                    }
                    else
                    {
                        pnlTransaction.Visible = false;
                        txtTransAmount.Text = "";
                        txtCusName.Text = "";
                        //btnGetDetails.Enabled = true;
                        //txtNubanAccount.Enabled = true;
                        txtTransAmount.Enabled = true;
                        btnPinPadDetails.Enabled = true;

                        MessageBox.Show("Unable to retrieve account details ");
                        return;
                    }


                }
            }
            catch (Exception ex)
            {

            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}