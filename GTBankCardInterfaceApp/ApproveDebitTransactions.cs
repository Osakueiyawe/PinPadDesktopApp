using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;

namespace GTBankCardInterfaceApp
{
    public partial class ApproveDebitTransactions : Form
    {
        Transaction Trans = new Transaction();
        Utilities util = new Utilities();
        public ApproveDebitTransactions()
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
                cmbAuthMode.Enabled = true;
                getOnlinePendingData();
            }
            else
            {
                cmbAuthMode.SelectedIndex = 1;
                cmbAuthMode.Enabled = false;
                getOfflinePendingData();
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
                            string tellertill = row.Cells[2].Value.ToString();
                            string customernumber = row.Cells[3].Value.ToString();
                            string transtype = row.Cells[4].Value.ToString();
                            double transamount = Convert.ToDouble(row.Cells[7].Value);
                            string customername = row.Cells[10].Value.ToString();
                            string depositorname = row.Cells[11].Value.ToString();
                            string remark = "PINPAD-" + transid.ToString() + "-" + AdminUser.branchcode + "/" + AdminUser.TellerId;


                            if (!Trans.IsInProgress(transid))
                            {
                                MessageBox.Show("This Transaction is already being processed by another approver");
                                getOnlinePendingData();
                                return;
                            }

                            PostResult = Trans.PostToBasis(customernumber, tellertill, transamount, Convert.ToUInt16(AdminUser.DepositExpl_Code), remark, transtype.ToUpper(), AdminUser.TellerId, AdminUser.branchcode);

                            if (PostResult.Equals("0")) // successful Transaction
                            {
                                ulong update = Trans.UpdateTransaction(AdminUser.TellerId, transid, AdminUser.branchcode, customernumber, remark, "APPROVED", "SUCCESS", cmbAuthMode.SelectedItem.ToString(), tellertill, AdminUser.DepositExpl_Code);
                                i++;
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
                                //    //Call Print Receipt.

                                //    // needs to populate depositors details
                                //    if (Trans.PrintReceipt("0-" + update.ToString(), transtype.ToUpper(), AdminUser.TellerId + "/" + transid.ToString(), appserve.ConvertToNuban(customernumber), customername, Convert.ToDouble(transamount), depositorname, AdminUser.branchcode))
                                //    {
                                //        Trans.UpdatePrintStatus(transid, 1);
                                //        MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence:" + update.ToString());
                                //    }
                                //    //else
                                //    //{
                                //    //    MessageBox.Show("Transaction Succesful. Receipt Failed to Print Check the printer and try Printing again from the Printing Menu:" + update.ToString());
                                //    //    //Trans.UpdatePrintStatus(update, 0); no need default is zero anyway
                                //    //    //Update Receipt status
                                //    //}

                                ////    MessageBox.Show("Transaction Succesful. Receipt Sent To Printer. Tran Sequence:" + update.ToString());
                                ////    // btnSubmitCr.Enabled = false;
                                //}

                                appserve.FastTrackUpdateSupervisor(transid.ToString(), "Posted", AdminUser.TellerId);
                            }
                            else if (PostResult.Equals("1")) //Above Teller Authorisation
                            {
                                MessageBox.Show("Transaction Pending: This Transaction is currently being processed by another Authorizer!!!");
                            }
                            else
                            {
                                Trans.UpdateTransaction(AdminUser.TellerId, transid, AdminUser.branchcode, customernumber, remark, "FAILED", PostResult, cmbAuthMode.SelectedItem.ToString(), tellertill, AdminUser.WithdrwalExpl_Code);
                                MessageBox.Show(transid + " Transaction Failed.Error is::" + PostResult);
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
                MessageBox.Show("Total No of Transactions Approved = : " + i.ToString());
            }
        }

        private void getOnlinePendingData()
        {
            AppDevService appserve = new AppDevService();

            try
            {
                DataTable dtselect = new DataTable();
                dtselect = appserve.getOnlineDepositPendingData(AdminUser.branchcode);
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
                return;
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
                                             ",[OriginatingBraCode] ,[TransactionStatus],[CustomerName],DepositorName,[TransDate] FROM dbo_Transactions WHERE TransactionStatus = 'PENDING' and transtype = 'DEPOSIT' and AuthenticationMode = 'Offline' and OriginatingBraCode = '" + AdminUser.branchcode + "' order by transdate desc";
                        Con.Open();
                        OleDbDataAdapter daSelect = new OleDbDataAdapter();
                        DataTable dtselect = new DataTable();
                        daSelect = new OleDbDataAdapter(accesscommand, Con);
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
                MessageBox.Show("A connection error occured. Please Verify that your PC is on the network");
                return;
            }            
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            btnReject.Enabled = false;
            int i = 0;
            foreach (DataGridViewRow row in dgvApproval.Rows)
            {
                DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;

                if (cell.Value != null)
                {
                    if (Convert.ToBoolean(cell.Value) != false)
                    {
                        ulong transid = Convert.ToUInt64(row.Cells[1].Value);//.ToString();
                        string tellertill = row.Cells[2].Value.ToString();
                        string customernumber = row.Cells[3].Value.ToString();
                        string transtype = row.Cells[4].Value.ToString();
                        double transamount = Convert.ToDouble(row.Cells[7].Value);
                        string customername = row.Cells[10].Value.ToString();
                        string remark = "PINPAD-" + transid.ToString() + "-" + AdminUser.branchcode + "/" + AdminUser.TellerId;
                        Trans.UpdateTransaction(AdminUser.TellerId, transid, AdminUser.branchcode, customernumber, remark, "FAILED", "REJECTED BY OPS HEAD", cmbAuthMode.SelectedItem.ToString(), tellertill, AdminUser.DepositExpl_Code);
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

            MessageBox.Show("Total No of Transactions Rejected = : " + i.ToString());
        }

        private void btnGetPendingTrans_Click(object sender, EventArgs e)
        {
            if (cmbAuthMode.SelectedItem.ToString().Equals("Online"))
            {
                getOnlinePendingData();
                cmbAuthMode.Enabled = true;
            }
            else
            {
                getOfflinePendingData();
                cmbAuthMode.Enabled = true;
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