using Dermalog.Afis.ImageContainer;
using Dermalog.Imaging.Capturing;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GTBankCardInterfaceApp
{
    public partial class FingerPrintValidation : Form
    {
        FingerPrint fingerPrint = new FingerPrint();

        public FingerPrintValidation()
        {
            try
            {
                InitializeComponent();

               fingerPrint._capDevice = DeviceManager.GetDevice(DeviceIdentity.FG_ZF1);

                fingerPrint.InitializeDevice();
            }
            catch (DeviceErrorException ex)
            {
                ErrHandler.WriteError("An exception occurred while trying to initialize device. Message - " + ex.Message + "|StackTrace - " + ex.StackTrace);

                if (ex.Message.StartsWith("The VC3 module could not be loaded in memory"))
                {
                    MessageBox.Show("Please confirm that the identity of the device is FG_ZF1.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    DialogResult = DialogResult.Abort;

                    Close();
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError("An exception occurred while trying to initialize device. Message - " + ex.Message + "|StackTrace - " + ex.StackTrace);

                if (ex.Message.StartsWith("The VC3 module could not be loaded in memory"))
                {
                    MessageBox.Show("Please confirm that the identity of the device is FG_ZF1.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    DialogResult = DialogResult.Abort;

                    Close();                  
                }
            }
        }

        private void BtnSubmitPhoneNumber_Click(object sender, EventArgs e)
        {
            try
            {
                AdminUser.CustomerPhoneNumber = txtPhoneNumber.Text.Trim();
                AdminUser.TransAmount = Convert.ToDouble(txtAmount.Text.Trim());

                DialogResult = DialogResult.OK;

                Close();
            }
            catch(Exception ex)
            {
                ErrHandler.WriteError("An exception occurred while trying to submit phone number. Message - " + ex.Message + "|StackTrace - " + ex.StackTrace);
            }
        }

        private void Btn_Validate_FingerPrint_Click(object sender, EventArgs e)
        {
            Btn_Validate_FingerPrint.Text = "Validating . . .";

            try
            {
                if (txtPhoneNumber.Text == string.Empty)
                {
                    Btn_Validate_FingerPrint.Text = "Validate";
                    MessageBox.Show("Please enter customer's registered phone number.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtPhoneNumber.Text.Length != 11)
                {
                    Btn_Validate_FingerPrint.Text = "Validate";
                    MessageBox.Show("Invalid phone number length.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool isValidPhoneNumber = double.TryParse(txtPhoneNumber.Text.Trim(), out double tempPhoneNumber);

                if (!isValidPhoneNumber)
                {
                    Btn_Validate_FingerPrint.Text = "Validate";
                    MessageBox.Show("Invalid phone number.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtAmount.Text == string.Empty)
                {
                    Btn_Validate_FingerPrint.Text = "Validate";
                    MessageBox.Show("Please enter amount.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool isValidAmount = double.TryParse(txtAmount.Text.Trim(), out double tempAmount);

                if (!isValidAmount)
                {
                    Btn_Validate_FingerPrint.Text = "Validate";
                    MessageBox.Show("Please enter a valid amount.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if(Convert.ToDouble(txtAmount.Text.Trim()) < 500)
                {
                    Btn_Validate_FingerPrint.Text = "Validate";
                    MessageBox.Show("Amount cannot be less than five hundred naira.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string base64ConvertedString = string.Empty;

                fingerPrint._capDevice.CaptureMode = CaptureMode.LIVE_IMAGE;

                if (fingerPrint._capDevice != null && fingerPrint._capDevice.IsCapturing && fingerPrint._capDevice.CaptureMode == CaptureMode.LIVE_IMAGE)
                {
                    fingerPrint._capDevice.Freeze(true);

                    var image = fingerPrint._capDevice.GetImage();

                    if (image != null)
                    {
                        Bitmap bitmap = (Bitmap)image;

                        try
                        {
                            RawImage rawImage = RawImageHelperForms.FromBitmap(bitmap);
                            fingerPrint.encoder.Format = Dermalog.Afis.FingerCode3.Enums.TemplateFormat.ISO19794_2_2005;
                            fingerPrint.encoder.ImpressionType = 0;
                            fingerPrint.encoder.FingerPosition = 1;
                            fingerPrint.rightTemplate = fingerPrint.encoder.Encode(bitmap);
                            fingerPrint.rightTemplate.GetData();

                            base64ConvertedString = Convert.ToBase64String(fingerPrint.rightTemplate.Data);

                            Btn_Validate_FingerPrint.Enabled = false;
                            Btn_Validate_FingerPrint.Text = "Click on Submit";
                            btnSubmitPhoneNumber.Enabled = true;
                            txtAmount.Enabled = false;
                            txtPhoneNumber.Enabled = false;

                            fingerPrint._capDevice.Stop();
                            fingerPrint._capDevice.Dispose();

                        }
                        catch (Exception ex)
                        {
                            ErrHandler.WriteError("An exception occured while trying to convert to ISO Template. Message - " + ex.Message + "|Stacktrace - " + ex.StackTrace);

                            if (ex.Message.StartsWith("No finger found"))
                            {
                                fingerPrint._capDevice.Stop();
                                fingerPrint._capDevice.Dispose();
                                MessageBox.Show("Finger image was not captured. Please try again.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                DialogResult = DialogResult.Cancel;
                                Close();
                            }
                        }
                    }
                    else
                    {
                        Btn_Validate_FingerPrint.Text = "Validate";
                        return;
                    }

                    AdminUser.FingerPrintImage = base64ConvertedString;
                }
            }
            catch (DeviceErrorException ex)
            {
                ErrHandler.WriteError(ex.ToString());
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError("An exception occurred in Btn_Validate_FingerPrint_Click. Message - " + ex.Message + "|StackTrace - " + ex.StackTrace);
            }
        }
    }
}