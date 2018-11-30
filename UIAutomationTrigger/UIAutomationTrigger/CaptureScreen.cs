using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace UIAutomationTrigger
{
    class CaptureScreen
    {
        public Image getScreen()
        {
            Image myImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(myImage);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            IntPtr dc1 = g.GetHdc();
            g.ReleaseHdc(dc1);

            Random objRand = new Random();
            string pic_name = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".jpg";
            string path = @"C:\Users\stephen\document\";
            myImage.Save(path + pic_name);
            return myImage;
        }

        //public byte[] ScreenStream(Bitmap bm)
        //{
        //    System.IO.MemorySt
        //}
    }
}
