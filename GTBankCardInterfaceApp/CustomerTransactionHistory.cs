using System;
using System.Data;
using System.Windows.Forms;
using System.Globalization;

namespace GTBankCardInterfaceApp
{
    public partial class CustomerTransactionHistory : Form
    {
        public CustomerTransactionHistory()
        {
            InitializeComponent();
        }

        private void CustomerTransactionHistory_Load(object sender, EventArgs e)
        {
            Text = Text + " --" + AdminUser.SVAccountNumber;
        }
    
        private void btnHistory_Click(object sender, EventArgs e)
        {
            DataTable history = new DataTable();
            AppDevService appserve = new AppDevService();
            DateTime fromdate = Convert.ToDateTime(dtmFrom.Text);
            DateTime todate = Convert.ToDateTime(dtmTo.Text);
            string startDate = fromdate.Date.ToString("ddMMMMyyyy", CultureInfo.CreateSpecificCulture("en-GB"));
            string cusnumber = "";
            string endDate = todate.Date.ToString("ddMMMMyyyy", CultureInfo.CreateSpecificCulture("en-GB"));

            if (AdminUser.SVAccountNumber.Trim().Length == 10)
            {
                 cusnumber = appserve.ConvertToOldAccountNumber(AdminUser.SVAccountNumber.Trim());
            }
            else
            {
                 cusnumber = AdminUser.SVAccountNumber;
            }
            string[] accounts = new string[5];
            accounts = cusnumber.Split('/');

            history = appserve.getTransactionHistory(accounts[0], accounts[1], accounts[2], accounts[3], accounts[4], startDate, endDate);

            if (history.Rows.Count > 0)
            {
                bdsApproval.DataSource = history;

                grdTransHistory.DataSource = bdsApproval;
                grdTransHistory.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                grdTransHistory.Refresh();
                grdTransHistory.ReadOnly = false;
            }
            else
            {
                MessageBox.Show("No Account activity in the specified period...");
                return;
            }
        }
    }
}