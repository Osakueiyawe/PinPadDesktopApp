using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace GTBankCardInterfaceApp
{
    public partial class PostOfflineApprovedDebitTransactions : Form
    {
        Transaction Trans = new Transaction();
        Utilities util = new Utilities();
        public PostOfflineApprovedDebitTransactions()
        {
            InitializeComponent();
        }

        private void PostOfflineApprovedDebitTransactions_Load(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.Width = 30;
            checkboxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            checkboxColumn.Frozen = true;
            dgvApproval.Columns.Insert(0, checkboxColumn);
            Rectangle rect = dgvApproval.GetCellDisplayRectangle(0, -1, true);
            rect.X = rect.Location.X + (rect.Width / 4);
            CheckBox checkboxHeader = new CheckBox();
            checkboxHeader.Name = "checkboxHeader";
            checkboxHeader.Size = new Size(18, 18);
            checkboxHeader.Location = rect.Location;
            checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
            dgvApproval.Controls.Add(checkboxHeader);

            cmbAuthMode.SelectedIndex = 0;

            getOnlinePendingData();
        }

        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvApproval.RowCount; i++)
            {
                dgvApproval[0, i].Value = ((CheckBox)dgvApproval.Controls.Find("checkboxHeader", true)[0]).Checked;
            }

            dgvApproval.EndEdit();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            AppDevService appserve = new AppDevService();
            btnApprove.Enabled = false;

            if (cmbAuthMode.SelectedItem.ToString().Equals("Online"))
            {
                string PostResult = "";
                int i = 0;
                int j = 0;

                foreach (DataGridViewRow row in dgvApproval.Rows)
                {
                    DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;

                    if (cell.Value != null)
                    {
                        if (Convert.ToBoolean(cell.Value) != false)
                        {
                            j++;
                            ulong transid = Convert.ToUInt64(row.Cells[1].Value);
                            string cusnum = "";
                            string tellertill = row.Cells[2].Value.ToString();
                            string customernumber = row.Cells[3].Value.ToString();

                            if (customernumber.Length == 10)
                            {
                                cusnum = appserve.ConvertToOldAccountNumber(customernumber);
                            }
                            else
                            {
                                cusnum = customernumber;
                            }

                            string transtype = row.Cells[4].Value.ToString();
                            double transamount = Convert.ToDouble(row.Cells[7].Value);
                            string customername = row.Cells[10].Value.ToString();
                            string depositorname = row.Cells[11].Value.ToString();
                            string remark = "PINPAD-" + transid.ToString() + "-" + AdminUser.branchcode + "/" + AdminUser.TellerId;

                            PostResult = Trans.PostToBasis(cusnum, tellertill, transamount, Convert.ToUInt16(AdminUser.DepositExpl_Code), remark, transtype.ToUpper(), AdminUser.TellerId, AdminUser.branchcode);

                            if (PostResult.Equals("0"))
                            {
                                ulong update = Trans.UpdateTransaction(AdminUser.TellerId, transid, AdminUser.branchcode, customernumber, remark, "APPROVED", "SUCCESS", cmbAuthMode.SelectedItem.ToString(), tellertill, AdminUser.DepositExpl_Code);

                                i++;
                            }
                            else if (PostResult.Equals("1"))
                            {
                                MessageBox.Show("The Amount is above your Approval Limit!!!");
                            }
                            else 
                            {
                                MessageBox.Show(transid + " Transaction Failed. Please Reinitiate. Error is::" + PostResult);
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                if (j == 0)
                {
                    MessageBox.Show("Please Select at least one transaction!!!");
                    btnApprove.Enabled = true;
                    return;
                }

                getOnlinePendingData();
                MessageBox.Show("Total No of Cells Approved = : " + i.ToString());
            }
        }

        private void getOnlinePendingData()
        {
            string conString = AdminUser.PinPadCon;
            using (SqlConnection sqlcon = new SqlConnection(conString))
            {
                try
                {
                    string sqlcommand = "SELECT  [TransactionID],'" + AdminUser.tillaccount + "' as TellerTillAccount  ,[CustomerNo],[Transtype],[OriginatingTellerId],[AuthenticationMode],[TransAmount]" +
                                         ",[OriginatingBraCode] ,[TransactionStatus],[CustomerName],DepositorName,[TransDate] FROM [PinPad].[dbo].[Transactions] WHERE TransactionStatus = 'PENDING' and transtype = 'DEPOSIT' and AuthenticationMode = 'Offline' and OriginatingBraCode = '" + AdminUser.branchcode + "' order by transdate desc";
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
                        btnApprove.Enabled = true;
                        btnReject.Enabled = true;
                    }
                    else
                    {
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.ToString());
                    MessageBox.Show("A connection error occured. Ensure your PC is on the network");
                }
                finally
                {
                    sqlcon.Close();
                }
            }
        }

        private void getOfflinePendingData()
        {
            try
            {
                using (OleDbConnection Con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\PinPad.gtb"))
                {
                    using (OleDbCommand accesscom = new OleDbCommand())
                    {
                        accesscom.Connection = Con;
                        string accesscommand = "SELECT  [TransactionID],TellerTillAccount ,[CustomerNo],[Transtype],[OriginatingTellerId],[AuthenticationMode],[TransAmount]" +
                                             ",[OriginatingBraCode] ,[TransactionStatus],[CustomerName],DepositorName,[TransDate] FROM dbo_Transactions WHERE TransactionStatus = 'PENDING' and transtype = 'DEPOSIT' and AuthenticationMode = 'Offline' order by transdate desc";
                        Con.Open();
                        OleDbDataAdapter daSelect = new OleDbDataAdapter();
                        DataTable dtselect = new DataTable();
                        daSelect = new OleDbDataAdapter(accesscommand, Con);//"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\PinPad.gtb");
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
                            btnApprove.Enabled = true;
                            btnReject.Enabled = true;
                        }
                        else
                        {
                            btnApprove.Enabled = false;
                            btnReject.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                MessageBox.Show("A connection error occured. Please Verify that your PC is on the network");
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            btnReject.Enabled = false;
            int i = 0;
            foreach (DataGridViewRow row in dgvApproval.Rows)
            {
                //Get the appropriate cell using index, name or whatever and cast to DataGridViewCheckBoxCell
                DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;

                if (cell.Value != null)
                {
                    if (Convert.ToBoolean(cell.Value) != false)
                    {
                        UInt64 transid = Convert.ToUInt64(row.Cells[1].Value);//.ToString();
                        string tellertill = row.Cells[2].Value.ToString();
                        string customernumber = row.Cells[3].Value.ToString();
                        string transtype = row.Cells[4].Value.ToString();
                        double transamount = Convert.ToDouble(row.Cells[7].Value);
                        string customername = row.Cells[10].Value.ToString();
                        string remark = "PINPAD-" + transid.ToString() + "-" + AdminUser.branchcode + "/" + AdminUser.TellerId;
                        Trans.UpdateTransactionAccessDB(AdminUser.TellerId, transid, AdminUser.branchcode, customernumber, remark, "FAILED", "REJECTED BY OPS HEAD", cmbAuthMode.SelectedItem.ToString());
                        i++;
                    }
                }
                else
                {
                    continue;
                }

            }

            if (cmbAuthMode.SelectedItem.ToString().Equals("Online"))
            {
                getOnlinePendingData();
            }
            else
            {
                getOfflinePendingData();
            }
            MessageBox.Show("Total No of Cells Approved = : " + i.ToString());
        }

        private void btnGetPendingTrans_Click(object sender, EventArgs e)
        {
            if (cmbAuthMode.SelectedItem.ToString().Equals("Online"))
            {
                getOnlinePendingData();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbAuthMode.Enabled = true;
            bdsApproval.DataSource = null;
            dgvApproval.DataSource = bdsApproval;
            btnGetPendingTrans.Enabled = true;
            btnApprove.Enabled = false;
            btnReject.Enabled = false;
        }
    }
}