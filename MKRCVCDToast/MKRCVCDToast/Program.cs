using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCDiagnosisToastLib;
using System.Windows.Forms;

namespace MKRCVCDToast
{
    class Program
    {

        private ToastType toast11 = null;
        private static ToastUtility toastTool = null;
        static void Main(string[] args)
        {
            toastTool = new PCDiagnosisToastLib.ToastUtility();
            Program test = new Program();
            test.RegisterToast1();
            test.ShowToast1();

        }


        private void RegisterToast1()
        {
            if (toast11 == null)
            {
                toast11 = new PCDiagnosisToastLib.ToastType();
                toast11.TemplateTypeEx = PCDiagnosisToastLib.ToastTemplateTypeEx.ImageAndSingleCaptionSingleText;
                toast11.ImagePath = @"C:\1.png";
                toast11.Caption = "Good";
                toast11.Text = "please create dvd or usb key for your system.";
                toast11.ToastActivated += msg1;
                toast11.ToastDismissed += msg1_dismissed;
                toast11.Identifier = "ID_2";
                toastTool.RegisterToast(toast11);
            }
        }


        private void ShowToast1()
        {
            if (toastTool == null)
            {
                return;
            }

            if (toast11 != null)
            {
                toastTool.ShowToast(toast11);
            }
        }


        private void msg1(object sender)
        {
            MessageBox.Show("Active in TestToast1");
        }

        private void msg1_dismissed(object sender)
        {
            MessageBox.Show("Dismissed in TestToast1");
        }


    }
}
