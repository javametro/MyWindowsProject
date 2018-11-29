using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using SHDocVw;
using System.Windows.Forms;

namespace CheckResult
{
    class Program
    {
        private enum CheckType
        {
            CHECK_PROCESS,
            CHECK_FILE
        }

        static void Main(string[] args)
        {
            bool bResult = false;
            string param = "";

            Program main = new Program();

            main.GetUrlFromBrowser();
            return;

            if (args.Length != 2)
            {
                Console.WriteLine("Argument is not correct.");
                return ;
            }

            Int32 arg1 = 0;
            Int32.TryParse(args[0], out arg1);

            if(arg1 == 0)
            {
                param = args[1];
                bResult = main.CheckProcess(param);
                Console.WriteLine("{0} is Exist? {1}", param, bResult);
            }
            else
            {
                param = args[1];
                bResult = main.CheckFileExist(param);
                Console.WriteLine("{0} is Exist? {1}", param, bResult);
            }
            
            

            
        }

        private bool CheckProcess(string process_name)
        {
            Process[] process_list = Process.GetProcesses();
            bool bRet = Array.Exists(process_list, element => element.ProcessName.Equals(process_name));
            return bRet;
        }

        private bool CheckFileExist(string filePath)
        {
            bool bRet = false;
            bRet = File.Exists(filePath);
            return bRet;
        }

        private string GetUrlFromBrowser()
        {
            //InternetExplorer browser;
            SHDocVw.WebBrowser browser;
            string myLocalLink;
            mshtml.IHTMLDocument2 mydoc;
            ShellWindows shellWindows = new ShellWindows();
           
            string filename;
            foreach (SHDocVw.WebBrowser ie in shellWindows)
            {
                filename = System.IO.Path.GetFileNameWithoutExtension(ie.FullName).ToLower();
                //if ((filename == "iexplore"))
                //{
                //    browser = ie;
                //    mydoc = browser.Document;
                //    myLocalLink = mydoc.url;
                //    MessageBox.Show(myLocalLink);
                //}
                /*else*/ if((filename == "MicrosoftEdge")){
                    MessageBox.Show("Hello");
                }
            }
            return "";
        }
    }
}
