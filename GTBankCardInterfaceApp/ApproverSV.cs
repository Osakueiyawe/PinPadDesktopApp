using System;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using Leadtools.Codecs;
using System.IO;
using Leadtools.WinForms;

namespace GTBankCardInterfaceApp
{
    public partial class ApproverSV : Form
    {
        public ApproverSV()
        {
            InitializeComponent();
        }

        private void ApproverSV_Load(object sender, EventArgs e)
        {
            AppDevService appserve = new AppDevService();
            if (AdminUser.SVAccountNumber.Trim().Length == 10)
            {
                string cusnumber = appserve.ConvertToOldAccountNumber(AdminUser.SVAccountNumber.Trim());
                getCustomerDetails(Convert.ToInt16(cusnumber.Substring(0, 3)), Convert.ToInt32(cusnumber.Substring(3, 6)));
            }
            else
            {
                string cusnumber = AdminUser.SVAccountNumber.Replace("/", "");
                getCustomerDetails(Convert.ToInt16(cusnumber.Substring(0, 3)), Convert.ToInt32(cusnumber.Substring(3, 6)));
            }
        }

        public bool getCustomerDetails(int bracode, int cusnum)
        {
            RasterCodecs codec;
            Utilities u = new Utilities();
            CustDetRetVal custdetails = null;
            byte[] imagedata = new byte[8056];
            object[] Images = null;
            int noofimages = 0;
            int i = 0;
            int appid = Convert.ToInt16(AdminUser.ApplicationID);
            AppDevService appserve = new AppDevService();
            string[] accounts = null;
            string[] mandates = null;
           
            Images = null;
            noofimages = 0;
            XmlDocument document = null;
            XPathNavigator navigator = null;
            XPathNodeIterator snodes = null;
            string retcode = null;
            custdetails = appserve.GetBasisCustomerDetails(bracode, cusnum);
            document = new XmlDocument();
            document.LoadXml(custdetails.custdet.Replace("&", " and "));
            navigator = document.CreateNavigator();
            snodes = navigator.Select("/Response/CODE");
            snodes.MoveNext();
            retcode = snodes.Current.Value;

            if (retcode != "1000")
            {
                snodes = navigator.Select("/Response/ERROR");
                snodes.MoveNext();
                return false;
            }

            accounts = custdetails.Accounts;
            Images = custdetails.picture;
            mandates = custdetails.Mandates;
            noofimages = Images.Length;

            if (!(Images == null))
            {
                foreach (object pix in Images)
                {
                    imagedata = (byte[])pix;
                    MemoryStream myImage = new MemoryStream(imagedata, true);
                    codec = new RasterCodecs();
                    RasterImageListItem a = new RasterImageListItem();
                    a.Image = codec.Load(myImage);
                    a.Text = mandates[i];
                    rasterImageList1.Items.Add(a);
                    i++;
                }
            }
            else
            {
                MessageBox.Show("No Image For the customer");
            }
            snodes = navigator.Select("/Response/CUSTOMERS/CUSTOMER/SIGNATORY_NAME");
            snodes.MoveNext();
            lblAcctName.Text = snodes.Current.Value;
            return true;
        }
    }
}