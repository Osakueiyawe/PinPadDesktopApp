using System;
using System.IO;
using System.Globalization;
using System.Configuration;

namespace GTBankCardInterfaceApp
{
    /// <summary>
    /// Summary description for ErrHandler
    /// Handles error by acception the error message
    /// Displays the page on which the error occured
    /// </summary>
    public class ErrHandler
    {
        public ErrHandler()
        {

        }

        public static void WriteError(string errorMessage)
        {
            try
            {
                string errorfolder = ConfigurationManager.AppSettings["ErrorFolder"];

                string errorPath = errorfolder + DateTime.Today.ToString("dd-MM-yy");

                if (!Directory.Exists(errorPath))
                {
                    Directory.CreateDirectory(errorPath);
                }

                string path = errorPath + "/" + "PinPadLog" + ".txt";

                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine("\r\nLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    string err = "Activity Log. Message: " + errorMessage;
                    w.WriteLine(err);
                    w.WriteLine("_______________________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                //WriteError(ex.Message);
            }
        }
    }
}