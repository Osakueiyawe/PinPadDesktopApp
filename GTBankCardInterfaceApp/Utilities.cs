using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using GTBSecure;
using System.Text;
using System.IO;
using Leadtools.WinForms;
using Leadtools.Codecs;
using System.Configuration;

namespace GTBankCardInterfaceApp
{
    class Utilities
    {
        OleDbConnection Con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\PinPad.mdb");

        Random random = new Random();

        XmlDocument xmlDocument = new XmlDocument();

        StringBuilder builder = new StringBuilder();

        AppDevService appDev = new AppDevService();

        RasterCodecs codec;

        CustDetRetVal custdetails = null;

        public static bool isServerUp(string ip, int port, int timout)
        {
            return true;
        }

        class TimeOutSocket
        {

            private static bool IsConnectionSuccessful = false;

            private static Exception socketexception;

            private static ManualResetEvent TimeoutObject = new ManualResetEvent(false);

            public static bool Connect(IPEndPoint remoteEndPoint, int timeoutMSec)
            {

                TimeoutObject.Reset();

                socketexception = null;

                string serverip = Convert.ToString(remoteEndPoint.Address);

                int serverport = remoteEndPoint.Port;

                TcpClient tcpclient = new TcpClient();

                try
                {
                    tcpclient.BeginConnect(serverip, serverport, new AsyncCallback(CallBackMethod), tcpclient);

                    if (TimeoutObject.WaitOne(timeoutMSec, false))
                    {

                        if (IsConnectionSuccessful)
                        {
                            tcpclient.Close();

                            return true;
                        }

                        else
                        {
                            tcpclient.Close();

                            return false;
                        }
                    }
                    else
                    {
                        tcpclient.Close();

                        return false;
                    }
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.ToString());
                    return false;
                }
            }

            private static void CallBackMethod(IAsyncResult asyncresult)
            {
                try
                {
                    IsConnectionSuccessful = false;

                    TcpClient tcpclient = asyncresult.AsyncState as TcpClient;

                    if (tcpclient.Client != null)
                    {
                        tcpclient.EndConnect(asyncresult);

                        IsConnectionSuccessful = true;
                    }

                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.ToString());
                    IsConnectionSuccessful = false;
                    socketexception = ex;
                }
                finally
                {
                    TimeoutObject.Set();
                }
            }
        }

        public bool checkifnumeric(string txt)
        {
            double number;
            bool result = double.TryParse(txt, out number);

            if (result == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool validateUserWithEoneAccessManager(string uid, string upass, string appversion, string tokenid)
        {
            try
            {
                int appid = Convert.ToInt16(AdminUser.ApplicationID);
                AppDevService appserve = new AppDevService();
                String eoneRetVal = null;
                XmlDocument document = null;
                XPathNavigator navigator = null;
                XPathNodeIterator snodes = null;
                String retcode = null;
                eoneRetVal = appserve.ValidateAdminUserOffSitewithAppver(uid, upass, appid, appversion, tokenid);
                document = new XmlDocument();
                document.LoadXml(eoneRetVal);
                navigator = document.CreateNavigator();
                snodes = navigator.Select("/Response/CODE");
                snodes.MoveNext();
                retcode = snodes.Current.Value;

                if (retcode != "1000")
                {
                    snodes = navigator.Select("/Response/ERROR");
                    snodes.MoveNext();
                    AdminUser.LoginErrorcode = retcode;
                    AdminUser.LoginErrorMsg = snodes.Current.Value;
                    return false;
                }
                snodes = navigator.Select("/Response/USER/ID");
                snodes.MoveNext();
                AdminUser.TellerId = snodes.Current.Value;

                snodes = navigator.Select("/Response/USER/NAME");
                snodes.MoveNext();
                AdminUser.username = snodes.Current.Value;

                snodes = navigator.Select("/Response/USER/BRANCH");
                snodes.MoveNext();
                AdminUser.branchcode = snodes.Current.Value;

                snodes = navigator.Select("/Response/USER/BRANCH_NAME");
                snodes.MoveNext();
                AdminUser.branchname = snodes.Current.Value;

                snodes = navigator.Select("/Response/USER/EMAIL");
                snodes.MoveNext();
                AdminUser.email = snodes.Current.Value;
                snodes = navigator.Select("/Response/ROLE/RID");
                snodes.MoveNext();
                AdminUser.roleid = snodes.Current.Value;

                snodes = navigator.Select("/Response/USER/DOMAINID");
                snodes.MoveNext();
                AdminUser.DomainId = snodes.Current.Value;

                snodes = navigator.Select("/Response/USER/TerminalID");
                snodes.MoveNext();
                AdminUser.TerminalID = snodes.Current.Value;

                snodes = navigator.Select("/Response/USER/TerminalSerial");
                snodes.MoveNext();
                AdminUser.TerminalSerial = snodes.Current.Value;
                
                AdminUser.webserviceavailable = true;
                return true;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                AdminUser.webserviceavailable = false;
                return false;
            }
        }

        public string FormatString(string strValor, int intNumDecimales)
        {
            string strAux = null;
            string strComas = null;
            string strPuntos = null;
            int intX = 0;
            bool bolMenos = false;

            strComas = "";
            if (strValor.Length == 0) return "";
            strValor = strValor.Replace(Application.CurrentCulture.NumberFormat.NumberGroupSeparator, "");

            if (strValor.Contains(Application.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {
                strAux = strValor.Substring(0, strValor.LastIndexOf(Application.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                strComas = strValor.Substring(strValor.LastIndexOf(Application.CurrentCulture.NumberFormat.NumberDecimalSeparator) + 1);
            }
            else
            {
                strAux = strValor;
            }

            if (strAux.Substring(0, 1) == Application.CurrentCulture.NumberFormat.NegativeSign)
            {
                bolMenos = true;
                strAux = strAux.Substring(1);
            }

            strPuntos = strAux;
            strAux = "";
            while (strPuntos.Length > 3)
            {
                strAux = Application.CurrentCulture.NumberFormat.NumberGroupSeparator + strPuntos.Substring(strPuntos.Length - 3, 3) + strAux;
                strPuntos = strPuntos.Substring(0, strPuntos.Length - 3);
            }
            if (intNumDecimales > 0)
            {
                if (strValor.Contains(Application.CurrentCulture.NumberFormat.PercentDecimalSeparator))
                {
                    strComas = Application.CurrentCulture.NumberFormat.PercentDecimalSeparator + strValor.Substring(strValor.LastIndexOf(Application.CurrentCulture.NumberFormat.PercentDecimalSeparator) + 1);
                    if (strComas.Length > intNumDecimales)
                    {
                        strComas = strComas.Substring(0, intNumDecimales + 1);
                    }
                }
            }
            strAux = strPuntos + strAux + strComas;

            return strAux;
        }

        public ulong CreateLocalUserAccessDB(string tellerid, string username, string userpass, string bracode, string roleid, string email, string tellertill)
        {
            object result = 0;

            try
            {
                using (OleDbConnection Con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\PinPad.gtb"))
                {
                    using (OleDbCommand accesscom = new OleDbCommand())
                    {
                        accesscom.Connection = Con;
                        accesscom.CommandText = "INSERT INTO dbo_LocalUser ( TellerId, UserName, UserPass, BraCode, RoleId, DateLastUpdated, UserEmail, TellerTillAccount,DepositLimit,OpsHeadRoleId,TellerRoleId,DepositITRoleId,DepositExpl_Code,AdminRoleId,BasisIP,BasisPort,PinPadDepositLimit,Branchname ) VALUES ('" + tellerid + "','" + username + "','" + Encryption.Encrypt(userpass, "PinPadKey123456789123456") + "','" + bracode + "','" + roleid + "','" + DateTime.Now + "','" + email + "','" + tellertill + "','" + AdminUser.DepositLimit + "','" + AdminUser.OpsHeadRoleId + "','" + AdminUser.TellerRoleId + "','" + AdminUser.DepositITRoleId + "','" + AdminUser.DepositExpl_Code + "','" + AdminUser.AdminRoleId + "','" + AdminUser.BasisIP + "','" + AdminUser.BasisPort + "','" + AdminUser.PinPadDepositLimit + "','" + AdminUser.branchname + "')";
                        accesscom.CommandType = CommandType.Text;

                        if (Con.State == ConnectionState.Closed)
                        {
                            Con.Open();
                        }

                        result = accesscom.ExecuteScalar();

                        return Convert.ToUInt64(result);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return 0;
            }
        }

        public bool checkIfLocalUserExistsAcessDB(string username)
        {
            try
            {
                using (OleDbConnection Con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\PinPad.gtb"))
                {
                    using (OleDbCommand accesscom = new OleDbCommand())
                    {
                        accesscom.Connection = Con;
                        accesscom.CommandText = "SELECT TellerId, UserName, UserPass, BraCode, RoleId, DateLastUpdated, UserEmail FROM  dbo_LocalUser where username = '" + username + "'";
                        accesscom.CommandType = CommandType.Text;

                        if (Con.State == ConnectionState.Closed)
                        {
                            Con.Open();
                        }

                        using (OleDbDataReader result = accesscom.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (result.Read())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return false;
            }
        }

        public bool getLogos()
        {
            object[] logos = new object[2];

            try
            {
                AppDevService appserve = new AppDevService();

                logos = appserve.getLogos();
                AdminUser.HeaderImage = (byte[])logos[0];
                AdminUser.FooterImage = (byte[])logos[1];
                return true;
            }

            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return false;
            }
        }

        public bool getLogosOfflineAccessDB()
        {
            try
            {
                using (OleDbConnection Con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\PinPad.gtb"))
                {
                    using (OleDbCommand accesscom = new OleDbCommand())
                    {
                        accesscom.Connection = Con;
                        accesscom.CommandText = "SELECT [Header], [Footer] FROM dbo_Images;";
                        accesscom.CommandType = CommandType.Text;

                        if (Con.State == ConnectionState.Closed)
                        {
                            Con.Open();
                        }

                        using (OleDbDataReader result = accesscom.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (result.Read())
                            {
                                AdminUser.HeaderImage = (byte[])result["Header"];
                                AdminUser.FooterImage = (byte[])result["Footer"];
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return false;
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
            }
        }

        public bool AuthenticateLocalUserAccessDB(string password, string username)
        {
            try
            {
                using (OleDbConnection Con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\PinPad.gtb"))
                {
                    using (OleDbCommand accesscom = new OleDbCommand())
                    {
                        accesscom.Connection = Con;

                        accesscom.CommandText = "SELECT [TellerId], [UserName], [UserPass], [BraCode], [RoleId], [DateLastUpdated], TellerTillAccount, [UserEmail],DepositLimit,OpsHeadRoleId,TellerRoleId,DepositITRoleId,DepositExpl_Code,AdminRoleId,BasisIP,BasisPort,PinPadDepositLimit,Branchname  FROM dbo_LocalUser WHERE [UserName]= '" + username + "' And UserPass= '" + Encryption.Encrypt(password, "PinPadKey123456789123456") + "'";
                        accesscom.CommandType = CommandType.Text;

                        if (Con.State == ConnectionState.Closed)
                        {
                            Con.Open();
                        }

                        using (OleDbDataReader result = accesscom.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (result.Read())
                            {
                                AdminUser.TellerId = result["TellerId"].ToString();
                                AdminUser.username = result["UserName"].ToString();
                                AdminUser.branchcode = result["BraCode"].ToString();
                                AdminUser.email = result["UserEmail"].ToString();
                                AdminUser.roleid = result["RoleId"].ToString();
                                //AdminUser.DomainId = result["TellerId"].ToString();
                                AdminUser.tillaccount = result["TellerTillAccount"].ToString();
                                AdminUser.DepositLimit = result["DepositLimit"].ToString();
                                AdminUser.OpsHeadRoleId = result["OpsHeadRoleId"].ToString();
                                AdminUser.TellerRoleId = result["TellerRoleId"].ToString();
                                AdminUser.DepositITRoleId = result["DepositITRoleId"].ToString();
                                AdminUser.DepositExpl_Code = result["DepositExpl_Code"].ToString();
                                AdminUser.AdminRoleId = result["AdminRoleId"].ToString();
                                AdminUser.BasisIP = result["BasisIP"].ToString();
                                AdminUser.BasisPort = result["BasisPort"].ToString();
                                AdminUser.PinPadDepositLimit = result["PinPadDepositLimit"].ToString();
                                AdminUser.branchname = result["Branchname"].ToString();

                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return false;
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
            }
        }

        public ulong UpdateLocalUserAccessDB(string tellerid, string username, string userpass, string bracode, string roleid, string email, string tellertill)
        {
            object result = 0;

            try
            {
                using (OleDbConnection Con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\PinPad.gtb"))
                {
                    using (OleDbCommand accesscom = new OleDbCommand())
                    {
                        accesscom.Connection = Con;

                        accesscom.CommandText = "UPDATE dbo_LocalUser SET TellerId = '" + tellerid + "', UserName = '" + username + "', UserPass = '" + Encryption.Encrypt(userpass, "PinPadKey123456789123456") + "', BraCode = '" + bracode + "', RoleId = '" + roleid + "', DateLastUpdated = '" + DateTime.Now + "', UserEmail = '" + email + "', TellerTillAccount = '" + tellertill + "', DepositLimit = '" + AdminUser.DepositLimit + "', OpsHeadRoleId = '" + AdminUser.OpsHeadRoleId + "', TellerRoleId = '" + AdminUser.TellerRoleId + "', DepositITRoleId = '" + AdminUser.DepositITRoleId + "', DepositExpl_Code = '" + AdminUser.DepositExpl_Code + "', AdminRoleId = '" + AdminUser.AdminRoleId + "', BasisIP = '" + AdminUser.BasisIP + "', BasisPort = '" + AdminUser.BasisPort + "', PinPadDepositLimit = '" + AdminUser.PinPadDepositLimit + "', Branchname = '" + AdminUser.branchname + "' WHERE [TellerId]= '" + tellerid + "'";
                        accesscom.CommandType = CommandType.Text;

                        if (Con.State == ConnectionState.Closed)
                        {
                            Con.Open();
                        }

                        result = accesscom.ExecuteScalar();

                        return Convert.ToUInt64(result);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return 0;
            }
        }

        public string[] getCustomerID(string cardNumber)
        {
            string[] response = new string[2];

            try
            {
                AppDevService appserve = new AppDevService();
                string encryptedstring = Secure.EncryptString(cardNumber);
                response = appserve.getCustomerID(encryptedstring);

                return response;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                response = null;
                return response;
            }
        }

        public void ToggleAuthMode()
        {
            AdminUser.LoginMode = "Online";
        }

        public bool verifyOldAccountFormat(string oldaccountnumber)
        {
            string[] accounts = null;
            accounts = oldaccountnumber.Split('/');

            if (accounts.Length == 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool verifyNubanCheckDigit(string bankcode, string NubanAccountNumber)
        {
            NubanAccountNumber = bankcode + NubanAccountNumber.Trim();

            if (NubanAccountNumber.Length != 13)
            {
                return false;
            }
            string NubanAcct = NubanAccountNumber.Substring(0, 12);
            int CheckDigitFromAcctNumber = Convert.ToInt16(NubanAccountNumber.Substring(12, 1));

            string MagicNumber = "373373373373";
            int TotalValue = 0;
            int calculatedCheckDigit = 0;

            for (int i = 0; i < 12; i++)
            {
                TotalValue = TotalValue + (Convert.ToInt16(MagicNumber.Substring(i, 1)) * Convert.ToInt16(NubanAcct.Substring(i, 1)));
            }
            if ((TotalValue % 10) == 0)
            {
                calculatedCheckDigit = 0;
            }
            else
            {
                calculatedCheckDigit = 10 - (TotalValue % 10);
            }

            if (CheckDigitFromAcctNumber == calculatedCheckDigit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string getcurcode(string oldaccountnumber)
        {
            try
            {
                string[] accounts = null;
                accounts = oldaccountnumber.Split('/');
                string curcode = "";
                curcode = accounts[2].ToString();
                return curcode;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return "1";
            }
        }

        public int GenerateRandomNumber(int length)
        {
            int finalRandomNumber = 0;

            string randomNumber = string.Empty;

            try
            {
                for (int i = 0; i < length; i++)
                {
                    randomNumber = string.Concat(randomNumber, random.Next(10).ToString());
                }

                finalRandomNumber = Convert.ToInt32(randomNumber);
                return finalRandomNumber;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError("An exception occurred while trying to generate random numbers. Message - " + ex.Message + "|Stacktrace - " + ex.StackTrace);
                return finalRandomNumber;
            }
        }

        public string[] GetCustomerDetailsUsingPhoneNumber(string customerPhoneNumber, string amount)
        {
            string[] customerDetails = new string[6];

            string responseCode = string.Empty;

            try
            {
                builder.Append("<FetchFundedAccountDetails><PhoneNumber>" + customerPhoneNumber + "</PhoneNumber><Amount> " + amount + "</Amount></FetchFundedAccountDetails>");

                string xmlRequest = builder.ToString();
              
                string response = appDev.GetNubanAccountWithFunds(xmlRequest);

                if (!string.IsNullOrEmpty(response))
                {
                    xmlDocument.LoadXml(response);

                    responseCode = xmlDocument.SelectSingleNode("Response/CODE").InnerXml;

                    if (responseCode == "1000")
                    {
                        customerDetails.SetValue("Successful", 0);
                        customerDetails.SetValue(xmlDocument.SelectSingleNode("Response/NUBAN").InnerXml, 1);
                        customerDetails.SetValue(xmlDocument.SelectSingleNode("Response/BVN").InnerXml, 2);
                        customerDetails.SetValue(xmlDocument.SelectSingleNode("Response/OLDACCOUNTNUMBER").InnerXml, 3);
                        customerDetails.SetValue(xmlDocument.SelectSingleNode("Response/CUSTOMERNAME").InnerXml, 4);
                        customerDetails.SetValue(xmlDocument.SelectSingleNode("Response/AVAILABLEBALANCE").InnerXml, 5);
                        builder.Length = 0;

                        return customerDetails;
                    }
                    else if(responseCode == "1002")
                    {
                        ErrHandler.WriteError("Customer's phone number is not attached to any account. MobileNumber - " + customerPhoneNumber);

                        customerDetails.SetValue("Phone number does not exist", 0);
                        builder.Length = 0;

                        return customerDetails;
                    }
                    else if(responseCode == "1003")
                    {
                        customerDetails.SetValue("No account is funded!", 0);
                        builder.Length = 0;
                        return customerDetails;
                    }
                    else
                    {
                        customerDetails.SetValue("Could not get details!", 0);
                        builder.Length = 0;
                        return customerDetails;
                    }
                }
                else
                {
                    ErrHandler.WriteError("Could not get customer's details from GetNubanAccountWithFunds. Response - " + response);
                    customerDetails.SetValue("Could not get details!", 0);
                    builder.Length = 0;
                    return customerDetails;
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError("An exception occurred in GetCustomerDetailsUsingPhoneNumber. Message - " + ex.Message + "|StackTrace - " + ex.StackTrace);
                customerDetails = null;
                builder.Length = 0;
                return customerDetails;
            }
        }

        public void PopulateOtherCustomerDetails(int branchCode, int customerNumber, TextBox name, Label customerLvl, ListBox customerAccounts, RasterImageList imageList)
        {
            string responseCode = string.Empty;
            string cusmandate = string.Empty;
            string[] accounts = null;
            string[] mandates = null;

            byte[] imagedata = new byte[8056];
            object[] Images = null;
            int noofimages = 0;

            try
            {
                custdetails = appDev.GetBasisCustomerDetails(branchCode, customerNumber);

                var document = new XmlDocument();
                document.LoadXml(custdetails.custdet.Replace("&", " and "));

                responseCode = document.SelectSingleNode("Response/CODE").InnerXml;

                if (responseCode != "1000")
                {

                }

                accounts = custdetails.Accounts;
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
                    customerAccounts.Items.Add("No Suitable Account For Transactions");
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
                            RasterImageListItem a = new RasterImageListItem
                            {
                                Image = codec.Load(myImage),
                                Text = mandates[i]
                            };

                            imageList.Items.Add(a);
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

                string customerName = document.SelectSingleNode("Response/CUSTOMERS/CUSTOMER/SIGNATORY_NAME").InnerXml;
                name.Text = customerName;

                string customerLevel = document.SelectSingleNode("Response/CUSTOMERS/CUSTOMER/CUST_TYPE").InnerXml;
                customerLvl.Text = "LEVEL " + customerLevel;

                AdminUser.CustomerLevel = customerLevel;
                AdminUser.CusName = customerName;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError("An exception occurred in PopulateOtherCustomerDetails. Message - " + ex.Message + "|Stacktrace - " + ex.StackTrace);
            }
        }

        public string[] UnlockWithSocket(string TerminalID, string TerminalSerial)
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
                    ErrHandler.WriteError("An exception occurred in UnlockWithSocket method. Message - " + ex.Message + "|StackTrace - " + ex.StackTrace);

                    string message = " Cannot Unlock Terminal Connected to network but cannot open Port";

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
                ErrHandler.WriteError("An exception occurred in UnlockWithSocket method. Message - " + ex.Message + "|StackTrace - " + ex.StackTrace);
                string message = "AN ERROR OCCURRED! ";

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

        public FingerPrintVerificationRequest GetRequestForNibssApi(string nuban, string bvn, string referenceId, string deviceId)
        {
            FingerPrintVerificationRequest request = new FingerPrintVerificationRequest();
            FingerPrintModel model = new FingerPrintModel();
            FingerImage fingerImage = new FingerImage();

            request.Nuban = nuban;
            model.BVN = bvn;
            model.DeviceId = deviceId;
            model.ReferenceNumber = referenceId;
            fingerImage.nist_impression_type = "0";
            fingerImage.position = "RT";
            fingerImage.type = "ISO_2005";
            fingerImage.value = AdminUser.FingerPrintImage;
            request.FingerPrint = model;
            request.FingerPrint.FingerImage = fingerImage;
            return request;
        }
    }
}