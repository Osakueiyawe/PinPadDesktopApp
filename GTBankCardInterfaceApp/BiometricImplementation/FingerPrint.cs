using Dermalog.Imaging.Capturing;
using System;
using System.Drawing;
using static GTBankCardInterfaceApp.FingerPrint.ImplementDevice;

namespace GTBankCardInterfaceApp
{
    public class FingerPrint
    {
        public Device _capDevice;
        public Dermalog.Afis.FingerCode3.Encoder encoder = new Dermalog.Afis.FingerCode3.Encoder();
        public Dermalog.Afis.FingerCode3.Template rightTemplate;

        public void InitializeDevice()
        {
            CaptureMode mode = CaptureMode.LIVE_IMAGE;

            BindEvents();
        }

        private void BindEvents()
        {
            _capDevice.Start();

            Onstart onstart = new Onstart(_capDevice_OnStart);

            _capDevice.OnImage += new OnImage(_capDevice_OnImage);
            _capDevice.OnDetect += new OnDetect(_capDevice_OnDetect);

            //this._capDevice.OnDeviceEvent += new DeviceEvent(_capDevice_OnDeviceEvent);
            //this._capDevice.OnError += new OnError(_capDevice_OnError);
            //this._capDevice.OnWarning += new OnWarning(_capDevice_OnWarning);
            //this._capDevice.OnStop += new OnStop(_capDevice_OnStop);
        }

        private void _capDevice_OnStart(object sender, DeviceEventBaseArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrHandler.WriteError("Än exception occurred in _capDevice_OnStart. Message - " + ex.Message + "|Stacktrace - " + ex.StackTrace);
            }
        }

        void _capDevice_OnImage(object sender, ImageEventArgs e)
        {

        }

        void _capDevice_OnDetect(object sender, DetectEventArgs e)
        {
          
        }

        public class ImplementDevice : Device
        {
            public void Dispose() { }

            public Property Property { get; set; }
            public CaptureMode[] CaptureModes { get; }

            public void Start() { }

            public void Stop() { }

            public DialogType[] AvailableDialogs()
            {
                var dialogTypes = new DialogType[] { };

                return dialogTypes;
            }

            public void ClearEventSubscriptions() { }

            public CaptureMode CaptureMode { get; set; }

            public void Freeze(bool status) { }

            public CameraType[] GetCameraTypes()
            {
                var camType = new CameraType[] { };

                return camType;
            }

            public object GetCurrentFrameInfo(FrameInfoTypes frameInfoType)
            {
                return null;
            }

            public DeviceInformations[] GetDeviceInformations()
            {
                var device = new DeviceInformations[] { };
                return device;
            }

            public Image GetImage() { return null; }

            public delegate void Onstart(object sender, DeviceEventArgs e);

            public void ShowDialog(DialogType dlgType) { }

            public event OnStart OnStart;

            public event OnStop OnStop;

            public event OnImage OnImage;

            public event OnError OnError;

            public event DeviceEvent OnDeviceEvent;

            public event OnWarning OnWarning;

            public event OnDetect OnDetect;

            //
            // Summary:
            //     Gets or sets the type of camera
            public CameraType CameraType { get; set; }
            //
            // Summary:
            //     Gets or sets active channel
            public uint Channel { get; set; }
            //
            // Summary:
            //     Gets the Device Identity
            public DeviceIdentity DeviceID { get; }
            //
            // Summary:
            //     Gets the contamination level of device Not all devices support this property
            //
            // Exceptions:
            //   T:Dermalog.Imaging.Capturing.DeviceException:
            public double Contamination { get; }
            //
            // Summary:
            //     Gets/Sets the color mode for the camera.
            public ColorMode ColorMode { get; set; }
            //
            // Summary:
            //     Sets Text customer information on LCD display Not all devices support this property
            //
            // Exceptions:
            //   T:System.NotImplementedException:
            //     The Nativ SDK doesn't support this feature. you may use older version of native
            //     SDK
            //
            //   T:Dermalog.Imaging.Capturing.DeviceException:
            //     For more detail refer to Dermalog.Imaging.Capturing.DeviceException
            public string DisplayText { get; set; }
            //
            // Summary:
            //     Gets a value indicating whether the device is freezed or not
            public bool IsFreezed { get; }
            //
            // Summary:
            //     Gets a value indicating whether determines that the device is capureing images
            //     or not
            public bool IsCapturing { get; }
            //
            // Summary:
            //     Gets available color modus.Dermalog.Imaging.Capturing.Device.ColorMode
            //
            // Exceptions:
            //   T:Dermalog.Imaging.Capturing.DeviceException:
            public ColorMode[] ColorModes { get; }
        }
    }
}