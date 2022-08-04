using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using GTBSecure;

namespace GTBankCardInterfaceApp
{
    public partial class Login : Form
    {
        Utilities u = new Utilities();
        Transaction Trans = new Transaction();
        Welcome frmWelcome = new Welcome();

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            AppDevService appserve = new AppDevService();
            string[] PinPadValues = null;

            if (txtUserName.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Your UserName", "GTBankCardInterfaceApp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtUserName.Focus();
                return;
            }
            if (txtPassword.Text.Equals(""))
            {
                MessageBox.Show("Please Enter Your Password", "GTBankCardInterfaceApp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtPassword.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtTokenCode.Text))
            {
                MessageBox.Show("Please Enter Code Generated from your Token Code", "GTBankCardInterfaceApp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtPassword.Focus();
                return;
            }

            try
            {
                PinPadValues = appserve.getPinPadValues();
                AdminUser.charge = PinPadValues[0];
                AdminUser.chargeslimit = PinPadValues[1];
                AdminUser.chargeaccount = PinPadValues[2].ToString();
                AdminUser.DepositLimit = PinPadValues[3];
                AdminUser.OpsHeadRoleId = PinPadValues[4];
                AdminUser.TellerRoleId = PinPadValues[5];
                AdminUser.DepositITRoleId = PinPadValues[6];
                AdminUser.WithdrwalExpl_Code = PinPadValues[7];
                AdminUser.DepositExpl_Code = PinPadValues[8];
                AdminUser.AdminRoleId = PinPadValues[9];
                AdminUser.BasisIP = PinPadValues[10];
                AdminUser.BasisPort = PinPadValues[11];
                AdminUser.PinPadDepositLimit = PinPadValues[12];
                AdminUser.PinPadWithdrawalLimit = PinPadValues[13];
                AdminUser.VATAccount = PinPadValues[14];
                AdminUser.ThirdCurLimit = PinPadValues[15];
                AdminUser.PinPadCon = PinPadValues[16];
                AdminUser.gapsCon = PinPadValues[17];
                AdminUser.BankCardCon = PinPadValues[18];
                AdminUser.ApplicationID = PinPadValues[19];
                AdminUser.HostID = PinPadValues[20];
                AdminUser.AllowedVersion = PinPadValues[21];
                AdminUser.Level1TransactionLimit = PinPadValues[22];
                AdminUser.Level2TransactionLimit = PinPadValues[23];
                AdminUser.Level1BalanceLimit = PinPadValues[24];
                AdminUser.Level2BalanceLimit = PinPadValues[25];
                AdminUser.IsCardLessAllowed = PinPadValues[26];
                AdminUser.TwigWebsrviceUrl = PinPadValues[27];
                //AdminUser.PowerCardBIN = PinPadValues[25];
                AdminUser.webserviceavailable = true;
                ErrHandler.WriteError("Connected to webservice and retrieved data succesfully");
            }
            catch (Exception ex)
            {
                AdminUser.webserviceavailable = false;
                ErrHandler.WriteError("Cannot connect to webservice. Message - " + ex.Message + "|Stacktrace - " + ex.StackTrace);
                AdminUser.BasisIP = "10.0.1.21";
                AdminUser.BasisPort = "1521";

            }

            string appver = string.Empty;

            if (AdminUser.webserviceavailable == true)
            {
                appver = GetAppVersion();

                if (!AdminUser.AllowedVersion.Equals(appver))
                {
                    MessageBox.Show("You are running an outdated version of PINPAD. Please liaise with your regional technology to install version : " + AdminUser.AllowedVersion + " The version on your system is: " + appver);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Error Connecting to authentication Server...");
                return;
            }

            if (Utilities.isServerUp(AdminUser.BasisIP, Convert.ToInt16(AdminUser.BasisPort), 15000))
            {
                ErrHandler.WriteError("Ping Succesful. Trying to authenticate Online....");

                if (u.validateUserWithEoneAccessManager(txtUserName.Text, Secure.EncryptString(txtPassword.Text), appver, txtTokenCode.Text))
                {
                    ErrHandler.WriteError("User Validated Succesfully....");
                    AdminUser.chargeaccount = AdminUser.branchcode + AdminUser.chargeaccount;
                    AdminUser.VATAccount = AdminUser.branchcode + AdminUser.VATAccount;
                    if ((AdminUser.roleid.Equals(AdminUser.TellerRoleId)) || (AdminUser.roleid.Equals(AdminUser.OpsHeadRoleId)) || AdminUser.roleid.Equals(AdminUser.DepositITRoleId))
                    {
                        if ((AdminUser.roleid.Equals(AdminUser.TellerRoleId)))
                        {
                            string tellertill = appserve.GetBasisTellerTillAcct(AdminUser.branchcode, AdminUser.TellerId, "1");

                            if (tellertill.Equals("0/0/0/0") || string.IsNullOrEmpty(tellertill))
                            {
                                MessageBox.Show("Cannot retrieve Till Account Please contact User Access to check your profile.");
                                
                                return;
                            }
                            else
                            {
                                AdminUser.tillaccount = tellertill;
                            }
                        }

                        if (u.checkIfLocalUserExistsAcessDB(txtUserName.Text))
                        {
                            u.UpdateLocalUserAccessDB(AdminUser.TellerId, txtUserName.Text, txtPassword.Text, AdminUser.branchcode, AdminUser.roleid, AdminUser.email, AdminUser.tillaccount);
                        }
                        else
                        {
                            u.CreateLocalUserAccessDB(AdminUser.TellerId, txtUserName.Text, txtPassword.Text, AdminUser.branchcode, AdminUser.roleid, AdminUser.email, AdminUser.tillaccount);
                        }

                        if (u.getLogos())
                        {

                        }
                        else
                        {
                            ErrHandler.WriteError("Cannot Retrieve Logo Online..  Trying Offline Access DB");

                            if (u.getLogosOfflineAccessDB())
                            {
                                ErrHandler.WriteError("Got Logo Offline. Though login in online Mode");
                            }
                            else
                            {
                                MessageBox.Show("Cannot retrieve Receipt Header and Footer. Please contact technology");
                                return;
                            }
                        }
                    }

                    if (AdminUser.roleid.Equals(AdminUser.OpsHeadRoleId) || AdminUser.roleid.Equals(AdminUser.TellerRoleId) || (AdminUser.roleid.Equals(AdminUser.DepositITRoleId) || (AdminUser.roleid.Equals(AdminUser.AdminRoleId))))
                    {
                        Trans.sweepLocalTransactionsAccessDB();
                        AdminUser.LoginMode = "Online";
                        MessageBox.Show("Login Succesful", "GTBankCardInterfaceApp", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Hide();

                        frmWelcome.Show();
                    }
                    else
                    {
                        MessageBox.Show("Only Operations staff are allowed to use this application. Your attempt has been logged", "Unauthorised Access-GTBankCardInterfaceApp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                else if (AdminUser.webserviceavailable == false)
                {
                    MessageBox.Show("Error Connecting to Authentication Server.", "GTBankCardInterfaceApp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPassword.Text = "";
                }
                else
                {
                    if (AdminUser.LoginErrorcode.Equals("1005"))

                    {
                        MessageBox.Show("You are running an outdated version of PINPAD. Please liaise with your regional technology to install version : " + AdminUser.AllowedVersion + " Your Detected version is : " + appver, "GTBankCardInterfaceApp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtPassword.Text = "";
                        return;
                    }

                    MessageBox.Show("Invalid UserName or Password :" + AdminUser.LoginErrorMsg, "GTBankCardInterfaceApp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPassword.Text = "";
                    return;
                }
            }
            else if (u.AuthenticateLocalUserAccessDB(txtPassword.Text, txtUserName.Text))
            {
                ErrHandler.WriteError("Offline Authentication....");
                AdminUser.LoginMode = "Offline";               
                txtPassword.Text = "";
               
                if (AdminUser.roleid.Trim().Equals(AdminUser.OpsHeadRoleId) || AdminUser.roleid.Trim().Equals(AdminUser.TellerRoleId) || (AdminUser.roleid.Equals(AdminUser.DepositITRoleId)))
                {
                    if (u.getLogosOfflineAccessDB())
                    {
                        AdminUser.LoginMode = "Offline";
                        MessageBox.Show("The Network is Not Available. Only deposit Transactions are Allowed!!!", "GTBankCardInterfaceApp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        this.Hide();
                        frmWelcome.Show();
                    }
                    else
                    {
                        MessageBox.Show("Cannot retrieve local logo Data...");
                        return;
                    }
                }
            }
            else
            {
                ErrHandler.WriteError("Everything Failed......");
                MessageBox.Show("Cannot Authenticate user");
                return;
            }
        }

        public void CheckForExistingInstance()
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("Another Instance of GTBankCardInterfaceApp is already running", "Multiple Instances Forbidden", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            CheckForExistingInstance();
        }

        private static string GetAppVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion;
            return version;
        }

        private static string AssemblyProductVersion
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
                return attributes.Length == 0 ?
                    "" :
                    ((AssemblyInformationalVersionAttribute)attributes[0]).InformationalVersion;
            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}