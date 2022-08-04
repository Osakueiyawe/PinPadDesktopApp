using System;
using System.Data;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
using CompletIT.Windows.Forms.Printing;
using CompletIT.Windows.Forms.Export;
using CompletIT.Windows.Forms.Export.Excel;
using CompletIT.Windows.Forms.Export.Pdf;
using System.Collections;

namespace GTBankCardInterfaceApp
{
    public partial class TransactionHistory : Form
    {
        DataTable dtselect = null;
        public TransactionHistory()
        {
            InitializeComponent();
        }

        private void btnGetTransDetails_Click(object sender, EventArgs e)
        {
            DateTime fromdate = Convert.ToDateTime(dtmFrom.Text);
            DateTime todate = Convert.ToDateTime(dtmTo.Text);
            string startDate = fromdate.Date.ToString("MM-dd-yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
            string endDate = todate.Date.ToString("MM-dd-yyyy", CultureInfo.CreateSpecificCulture("en-GB"));

            if (cmbTransType.SelectedItem != null)
            {
                getPendingData(startDate, endDate, cmbTransType.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Please Select Report Type.....");
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            //You can export any DataGridView control, no matter managed or not by the extension
            DGVEExcelExportSettingsEditor editor = new DGVEExcelExportSettingsEditor();
            DGVEExcelExportSettings settings = new DGVEExcelExportSettings();
            //Set values to any of the other properties of the settings object...
            DGVEBaseExportSettingsEditorForm dialog = DGVEExportSettingsEditorFormBuilder.CreateWrappingForm(editor);
            dialog.Settings = settings;

            if (DialogResult.OK != dialog.ShowDialog())
                return;

            DGVEExcelExporter exporter = new DGVEExcelExporter();
            exporter.ExportFailed += new EventHandler<ExportFailedEventArgs>(exporter_ExportFailed);
            exporter.Export(dgvHistory, settings);
        }

        private void exporter_ExportFailed(object sender, ExportFailedEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        private void getPendingData(string startdate, string enddate, string transtype)
        {
            AppDevService appserve = new AppDevService();
            double withdrawaltotal = 0;
            double deposittotal = 0;
            object dptotl = null;
            object crtotl = null;
            Utilities util = new Utilities();

            string squery = "";

            if (transtype.ToUpper().Equals("ALL"))
            {
                squery = "%";
            }
            else if (transtype.ToUpper().Equals("SUCCESS"))
            {
                squery = "APPROVED";
            }

            if (transtype.ToUpper().Equals("FAILED"))
            {
                squery = "FAILED";
            }

            try
            {
                dtselect = appserve.getPinPadTransactionHistory(startdate, enddate, transtype, AdminUser.branchcode, cmbTeller.SelectedValue.ToString());

                if (dtselect.Rows.Count > 0)
                {
                    DataRow Deposit;
                    DataRow Withdrawal;
                    DataRow Total;
                    Deposit = dtselect.NewRow();
                    Withdrawal = dtselect.NewRow();
                    Total = dtselect.NewRow();
                    Deposit[0] = 0;
                    Deposit[1] = "";
                    Deposit[2] = "";
                    Deposit[3] = "DEPOSIT";
                    Deposit[4] = dtselect.Compute("sum(TransAmount)", "TransType = 'DEPOSIT'");
                    dptotl = dtselect.Compute("sum(TransAmount)", "TransType = 'DEPOSIT'");

                    try
                    {
                        deposittotal = Convert.ToDouble(dtselect.Compute("sum(TransAmount)", "TransType = 'DEPOSIT'"));
                    }
                    catch (Exception ex)
                    {
                        deposittotal = 0.00;
                    }

                    Deposit[5] = DateTime.Now;
                    Deposit[6] = "NO OF DEPOSIT TRANSACTIONS";
                    Deposit[7] = dtselect.Compute("COUNT(TransAmount)", "TransType = 'DEPOSIT'"); ;
                    Deposit[8] = "";
                    Deposit[9] = "";
                    Deposit[10] = "";

                    Withdrawal[0] = 0;
                    Withdrawal[1] = "";
                    Withdrawal[2] = "";
                    Withdrawal[3] = "WITHDRAWAL";
                    Withdrawal[4] = dtselect.Compute("sum(TransAmount)", "TransType = 'WITHDRAWAL'");
                    crtotl = dtselect.Compute("sum(TransAmount)", "TransType = 'WITHDRAWAL'");

                    try
                    {

                        withdrawaltotal = Convert.ToDouble(dtselect.Compute("sum(TransAmount)", "TransType = 'WITHDRAWAL'"));
                    }
                    catch (Exception ex)
                    {
                        withdrawaltotal = 0.00;
                    }
                    Withdrawal[5] = DateTime.Now;
                    Withdrawal[6] = "NO OF WITHDRAWAL TRANSACTIONS";
                    Withdrawal[7] = dtselect.Compute("COUNT(TransAmount)", "TransType = 'WITHDRAWAL'");
                    Withdrawal[8] = "";
                    Withdrawal[9] = "";
                    Withdrawal[10] = "";

                    Total[0] = 0;
                    Total[1] = "";
                    Total[2] = "";
                    Total[3] = "NET-TOTAL";
                    Total[4] = deposittotal - withdrawaltotal;
                    Total[5] = DateTime.Now;
                    Total[6] = "TOTAL NO OF TRANSACTIONS";
                    Total[7] = dtselect.Compute("COUNT(TransAmount)", "");
                    Total[8] = "";
                    Total[9] = "";
                    Total[10] = "";

                    dtselect.Rows.Add(Deposit);

                    dtselect.Rows.Add(Withdrawal);

                    dtselect.Rows.Add(Total);
                }

                if (dtselect.Rows.Count > 0)
                {
                    bdsHistory.DataSource = dtselect;

                    dgvHistory.DataSource = bdsHistory;
                    dgvHistory.Refresh();
                    btnExportToExcel.Enabled = true;
                    btnExportToPdf.Enabled = true;
                }
                else
                {
                    bdsHistory.DataSource = dtselect;

                    dgvHistory.DataSource = bdsHistory;
                    dgvHistory.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dgvHistory.Refresh();
                    btnExportToExcel.Enabled = false;
                    btnExportToPdf.Enabled = false;
                    MessageBox.Show("No Record found");

                    return;
                }
                int i = dgvHistory.RowCount - 1;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                MessageBox.Show("A connection error occured. Please Verify that Basis is Up And your PC is on the network:: " + ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DateTime fromdate = Convert.ToDateTime(dtmFrom.Text);
            DateTime todate = Convert.ToDateTime(dtmTo.Text);
            string startDate = fromdate.Date.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-GB"));

            string endDate = todate.Date.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-GB"));
            DGVEPrintSettings prtsettings = new DGVEPrintSettings();
            prtsettings.HeaderText = "Branch:" + AdminUser.branchcode + " Report From " + startDate + " To " + endDate + ".  Teller:" + cmbTeller.SelectedValue.ToString();
            prtsettings.Landscape = true;
            prtsettings.PrintHeaderText = true;
            prtsettings.PrintPageNumbers = true;
            prtsettings.PrintRowHeaders = true;
            prtsettings.PrintHeaderTextOnEveryPage = true;
            prtsettings.MarginLeft = 1;
            prtsettings.MarginRight = 1;
            prtsettings.MarginBottom = 1;
            prtsettings.MarginTop = 1;
            DGVEPrintManager.Print(dgvHistory, prtsettings);

        }

        private void btnExportToPdf_Click(object sender, EventArgs e)
        {
            //You can export any DataGridView control, no matter managed or not by the extension
            DGVEPdfExporter pdfExporter = new DGVEPdfExporter();
            DGVEBaseExportSettingsEditorForm dialog = DGVEExportSettingsEditorFormBuilder.CreateWrappingForm(pdfExporter);
            dialog.Settings = new DGVEPdfExportSettings();

            if (DialogResult.OK != dialog.ShowDialog())
                return;

            pdfExporter.ExportFailed += new EventHandler<ExportFailedEventArgs>(exporter_ExportFailed);
            pdfExporter.Export(dgvHistory, dialog.Settings);
        }

        private void TransactionHistory_Load(object sender, EventArgs e)
        {
            if (AdminUser.roleid.Equals(AdminUser.OpsHeadRoleId))
            {
                lblTellerSelect.Visible = true;
                cmbTeller.Enabled = true;
                GetBranchTeller();
            }
            else
            {
                lblTellerSelect.Visible = false;
                cmbTeller.Enabled = false;
                ArrayList a = new ArrayList();
                a.Add(AdminUser.TellerId);
                cmbTeller.DataSource = a;
                cmbTeller.DisplayMember = (string)(a[0]);
            }
        }

        private void GetBranchTeller()
        {
            AppDevService appserve = new AppDevService();
            string conString = AdminUser.PinPadCon;//.ConnectionString;
            SqlConnection sqlcon = new SqlConnection(conString);
            DateTime fromdate = Convert.ToDateTime(dtmFrom.Text);
            DateTime todate = Convert.ToDateTime(dtmTo.Text);
            string startDate = fromdate.Date.ToString("MM-dd-yyyy", CultureInfo.CreateSpecificCulture("en-GB"));

            string endDate = todate.Date.ToString("MM-dd-yyyy", CultureInfo.CreateSpecificCulture("en-GB"));

            dtselect = appserve.GetBranchTeller(AdminUser.branchcode);

            cmbTeller.DataSource = dtselect;
            cmbTeller.DisplayMember = "OriginatingTellerId";
            cmbTeller.ValueMember = "OriginatingTellerId";
        }
    }
}