using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using Leadtools.Codecs;
using Leadtools.WinForms;
using ECRCommXLib;
using System.Threading;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Configuration;
using Dermalog.Imaging.Capturing;

namespace GTBankCardInterfaceApp
{
    public partial class CustomerDetail : Form
    {
        enum PinPadRes { Success = 0, Error = 1, InProgress = 2 };
        RasterCodecs codec;
        Utilities u = new Utilities();
        AppDevService appDev = new AppDevService();
        string remark = "";
        string PostResult = "";
        ulong InsertResult = 0;
        CustDetRetVal custdetails = null;
        byte[] imagedata = new byte[8056];
        object[] Images = null;
        int noofimages = 0;
        int i = 0;

        public CustomerDetail()
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
            string retcode = null;
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

            if (accounts != null)
            {
                foreach (string account in accounts)
                {
                    lstAccounts.Items.Add("(" + account + ")");
                }
            }
            if (mandates != null)
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
            if (Images != null)
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
            lblCustomerLevel.Text = "LEVEL " + snodes.Current.Value;
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
            btnPinPadDetails.Enabled = true;
            pnlAcctDetails.Visible = false;
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
            thread.Name = "CardHolderDeposit"; 
            thread.Start();
        }
        
        private void btnPinPadDetails_Click(object sender, EventArgs e)
        {
            btnPinPadDetails.Enabled = false;
            lblProgress.Text = "";
            string[] cusdetails = null;
            GTLib m = new GTLib();
            Transaction Trans = new Transaction();
            AppDevService appserve = new AppDevService();
            string cardnumber = string.Empty;
            string AuthAmount = string.Empty;
            string AuthCode = string.Empty;
            string MerchantID = string.Empty;
            uint Stan = 0;
            string result = string.Empty;
            string AuthSource = string.Empty;

            try
            {

                m.CommOpenAuto(115200, 100);

                if (m.LastResult == (byte)PinPadRes.Error)//IJENICO
                {
                    ErrHandler.WriteError("Terminal Serial: " + AdminUser.TerminalSerial);
                    if (!string.IsNullOrEmpty(AdminUser.TerminalSerial))
                    {
                        string[] resultvalues = UnlockWithSocket(AdminUser.TerminalID, AdminUser.TerminalSerial);
                        ErrHandler.WriteError("Result from Unlock with Socket =>" + string.Join(", ", resultvalues));

                        if (resultvalues.Length == 2)
                        {
                            string responseCode = string.Empty;
                            string responseMessage = string.Empty;
                            string decryptedMessage = string.Empty;
                            responseCode = resultvalues[0];
                            responseMessage = resultvalues[1];
                            if (responseCode.Trim().Equals("00"))
                            {
                                decryptedMessage = GTBSecure.Secure.DecryptString(responseMessage);
                                string[] messagevalues = decryptedMessage.Split('|');
                                ErrHandler.WriteError("Decrypted Values =>" + string.Join(", ", messagevalues));
                                
                                string transtype = messagevalues[4];
                                AuthSource = messagevalues[5];
                                if (transtype.Trim().ToUpper().Equals("WITHDRAWAL"))
                                {
                                    Stan = Convert.ToUInt32(messagevalues[0]);
                                    cardnumber = messagevalues[1];
                                    AuthAmount = messagevalues[5];
                                    AuthAmount = AuthAmount.Split('=')[1];
                                    AuthCode = messagevalues[3];                                    
                                    MerchantID = AdminUser.TerminalID + "_" + AdminUser.TerminalSerial;

                                }
                                else
                                {
                                    ErrHandler.WriteError("Transaction type for Cardholder withdrawal: " + decryptedMessage);
                                    MessageBox.Show("The customer entered a different transaction type " + transtype + " please reinitiate using the correct screen. ");
                                    return;
                                }

                                //string transtype = messagevalues[4];
                                //AuthSource = messagevalues[6];
                                //if (transtype.Trim().ToUpper().Equals("WITHDRAWAL"))
                                //{
                                //    Stan = Convert.ToUInt32(messagevalues[0]);
                                //    cardnumber = messagevalues[1];
                                //    AuthAmount = AuthAmount.Split('=')[1];
                                //    AuthCode = messagevalues[3];
                                //    AuthAmount = messagevalues[5];
                                //    MerchantID = AdminUser.TerminalID + "_" + AdminUser.TerminalSerial;

                                //}
                                //else
                                //{
                                //    MessageBox.Show("The customer entered a different transaction type " + transtype + " please reinitiate using the correct screen. ");
                                //    return;
                                //}
                            }
                            else
                            {
                                MessageBox.Show("An Error Occured while retrieving values from the network PINPAD. Please Ensure the terminal is initiated and retry");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("An Error Occured while retrieving values from the network PINPAD. Please Ensure the terminal is initiated and retry");
                            return;
                        }
                    }
                    else
                    {
                        string a = m.LastErrorDescription;
                        MessageBox.Show("An Error Occured Communicating with the Device. Please Ensure No card is in the terminal and Press the F key to enter ECR MODE! : " + a);
                        btnPinPadDetails.Enabled = true;
                        return;
                    }
                }
                else
                {
                    string message2 = null;
                    m.Init((Convert.ToByte(AdminUser.HostID)), "ENTER AMOUNT[NGN]:", message2);
                    m.ValidateCardPIN();
                    byte bRes = m.LastResult;

                    while (m.LastResult == (byte)PinPadRes.InProgress)
                    {
                        lblProgress.Visible = true;
                        lblProgress.Text = "Authenticating Card... Please wait.";
                    }

                    if (m.LastResult == (byte)PinPadRes.Error)
                    {
                        lblProgress.Text = "Error Authenticating Card.";
                        lblProgress.Text = lblProgress.Text + m.LastErrorDescription;
                        lblProgress.ForeColor = Color.Red;
                        return;
                    }
                    else if (m.LastResult == (byte)PinPadRes.Success)
                    {
                        cardnumber = m.CardNumber;
                        AuthAmount = m.Amount;
                        AuthCode = m.AuthorizationCode;
                        MerchantID = m.MerchantID;
                        Stan = m.STAN;
                    }
                }

                if (AuthSource.ToLower() != "biover")
                {
                    cusdetails = u.getCustomerID(cardnumber);

                    if (cusdetails == null)
                    {
                        lblProgress.Text = "Card Type Not Permitted on PinPad......";
                        lblProgress.Text = lblProgress.Text + m.LastErrorDescription;
                        lblProgress.ForeColor = Color.Red;

                        MessageBox.Show("Card Type Not Permitted on PinPad......");
                        return;
                    }
                }
                else
                {
                    //in this case, the card number is the customer's account number
                    string custNumber= appserve.ConvertToOldAccountNumber(cardnumber);
                    ErrHandler.WriteError("Account Number retrieved: " + custNumber + "for "+ cardnumber);

                    if (u.verifyOldAccountFormat(custNumber) == false)
                    {
                        MessageBox.Show("Validation successful but unable to retrieve account details ");
                        return;
                    }

                    //get the customer id from the account number
                    cusdetails = custNumber.Split('/');
                }

                //5399830647113826,5399831601646553,5399831604035051
                if (getCustomerDetails(Convert.ToInt16(cusdetails[0]), Convert.ToInt32(cusdetails[1])))
                {

                    lblProgress.Text = AuthSource.ToLower() == "biover" ? "Biometric Successfully Authenticated" : "Card Successfully Authenticated.";
                    lblProgress.ForeColor = Color.Green;
                    btnPinPadDetails.Enabled = false;
                    txtBraCode.Text = cusdetails[0].ToString();
                    txtCusNum.Text = cusdetails[1].ToString();
                    pnlAcctDetails.Visible = true;
                    pnlTransaction.Visible = true;
                    txtTransAmount.Text = (Convert.ToDouble(AuthAmount) / 100).ToString();
                    txtTransAmount.Enabled = false;
                    CardAuth.AuthAmount = Convert.ToDecimal(AuthAmount);
                    CardAuth.AuthCode = AuthCode;
                    CardAuth.MerchantID = MerchantID;
                    CardAuth.Stan = Stan;
                    CardAuth.CardNumber = AuthSource.ToLower() == "biover" ? "CardLess" : cardnumber;

                    
                    if ((Convert.ToDouble(AuthAmount) / 100) < Convert.ToDouble(AdminUser.chargeslimit))
                    {
                        txtCharge.Text = AdminUser.charge;
                    }

                    DataTable opportunites = new DataTable();
                    opportunites = appserve.getOIOpportunities(cusdetails[0], cusdetails[1]);
                    lstOpportunities.Text = string.Empty;
                    if (opportunites.Rows.Count > 0)
                    {
                        pnlOpportunities.Visible = true;
                        appserve.UpdateOISelectionbyTeller(cusdetails[0], cusdetails[1], AdminUser.TellerId);
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
                    btnPinPadDetails.Enabled = true;

                    MessageBox.Show("Validation successful but Unable to retrieve account details ");
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                lblProgress.Text = "An error occured!";
            }
            finally
            {
                m.CommClose();
            }
        }

        private string[] UnlockWithSocket(string TerminalID, string TerminalSerial)
        {
            string responsebad;
            byte[] bytes = new byte[1024];
            try
            {
                var HOSTFORSOCKET = ConfigurationManager.AppSettings["HOSTFORSOCKET"];
                IPHostEntry ipHostInfo = Dns.GetHostEntry(HOSTFORSOCKET);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                var PORTFORSOCKET = int.Parse(ConfigurationManager.AppSettings["PORTFORSOCKET"]);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, PORTFORSOCKET);
                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                var TIMEOUTSOCKET = int.Parse(ConfigurationManager.AppSettings["TIMEOUTSOCKET"]);
                sender.SendTimeout = TIMEOUTSOCKET;
                sender.ReceiveTimeout = TIMEOUTSOCKET;
                sender.NoDelay = true;
                try
                {
                    sender.Connect(remoteEP);

                    string sendTerminalDetails = string.Concat("F=Unlock, ", TerminalID, ", ", TerminalSerial);
                    byte[] msg = Encoding.ASCII.GetBytes(sendTerminalDetails);

                    if (sender.Connected)
                    {
                        int bytesSent = sender.Send(msg);
                    }

                    int bytesRec = sender.Receive(bytes);

                    var response = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    response = response.TrimStart('|');

                    int retrycounter = 0;
                    while (!response.Contains("00") && retrycounter > 5)
                    {
                        retrycounter++;
                        UnlockWithSocket(TerminalID, TerminalSerial);
                    }

                    var splitResponse = response.Split('|');

                    return splitResponse;
                }
                catch (Exception ex)
                {
                    string message = " Cannot Unlock Terminal Connected to network but cannot open Port" + ex.StackTrace;

                    DialogResult resultDialog = MessageBox.Show(
                        message,
                        "PINPAD",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error);
                    if (resultDialog == DialogResult.Retry)
                    {
                        UnlockWithSocket(TerminalID, TerminalSerial);
                    }
                }
                finally
                {
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
            }
            catch (Exception ex)
            {
                string message = "THE ERROR IS " + ex.StackTrace;

                DialogResult resultDialog = MessageBox.Show(
                    message,
                    "PINPAD",
                    MessageBoxButtons.RetryCancel,
                    MessageBoxIcon.Error);
                if (resultDialog == DialogResult.Retry)
                {
                    UnlockWithSocket(TerminalID, TerminalSerial);
                }
            }
            responsebad = "25|BADRESPONSE";
            return responsebad.Split('|');
        }

        private void lstAccounts_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string availbalance = "";
                string[] accounts = new string[3];
                accounts = lstAccounts.SelectedItem.ToString().Split('-');
                txtNuban.Text = accounts[0].Replace("(", "").Replace(")", "");
                txtCusAcctDebit.Text = accounts[1];
                availbalance = accounts[2].Replace("(", "").Replace(")", "");
                AdminUser.AvailableBalance = accounts[2].Replace("(", "").Replace(")", "");
                string[] telleraccts = new string[5];
                string[] custaccts = new string[5];
                telleraccts = txtTellerTillDr.Text.Split('/');
                custaccts = txtCusAcctDebit.Text.Split('/');

                if (!custaccts[2].ToString().Equals("1"))
                {
                    txtCharge.Text = "0";
                }
                else if (Convert.ToDouble(txtTransAmount.Text) < (Convert.ToDouble(AdminUser.chargeslimit)))
                {
                    txtCharge.Text = AdminUser.charge;
                }
                else
                {
                    txtCharge.Text = "0";
                }

                telleraccts[2] = custaccts[2];

                txtTellerTillDr.Text = telleraccts[0].ToString() + "/" + telleraccts[1].ToString() + "/" + telleraccts[2].ToString() + "/" + telleraccts[3].ToString() + "/" + telleraccts[4].ToString();

                AppDevService appserve = new AppDevService();

                string restriction = appserve.checkrestriction(txtCusAcctDebit.Text);

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

                if (Convert.ToDouble(availbalance) < (Convert.ToDouble(txtTransAmount.Text)))
                {
                    btnSubmitDr.Enabled = false;
                }
                else
                {
                    btnSubmitDr.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                ErrHandler.WriteError("An exception occurred while trying to retrieve other customer details from selected account. Message - " + ex.Message + "|StackTrace - " + ex.StackTrace);
                MessageBox.Show("An error occurred while trying to get customer details from selected account.");
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            AppDevService appserve = new AppDevService();
            string result = appserve.UpdateOIOpportunities(txtBraCode.Text, txtCusNum.Text, txtFeedBack.Text, AdminUser.TellerId);
            label18.Text = "Opportunites update submitted succesfully";
            MessageBox.Show("Opportunites update submitted succesfully");
            btnSubmit.Enabled = false;
        }

        private void Post()
        {
            remark = "";
            PostResult = "";
            InsertResult = 0;

            try
            {
                string curcode = "";
                Transaction Trans = new Transaction();
                TS_ToggleButton(btnSubmitDr);
                TS_ToggleButton(btnCancel);
                TS_ShowProgressBar(PostingprogressBar);

                TS_AddLogText("Processing Transaction ......");

                AdminUser.SVAccountNumber = txtCusAcctDebit.Text;

                InsertResult = Trans.InitiateTransaction(AdminUser.branchcode, AdminUser.TellerId, txtCusAcctDebit.Text, Convert.ToDecimal(txtTransAmount.Text), 
                    AdminUser.LoginMode, "WITHDRAWAL", txtCusName.Text.Trim(), txtTellerTillDr.Text, txtCusName.Text, 
                    CardAuth.CardNumber, CardAuth.Stan, CardAuth.MerchantID, CardAuth.AuthCode, CardAuth.AuthAmount, "None Required");

                curcode = u.getcurcode(txtCusAcctDebit.Text);

                if (InsertResult != 0)
                {
                    remark = "PINPAD-" + InsertResult + "-" + AdminUser.branchcode + "/" + AdminUser.TellerId + " CASH WITHDRAWAL BY " + txtCusName.Text.ToUpper();// THIS ALLOWS FOR UNIQUE RETRIEVAL OF transaction sequence from Basis.

                    TS_AddLogText("Processing Transaction ......");

                    PostResult = Trans.PostToBasis(txtCusAcctDebit.Text, txtTellerTillDr.Text, Convert.ToDouble(txtTransAmount.Text), Convert.ToUInt16(AdminUser.WithdrwalExpl_Code), remark, "WITHDRAWAL", AdminUser.TellerId, AdminUser.branchcode);

                    if (PostResult.Equals("0"))
                    {
                        ulong update = Trans.UpdateTransaction(AdminUser.TellerId, InsertResult, AdminUser.branchcode, txtTellerTillDr.Text, remark, "APPROVED", "SUCCESS", AdminUser.LoginMode, AdminUser.tillaccount, AdminUser.WithdrwalExpl_Code);

                        if (update == 0)
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

                        Trans.UpdateTransactionForApproval(InsertResult);

                        MessageBox.Show("Transaction Awaiting Ops Head Approval.");
                    }
                    else
                    {
                        Trans.UpdateTransaction(AdminUser.TellerId, InsertResult, AdminUser.branchcode, txtTellerTillDr.Text, remark, "FAILED", PostResult, AdminUser.LoginMode, AdminUser.tillaccount, AdminUser.WithdrwalExpl_Code);
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

        private void BtnGetDetailsFromFingerPrint_Click_1(object sender, EventArgs e)
        {
            string nuban = string.Empty;

            string bvn = string.Empty;

            string oldAcccountNumber = string.Empty;

            string customerName = string.Empty;

            string availableBalance = string.Empty;

            string[] splittedAccountNumber = new string[] { };

            string institutionCode = string.Empty;

            string referenceId = string.Empty;

            string deviceId = string.Empty;

            try
            {
                FingerPrint fingerPrint = new FingerPrint();

                FingerPrintValidation mobileNumber = new FingerPrintValidation();

                DialogResult result = mobileNumber.ShowDialog();

                if (result != DialogResult.OK)
                {
                    return;
                }

                var customerDetails = u.GetCustomerDetailsUsingPhoneNumber(AdminUser.CustomerPhoneNumber, 
                    AdminUser.TransAmount.ToString());

                if (customerDetails != null)
                {
                    if (customerDetails[0].Equals("Successful"))
                    {
                        nuban = customerDetails[1];
                        bvn = customerDetails[2];
                        oldAcccountNumber = customerDetails[3];
                        splittedAccountNumber = oldAcccountNumber.Split('/');
                        customerName = customerDetails[4];
                        availableBalance = customerDetails[5];
                    }
                    else if (customerDetails[0].StartsWith("Customer's phone number"))
                    {
                        MessageBox.Show("Customer's phone number is not attached to any account. Please reconfirm number and try again.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (customerDetails[0].StartsWith("No account is funded"))
                    {
                        MessageBox.Show("No account is funded for this customer!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Could not get customer's details!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Could not get customer's details!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    try
                    {
                        institutionCode = ConfigurationManager.AppSettings["InstitutionCode"];
                    }
                    catch (Exception ex)
                    {
                        ErrHandler.WriteError("Could not get InstitutionCode from config. Message - " + ex.Message + "|Stacktrace - " + ex.StackTrace);
                        institutionCode = "00058";
                    }

                    int randomNumber = u.GenerateRandomNumber(4);

                    referenceId = institutionCode + DateTime.Today.ToString("yyyyMMddhhmmss") + randomNumber;

                    //deviceId = "Z" + institutionCode + "1" + "FGZF";
                    deviceId = "Z" + institutionCode + "1" + "41SD";

                    FingerPrintVerificationRequest request = u.GetRequestForNibssApi(nuban, bvn, referenceId, deviceId.Trim());

                    var fingerPrintVerificationResponse = appDev.GetVerificationResponse(request);

                    if (fingerPrintVerificationResponse != null)
                    {
                        if (fingerPrintVerificationResponse.ValidFingerPrint)
                        {
                            pnlAcctDetails.Visible = true;
                            pnlTransaction.Visible = true;
                            txtBraCode.Text = splittedAccountNumber[0];
                            txtCusNum.Text = splittedAccountNumber[1];
                            txtCusAcctDebit.Text = oldAcccountNumber;
                            txtNuban.Text = nuban;
                            txtTransAmount.Text = AdminUser.TransAmount.ToString();

                            string accountKey = string.Empty;

                            accountKey = nuban + ")-" + oldAcccountNumber + "-(" + availableBalance;

                            lstAccounts.Items.Add("(" + accountKey + ")");

                            if (AdminUser.roleid.Equals(AdminUser.TellerRoleId))
                            {
                                txtTellerTillDr.Text = AdminUser.tillaccount;
                            }
                            else
                            {
                                txtTellerTillDr.Text = "1/1/1/1/0";
                            }

                            u.PopulateOtherCustomerDetails(Convert.ToInt32(splittedAccountNumber[0]), Convert.ToInt32(splittedAccountNumber[1]), txtCusName, lblCustomerLevel, lstAccounts, rasterImageList1);

                            btnSubmit.Enabled = true;
                            btnGetDetailsFromFingerPrint.Enabled = false;

                            txtFeedBack.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Fingerprint does not match customer's profile.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not verify customer's fingerprint. Please try again.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (DeviceErrorException ex)
                {
                    ErrHandler.WriteError(ex.ToString());
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.ToString());
                }
            }
            catch (DeviceErrorException ex)
            {
                ErrHandler.WriteError(ex.ToString());
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError("An error occurred while trying to get customer details using fingerprint image. Message - " + ex.Message + "|StackTrace - " + ex.StackTrace);

                if (ex.Message.StartsWith("The license for the SDK is not valid"))
                {
                    MessageBox.Show("Please ensure that the fingerprint device is properly connected then try again.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }
    }
}