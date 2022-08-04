using System;
using System.Windows.Forms;
using System.Data;
using System.Configuration;
using System.Threading;
using System.Data.SqlClient;

namespace GTBankCardInterfaceApp
{
    public partial class frmMain : Form
    {
        string BalloonTipText = "";
        SqlConnection PinPadConOffline = new SqlConnection(ConfigurationManager.AppSettings["PinPadConOffline"]);
        SqlConnection PinPadCon = new SqlConnection(AdminUser.PinPadCon);
        string Transtype = "";
        string OriginatingTellerId = "";
        string TransAmount = "";
        string OriginatingBraCode = "";

        public frmMain()
        {
            InitializeComponent();

        }
        private void abortAcitivityMonitoring()
        {
            txtActivity.Focus();
        }

        /// <summary>
        /// Because the thread that is triggering the event, and will
        /// finaly end up doing your code is not created on our GUI-thread
        /// so we need to create a delegate and invoke the method.
        /// </summary>
        /// <param name="text">string; What text to add to txtAcitivty</param>
        private delegate void AddLogText(string text);
        private void TS_AddLogText(string text)
        {
            if (InvokeRequired)
            {
                AddLogText del = new AddLogText(TS_AddLogText);
                Invoke(del, text);
            }
            else
            {
                txtActivity.Text += text;
            }
        }

        private void CheckForPendingTransTeller(string bra_code, string teller_id, int isopshead)
        {
            DataTable pendingtrans = null;
            try
            {
                pendingtrans = new DataTable();
                AppDevService appserve = new AppDevService();

                System.Media.SoundPlayer aSoundPlayer = new System.Media.SoundPlayer(Application.StartupPath + "\\pinpad.wav");  //Creates a sound player with the mentioned file. You can even load a stream in to this class

                pendingtrans = appserve.CheckForPendingTransTeller(bra_code, teller_id, isopshead);
                if (pendingtrans != null)
                {
                    if (pendingtrans.Rows.Count > 0)
                    {
                        Transtype = pendingtrans.Rows[0]["Transtype"].ToString();
                        OriginatingTellerId = pendingtrans.Rows[0]["OriginatingTellerId"].ToString();
                        TransAmount = pendingtrans.Rows[0]["TransAmount"].ToString();
                        OriginatingBraCode = pendingtrans.Rows[0]["OriginatingBraCode"].ToString();

                        if ((AdminUser.roleid.Equals(AdminUser.TellerRoleId)) & (AdminUser.IsOverAuthlimit == true) & (!string.IsNullOrEmpty(AdminUser.SVAccountNumber)))//Teller
                        {
                            if (OriginatingTellerId.Equals(AdminUser.TellerId))
                            {
                                BalloonTipText = "Awaiting Approval...";
                                PinPadNotifyIcon.Visible = true;
                                PinPadNotifyIcon.ShowBalloonTip(1000, "PinPad ", BalloonTipText, ToolTipIcon.Info);

                                aSoundPlayer.PlayLooping();
                            }
                        }
                        else if ((AdminUser.roleid.Equals(AdminUser.TellerRoleId)) & (AdminUser.IsOverAuthlimit == false) & (!string.IsNullOrEmpty(AdminUser.SVAccountNumber)))//Teller
                        {
                            if (OriginatingTellerId.Equals(AdminUser.TellerId))
                            {
                                BalloonTipText = "Processing Transaction...";// OriginatingBraCode + "/" + OriginatingTellerId + " " + TransAmount + " " + Transtype;
                                PinPadNotifyIcon.Visible = true;
                                PinPadNotifyIcon.ShowBalloonTip(1000, "PinPad ", BalloonTipText, ToolTipIcon.Info);

                                aSoundPlayer.PlayLooping();
                            }
                        }
                        else if (AdminUser.roleid.Equals(AdminUser.OpsHeadRoleId))// Ops Head
                        {

                            BalloonTipText = OriginatingBraCode + "/" + OriginatingTellerId + " " + TransAmount + " " + Transtype;
                            PinPadNotifyIcon.Visible = true;
                            PinPadNotifyIcon.ShowBalloonTip(1000, "PinPad ", BalloonTipText, ToolTipIcon.Info);

                            aSoundPlayer.PlayLooping();
                        }
                        else
                        {
                            aSoundPlayer.Stop();
                            PinPadNotifyIcon.Visible = false;
                            return;
                        }
                    }
                    else
                    {
                        aSoundPlayer.Stop();
                        PinPadNotifyIcon.Visible = false;
                        return;
                    }
                }
                
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
            }
            finally
            {
                PinPadCon.Close();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Hide();
            Thread thread = new Thread(new ThreadStart(createNode));
            thread.IsBackground = true;
            thread.Name = "Flash for Override";
            thread.Start();
            startToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;
        }

        public void createNode()
        {
            while (true)
            {

                try
                {
                    if (AdminUser.roleid.Equals(AdminUser.OpsHeadRoleId))
                    {
                        CheckForPendingTransTeller(AdminUser.branchcode, AdminUser.TellerId, 1);
                    }
                    else
                    {

                        CheckForPendingTransTeller(AdminUser.branchcode, AdminUser.TellerId, 0);
                    }

                    Thread.Sleep(/* milliseconds to wait */ 20000); // Check every 20 seconds.
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.ToString());
                }
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PinPadNotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            if (AdminUser.roleid.Equals(AdminUser.OpsHeadRoleId) & BalloonTipText.Contains("WITHDRAWAL"))// Ops Head
            {
                ApproveTransactions newApproval = new ApproveTransactions();
                newApproval.ShowDialog();
            }
            if (AdminUser.roleid.Equals(AdminUser.OpsHeadRoleId) & BalloonTipText.Contains("DEPOSIT"))// Ops Head
            {
                ApproveDebitTransactions newApproval = new ApproveDebitTransactions();
                newApproval.ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            PinPadNotifyIcon.Visible = false;
            PinPadNotifyIcon.Dispose();
        }
    }
}