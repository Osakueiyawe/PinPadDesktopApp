using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.PointOfService;

namespace GTBankCardInterfaceApp
{
    public static class AdminUser
    {
        public static string username = "";
        public static string email = "";
        public static string branchcode = "";
        public static string branchname = "";
        public static string roleid = "";
        public static string tillaccount = "";
        public static string LoginMode = "";
        public static string CusDepositAccount = "";
        public static string CusWithdrawalAccount = "";
        public static string TellerId = "";
        public static string DomainId = "";
        public static string CusName = "";
        public static double TransAmount = 0.0;
        public static string Transtype = "";
        public static bool IsOverAuthlimit = false;
        public static bool webserviceavailable = false;
        public static byte[] HeaderImage = null;
        public static byte[] FooterImage = null;
        public static string charge = "";
        public static string chargeslimit = "";
        public static string chargeaccount = "";
        public static string DepositLimit = "";
        public static string OpsHeadRoleId = "";
        public static string TellerRoleId = "";
        public static string DepositITRoleId = "";
        public static string WithdrwalExpl_Code = "";
        public static string DepositExpl_Code = "";
        public static string AdminRoleId = "";
        public static string BasisIP = "";
        public static string BasisPort = "";
        public static string SVAccountNumber = "";
        public static string AnalyisAmount = "0";
        public static string AnalysedAmount = "0";
        public static string CashAnalysis = "";
        public static string PinPadDepositLimit = "0";
        public static string PinPadWithdrawalLimit = "0";
        public static string VATAccount = "0";
        public static string ThirdCurLimit  = "0";

        public static string PinPadCon = "";
        public static string gapsCon = "";
        public static string BankCardCon = "";
        public static string ApplicationID = "";
        public static string HostID = "";
        public static string PowerCardBIN = "";
        public static string AllowedVersion = "";
        public static string LoginErrorcode = "";
        public static string LoginErrorMsg = "";
        public static string CustomerLevel = "";

        public static string AvailableBalance = "0";
        public static string Level1TransactionLimit = "0";
        public static string Level2TransactionLimit = "0";
        public static string Level1BalanceLimit = "0";
        public static string Level2BalanceLimit = "0";
        public static string IsCardLessAllowed = "0";

        public static string TerminalID = string.Empty;
        public static string TerminalSerial = string.Empty;
        
        public static string PinPadType = string.Empty;
        public static string TwigWebsrviceUrl = string.Empty;
        public static string Transid = string.Empty;

        public static string CustomerPhoneNumber = string.Empty;
        public static string FingerPrintImage = string.Empty;
    }
}
