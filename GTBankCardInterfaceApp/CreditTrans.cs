using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ECRCommXLib;
using System.Threading;

namespace GTBankCardInterfaceApp
{
    public partial class CreditTrans : Form
    {
        enum PinPadRes { Success = 0, Error = 1, InProgress = 2 };
        Transaction Trans = new Transaction();
        Utilities util = new Utilities();
        public CreditTrans()
        {
            InitializeComponent();
        }

        private delegate void AddLogText(string text);
        private delegate void ShowProgressBar(ProgressBar b);
        private delegate void ToggleButton(Button a);
        private delegate void EnableButton(Button a);
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string curcode = "";
            curcode = util.getcurcode(txtCusAcctCredit.Text);

            if (txtCusName.Text.Equals(""))
            {

                MessageBox.Show("Please enter the Depositors Name");
                txtCusName.Focus();
                return;
            }
            if (txtAcctName.Text.Equals(""))
            {
                MessageBox.Show("Please enter the Account Name");
                txtAcctName.Focus();
                return;
            }
            if (AdminUser.LoginMode.Equals("Offline"))
            {
                if (cmbTransCurrency.SelectedIndex == 0)
                {
                    MessageBox.Show("Please Select The transfer Currency");
                    return;
                }

                if (cmbAcctType.SelectedIndex == 0)
                {
                    if ((txtCusAcctCredit.Text.Length != 10) || (txtCusAcctCredit.Text.Contains("/")) || (!util.verifyNubanCheckDigit("058", txtCusAcctCredit.Text)))
                    {
                        MessageBox.Show("Account is not a valid NUBAN account");
                        return;
                    }
                }
                else if (cmbAcctType.SelectedIndex == 1)
                {
                    if (!((txtCusAcctCredit.Text.Length > 10) & (txtCusAcctCredit.Text.Contains("/")) & (txtCusAcctCredit.Text.Length < 18) & util.verifyOldAccountFormat(txtCusAcctCredit.Text)))
                    {
                        MessageBox.Show("Account is not in valid Old account Format");
                        return;
                    }
                }
            }
            if (txtTransAmount.Text.Equals(""))
            {
                MessageBox.Show("Please enter the transaction Ammount");
                return;
            }

            if (!util.checkifnumeric(txtTransAmount.Text))
            {
                MessageBox.Show("Please Enter a Valid Transaction Amount");
                return;
            }
            if (Convert.ToDouble(txtTransAmount.Text) == 0)
            {
                MessageBox.Show("Transaction Amount Cannot be zero");
                return;
            }

            if (txtCusAcctCredit.Text.Equals(""))
            {
                MessageBox.Show("Please Enter the account number to credit");
                return;
            }

            if (Convert.ToDouble(AdminUser.AnalysedAmount) != Convert.ToDouble(txtTransAmount.Text))
            {
                MessageBox.Show("The analysed amount is different from the tendered cash. Please analyse again");
                return;
            }

            Thread thread = new Thread(new ThreadStart(Post));
            thread.IsBackground = true;
            thread.Name = "ThirdPartyDeposit";
            thread.Start();
        }

        private void Post()
        {
            try
            {
                AppDevService appserve = new AppDevService();
                string remark = "";
                string curcode = "";
                string PostResult = "";
                ulong InsertResult = 0;
                AdminUser.SVAccountNumber = txtCusAcctCredit.Text;

                TS_ToggleButton(btnSubmitCr);
                TS_ToggleButton(btnCancel);
                TS_ToggleButton(btnCashAnalysis);
                TS_ShowProgressBar(PostingprogressBar);
                TS_AddLogText("Processing Transaction ......");
                if (AdminUser.LoginMode.Equals("Online"))
                {
                    InsertResult = Trans.InitiateTransaction(AdminUser.branchcode, AdminUser.TellerId, txtCusAcctCredit.Text, Convert.ToDecimal(txtTransAmount.Text), AdminUser.LoginMode, "DEPOSIT", txtAcctName.Text.Trim(), txtTellerTillCr.Text, txtCusName.Text, CardAuth.CardNumber, CardAuth.Stan, CardAuth.MerchantID, CardAuth.AuthCode, CardAuth.AuthAmount, lblAnalysis.Text);
                    ulong update = 0;
                    if (InsertResult != 0)
                    {
                        remark = "PINPAD-" + InsertResult + "-" + AdminUser.branchcode + "/" + AdminUser.TellerId + "3RD PARTY CASH DEPOSIT BY " + txtCusName.Text.ToUpper();// THIS ALLOWS FOR UNIQUE RETRIEVAL OF transaction sequence from Basis.

                        PostResult = Trans.PostToBasis(txtCusAcctCredit.Text, txtTellerTillCr.Text, Convert.ToDouble(txtTransAmount.Text), Convert.ToUInt16(AdminUser.DepositExpl_Code), remark, "DEPOSIT", AdminUser.TellerId, AdminUser.branchcode);

                        curcode = util.getcurcode(txtCusAcctCredit.Text);
                        if (PostResult.Equals("0"))
                        {
                            update = Trans.UpdateTransaction(AdminUser.TellerId, InsertResult, AdminUser.branchcode, txtCusAcctCredit.Text, remark, "APPROVED", "SUCCESS", AdminUser.LoginMode, AdminUser.tillaccount, AdminUser.DepositExpl_Code);

                            if (Trans.PrintReceipt(update.ToString(), "DEPOSIT", AdminUser.TellerId + "/" + InsertResult.ToString(), txtNubanAccount.Text, txtAcctName.Text, Convert.ToDouble(txtTransAmount.Text), txtCusName.Text, AdminUser.branchname, curcode))
                            {
                                Trans.UpdatePrintStatus(InsertResult, 1);

                                MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence:" + update.ToString());

                                AdminUser.AnalyisAmount = "";
                            }
                            else
                            {
                                MessageBox.Show("Transaction Succesful. Receipt Failed to Print Check the printer and try Printing again from the Printing Menu:" + update.ToString());

                                TS_ShowProgressBar(PostingprogressBar);
                                AdminUser.AnalyisAmount = "";
                                AdminUser.CashAnalysis = "";
                            }
                        }

                        else if (PostResult.Equals("1"))
                        {
                            AdminUser.IsOverAuthlimit = true;

                            Trans.UpdateTransactionForApproval(InsertResult);

                            if (Trans.PrintReceipt(InsertResult.ToString(), "DEPOSIT", AdminUser.TellerId + "/" + InsertResult.ToString(), txtNubanAccount.Text, txtAcctName.Text, Convert.ToDouble(txtTransAmount.Text), txtCusName.Text, AdminUser.branchname, curcode))
                            {
                                Trans.UpdatePrintStatus(InsertResult, 1);

                                MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence:" + update.ToString());
                                AdminUser.AnalyisAmount = "";
                                AdminUser.CashAnalysis = "";
                            }
                            else
                            {
                                MessageBox.Show("Transaction Succesful. Receipt Failed to Print Check the printer and try Printing again from the Printing Menu:" + update.ToString());
                                AdminUser.AnalyisAmount = "";
                                AdminUser.CashAnalysis = "";
                            }

                            MessageBox.Show("Transaction Awaiting Approval.");
                        }
                        else
                        {
                            Trans.UpdateTransaction(AdminUser.TellerId, InsertResult, AdminUser.branchcode, AdminUser.CusDepositAccount, remark, "FAILED", PostResult, AdminUser.LoginMode, AdminUser.tillaccount, AdminUser.DepositExpl_Code);
                            AdminUser.AnalyisAmount = "";
                            AdminUser.CashAnalysis = "";

                            MessageBox.Show("Transaction Failed with Error::" + PostResult);
                        }

                    }
                }
                else if (AdminUser.LoginMode.Equals("Offline"))
                {
                    string s = DateTime.Now.ToString();
                    if (Trans.InitiateTransactionAccessDB(AdminUser.branchcode, AdminUser.TellerId, txtCusAcctCredit.Text, Convert.ToDecimal(txtTransAmount.Text), AdminUser.LoginMode, "DEPOSIT", txtAcctName.Text.ToUpper(), txtTellerTillCr.Text, txtCusName.Text.ToUpper(), s))
                    {
                        InsertResult = Trans.getTransIdOfflineAccessDB(txtCusAcctCredit.Text, Convert.ToDouble(txtTransAmount.Text), txtCusName.Text.ToUpper(), s);
                    }

                    if (InsertResult != 0)
                    {
                        if (Trans.PrintReceipt("F-" + InsertResult.ToString(), "DEPOSIT", AdminUser.TellerId + "/" + InsertResult.ToString(), txtCusAcctCredit.Text, txtAcctName.Text, Convert.ToDouble(txtTransAmount.Text), txtCusName.Text, AdminUser.branchname, "1"))
                        {
                            Trans.UpdatePrintStatus(InsertResult, 1);

                            MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence:" + InsertResult.ToString());
                            AdminUser.AnalyisAmount = "";
                            AdminUser.CashAnalysis = "";
                        }
                        else
                        {

                            MessageBox.Show("Transaction Succesful. Receipt Failed to Print Check the printer and try Printing again from the Printing Menu:" + InsertResult.ToString());
                            AdminUser.AnalyisAmount = "";
                            AdminUser.CashAnalysis = "";
                        }

                        AdminUser.AnalyisAmount = "";
                        AdminUser.CashAnalysis = "";
                        MessageBox.Show("Awaiting  Approval....");
                        return;
                    }
                    else
                    {
                        AdminUser.AnalyisAmount = "";
                        AdminUser.CashAnalysis = "";
                        MessageBox.Show("An Error Occured Initiating Transaction Please try Again..");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occured Initiating Transaction Please try Again.." + ex.Message);
            }
            finally
            {
                TS_AddLogText("Transaction Completed...");
                TS_EnableButton(btnCancel);
                TS_ShowProgressBar(PostingprogressBar);
                AdminUser.CashAnalysis = "";
                AdminUser.SVAccountNumber = "";
                Thread.CurrentThread.Abort();
            }
        }

        private void CreditTrans_Load(object sender, EventArgs e)
        {
            cmbAcctType.SelectedIndex = 0;
            lblExample.Text = "(0004498715)-MAX Ten(10) Digits ";
            util.ToggleAuthMode();
            if (AdminUser.LoginMode.Equals("Online"))
            {
                Text = Text + "  " + "Online";
                pnlGetDetailsOnline.Enabled = true;
                pnlCusDetails.Enabled = false;
            }
            else if (AdminUser.LoginMode.Equals("Offline"))
            {
                pnlGetDetailsOnline.Enabled = true;
                btnGetDetails.Enabled = false;
                Text = Text + "  " + "Offline";
                pnlCusDetails.Enabled = true;
            }
            if (AdminUser.roleid.Equals(AdminUser.TellerRoleId))
            {
                txtTellerTillCr.Text = AdminUser.tillaccount;
            }
            else
            {
                txtTellerTillCr.Text = "1/1/1/1/0";
            }
            if (txtTellerTillCr.Text.Equals(""))
            {
                MessageBox.Show("An error Occured retrieving teller TILL. Contact Technology Immediately.");
                return;
            }
            cmbTransCurrency.SelectedIndex = 0;
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

        private void txtTransAmount_KeyUp(object sender, KeyEventArgs e)
        {
            txtTransAmount.Text = util.FormatString(txtTransAmount.Text, 2);
            txtTransAmount.Select(txtTransAmount.TextLength, 0);
        }

        private void txtTransAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !(e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AdminUser.CashAnalysis = "";
            AdminUser.SVAccountNumber = "";
            Close();
        }

        private void btnGetDetails_Click(object sender, EventArgs e)
        {
            AppDevService appserve = new AppDevService();
            if (txtNubanAccount.Text.Equals(""))
            {
                MessageBox.Show("Please Enter the customers Nuban Account Number");
                return;
            }

            if (AdminUser.LoginMode.Equals("Online"))
            {
                string[] cusdetails = null;
                if (txtNubanAccount.Text.Trim().Length == 10)
                {
                    cusdetails = appserve.getcusDepositDetails(txtNubanAccount.Text);
                }
                else
                {
                    if ((txtNubanAccount.Text.Length > 10) & (txtNubanAccount.Text.Contains("/")) & (txtNubanAccount.Text.Length < 18))
                    {
                        MessageBox.Show("Account is not in valid Old account Format:" + "::Format(2051393371590) No Slashes Accepted");
                        return;
                    }

                    cusdetails = appserve.getcusDepositDetailsOldAccount(txtNubanAccount.Text);
                }
                if (!(string.IsNullOrEmpty(cusdetails[0]) || string.IsNullOrEmpty(cusdetails[1])))
                {
                    pnlGetDetailsOnline.Enabled = false;
                    btnCashAnalysis.Enabled = true;
                    txtCusAcctCredit.Text = cusdetails[0].ToString().Trim();
                    txtAcctName.Text = cusdetails[1].ToString().Trim();
                    string[] telleraccts = new string[5];
                    string[] custaccts = new string[5];
                    telleraccts = txtTellerTillCr.Text.Split('/');
                    custaccts = txtCusAcctCredit.Text.Split('/');
                    telleraccts[2] = custaccts[2];
                    if (custaccts[2].ToString().Trim().Equals("46"))
                    {
                        cmbTransCurrency.SelectedIndex = 4;
                    }
                    else
                    {
                        cmbTransCurrency.SelectedIndex = Convert.ToInt32(custaccts[2]);
                    }
                    txtTellerTillCr.Text = telleraccts[0].ToString() + "/" + telleraccts[1].ToString() + "/" + telleraccts[2].ToString() + "/" + telleraccts[3].ToString() + "/" + telleraccts[4].ToString();
                }
                else
                {
                    MessageBox.Show("Unable to retrieve customer details Please verify account Number");
                    btnPinPadDetails.Enabled = true;
                    pnlGetDetailsOnline.Enabled = true;
                    btnCashAnalysis.Enabled = false;
                }
            }
            else //offline transactions
            {

            }
        }

        private void btnPinPadDetails_Click(object sender, EventArgs e)
        {
            AppDevService appserve = new AppDevService();
            btnPinPadDetails.Enabled = false;
            lblTransProgress.Visible = false;
            string[] cusdetails = null;
            util.ToggleAuthMode();
            GTLib m = new GTLib();
            byte bRes = m.LastResult;
            try
            {
                m.CommOpenAuto(115200, 100);
                if (m.LastResult == (byte)PinPadRes.Error)
                {
                    string a = m.LastErrorDescription;

                    MessageBox.Show("An Error Occured Communicating with the Device. Please Ensure No card is in the terminal and Press the F key to enter ECR MODE! : " + a);
                    btnPinPadDetails.Enabled = true;
                    btnCashAnalysis.Enabled = false;
                    return;
                }
                else
                {
                    m.Init((Convert.ToByte(AdminUser.HostID)), "ENTER AMOUNT[NGN]:", "ENTER ACCOUNT NO");

                    m.ValidateCardNoPIN(0);

                }

                while (m.LastResult == (byte)PinPadRes.InProgress)
                {
                    lblTransProgress.Visible = true;
                    lblTransProgress.Text = "Retrieving Details... Please wait.";
                }

                if (AdminUser.LoginMode.Equals("Online"))
                {
                    if (m.LastResult == (byte)PinPadRes.Success)
                    {
                        string s = m.CustomersData.Trim();
                        if (s.Length == 10)
                        {
                            txtNubanAccount.Text = m.CustomersData.Trim();//"0004498914";

                            cusdetails = appserve.getcusDepositDetails(txtNubanAccount.Text);
                            if (!(string.IsNullOrEmpty(cusdetails[0]) || string.IsNullOrEmpty(cusdetails[1])))
                            {
                                txtCusAcctCredit.Text = cusdetails[0].ToString();
                                txtAcctName.Text = cusdetails[1].ToString();

                                string[] telleraccts = new string[5];
                                string[] custaccts = new string[5];
                                telleraccts = txtTellerTillCr.Text.Split('/');
                                custaccts = txtCusAcctCredit.Text.Split('/');
                                telleraccts[2] = custaccts[2];//assign the currency code of the cus account to the teller account.
                                if (custaccts[2].ToString().Trim().Equals("46"))
                                {
                                    cmbTransCurrency.SelectedIndex = 4;
                                }
                                else
                                {
                                    cmbTransCurrency.SelectedIndex = Convert.ToInt32(custaccts[2]);
                                }
                                txtTellerTillCr.Text = telleraccts[0].ToString() + "/" + telleraccts[1].ToString() + "/" + telleraccts[2].ToString() + "/" + telleraccts[3].ToString() + "/" + telleraccts[4].ToString();
                                btnCashAnalysis.Enabled = true;
                                lblTransProgress.Visible = true;
                                lblTransProgress.Text = "Values Retrieved Succesfully From PIN PAD.";
                                lblTransProgress.ForeColor = Color.Green;
                                txtTransAmount.Text = (Convert.ToDouble(m.Amount) / 100).ToString();
                                btnGetDetails.Enabled = false;
                                txtNubanAccount.Enabled = false;
                                txtTransAmount.Enabled = false;
                                btnPinPadDetails.Enabled = false;
                                CardAuth.AuthAmount = 0;// Convert.ToDecimal(m.Amount);
                                CardAuth.AuthCode = "";// m.AuthorizationCode;
                                CardAuth.MerchantID = "";//m.MerchantID;
                                CardAuth.Stan = 0;// m.STAN;
                                CardAuth.CardNumber = "";// m.CardNumber;
                                btnCashAnalysis.Enabled = true;
                            }
                            else
                            {
                                btnGetDetails.Enabled = true;
                                btnPinPadDetails.Enabled = true;
                                txtNubanAccount.Text = m.CustomersData.Trim();
                                btnCashAnalysis.Enabled = false;

                                m.CommClose();
                                lblTransProgress.Text = "Invalid Account Entered.";

                                MessageBox.Show("Invalid Account Entered.");
                                return;
                            }
                        }
                        else if (s.Length == 12 || s.Length == 13) //savings or current 
                        {
                            txtNubanAccount.Text = m.CustomersData.Trim();
                            cusdetails = appserve.getcusDepositDetailsOldAccount(m.CustomersData.Trim());
                            if (!(string.IsNullOrEmpty(cusdetails[0]) || string.IsNullOrEmpty(cusdetails[1])))
                            {
                                txtCusAcctCredit.Text = cusdetails[0].ToString();
                                txtAcctName.Text = cusdetails[1].ToString();

                                string[] telleraccts = new string[5];
                                string[] custaccts = new string[5];
                                telleraccts = txtTellerTillCr.Text.Split('/');
                                custaccts = txtCusAcctCredit.Text.Split('/');
                                telleraccts[2] = custaccts[2];//assign the currency code of the cus account to the teller account.
                                if (custaccts[2].ToString().Trim().Equals("46"))
                                {
                                    cmbTransCurrency.SelectedIndex = 4;
                                }
                                else
                                {
                                    cmbTransCurrency.SelectedIndex = Convert.ToInt32(custaccts[2]);
                                }
                                txtTellerTillCr.Text = telleraccts[0].ToString() + "/" + telleraccts[1].ToString() + "/" + telleraccts[2].ToString() + "/" + telleraccts[3].ToString() + "/" + telleraccts[4].ToString();

                                btnCashAnalysis.Enabled = true;
                                lblTransProgress.Visible = true;
                                lblTransProgress.Text = "Values Retrieved Succesfully From PIN PAD.";
                                lblTransProgress.ForeColor = Color.Green;
                                txtTransAmount.Text = (Convert.ToDouble(m.Amount) / 100).ToString();
                                btnGetDetails.Enabled = false;
                                txtNubanAccount.Enabled = false;
                                txtTransAmount.Enabled = false;
                                btnPinPadDetails.Enabled = false;
                                CardAuth.AuthAmount = 0;// Convert.ToDecimal(m.Amount);
                                CardAuth.AuthCode = "";// m.AuthorizationCode;
                                CardAuth.MerchantID = "";//m.MerchantID;
                                CardAuth.Stan = 0;// m.STAN;
                                CardAuth.CardNumber = "";// m.CardNumber;
                                btnCashAnalysis.Enabled = true;
                            }
                            else
                            {
                                btnGetDetails.Enabled = true;
                                btnPinPadDetails.Enabled = true;
                                txtNubanAccount.Text = m.CustomersData.Trim();

                                m.CommClose();
                                lblTransProgress.Text = "Invalid Account Entered.";
                                MessageBox.Show("Invalid Account Entered.");
                                return;
                            }
                        }
                        else
                        {
                            btnGetDetails.Enabled = true;
                            txtNubanAccount.Text = m.CustomersData.Trim();
                            btnPinPadDetails.Enabled = true;
                            m.CommClose();
                            lblTransProgress.Text = "Invalid Account Entered.";

                            MessageBox.Show("Invalid Account Entered.");
                            return;
                        }
                    }
                    else if (m.LastResult == (byte)PinPadRes.Error)
                    {
                        txtTransAmount.Text = "";
                        txtCusName.Text = "";
                        btnGetDetails.Enabled = true;
                        txtNubanAccount.Enabled = true;
                        txtTransAmount.Enabled = true;
                        btnPinPadDetails.Enabled = true;
                        lblTransProgress.Visible = true;
                        lblTransProgress.Text = "Failure Communicating With Pin Pad....";

                        MessageBox.Show("Failure Communicating With Pin Pad....");
                    }
                }
                else if (AdminUser.LoginMode.Equals("Offline"))
                {
                    if (m.LastResult == (byte)PinPadRes.Success)
                    {
                        string s = m.CustomersData.Trim();

                        if (s.Length == 10)
                        {
                            txtNubanAccount.Text = m.CustomersData.Trim();
                            txtCusAcctCredit.Text = m.CustomersData.Trim();
                            btnCashAnalysis.Enabled = true;
                            lblTransProgress.Visible = true;
                            lblTransProgress.Text = "Values Retrieved Succesfully From PIN PAD.";
                            lblTransProgress.ForeColor = Color.Green;
                            txtTransAmount.Text = (Convert.ToDouble(m.Amount) / 100).ToString();
                            btnGetDetails.Enabled = false;
                            txtNubanAccount.Enabled = false;
                            txtTransAmount.Enabled = false;
                            btnPinPadDetails.Enabled = false;
                            CardAuth.AuthAmount = 0;// Convert.ToDecimal(m.Amount);
                            CardAuth.AuthCode = "";// m.AuthorizationCode;
                            CardAuth.MerchantID = "";//m.MerchantID;
                            CardAuth.Stan = 0;// m.STAN;
                            CardAuth.CardNumber = "";// m.CardNumber;
                            btnCashAnalysis.Enabled = true;
                        }
                        else if (s.Length == 12 || s.Length == 13) //savings or current 
                        {
                            txtNubanAccount.Text = m.CustomersData.Trim();
                            txtCusAcctCredit.Text = m.CustomersData.Trim();
                            btnCashAnalysis.Enabled = true;
                            lblTransProgress.Visible = true;
                            lblTransProgress.Text = "Values Retrieved Succesfully From PIN PAD.";
                            lblTransProgress.ForeColor = Color.Green;
                            txtTransAmount.Text = (Convert.ToDouble(m.Amount) / 100).ToString();
                            btnGetDetails.Enabled = false;
                            txtNubanAccount.Enabled = false;
                            txtTransAmount.Enabled = false;
                            btnPinPadDetails.Enabled = false;
                            CardAuth.AuthAmount = 0;// Convert.ToDecimal(m.Amount);
                            CardAuth.AuthCode = "";// m.AuthorizationCode;
                            CardAuth.MerchantID = "";//m.MerchantID;
                            CardAuth.Stan = 0;// m.STAN;
                            CardAuth.CardNumber = "";// m.CardNumber;
                            btnCashAnalysis.Enabled = true;
                        }
                    }
                    else if (m.LastResult == (byte)PinPadRes.Error)
                    {
                        txtTransAmount.Text = "";
                        txtCusName.Text = "";
                        btnGetDetails.Enabled = true;
                        txtNubanAccount.Enabled = true;
                        txtTransAmount.Enabled = true;
                        btnPinPadDetails.Enabled = true;
                        lblTransProgress.Visible = true;
                        lblTransProgress.Text = "Failure Communicating With Pin Pad....";

                        MessageBox.Show("Failure Communicating With Pin Pad....");
                    }
                }
            }
            catch (Exception ex)
            {
                lblTransProgress.Visible = true;
                lblTransProgress.Text = "An Error occured: " + ex.Message;
            }
            finally
            {
                m.CommClose();
            }
        }

        private void cmbTransCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] telleraccts = new string[5];
            telleraccts = txtTellerTillCr.Text.Split('/');

            if (cmbTransCurrency.SelectedIndex == 1)
            {
                telleraccts[2] = "1";
                txtTellerTillCr.Text = telleraccts[0].ToString() + "/" + telleraccts[1].ToString() + "/" + telleraccts[2].ToString() + "/" + telleraccts[3].ToString() + "/" + telleraccts[4].ToString();
            }
            else if (cmbTransCurrency.SelectedIndex == 2)
            {
                telleraccts[2] = "2";
                txtTellerTillCr.Text = telleraccts[0].ToString() + "/" + telleraccts[1].ToString() + "/" + telleraccts[2].ToString() + "/" + telleraccts[3].ToString() + "/" + telleraccts[4].ToString();
            }
            else if (cmbTransCurrency.SelectedIndex == 3)
            {
                telleraccts[2] = "3";
                txtTellerTillCr.Text = telleraccts[0].ToString() + "/" + telleraccts[1].ToString() + "/" + telleraccts[2].ToString() + "/" + telleraccts[3].ToString() + "/" + telleraccts[4].ToString();
            }
            else if (cmbTransCurrency.SelectedIndex == 4)
            {
                telleraccts[2] = "46";
                txtTellerTillCr.Text = telleraccts[0].ToString() + "/" + telleraccts[1].ToString() + "/" + telleraccts[2].ToString() + "/" + telleraccts[3].ToString() + "/" + telleraccts[4].ToString();
            }
        }

        private void cmbAcctType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAcctType.SelectedIndex == 0)
            {
                lblExample.Text = "E.g (0004498715)-MAX Ten(10) Digits ";
            }
            else if (cmbAcctType.SelectedIndex == 1)
            {
                lblExample.Text = "E.g (205/153376/1/1/0) Separate with slash";
            }
        }

        private void txtTransAmount_TextChanged(object sender, EventArgs e)
        {
            txtTransAmount.Text = util.FormatString(txtTransAmount.Text, 2);
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

        private void btnSubmitCr_MouseEnter(object sender, EventArgs e)
        {
            lblAnalysis.Text = AdminUser.CashAnalysis;
        }
    }
}