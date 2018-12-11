using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;
using Windows.UI.Notifications;
using System.Windows.Forms;



namespace AlarmTimer
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;

            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //string toastString = "<toast><visual><binding template=\"ToastImageAndText01\"><text id=\"1\"> Default String </text></binding></visual></toast>";
            //Windows.Data.Xml.Dom.XmlDocument tileXml = new Windows.Data.Xml.Dom.XmlDocument();
            //tileXml.LoadXml(toastString);
            //var toast = new ToastNotification((tileXml));
            //ToastNotificationManager.CreateToastNotifier().Show(toast);
            //  Console.WriteLine("Hello World");
            MessageBox.Show("Test for AlarmTimer", "AlarmTimer", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

        }
    }

    
}
