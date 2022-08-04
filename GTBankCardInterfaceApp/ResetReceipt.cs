using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace GTBankCardInterfaceApp
{
    public partial class ResetReceipt : Form
    {
        DataTable dtselect = null;
        Transaction Trans;
        Utilities util = new Utilities();
        public ResetReceipt()
        {
            InitializeComponent();
        }

        private void btngetTransDetails_Click(object sender, EventArgs e)
        {
            getPendingData();
        }
        private void getPendingData()
        {
            AppDevService appserve = new AppDevService();

            try
            {
                DateTime fromdate = Convert.ToDateTime(dtmFrom.Text);
                string traDate = fromdate.Date.ToString("MM-dd-yyyy", CultureInfo.CreateSpecificCulture("en-GB"));

                dtselect = new DataTable();

                dtselect = appserve.getReceiptResetPendingData(traDate, txtAcctno.Text, txtTraAmt.Text);

                if (dtselect.Rows.Count > 0)
                {
                    bdsHistory.DataSource = dtselect;
                    dgvReceipt.DataSource = bdsHistory;
                    dgvReceipt.Refresh();
                }
                else
                {
                    bdsHistory.DataSource = dtselect;

                    dgvReceipt.DataSource = bdsHistory;
                    dgvReceipt.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dgvReceipt.Refresh();
                    MessageBox.Show("No Record found");

                    return;
                }
                int i = dgvReceipt.RowCount - 1;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                MessageBox.Show("A connection error occured. Please Verify that Basis is Up And your PC is on the network:: " + ex.Message);
            }
        }

        private void btnResetTransaction_Click(object sender, EventArgs e)
        {
            Trans = new Transaction();
            ulong i = 0;
            foreach (DataGridViewRow row in dgvReceipt.Rows)
            {
                //Get the appropriate cell using index, name or whatever and cast to DataGridViewCheckBoxCell
                DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;

                if (cell.Value != null)
                {
                    if (Convert.ToBoolean(cell.Value) != false)
                    {
                        ulong transid = Convert.ToUInt64(row.Cells[1].Value);

                        i = Trans.UpdateTransactionForReceiptReprint(transid);

                        if (i > 0)
                        {
                            MessageBox.Show("Transaction Reset successfully");
                            getPendingData();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("An error Occured please try again");
                            getPendingData();
                            return;
                        }
                    }
                }
            }
        }

        private void ResetReceipt_Load(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.Width = 30;
            checkboxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            checkboxColumn.Frozen = true;
            dgvReceipt.Columns.Insert(0, checkboxColumn);
            // add checkbox header
            Rectangle rect = dgvReceipt.GetCellDisplayRectangle(0, -1, true);
            // set checkbox header to center of header cell. +1 pixel to position correctly.
            rect.X = rect.Location.X + (rect.Width / 4);
            CheckBox checkboxHeader = new CheckBox();
            checkboxHeader.Name = "checkboxHeader";
            checkboxHeader.Size = new Size(18, 18);
            checkboxHeader.Location = rect.Location;

            checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
            dgvReceipt.Controls.Add(checkboxHeader);

        }

        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvReceipt.RowCount; i++)
            {
                dgvReceipt[0, i].Value = ((CheckBox)dgvReceipt.Controls.Find("checkboxHeader", true)[0]).Checked;
            }

            dgvReceipt.EndEdit();
        }

        private void txtTraAmt_TextChanged(object sender, EventArgs e)
        {
            txtTraAmt.Text = util.FormatString(txtTraAmt.Text, 2);
            txtTraAmt.Select(txtTraAmt.TextLength, 0);
        }
    }
}