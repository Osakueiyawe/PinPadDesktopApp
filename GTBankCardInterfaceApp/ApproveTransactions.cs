using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace GTBankCardInterfaceApp
{
    public partial class ApproveTransactions : Form
    {
        Transaction Trans = new Transaction();
        Utilities util = new Utilities();
        public ApproveTransactions()
        {
            InitializeComponent();
        }

        private void ApproveTransactions_Load(object sender, EventArgs e)
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

            if (AdminUser.LoginMode.Equals("Online"))
            {
                cmbAuthMode.SelectedIndex = 0;
                getOnlinePendingData();
            }
            else
            {
                MessageBox.Show("Only Online Transactions allowed");
                Hide();
            }
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
            //if (cmbAuthMode.SelectedItem.ToString().Equals("Offline"))
            //{
            //    int i = 0;
            //    foreach (DataGridViewRow row in dgvApproval.Rows)
            //    {
            //        //Get the appropriate cell using index, name or whatever and cast to DataGridViewCheckBoxCell
            //        DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;
            //        if (cell.Value != null)
            //        {
            //            if (Convert.ToBoolean(cell.Value) != false)
            //            {

            //                UInt64 transid = Convert.ToUInt64(row.Cells[1].Value);//.ToString();
            //                string tellertill = row.Cells[2].Value.ToString();
            //                string customernumber = row.Cells[3].Value.ToString();
            //                string transtype = row.Cells[4].Value.ToString();
            //                double transamount = Convert.ToDouble(row.Cells[7].Value);
            //                string customername = row.Cells[10].Value.ToString();
            //                string depositorname = row.Cells[11].Value.ToString();
            //                string remark = "PINPAD-" + transid.ToString() + "-" + AdminUser.branchcode + "/" + AdminUser.TellerId;

            //               // Converter t = new Converter(ConfigurationSettings.AppSettings["Oracleconstring"].ToString());
            //                UInt64 update = Trans.UpdateTransaction(AdminUser.TellerId, transid, AdminUser.branchcode, customernumber, remark, "APPROVED", "PENDING", cmbAuthMode.SelectedItem.ToString(),tellertill);

            //                if (update == 0)// succesful but couldnt update local DB with success status. Log for the service to update. But allow transaction to go through.
            //                {
            //                    //log into pending update table;
            //                    //Call Print Receipt.
            //                    MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence could not be retrieved:");
            //                    i++;
            //                    //  btnSubmitCr.Enabled = false;
            //                }
            //                else
            //                {
            //                    //Call Print Receipt.

            //                    // needs to populate depositors details
            //                   // if (

            //                    if (Trans.PrintReceipt("F-" + transid.ToString(), transtype.ToUpper(), AdminUser.TellerId + "/" + transid.ToString(), customernumber, customername, Convert.ToDouble(transamount), depositorname, AdminUser.branchcode))
            //                    {
            //                        // {
            //                        Trans.UpdatePrintStatusOffline(transid, 1);
            //                        MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence:" + transid.ToString());
            //                    }

            //            else
            //            {
            //                MessageBox.Show("Transaction Succesful. Receipt Failed to Print Check the printer and try Printing again from the Printing Menu:" + transid.ToString());
            //                //Trans.UpdatePrintStatus(update, 0); no need default is zero anyway
            //                //Update Receipt status
            //            }
            //                     // }
            //                    //else
            //                    //{
            //                    //    MessageBox.Show("Transaction Succesful. Receipt Failed to Print Check the printer and try Printing again from the Printing Menu:" + update.ToString());
            //                    //    //Trans.UpdatePrintStatus(update, 0); no need default is zero anyway
            //                    //    //Update Receipt status
            //                    //}
            //                    i++;
            //                    //    MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence:" + update.ToString());
            //                    //    // btnSubmitCr.Enabled = false;
            //                }
            //            }
            //        }
            //    }
            //    if (i == 0)
            //    {
            //        MessageBox.Show("Please Select at least one transaction");
            //        btnApprove.Enabled = true;
            //        return;

            //    }
            //    getOfflinePendingData();
            //    MessageBox.Show("Total No of Transactions Approved = : " + i.ToString());
            //}

            if (cmbAuthMode.SelectedItem.ToString().Equals("Online"))
            {
                string PostResult = "";
                int i = 0;
                int j = 0;

                foreach (DataGridViewRow row in dgvApproval.Rows)
                {
                    string curcode = "";
                    DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;

                    if (cell.Value != null)
                    {
                        if (Convert.ToBoolean(cell.Value) != false)
                        {
                            j++;
                            ulong transid = Convert.ToUInt64(row.Cells[1].Value);

                            if (!Trans.IsInProgress(transid))
                            {
                                MessageBox.Show("This Transaction is already being processed by another approver");
                                getOnlinePendingData();
                                return;
                            }

                            string tellertill = row.Cells[2].Value.ToString();
                            string customernumber = row.Cells[3].Value.ToString();
                            string transtype = row.Cells[4].Value.ToString();
                            double transamount = Convert.ToDouble(row.Cells[7].Value);
                            string customername = row.Cells[10].Value.ToString();
                            string depositorname = row.Cells[11].Value.ToString();
                            string remark = "PINPAD-" + transid.ToString() + "-" + AdminUser.branchcode + "/" + AdminUser.TellerId;
                            PostResult = Trans.PostToBasis(customernumber, tellertill, transamount, Convert.ToUInt16(AdminUser.WithdrwalExpl_Code), remark, transtype.ToUpper(), AdminUser.TellerId, AdminUser.branchcode);

                            curcode = util.getcurcode(customernumber);

                            if (PostResult.Equals("0")) // successful Transaction
                            {
                                ulong update = Trans.UpdateTransaction(AdminUser.TellerId, transid, AdminUser.branchcode, customernumber, remark, "APPROVED", "SUCCESS", cmbAuthMode.SelectedItem.ToString(), tellertill, AdminUser.WithdrwalExpl_Code);

                                appserve.FastTrackUpdateSupervisor(transid.ToString(), "Posted", AdminUser.TellerId);
                                //if (update == 0)// succesful but couldnt update local DB with success status. Log for the service to update. But allow transaction to go through.
                                //{
                                //    //log into pending update table;
                                //    //Call Print Receipt.
                                //    MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence could not be retrieved:");
                                //    i++;
                                //    //  btnSubmitCr.Enabled = false;
                                //}
                                //else
                                //{
                                //Call Print Receipt.
                                i++;
                                // needs to populate depositors details
                                //if (Trans.PrintReceipt("0-" + update.ToString(), transtype.ToUpper(), AdminUser.TellerId + "/" + transid.ToString(), appserve.ConvertToNuban(customernumber), customername, Convert.ToDouble(transamount), depositorname, AdminUser.branchname,curcode))
                                //{

                                //    Trans.UpdatePrintStatus(transid, 1);
                                //    MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence:" + update.ToString());
                                //}
                                //else
                                //{
                                //    MessageBox.Show("Transaction Succesful. Receipt Failed to Print Check the printer and try Printing again from the Printing Menu:" + update.ToString());
                                //    //Trans.UpdatePrintStatus(update, 0); no need default is zero anyway
                                //    //Update Receipt status
                                //}

                                //    MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence:" + update.ToString());
                                //    // btnSubmitCr.Enabled = false;
                            }

                            else if (PostResult.Equals("1"))
                            {
                                MessageBox.Show("Transaction Pending: This Transaction is currently being processed by another Authorizer!!!");
                            }
                            else
                            {
                                Trans.UpdateTransaction(AdminUser.TellerId, transid, AdminUser.branchcode, customernumber, remark, "FAILED", PostResult, cmbAuthMode.SelectedItem.ToString(), tellertill, AdminUser.WithdrwalExpl_Code);
                                MessageBox.Show(transid + " Transaction Failed.  Error is::" + PostResult);
                            }
                        }
                    }
                }
                if (j == 0)
                {
                    MessageBox.Show("Please Select at least one transaction!!!");
                    btnApprove.Enabled = true;
                    return;

                }
                getOnlinePendingData();
                MessageBox.Show("Total No of Transactions Approved = : " + i.ToString());
            }
        }

        private void getOnlinePendingData()
        {
            try
            {
                AppDevService appserve = new AppDevService();

                DataTable dtselect = new DataTable();
                dtselect = appserve.getOnlineWithdrawalPendingData(AdminUser.branchcode);
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
                MessageBox.Show("A connection error occured. " + ex.Message);
            }          
        }

        private void getOfflinePendingData()
        {
            string conString = ConfigurationSettings.AppSettings["PinPadConOffline"];//.ConnectionString;
            using (SqlConnection sqlcon = new SqlConnection(conString))
            {
                try
                {
                    string sqlcommand = "SELECT  [TransactionID],TellerTillAccount ,[CustomerNo],[Transtype],[OriginatingTellerId],[AuthenticationMode],[TransAmount]" +
                                         ",[OriginatingBraCode] ,[TransactionStatus],[CustomerName],DepositorName,[TransDate] FROM [PinPad].[dbo].[Transactions] WHERE TransactionStatus = 'PENDING' and transtype = 'WITHDRAWAL' and AuthenticationMode = 'Offline' and OriginatingBraCode = '" + AdminUser.branchcode + "' order by transdate desc";
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
                    ErrHandler.WriteError("An exception occurred in getOfflinePendingData. Message - " + ex.Message + "|Stacktrace - " + ex.StackTrace);
                    MessageBox.Show("A connection error occured. Please Verify that your PC is on the network");
                }
                finally
                {
                    sqlcon.Close();
                }
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            AppDevService appserve = new AppDevService();
            btnReject.Enabled = false;
            int i = 0;
            foreach (DataGridViewRow row in dgvApproval.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;

                if (cell.Value != null)
                {
                    if (Convert.ToBoolean(cell.Value) != false)
                    {
                        ulong transid = Convert.ToUInt64(row.Cells[1].Value);
                        string tellertill = row.Cells[2].Value.ToString();
                        string customernumber = row.Cells[3].Value.ToString();
                        string transtype = row.Cells[4].Value.ToString();
                        double transamount = Convert.ToDouble(row.Cells[7].Value);
                        string customername = row.Cells[10].Value.ToString();
                        string remark = "PINPAD-" + transid.ToString() + "-" + AdminUser.branchcode + "/" + AdminUser.TellerId;
                        Trans.UpdateTransaction(AdminUser.TellerId, transid, AdminUser.branchcode, customernumber, remark, "FAILED", "REJECTED BY OPS HEAD", cmbAuthMode.SelectedItem.ToString(), tellertill, AdminUser.WithdrwalExpl_Code);
                        i++;
                        appserve.FastTrackUpdateSupervisor(transid.ToString(), "Rejected", AdminUser.TellerId);
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
            MessageBox.Show("Total No of Cells Rejected = : " + i.ToString());
        }

        private void btnGetPendingTrans_Click(object sender, EventArgs e)
        {
            if (cmbAuthMode.SelectedItem.ToString().Equals("Online"))
            {
                getOnlinePendingData();
            }
            else
            {
                getOfflinePendingData();
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

        private void btnViewCusDetails_Click(object sender, EventArgs e)
        {
            int j = 0;
            foreach (DataGridViewRow row in dgvApproval.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;

                if ((cell.Value != null) & (Convert.ToBoolean(cell.Value) != false))
                {
                    j++;

                    string customernumber = row.Cells[3].Value.ToString();
                    AdminUser.SVAccountNumber = customernumber;
                }
            }
            if (j > 1)
            {
                MessageBox.Show("You can Only view the details for One account at a time.");
                return;
            }
            else if (j == 0)
            {
                MessageBox.Show("Please Select at least one transaction");
                return;

            }
            else if (j == 1)
            {
                ApproverSV apprSVscreen = new ApproverSV();
                apprSVscreen.ShowDialog();
            }
        }

        private void btnTransHistory_Click(object sender, EventArgs e)
        {
            int j = 0;
            foreach (DataGridViewRow row in dgvApproval.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;

                if ((cell.Value != null) & (Convert.ToBoolean(cell.Value) != false))
                {
                    j++;

                    string customernumber = row.Cells[3].Value.ToString();
                    AdminUser.SVAccountNumber = customernumber;

                }
            }
            if (j > 1)
            {
                MessageBox.Show("You can Only view the details for One account at a time.");
                return;
            }
            else if (j == 0)
            {
                MessageBox.Show("Please Select at least one transaction");
                return;

            }
            else if (j == 1)
            {
                CustomerTransactionHistory Historyscreen = new CustomerTransactionHistory();
                Historyscreen.ShowDialog();
            }
        }
    }
}
