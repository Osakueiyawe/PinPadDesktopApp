using System;
using System.Drawing;
using System.Windows.Forms;

namespace GTBankCardInterfaceApp
{
    public partial class Welcome : Form
    {
        frmMain frmNotifier = new frmMain();
        Transaction Trans = new Transaction();
        public Welcome()
        {
            InitializeComponent();
        }

        private void Welcome_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuViewCusDetails_Click(object sender, EventArgs e)
        {
            bool found = false;
            // get all of the MDI children in an array
            Form[] charr = MdiChildren;

            if (charr.Length == 0)
            {
                CustomerDetail newMDICusDetail = new CustomerDetail();
                newMDICusDetail.MdiParent = this;
                newMDICusDetail.StartPosition = FormStartPosition.Manual;
                newMDICusDetail.Location = new Point(0, 0);
                newMDICusDetail.Show();
            }
            else     // child forms are opened
            {
                foreach (Form mdiChild in MdiChildren)
                {
                    if (mdiChild.Name == "CustomerDetail")
                    {
                        mdiChild.WindowState = FormWindowState.Normal;
                        mdiChild.Activate();
                        found = true;
                        break;
                    }
                    else
                        found = false;
                }

                if (found == false)
                {
                    CustomerDetail newMDICusDetail = new CustomerDetail();
                    newMDICusDetail.MdiParent = this;
                    newMDICusDetail.StartPosition = FormStartPosition.Manual;
                    newMDICusDetail.Location = new Point(0, 0);
                    newMDICusDetail.Show();
                }
            }
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            if (AdminUser.IsCardLessAllowed.Equals("0"))
            {
                cardLessDepositsToolStripMenuItem.Visible = false;
                cardLessWithdrawalToolStripMenuItem.Visible = false;
            }
            else if (AdminUser.IsCardLessAllowed.Equals("1"))
            {
                cardLessDepositsToolStripMenuItem.Visible = true;
                cardLessWithdrawalToolStripMenuItem.Visible = true;
            }

            if (AdminUser.roleid.Equals(AdminUser.TellerRoleId))//Teller Login
            {
                frmNotifier.Show();
                tlStrpAdminReport.Visible = false;
                approvalScreenToolStripMenuItem.Enabled = false;
                depositApprovalScreenToolStripMenuItem.Enabled = false;
            }
            else if (AdminUser.roleid.Equals(AdminUser.DepositITRoleId))//Teller Login
            {
                tlStrpApprove.Visible = false; //cannot see the approval screen
                tlStrpAdminReport.Visible = false;
                mnuViewCusDetails.Enabled = false;
            }
            else if (AdminUser.roleid.Equals(AdminUser.OpsHeadRoleId)) //Ops Head Login
            {
                frmNotifier.Show();
                mnuViewCusDetails.Visible = false;
                tlStrpAdminReport.Visible = false;
                tlStrpGeneral.Visible = false;
                poToolStripMenuItem.Enabled = false;
                tlStrpApprove.Visible = true;

            }
            else if (AdminUser.roleid.Equals(AdminUser.AdminRoleId)) //Tech Audit Head Login
            {
                tlStrpApprove.Visible = false;
                tlStrpGeneral.Visible = false;
                tlStrpReport.Visible = true;  //
                tlStrpAdminReport.Visible = true;
            }

            if (AdminUser.LoginMode.Equals("Offline"))
            {
                tlStrpApprove.Enabled = false;
                mnuViewCusDetails.Enabled = false;
                toolStripDeposit.Enabled = false;
            }

            lblUserName.Text = AdminUser.username.ToUpper() + " - " + AdminUser.branchcode;
        }

        private void pinPadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void accountNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void approvalScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool found = false;
            // get all of the MDI children in an array
            Form[] charr = this.MdiChildren;

            if (charr.Length == 0)     // no child form is opened
            {
                ApproveTransactions newMDICusDetail = new ApproveTransactions();
                newMDICusDetail.MdiParent = this;
                // The StartPosition property is essential
                // for the location property to work
                newMDICusDetail.StartPosition = FormStartPosition.Manual;
                newMDICusDetail.Location = new Point(0, 0);
                newMDICusDetail.Show();
            }
            else     // child forms are opened
            {
                foreach (Form mdiChild in this.MdiChildren)
                {
                    if (mdiChild.Name == "ApproveTransactions")
                    {
                        mdiChild.WindowState = FormWindowState.Normal;
                        mdiChild.Activate();
                        found = true;
                        break;   // exit loop
                    }
                    else
                        found = false;      // make sure flag is set to false if the form is not found
                }

                if (found == false)
                {
                    ApproveTransactions newMDICusDetail = new ApproveTransactions();
                    newMDICusDetail.MdiParent = this;
                    // The StartPosition property is essential
                    // for the location property to work
                    newMDICusDetail.StartPosition = FormStartPosition.Manual;
                    newMDICusDetail.Location = new Point(0, 0);
                    newMDICusDetail.Show();
                }
            }




        }

        private void transactionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length > 0)
            {
                foreach (Form mdiChild in this.MdiChildren)
                {
                    if (mdiChild.Name == "TransactionHistory")
                    {
                        mdiChild.Activate();
                        mdiChild.WindowState = FormWindowState.Normal;
                        return;
                    }
                    else
                    {
                        TransactionHistory newMDICusDetail = new TransactionHistory();
                        newMDICusDetail.MdiParent = this;
                        newMDICusDetail.WindowState = FormWindowState.Normal;
                        newMDICusDetail.Show();
                    }
                }
            }
            else
            {
                TransactionHistory newMDICusDetail = new TransactionHistory();
                newMDICusDetail.MdiParent = this;
                newMDICusDetail.WindowState = FormWindowState.Normal;
                newMDICusDetail.Show();
            }
        }

        private void printReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length > 0)
            {
                foreach (Form mdiChild in this.MdiChildren)
                {
                    if (mdiChild.Name == "PrintPendingReceipt")
                    {
                        mdiChild.Activate();
                        return;
                    }
                    else
                    {
                        PrintPendingReceipt newMDICusDetail = new PrintPendingReceipt();
                        newMDICusDetail.MdiParent = this;
                        newMDICusDetail.Show();
                    }
                }
            }
            else
            {
                PrintPendingReceipt newMDICusDetail = new PrintPendingReceipt();
                newMDICusDetail.MdiParent = this;
                newMDICusDetail.Show();
            }
        }

        private void toolStripDeposit_Click(object sender, EventArgs e)
        {
            bool found = false;
            // get all of the MDI children in an array
            Form[] charr = this.MdiChildren;

            if (charr.Length == 0)     // no child form is opened
            {
                CardHolderDeposit newMDICusDetail = new CardHolderDeposit();
                newMDICusDetail.MdiParent = this;
                newMDICusDetail.StartPosition = FormStartPosition.Manual;
                newMDICusDetail.Location = new Point(0, 0);
                newMDICusDetail.Show();
            }
            else
            {
                foreach (Form mdiChild in this.MdiChildren)
                {
                    if (mdiChild.Name == "CardHolderDeposit")
                    {
                        mdiChild.WindowState = FormWindowState.Normal;
                        mdiChild.Activate();
                        found = true;
                        break;
                    }
                    else
                        found = false;
                }

                if (found == false)
                {
                    CardHolderDeposit newMDICusDetail = new CardHolderDeposit();
                    newMDICusDetail.MdiParent = this;
                    newMDICusDetail.StartPosition = FormStartPosition.Manual;
                    newMDICusDetail.Location = new Point(0, 0);
                    newMDICusDetail.Show();
                }
            }
        }

        private void tlStrpGeneral_Click(object sender, EventArgs e)
        {
            Utilities u = new Utilities();
            u.ToggleAuthMode();
            if (AdminUser.LoginMode.Equals("Offline"))
            {
                tlStrpApprove.Enabled = false;
                mnuViewCusDetails.Enabled = false;
                toolStripDeposit.Enabled = false;
                Text = "GTBank Card Interface Application - Welcome " + AdminUser.LoginMode;
            }
            else if (AdminUser.LoginMode.Equals("Online") & (AdminUser.roleid.Equals(AdminUser.OpsHeadRoleId) || AdminUser.roleid.Equals(AdminUser.TellerRoleId)))
            {
                tlStrpApprove.Enabled = true;
                mnuViewCusDetails.Enabled = true;
                toolStripDeposit.Enabled = true;
                Text = "GTBank Card Interface Application - Welcome " + AdminUser.LoginMode;
            }
        }

        private void Welcome_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F8) & (AdminUser.roleid.Equals(AdminUser.OpsHeadRoleId)))
            {
                ApproveTransactions newApproval = new ApproveTransactions();
                newApproval.ShowDialog();
            }
        }

        private void manageLogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageLogo newLogo = new ManageLogo();
            newLogo.ShowDialog();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmNotifier.Close();
            Close();
            Application.Exit();
        }

        private void Welcome_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void depositApprovalScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool found = false;
            // get all of the MDI children in an array
            Form[] charr = this.MdiChildren;

            if (charr.Length == 0)     // no child form is opened
            {
                ApproveDebitTransactions newMDICusDetail = new ApproveDebitTransactions();
                newMDICusDetail.MdiParent = this;

                newMDICusDetail.StartPosition = FormStartPosition.Manual;
                newMDICusDetail.Location = new Point(0, 0);
                newMDICusDetail.Show();
            }
            else     // child forms are opened
            {
                foreach (Form mdiChild in this.MdiChildren)
                {
                    if (mdiChild.Name == "ApproveDebitTransactions")
                    {
                        mdiChild.WindowState = FormWindowState.Normal;
                        mdiChild.Activate();
                        found = true;
                        break;   // exit loop
                    }
                    else
                        found = false;      // make sure flag is set to false if the form is not found
                }

                if (found == false)
                {
                    ApproveDebitTransactions newMDICusDetail = new ApproveDebitTransactions();
                    newMDICusDetail.MdiParent = this;

                    newMDICusDetail.StartPosition = FormStartPosition.Manual;
                    newMDICusDetail.Location = new Point(0, 0);
                    newMDICusDetail.Show();
                }
            }
        }

        private void poToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool found = false;
            // get all of the MDI children in an array
            Form[] charr = this.MdiChildren;

            if (charr.Length == 0)     // no child form is opened
            {
                PostOfflineApprovedDebitTransactions newMDICusDetail = new PostOfflineApprovedDebitTransactions();
                newMDICusDetail.MdiParent = this;

                newMDICusDetail.StartPosition = FormStartPosition.Manual;
                newMDICusDetail.Location = new Point(0, 0);
                newMDICusDetail.Show();
            }
            else     // child forms are opened
            {
                foreach (Form mdiChild in this.MdiChildren)
                {
                    if (mdiChild.Name == "PostOfflineApprovedDebitTransactions")
                    {
                        mdiChild.WindowState = FormWindowState.Normal;
                        mdiChild.Activate();
                        found = true;
                        break;   // exit loop
                    }
                    else
                        found = false;      // make sure flag is set to false if the form is not found
                }

                if (found == false)
                {
                    PostOfflineApprovedDebitTransactions newMDICusDetail = new PostOfflineApprovedDebitTransactions();
                    newMDICusDetail.MdiParent = this;

                    newMDICusDetail.StartPosition = FormStartPosition.Manual;
                    newMDICusDetail.Location = new Point(0, 0);
                    newMDICusDetail.Show();
                }
            }
        }

        private void tlStrpApprove_Click(object sender, EventArgs e)
        {
            Utilities u = new Utilities();
            u.ToggleAuthMode();

            if (AdminUser.LoginMode.Equals("Offline"))
            {
                tlStrpApprove.Enabled = false;
                mnuViewCusDetails.Enabled = false;
                toolStripDeposit.Enabled = false;
                this.Text = "GTBank Card Interface Application - Welcome " + AdminUser.LoginMode;

            }
            else if (AdminUser.LoginMode.Equals("Online") & (AdminUser.roleid.Equals(AdminUser.OpsHeadRoleId) || AdminUser.roleid.Equals(AdminUser.TellerRoleId)))
            {
                tlStrpApprove.Enabled = true;
                mnuViewCusDetails.Enabled = true;
                toolStripDeposit.Enabled = true;
                this.Text = "GTBank Card Interface Application - Welcome " + AdminUser.LoginMode;
            }
        }

        private void resetReceiptForReprintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool found = false;
            // get all of the MDI children in an array
            Form[] charr = this.MdiChildren;

            if (charr.Length == 0)     // no child form is opened
            {
                ResetReceipt newMDICusDetail = new ResetReceipt();
                newMDICusDetail.MdiParent = this;

                newMDICusDetail.StartPosition = FormStartPosition.Manual;
                newMDICusDetail.Location = new Point(0, 0);
                newMDICusDetail.Show();
            }
            else     // child forms are opened
            {
                foreach (Form mdiChild in this.MdiChildren)
                {
                    if (mdiChild.Name == "ResetReceipt")
                    {
                        mdiChild.WindowState = FormWindowState.Normal;
                        mdiChild.Activate();
                        found = true;
                        break;   // exit loop
                    }
                    else
                        found = false;      // make sure flag is set to false if the form is not found
                }

                if (found == false)
                {
                    ResetReceipt newMDICusDetail = new ResetReceipt();
                    newMDICusDetail.MdiParent = this;
                    newMDICusDetail.StartPosition = FormStartPosition.Manual;
                    newMDICusDetail.Location = new Point(0, 0);
                    newMDICusDetail.Show();
                }
            }
        }

        private void cardLessDepositsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool found = false;
            // get all of the MDI children in an array
            Form[] charr = this.MdiChildren;

            if (charr.Length == 0)     // no child form is opened
            {
                CardLessDeposit newMDICusDetail = new CardLessDeposit();
                newMDICusDetail.MdiParent = this;
                newMDICusDetail.StartPosition = FormStartPosition.Manual;
                newMDICusDetail.Location = new Point(0, 0);
                newMDICusDetail.Show();
            }
            else
            {
                foreach (Form mdiChild in this.MdiChildren)
                {
                    if (mdiChild.Name == "CardLessDeposit")
                    {
                        mdiChild.WindowState = FormWindowState.Normal;
                        mdiChild.Activate();
                        found = true;
                        break;   // exit loop
                    }
                    else
                        found = false;      // make sure flag is set to false if the form is not found
                }

                if (found == false)
                {
                    CardLessDeposit newMDICusDetail = new CardLessDeposit();
                    newMDICusDetail.MdiParent = this;
                    newMDICusDetail.StartPosition = FormStartPosition.Manual;
                    newMDICusDetail.Location = new Point(0, 0);
                    newMDICusDetail.Show();
                }
            }
        }

        private void cardLessWithdrawalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool found = false;
            // get all of the MDI children in an array
            Form[] charr = MdiChildren;

            if (charr.Length == 0)     // no child form is opened
            {
                CardLessWithdrawal newMDICusDetail = new CardLessWithdrawal();
                newMDICusDetail.MdiParent = this;
                newMDICusDetail.StartPosition = FormStartPosition.Manual;
                newMDICusDetail.Location = new Point(0, 0);
                newMDICusDetail.Show();
            }
            else
            {
                foreach (Form mdiChild in this.MdiChildren)
                {
                    if (mdiChild.Name == "CardLessWithdrawal")
                    {
                        mdiChild.WindowState = FormWindowState.Normal;
                        mdiChild.Activate();
                        found = true;
                        break;   // exit loop
                    }
                    else
                        found = false;      // make sure flag is set to false if the form is not found
                }

                if (found == false)
                {
                    CardLessWithdrawal newMDICusDetail = new CardLessWithdrawal();
                    newMDICusDetail.MdiParent = this;
                    newMDICusDetail.StartPosition = FormStartPosition.Manual;
                    newMDICusDetail.Location = new Point(0, 0);
                    newMDICusDetail.Show();
                }
            }
        }

        private void cardLessWithDrawalMobileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool found = false;
            // get all of the MDI children in an array
            Form[] charr = this.MdiChildren;

            if (charr.Length == 0)     // no child form is opened
            {
                FastTrackWithdrawal newMdiCusDetail = new FastTrackWithdrawal();
                newMdiCusDetail.MdiParent = this;
                newMdiCusDetail.StartPosition = FormStartPosition.Manual;
                newMdiCusDetail.Location = new Point(0, 0);
                newMdiCusDetail.Show();
            }
            else
            {
                foreach (Form mdiChild in this.MdiChildren)
                {
                    if (mdiChild.Name == "FastTrackWithdrawal")
                    {
                        mdiChild.WindowState = FormWindowState.Normal;
                        mdiChild.Activate();
                        found = true;
                        break;   // exit loop
                    }
                    else
                        found = false;      // make sure flag is set to false if the form is not found
                }

                if (found == false)
                {
                    FastTrackWithdrawal newMDICusDetail = new FastTrackWithdrawal();
                    newMDICusDetail.MdiParent = this;
                    newMDICusDetail.StartPosition = FormStartPosition.Manual;
                    newMDICusDetail.Location = new Point(0, 0);
                    newMDICusDetail.Show();
                }
            }
        }

        private void cardLessDepositMobileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool found = false;
            // get all of the MDI children in an array
            Form[] charr = this.MdiChildren;

            if (charr.Length == 0)     // no child form is opened
            {
                FastTrackDeposit newMdiCusDetail = new FastTrackDeposit();
                newMdiCusDetail.MdiParent = this;
                newMdiCusDetail.StartPosition = FormStartPosition.Manual;
                newMdiCusDetail.Location = new Point(0, 0);
                newMdiCusDetail.Show();
            }
            else
            {
                foreach (Form mdiChild in this.MdiChildren)
                {
                    if (mdiChild.Name == "FastTrackDeposit")
                    {
                        mdiChild.WindowState = FormWindowState.Normal;
                        mdiChild.Activate();
                        found = true;
                        break;   // exit loop
                    }
                    else
                        found = false;
                }

                if (found == false)
                {
                    FastTrackDeposit newMDICusDetail = new FastTrackDeposit();
                    newMDICusDetail.MdiParent = this;
                    newMDICusDetail.StartPosition = FormStartPosition.Manual;
                    newMDICusDetail.Location = new Point(0, 0);
                    newMDICusDetail.Show();
                }
            }
        }
    }
}