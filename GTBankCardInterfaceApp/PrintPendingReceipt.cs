using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace GTBankCardInterfaceApp
{
    public partial class PrintPendingReceipt : Form
    {
        Transaction Trans = new Transaction();
        Utilities util = new Utilities();
        public PrintPendingReceipt()
        {
            InitializeComponent();
        }
        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvApproval.RowCount; i++)
            {
                dgvApproval[0, i].Value = ((CheckBox)dgvApproval.Controls.Find("checkboxHeader", true)[0]).Checked;
            }

            dgvApproval.EndEdit();
        }
        private void PrintPendingReceipt_Load(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.Width = 30;
            checkboxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvApproval.Columns.Insert(0, checkboxColumn);
            // add checkbox header
            Rectangle rect = dgvApproval.GetCellDisplayRectangle(0, -1, true);
            // set checkbox header to center of header cell. +1 pixel to position correctly.
            rect.X = rect.Location.X + (rect.Width / 4);
            CheckBox checkboxHeader = new CheckBox();
            checkboxHeader.Name = "checkboxHeader";
            checkboxHeader.Size = new Size(18, 18);
            checkboxHeader.Location = rect.Location;
            checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
            dgvApproval.Controls.Add(checkboxHeader);

            refreshGrid();
        }
        private void refreshGrid()
        {
            if (AdminUser.LoginMode.Equals("Online"))
            {
                getOnlinePendingData();
            }
            else
            {
                getOfflinePendingData();
            }
        }
        private void btnPrintSelected_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (DataGridViewRow row in dgvApproval.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;

                if (cell.Value != null)
                {
                    if (Convert.ToBoolean(cell.Value) != false)
                    {
                        string curcode = "";
                        ulong transid = Convert.ToUInt64(row.Cells[1].Value);
                        string tellertill = row.Cells[2].Value.ToString();
                        string customernumber = row.Cells[3].Value.ToString();
                        string transtype = row.Cells[4].Value.ToString();
                        double transamount = Convert.ToDouble(row.Cells[7].Value);
                        string customername = row.Cells[10].Value.ToString();
                        string depositorname = row.Cells[11].Value.ToString();
                        string authmode = row.Cells[6].Value.ToString();
                        string remark = "PINPAD-" + transid.ToString() + "-";

                        string id = "";

                        if (authmode.ToUpper().Equals("ONLINE"))
                        {
                            id = "O-" + transid.ToString();
                        }
                        else
                        {
                            id = "F-" + transid.ToString();
                        }

                        curcode = util.getcurcode(customernumber);

                        if (Trans.PrintReceipt(id, transtype.ToUpper(), AdminUser.TellerId + "/" + transid.ToString(), customernumber, customername, Convert.ToDouble(transamount), depositorname, AdminUser.branchname, curcode))
                        {
                            Trans.UpdatePrintStatus(transid, 1);
                            MessageBox.Show("Receipt succesfully Sent To Printer.");
                        }
                        else
                        {
                            MessageBox.Show("Receipt Failed to Print Check the printer and try Printing again from the Printing Menu:");
                        }

                        i++;

                        refreshGrid();
                    }
                }
            }
        }
        private void getOnlinePendingData()
        {
            AppDevService appserve = new AppDevService();
            string conString = AdminUser.PinPadCon;
            SqlConnection sqlcon = new SqlConnection(conString);

            try
            {
                DataTable dtselect = new DataTable();
                dtselect = appserve.getOnlinePendingPrintingData(AdminUser.TellerId);

                bdsApproval.DataSource = dtselect;

                dgvApproval.DataSource = bdsApproval;
                dgvApproval.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgvApproval.Refresh();
                dgvApproval.ReadOnly = false;

                int i = dgvApproval.RowCount - 1;

                if (dgvApproval.Rows.Count > 0)
                {
                    btnPrintSelected.Enabled = true;
                }
                else
                {
                    btnPrintSelected.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                MessageBox.Show("A connection error occured. Please Verify that Basis is Up And your PC is on the network:" + ex.Message);
            }
        }
        private void getOfflinePendingData()
        {
            string conString = ConfigurationManager.AppSettings["PinPadConOffline"];//.ConnectionString;
            SqlConnection sqlcon = new SqlConnection(conString);

            try
            {
                string sqlcommand = "SELECT  [TransactionID],TellerTillAccount ,[CustomerNo],[Transtype],[OriginatingTellerId],[AuthenticationMode],[TransAmount]" +
                                     ",[OriginatingBraCode] ,[TransactionStatus],[CustomerName],DepositorName,[TransDate] FROM [PinPad].[dbo].[Transactions] WHERE TransactionStatus = 'APPROVED' and AuthenticationMode = 'Offline' AND PrintStatus = 0 and OriginatingTellerId = '" + AdminUser.TellerId + "' order by transdate desc";
                sqlcon.Open();
                SqlDataAdapter daSelect = new SqlDataAdapter();
                DataTable dtselect = new DataTable();
                daSelect = new SqlDataAdapter(sqlcommand, conString);
                daSelect.SelectCommand.CommandTimeout = 0;
                daSelect.Fill(dtselect);
                bdsApproval.DataSource = dtselect;

                dgvApproval.DataSource = bdsApproval;
                dgvApproval.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgvApproval.Refresh();
                dgvApproval.ReadOnly = false;

                int i = dgvApproval.RowCount - 1;
                if (dgvApproval.Rows.Count > 0)
                {
                    btnPrintSelected.Enabled = true;
                }
                else
                {
                    btnPrintSelected.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                MessageBox.Show("A connection error occured. Please Verify that Basis is Up And your PC is on the network");
            }
            finally
            {
                sqlcon.Close();
            }
        }
    }
}