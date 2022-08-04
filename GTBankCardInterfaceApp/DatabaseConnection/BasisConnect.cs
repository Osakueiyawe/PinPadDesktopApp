using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTBankCardInterfaceApp.DatabaseConnection
{
    class BasisConnect
    {
        public DataTable GetAccountdetailsfromPhone(string phonenumber)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Select");
            dt.Columns.Add("TransType");
            dt.Columns.Add("RequestDate");
            try
            {
                var query = "select a.mob_num AS \"MobileNumber\", a.bra_code AS \"BranchCode\", a.cus_num AS \"CustomerNo\", b.map_acc_no AS \"BenAcctNo\" from midwareusr.address_ussd a, banksys.map_acct b where a.bra_code = b.bra_code and a.cus_num = b.cus_num and a.mob_num = :phoneNumber";
                OracleConnection OraConn = new OracleConnection(ConfigurationManager.AppSettings["BasisCon"]);                
                OracleCommand oracomm = new OracleCommand();
                OraConn.Open();
                oracomm.Connection = OraConn;
                oracomm.CommandType = CommandType.Text;
                oracomm.CommandText = query;                
                oracomm.Parameters.Add(":phoneNumber", phonenumber);
                OracleDataReader oradrread = oracomm.ExecuteReader(CommandBehavior.CloseConnection);                
                dt.Load(oradrread);
               
                foreach (DataRow dr in dt.Rows)
                {
                    dr["Select"] = "Select";
                    dr["TransType"] = "DEPOSIT";
                    dr["RequestDate"] = DateTime.Now.ToString();
                }

            }
            catch(Exception ex)
            {

            }
            return dt;
        }
    }
}
