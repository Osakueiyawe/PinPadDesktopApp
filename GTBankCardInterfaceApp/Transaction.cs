using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Xml.XPath;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Data.OleDb;
using GTBSecure;
using Microsoft.PointOfService;

namespace GTBankCardInterfaceApp
{
    class Transaction
    {
        Utilities util = new Utilities();
        ConvertToNaira AmtInNaira = new ConvertToNaira();
        const int MAX_LINE_WIDTHS = 2;
        string PrinterName = "";
        /// <summary>
        /// PosPrinter object.
        /// </summary>
        PosPrinter m_Printer = null;
        public static DateTime postdate = DateTime.Now;
        SqlConnection PinPadCon = new SqlConnection(AdminUser.PinPadCon);
        SqlConnection PinPadSeqConn = new SqlConnection(AdminUser.PinPadCon);
        SqlConnection PinPadConOffline = new SqlConnection(ConfigurationManager.AppSettings["PinPadConOffline"]);

        public bool InitiateTransactionAccessDB(string OrigbraCode, string tellerId, string customerAccountNo, decimal amount, string authmode, string transType, string cusName, string tellertill, string depname, string datetime)// third party deposits Offline Transactions
        {
            object result = 0;

            try
            {
                using (OleDbConnection Con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\PinPad.gtb"))
                {
                    using (OleDbCommand accesscom = new OleDbCommand())
                    {
                        accesscom.Connection = Con;
                        accesscom.CommandText = "INSERT INTO dbo_Transactions ( CustomerNo, Transtype, OriginatingTellerId, AuthenticationMode, TransAmount, OriginatingBraCode, CustomerName, TransDate, TellerTillAccount, DepositorName ) VALUES " +
                            "('" + customerAccountNo + "','" + transType + "','" + tellerId + "','" + authmode + "','" + amount + "','" + OrigbraCode + "','" + cusName + "','" + datetime + "','" + tellertill + "','" + depname + "')";
                        accesscom.CommandType = CommandType.Text;

                        if (Con.State == ConnectionState.Closed)
                        {
                            Con.Open();
                        }

                        result = accesscom.ExecuteScalar();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return false;
            }
        }

        public ulong getTransIdOfflineAccessDB(string customerAccountNo, double amount, string depname, string datetime)
        {

            try
            {
                using (OleDbConnection Con2 = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\PinPad.gtb"))
                {

                    using (OleDbCommand accesscom2 = new OleDbCommand())
                    {
                        accesscom2.Connection = Con2;
                        accesscom2.CommandType = CommandType.Text;
                        accesscom2.CommandText = "SELECT TransactionID FROM dbo_Transactions where CustomerNo = '" + customerAccountNo + "' and Transamount = " + amount + " and DepositorName = '" + depname + "' and TransDate = '" + datetime + "'";

                        if (Con2.State == ConnectionState.Closed)
                        {
                            Con2.Open();
                        }

                        using (OleDbDataReader result = accesscom2.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (result.Read())
                            {
                                ulong transid = Convert.ToUInt64(result["TransactionID"]);
                                return transid;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return 0;
            }
        }

        public ulong InitiateTransaction(string OrigbraCode, string tellerId, string customerAccountNo, 
            decimal amount, string authmode, string transType, string cusName, string tellertill, 
            string depname, string CardNumber, uint Stan, string MerchantID, string AuthCode, 
            decimal AuthAmount, string cashanalysis) // Online Transactions
        {
            string maskedPAN = CardNumber;

            if (!string.IsNullOrEmpty(CardNumber))
            {
                if (!CardNumber.Equals("CardLess"))
                {
                    maskedPAN = "XXXXXXXXXXXX" + CardNumber.Substring(12, 4); //PCIDSS truncation allows only last four digits be stored
                }
            }
            else
            {
                maskedPAN = "No PAN Supplied.";
            }

            ulong result = 0;

            try
            {
                AppDevService appserve = new AppDevService();
                result = appserve.InitiateTransaction(OrigbraCode, tellerId, customerAccountNo, amount, authmode, transType, cusName, 
                    tellertill, depname, maskedPAN, Stan, MerchantID, AuthCode, AuthAmount, cashanalysis); // Online Transactions
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return 0;
            }
        }

        public string PostToBasis(string CustAccount_AcctFrom, string TellerTill_AcctTo, double amount, int explcode, string remarks, string transtype, string tellerid, string bracode, string id = "-1")
        {
            try
            {
                int appid = Convert.ToInt16(AdminUser.ApplicationID);
                AppDevService appserve = new AppDevService();
                string eoneRetVal = null;
                XmlDocument document = null;
                XPathNavigator navigator = null;
                XPathNodeIterator snodes = null;
                string retcode = null;

                if (transtype.ToUpper().Equals("DEPOSIT") | transtype.ToUpper().Equals("DEPOSIT-M")) // Switch account to debit and account to credit; Debit Till Credit Cus account
                {
                    string serviceid = "";
                    if (remarks.Contains("3RD PARTY"))
                    {
                        serviceid = "3";
                    }
                    else
                    {
                        serviceid = "2";
                    }

                    string param = TellerTill_AcctTo + "|" + CustAccount_AcctFrom + "|" + amount.ToString() + "|" + explcode.ToString() + "|" + remarks + "|" + AdminUser.roleid + "|" + transtype + "|" + serviceid + "|" + id;
                    eoneRetVal = appserve.Transfer(Secure.EncryptString(param));
                }
                else if (transtype.ToUpper().Equals("WITHDRAWAL") | transtype.ToUpper().Equals("WITHDRAWAL-M")) // Default Implementation (Debit Cus account credit Teller Till
                {
                    string serviceid = "1"; 
                    string param = CustAccount_AcctFrom + "|" + TellerTill_AcctTo + "|" + amount.ToString() + "|" + explcode.ToString() + "|" + remarks + "|" + AdminUser.roleid + "|" + transtype + "|" + serviceid + "|" + id;
                    eoneRetVal = appserve.Transfer(Secure.EncryptString(param));
                }
                else
                {
                    return "INVALID TRANSACTION TYPE.";
                }

                document = new XmlDocument();
                document.LoadXml(eoneRetVal);
                navigator = document.CreateNavigator();
                snodes = navigator.Select("/Response/CODE");
                snodes.MoveNext();
                retcode = snodes.Current.Value;

                if (retcode.Equals("1000"))
                {
                    snodes = navigator.Select("/Response/MESSAGE");
                    snodes.MoveNext();
                    return "0"; // succesful
                }

                else if (retcode.Equals("1010"))
                {
                    snodes = navigator.Select("/Response/MESSAGE");
                    snodes.MoveNext();
                    return "1";
                }
                else
                {
                    snodes = navigator.Select("/Response/ERROR");
                    snodes.MoveNext();
                    return snodes.Current.Value;
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return ex.Message;
            }
        }

        public string PostChargesToBasis(string CustAccount_AcctFrom, string CommAccount, string VATAccount, double amount, int explcode, string remarks, string transtype, string tellerid, string bracode)
        {
            try
            {
                AppDevService appserve = new AppDevService();
                string eoneRetVal = null;
                XmlDocument document = null;
                XPathNavigator navigator = null;
                XPathNodeIterator snodes = null;
                string retcode = null;

                string param = CustAccount_AcctFrom + "|" + CommAccount + "|" + VATAccount + "|" + amount.ToString() + "|" + explcode.ToString() + "|" + remarks;
                eoneRetVal = appserve.TransferCharges(Secure.EncryptString(param));

                document = new XmlDocument();
                document.LoadXml(eoneRetVal);
                navigator = document.CreateNavigator();
                snodes = navigator.Select("/Response/CODE");
                snodes.MoveNext();
                retcode = snodes.Current.Value;


                if (retcode.Equals("1000"))
                {
                    snodes = navigator.Select("/Response/MESSAGE");
                    snodes.MoveNext();
                    return "0";
                }

                else if (retcode.Equals("1005"))
                {
                    snodes = navigator.Select("/Response/MESSAGE");
                    snodes.MoveNext();
                    return "1";
                }
                else
                {
                    snodes = navigator.Select("/Response/ERROR");
                    snodes.MoveNext();
                    return snodes.Current.Value;
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return ex.Message;
            }
        }

        public ulong UpdateTransaction(string apprvTeller, ulong transId, string bracode, string acctno, string rmks, string status, string failreason, string authmode, string tellertill, string expl_code)
        {
            try
            {
                ulong result = 0;
                AppDevService appdevserve = new AppDevService();
                result = appdevserve.UpdateTransaction(apprvTeller, transId, bracode, acctno, rmks, status, failreason, authmode, tellertill, expl_code);
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return 0;
            }
        }

        public ulong UpdateTransactionForApproval(ulong transId)
        {
            try
            {
                ulong result = 0;
                AppDevService appserve = new AppDevService();
                result = appserve.UpdateTransactionForApproval(transId);
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return 0;
            }
        }

        public ulong UpdateTransactionForReceiptReprint(ulong transId)
        {
            try
            {
                ulong result = 0;
                AppDevService appserve = new AppDevService();
                result = appserve.UpdateTransactionForReceiptReprint(transId);
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return 0;
            }
        }

        public bool UpdateTransactionAccessDB(string apprvTeller, ulong transId, string bracode, string acctno, string rmks, string status, string failreason, string authmode)
        {
            try
            {
                AppDevService appdevserv = new AppDevService();
                string basisSequence = "OFFLINE";

                object result = 0;

                using (OleDbConnection Con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\PinPad.gtb"))

                {

                    using (OleDbCommand accesscom = new OleDbCommand())
                    {
                        accesscom.Connection = Con;
                        accesscom.CommandText = "UPDATE dbo_Transactions SET TransactionStatus = '" + status + "', ApprovingTellerId = '" + apprvTeller + "' , BasisTransSequence = '" + basisSequence + "', FailReason = '" + failreason + "' WHERE TransactionID=  " + transId + "";
                        accesscom.CommandType = CommandType.Text;

                        if (Con.State == ConnectionState.Closed)
                        {
                            Con.Open();
                        }

                        result = accesscom.ExecuteScalar();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return false;
            }
        }

        public bool PrintReceipt(string transid, string transtype, string slipnum, string accountno, string acctname, double amount, string depositorsname, string branchname, string curcode) //
        {
            if (IntialisePrinter()) // pos printer is available and online 
            {
                ErrHandler.WriteError("Pos dll is working");
                string currency = "";
                string subcurrency = "";
                string curshortcode = "";

                if (curcode.Equals("1"))
                {
                    currency = "Naira";
                    subcurrency = "Kobo";
                    curshortcode = "NGN";
                }
                else if (curcode.Equals("2"))
                {
                    currency = "Dollar";
                    subcurrency = "Cent";
                    curshortcode = "USD";
                }
                else if (curcode.Equals("3"))
                {
                    currency = "Pounds";
                    subcurrency = "Shillings";
                    curshortcode = "GBP";
                }
                else if (curcode.Equals("46"))
                {
                    currency = "Euro";
                    subcurrency = "Cents";
                    curshortcode = "EUR";
                }

                DialogResult dialogResult;
                bool bBuffering = true;
                DateTime nowDate = DateTime.Now;							//System date
                DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();	//Date Format
                dateFormat.MonthDayPattern = "MM";
                string strDate = nowDate.ToString("dd-MM-yyyy  HH:mm:ss", dateFormat);

                int[] RecLineChars = new int[MAX_LINE_WIDTHS] { 0, 0 };
                long lRecLineCharsCount;

                Cursor.Current = Cursors.WaitCursor;

                if (m_Printer.CapRecPresent == true)
                {
                    while (true)
                    {
                        try
                        {
                            m_Printer.TransactionPrint(PrinterStation.Receipt
                                , PrinterTransactionControl.Transaction);
                            PrintHeaderFooter(0);

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                               + "------------------------------------------" + "\n");
                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                                + paddstring("BRANCH NAME", 15) + ":" + branchname.ToUpper() + "\n");
                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                              + "------------------------------------------" + "\n");

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|500uF");

                            string strPrintData = "";

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                                + paddstring("TRAN ID", 15) + ":" + transid.ToUpper() + "\n");

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("TRAN DATE", 15) + ":" + strDate.ToUpper() + "\n");

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("PRINT DATE", 15) + ":" + strDate.ToUpper() + "\n");
                            //m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                            // + paddstring("TRAN ID",15)+   ":" + transid.ToUpper() + "\n");
                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("TRAN TYPE", 15) + ":" + transtype.ToUpper() + "\n");
                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("SLIP NO", 15) + ":" + slipnum.ToUpper() + "\n");

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("ACCOUNT NO", 15) + ":" + accountno.ToUpper() + "\n");

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("ACCOUNT NAME", 15) + ":" + acctname.ToUpper() + "\n");
                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("AMOUNT", 15) + ":" + curshortcode + util.FormatString((amount.ToString("F2")), 2) + "\n");
                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("AMT IN WORDS", 15) + ":" + AmtInNaira.Num2Naira(amount.ToString(), currency, subcurrency) + "\n");

                            if (transtype.Equals("DEPOSIT"))
                            {
                                m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                                 + paddstring("DEPOSITOR", 15) + ":" + depositorsname.ToUpper() + "\n");
                            }

                            m_Printer.PrintNormal(PrinterStation.Receipt, strPrintData + "\n");
                            PrintHeaderFooter(1);
                            //Make 5mm speces
                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|500uF");

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|fP");


                            m_Printer.TransactionPrint(PrinterStation.Receipt
                             , PrinterTransactionControl.Normal);

                            m_Printer.TransactionPrint(PrinterStation.Receipt
                                , PrinterTransactionControl.Transaction);
                            PrintHeaderFooter(0);

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                               + "------------------------------------------" + "\n");
                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                                + paddstring("BRANCH NAME", 15) + ":" + branchname.ToUpper() + "\n");
                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                              + "------------------------------------------" + "\n");

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|500uF");

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                                + paddstring("TRAN ID", 15) + ":" + transid.ToUpper() + "\n");

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("TRAN DATE", 15) + ":" + strDate.ToUpper() + "\n");

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("PRINT DATE", 15) + ":" + strDate.ToUpper() + "\n");
                            //m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                            // + paddstring("TRAN ID",15)+   ":" + transid.ToUpper() + "\n");
                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("TRAN TYPE", 15) + ":" + transtype.ToUpper() + "\n");
                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("SLIP NO", 15) + ":" + slipnum.ToUpper() + "\n");

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("ACCOUNT NO", 15) + ":" + accountno.ToUpper() + "\n");

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("ACCOUNT NAME", 15) + ":" + acctname.ToUpper() + "\n");
                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("AMOUNT", 15) + ":" + curshortcode + util.FormatString((amount.ToString("F2")), 2) + "\n");
                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                             + paddstring("AMT IN WORDS", 15) + ":" + AmtInNaira.Num2Naira(amount.ToString(), currency, subcurrency) + "\n");

                            if (transtype.Equals("DEPOSIT"))
                            {
                                m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|N"
                                 + paddstring("DEPOSITOR", 15) + ":" + depositorsname.ToUpper() + "\n");
                            }

                            m_Printer.PrintNormal(PrinterStation.Receipt, strPrintData + "\n");
                            PrintHeaderFooter(1);

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|500uF");

                            m_Printer.PrintNormal(PrinterStation.Receipt, "\u001b|fP");
 
                            m_Printer.TransactionPrint(PrinterStation.Receipt
                             , PrinterTransactionControl.Normal);

                            m_Printer.Release();
                            return true;
                        }
                        catch (PosControlException ex)
                        {
                            if (ex.ErrorCode == ErrorCode.Illegal && ex.ErrorCodeExtended == 1004)
                            {
                                MessageBox.Show("Unable to print receipt.\n" + GetErrorCode(ex)
                                    , "GTBankCardInterfaceApp", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                m_Printer.ClearOutput();
                                bBuffering = false;
                                m_Printer.Release();
                                return false;
                            }

                            dialogResult = MessageBox.Show("Unable to print receipt.\n" + GetErrorCode(ex) + "\n\nRetry?"
                                , "GTBankCardInterfaceApp", MessageBoxButtons.AbortRetryIgnore);

                            try
                            {
                                m_Printer.ClearOutput();
                            }
                            catch (PosControlException)
                            {
                                m_Printer.Release();
                                return false;
                            }

                            if (dialogResult == DialogResult.Abort || dialogResult == DialogResult.Ignore)
                            {
                                m_Printer.Release();
                                return false;
                            }

                            continue;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (PrintSlipReceipt(transid, transtype, slipnum, accountno, acctname, amount, depositorsname, branchname, curcode)) // Print to Regular Printer
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ulong CreateLocalUser(string tellerid, string username, string userpass, decimal bracode, int roleid, string email, string tellertill)
        {
            object result = 0;

            try
            {
                using (SqlCommand sqlcomm = new SqlCommand())
                {
                    using (SqlConnection PinPadConOffline = new SqlConnection(ConfigurationManager.AppSettings["PinPadConOffline"]))
                    {
                        sqlcomm.Connection = PinPadConOffline;
                        sqlcomm.Parameters.Add("@TellerId", SqlDbType.NChar).Value = tellerid;
                        sqlcomm.Parameters.Add("@UserName", SqlDbType.NChar).Value = username;
                        sqlcomm.Parameters.Add("@UserPass", SqlDbType.NChar).Value = userpass;
                        sqlcomm.Parameters.Add("@BraCode", SqlDbType.Int).Value = bracode;
                        sqlcomm.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleid;
                        sqlcomm.Parameters.Add("@DateLastUpdated", SqlDbType.DateTime).Value = email;
                        sqlcomm.Parameters.Add("@UserEmail", SqlDbType.NChar).Value = email;
                        sqlcomm.Parameters.Add("@TellerTillAcct", SqlDbType.NChar).Value = tellertill;
                        sqlcomm.CommandText = "usp_LocalUserInsert";
                        sqlcomm.CommandType = CommandType.StoredProcedure;

                        if (PinPadConOffline.State == ConnectionState.Closed)
                        {
                            PinPadConOffline.Open();
                        }

                        result = sqlcomm.ExecuteScalar();

                        return Convert.ToUInt64(result);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return 0;
            }
            finally
            {
                if (PinPadConOffline.State == ConnectionState.Open)
                {
                    PinPadConOffline.Close();
                }
            }
        }

        public bool checkIfLocalUserExists(string username)
        {
            try
            {
                using (SqlCommand sqlcomm = new SqlCommand())
                {
                    using (SqlConnection PinPadConOffline = new SqlConnection(ConfigurationManager.AppSettings["PinPadConOffline"]))
                    {
                        sqlcomm.Connection = PinPadConOffline;

                        sqlcomm.Parameters.Add("@username", SqlDbType.NChar).Value = username;
                        sqlcomm.CommandText = "usp_LocalUserSelect";
                        sqlcomm.CommandType = CommandType.StoredProcedure;

                        if (PinPadConOffline.State == ConnectionState.Closed)
                        {
                            PinPadConOffline.Open();
                        }

                        using (SqlDataReader result = sqlcomm.ExecuteReader(CommandBehavior.CloseConnection))
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
            finally
            {
                if (PinPadConOffline.State == ConnectionState.Open)
                {
                    PinPadConOffline.Close();
                }
            }
        }

        public bool AuthenticateLocalUser(string email, string username)
        {
            try
            {
                using (SqlCommand sqlcomm = new SqlCommand())
                {
                    using (SqlConnection PinPadConOffline = new SqlConnection(ConfigurationManager.AppSettings["PinPadConOffline"]))
                    {
                        sqlcomm.Connection = PinPadConOffline;
                        sqlcomm.Parameters.Add("@username", SqlDbType.NChar).Value = username;
                        sqlcomm.Parameters.Add("@email", SqlDbType.NChar).Value = email;
                        sqlcomm.CommandText = "usp_LocalUserAuthenticate";
                        sqlcomm.CommandType = CommandType.StoredProcedure;
                        if (PinPadConOffline.State == ConnectionState.Closed)
                        {
                            PinPadConOffline.Open();
                        }

                        using (SqlDataReader result = sqlcomm.ExecuteReader(CommandBehavior.CloseConnection))
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
            finally
            {
                if (PinPadConOffline.State == ConnectionState.Open)
                {
                    PinPadConOffline.Close();
                }
            }
        }
        public ulong UpdateLocalUser(string tellerid, string username, string userpass, decimal bracode, int roleid, string email, string tellertill)
        {
            object result = 0;

            try
            {
                using (SqlCommand sqlcomm = new SqlCommand())
                {
                    using (SqlConnection PinPadConOffline = new SqlConnection(ConfigurationManager.AppSettings["PinPadConOffline"]))
                    {

                        sqlcomm.Connection = PinPadConOffline;
                        sqlcomm.Parameters.Add("@TellerId", SqlDbType.NChar).Value = tellerid;
                        sqlcomm.Parameters.Add("@UserName", SqlDbType.NChar).Value = username;
                        sqlcomm.Parameters.Add("@UserPass", SqlDbType.NChar).Value = userpass;
                        sqlcomm.Parameters.Add("@BraCode", SqlDbType.Int).Value = bracode;
                        sqlcomm.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleid;
                        sqlcomm.Parameters.Add("@DateLastUpdated", SqlDbType.DateTime).Value = email;
                        sqlcomm.Parameters.Add("@UserEmail", SqlDbType.NChar).Value = email;
                        sqlcomm.Parameters.Add("@TellerTillAcct", SqlDbType.NChar).Value = tellertill;
                        sqlcomm.CommandText = "usp_LocalUserUpdate";
                        sqlcomm.CommandType = CommandType.StoredProcedure;

                        if (PinPadConOffline.State == ConnectionState.Closed)
                        {
                            PinPadConOffline.Open();
                        }

                        result = sqlcomm.ExecuteScalar();

                        return Convert.ToUInt64(result);
                    }
                }
            }

            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return 0;
            }
            finally
            {
                if (PinPadConOffline.State == ConnectionState.Open)
                {
                    PinPadConOffline.Close();
                }
            }
        }

        public bool IntialisePrinter()
        {
            string strLogicalName = "PosPrinter";

            try
            {
                PosExplorer posExplorer = new PosExplorer();

                DeviceInfo deviceInfo = null;
                ErrHandler.WriteError("POS dll is working");
                try
                {
                    deviceInfo = posExplorer.GetDevice(DeviceType.PosPrinter, strLogicalName);
                    m_Printer = (PosPrinter)posExplorer.CreateInstance(deviceInfo);
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.ToString());
                    return false;
                }

                AddOutputComplete(m_Printer);

                m_Printer.Open();

                m_Printer.Claim(1000);

                m_Printer.DeviceEnabled = true;

                m_Printer.RecLetterQuality = true;

                m_Printer.MapMode = MapMode.Metric;

                return true;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return false;
            }
        }

        protected void AddOutputComplete(object eventSource)
        {
            EventInfo outputCompleteEvent = eventSource.GetType().GetEvent("OutputCompleteEvent");
            if (outputCompleteEvent != null)
            {
                outputCompleteEvent.AddEventHandler(eventSource,
                    new OutputCompleteEventHandler(OnOutputCompleteEvent));
            }
        }

        protected void OnOutputCompleteEvent(object source, OutputCompleteEventArgs e)
        {

        }

        private long GetRecLineChars(ref int[] RecLineChars)
        {
            long lRecLineChars = 0;
            long lCount;
            int i;

            lCount = m_Printer.RecLineCharsList.GetLength(0);

            if (lCount == 0)
            {
                lRecLineChars = 0;
            }
            else
            {
                if (lCount > MAX_LINE_WIDTHS)
                {
                    lCount = MAX_LINE_WIDTHS;
                }

                for (i = 0; i < lCount; i++)
                {
                    RecLineChars[i] = m_Printer.RecLineCharsList[i];
                }

                lRecLineChars = lCount;
            }

            return lRecLineChars;
        }

        private string GetErrorCode(PosControlException ex)
        {
            string strErrorCodeEx = "";

            switch (ex.ErrorCodeExtended)
            {
                case PosPrinter.ExtendedErrorCoverOpen:
                case PosPrinter.ExtendedErrorJournalEmpty:
                case PosPrinter.ExtendedErrorReceiptEmpty:
                case PosPrinter.ExtendedErrorSlipEmpty:
                    strErrorCodeEx = ex.Message;
                    break;
                default:
                    string strEC = ex.ErrorCode.ToString();
                    string strECE = ex.ErrorCodeExtended.ToString();
                    strErrorCodeEx = "ErrorCode =" + strEC + "\nErrorCodeExtended =" + strECE + "\n"
                        + ex.Message;
                    break;
            }

            return strErrorCodeEx;
        }

        public string paddstring(string val, int length)
        {
            if (val.Equals(null))
            {
                val = "";
            }
            while (val.Length < length)
            {
                val = val + ' ';
            }
            return val;
        }

        public bool UpdatePrintStatus(ulong transsequence, int printstatus)
        {
            try
            {
                bool result = false;
                AppDevService appserve = new AppDevService();
                result = appserve.UpdatePrintStatus(transsequence, printstatus);
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return false;
            }
        }

        public bool UpdatePrintStatusOffline(ulong transsequence, int printstatus)
        {
            try
            {
                object result = 0;

                using (OleDbConnection Con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\PinPad.gtb"))
                {
                    using (OleDbCommand accesscom = new OleDbCommand())
                    {
                        accesscom.Connection = Con;

                        accesscom.CommandText = "Update dbo_Transactions set PrintStatus = " + printstatus + " where TransactionID = " + transsequence;
                        accesscom.CommandType = CommandType.Text;

                        if (Con.State == ConnectionState.Closed)
                        {
                            Con.Open();
                        }
                        result = accesscom.ExecuteNonQuery();
                        return true;
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
                PinPadConOffline.Close();
            }
        }

        public string getTransactionAccount(string Accountcombined, int index)//index 0 returns nuban index 1 retuns old account
        {
            string[] accounts = new string[2];// 
            string nubanaccount = null;
            string oldaccount = null;
            accounts = Accountcombined.Split('-');
            nubanaccount = accounts[0];
            oldaccount = accounts[1];

            if (index == 0)
            {

                return nubanaccount;
            }
            else if (index == 1)
            {
                return oldaccount;

            }
            else { return ""; }
        }

        public void sweepLocalTransactionsAccessDB()
        {
            try
            {
                using (OleDbConnection Con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\PinPad.gtb"))
                {
                    using (OleDbCommand accesscom = new OleDbCommand())
                    {
                        accesscom.Connection = Con;
                        accesscom.CommandText = "SELECT [TransactionID], [CustomerNo], [Transtype], [OriginatingTellerId], [AuthenticationMode], [TransAmount], [ApprovingTellerID], [OriginatingBraCode], [TransactionStatus], [BasisTransSequence], [CustomerName], [TransDate], [TellerTillAccount], [FailReason], [PrintStatus], [DepositorName] FROM dbo_Transactions WHERE TransactionStatus='PENDING' And AuthenticationMode='Offline';";
                        accesscom.CommandType = CommandType.Text;

                        if (Con.State == ConnectionState.Closed)
                        {
                            Con.Open();
                        }

                        using (OleDbDataReader result = accesscom.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (result.Read())
                            {
                                int TransactionID = 0;
                                string CustomerNo = "";
                                string Transtype = "";
                                string OriginatingTellerId = "";
                                string AuthenticationMode = "";
                                double TransAmount = 0;
                                string ApprovingTellerID = "";
                                string OriginatingBraCode = "";
                                string TransactionStatus = "";
                                string BasisTransSequence = "";
                                string CustomerName = "";
                                DateTime TransDate = DateTime.Now;
                                string TellerTillAccount = "";
                                string FailReason = "";
                                bool PrintStatus = false;
                                string DepositorName = "";

                                if (result["TransactionID"] != DBNull.Value)
                                {
                                    TransactionID = Convert.ToUInt16(result["TransactionID"].ToString());
                                }

                                if (result["CustomerNo"] != DBNull.Value)
                                {
                                    CustomerNo = (String)result["CustomerNo"];
                                }
                                if (result["Transtype"] != DBNull.Value)
                                {
                                    Transtype = (String)result["Transtype"];
                                }
                                if (result["OriginatingTellerId"] != DBNull.Value)
                                {
                                    OriginatingTellerId = (String)result["OriginatingTellerId"];
                                }
                                if (result["AuthenticationMode"] != DBNull.Value)
                                {
                                    AuthenticationMode = (String)result["AuthenticationMode"];
                                }

                                if (result["TransAmount"] != DBNull.Value)
                                {
                                    TransAmount = Convert.ToDouble(result["TransAmount"].ToString());
                                }
                                if (result["ApprovingTellerID"] != DBNull.Value)
                                {
                                    ApprovingTellerID = (String)result["ApprovingTellerID"];
                                }
                                if (result["OriginatingBraCode"] != DBNull.Value)
                                {
                                    OriginatingBraCode = (String)result["OriginatingBraCode"];
                                }
                                if (result["TransactionStatus"] != DBNull.Value)
                                {
                                    TransactionStatus = (String)result["TransactionStatus"];
                                }
                                if (result["BasisTransSequence"] != DBNull.Value)
                                {
                                    BasisTransSequence = (String)result["BasisTransSequence"];
                                }
                                if (result["CustomerName"] != DBNull.Value)
                                {
                                    CustomerName = (String)result["CustomerName"];
                                }
                                if (result["TransDate"] != DBNull.Value)
                                {
                                    TransDate = Convert.ToDateTime(result["TransDate"]);
                                }
                                if (result["TellerTillAccount"] != DBNull.Value)
                                {
                                    TellerTillAccount = (String)result["TellerTillAccount"];
                                }
                                if (result["FailReason"] != DBNull.Value)
                                {
                                    FailReason = (String)result["FailReason"];
                                }
                                if (result["PrintStatus"] != DBNull.Value)
                                {
                                    PrintStatus = Convert.ToBoolean(result["PrintStatus"].ToString());
                                }
                                if (result["DepositorName"] != DBNull.Value)
                                {
                                    DepositorName = (String)result["DepositorName"];
                                }

                                if (InsertIntoCentralDB(TransactionID, CustomerNo, Transtype, OriginatingTellerId, AuthenticationMode, TransAmount, ApprovingTellerID, OriginatingBraCode, TransactionStatus, BasisTransSequence, CustomerName, TransDate, TellerTillAccount, FailReason, PrintStatus, DepositorName))
                                {
                                    DeleteLocalTransactionAccessDB(TransactionID);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return;
            }
        }

        public bool InsertIntoCentralDB(int TransactionID, string CustomerNo, string Transtype, string OriginatingTellerId, string AuthenticationMode, double TransAmount, string ApprovingTellerID, string OriginatingBraCode, string TransactionStatus, string BasisTransSequence, string CustomerName, DateTime TransDate, string TellerTillAccount, string FailReason, bool PrintStatus, string DepositorName)
        {
            try
            {
                bool result = false;
                AppDevService appserve = new AppDevService();
                result = appserve.InsertIntoCentralDB(TransactionID, CustomerNo, Transtype, OriginatingTellerId, AuthenticationMode, TransAmount, ApprovingTellerID, OriginatingBraCode, TransactionStatus, BasisTransSequence, CustomerName, TransDate, TellerTillAccount, FailReason, PrintStatus, DepositorName);
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return false;
            }
        }
        public bool DeleteLocalTransaction(int TransactionID)
        {
            using (SqlConnection PinPadConOffline = new SqlConnection(ConfigurationManager.AppSettings["PinPadConOffline"]))
            {
                try
                {
                    using (SqlCommand sqlcomm = new SqlCommand())
                    {
                        sqlcomm.Connection = PinPadConOffline;
                        sqlcomm.Parameters.Add("@TransactionID", SqlDbType.NChar).Value = TransactionID;
                        sqlcomm.CommandText = "usp_TransactionsDelete";
                        sqlcomm.CommandType = CommandType.StoredProcedure;

                        if (PinPadConOffline.State == ConnectionState.Closed)
                        {
                            PinPadConOffline.Open();
                        }
                        int result = sqlcomm.ExecuteNonQuery();

                        if (result > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.ToString());
                    if (PinPadConOffline.State == ConnectionState.Open) { PinPadConOffline.Close(); }
                    return false;
                }
                finally
                {
                    if (PinPadConOffline.State == ConnectionState.Open) { PinPadConOffline.Close(); }
                }
            }
        }

        public bool DeleteLocalTransactionAccessDB(int TransactionID)
        {
            try
            {
                using (OleDbConnection Con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\PinPad.gtb"))
                {
                    using (OleDbCommand accesscom = new OleDbCommand())
                    {
                        accesscom.Connection = Con;
                        accesscom.CommandText = "DELETE * FROM dbo_Transactions WHERE [TransactionID]= " + TransactionID + "";
                        accesscom.CommandType = CommandType.Text;

                        if (Con.State == ConnectionState.Closed)
                        {
                            Con.Open();
                        }

                        int result = accesscom.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return false;
            }          
        }

        public void PrintHeaderFooter(int location)
        {
            try
            {
                MemoryStream m = null;

                if (location == 0)
                {
                    m = new MemoryStream(AdminUser.HeaderImage);

                }
                else if (location == 1)
                {
                    m = new MemoryStream(AdminUser.FooterImage);
                }
  
                System.Drawing.Bitmap bitmap = null;
                bitmap = new System.Drawing.Bitmap(m, true);

                m_Printer.PrintMemoryBitmap(PrinterStation.Receipt, bitmap, (m_Printer.RecLineWidth), PosPrinter.PrinterBitmapCenter);
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
            }

        }

        public bool PrintSlipReceipt(string transid, string transtype, string slipnum, string accountno, string acctname, double amount, string depositorsname, string branchname, string curcode)
        {
            try
            {
                string currency = "";
                string subcurrency = "";
                string curshortcode = "";

                if (curcode.Equals("1"))
                {
                    currency = "Naira";
                    subcurrency = "Kobo";
                    curshortcode = "NGN";
                }
                else if (curcode.Equals("2"))
                {
                    currency = "Dollar";
                    subcurrency = "Cent";
                    curshortcode = "USD";
                }
                else if (curcode.Equals("3"))
                {
                    currency = "Pounds";
                    subcurrency = "Shillings";
                    curshortcode = "GBP";
                }
                else if (curcode.Equals("46"))
                {
                    currency = "Euro";
                    subcurrency = "Cents";
                    curshortcode = "EURO";
                }

                DateTime nowDate = DateTime.Now;							
                DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();
                dateFormat.MonthDayPattern = "MM";
                string strDate = nowDate.ToString("dd-MM-yyyy  HH:mm:ss", dateFormat);

                PrintDocument receiptFile = new PrintDocument();
                PrintDialog printerselect = new PrintDialog();

                CrystalDecisions.CrystalReports.Engine.ReportDocument chqReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                if (string.IsNullOrEmpty(PrinterName))
                {
                    DialogResult dr = printerselect.ShowDialog();

                    if (dr.Equals(DialogResult.OK))
                    {
                        PrinterName = receiptFile.PrinterSettings.PrinterName;
                    }
                    else
                    {
                        return false;
                    }
                }
                chqReport.Load(Application.StartupPath + "\\Receipt.rpt");

                chqReport.SetParameterValue("braName", branchname);
                chqReport.SetParameterValue("transID", transid);
                chqReport.SetParameterValue("transDate", strDate);
                chqReport.SetParameterValue("transType", transtype);
                chqReport.SetParameterValue("slpNo", slipnum);
                chqReport.SetParameterValue("acctNo", accountno);
                chqReport.SetParameterValue("acctName", acctname);
                chqReport.SetParameterValue("amount", util.FormatString((amount.ToString("F2")), 2));
                chqReport.SetParameterValue("amtInwords", AmtInNaira.Num2Naira(amount.ToString(), currency, subcurrency));

                if (transtype.Equals("DEPOSIT"))
                {
                    chqReport.SetParameterValue("depositor", "DEPOSITOR");
                    chqReport.SetParameterValue("depositorname", depositorsname.ToUpper());
                }
                else
                {
                    chqReport.SetParameterValue("depositor", "");
                    chqReport.SetParameterValue("depositorname", "");
                }
                chqReport.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
                chqReport.PrintOptions.PrinterName = PrinterName;

                chqReport.PrintToPrinter(1, false, 1, 1);

                return true;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return false;
            }
        }

        public bool IsInProgress(ulong transid)
        {
            try
            {
                bool result = false;
                AppDevService appserve = new AppDevService();
                result = appserve.IsInProgress(transid);
                return result;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return false;
            }
        }
    }
}