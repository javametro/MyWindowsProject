using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.IO;

namespace mkrtesttool
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckNECModel();
            CheckNECSign();
            CheckFolder();

        }

        public static void CheckNECModel()
        {
            string value = "";
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey store_content_modifier = hklm.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Store\\");
            value = (string)store_content_modifier.GetValue("StoreContentModifier");

            if (value.CompareTo("Lenovo5_NECPC1") == 0)
            {
                Console.WriteLine("Consumer Model");
            }
            else if (value.CompareTo("Lenovo6_NECPC2") == 0)
            {
                Console.WriteLine("Commercial Model");
            }
            else
            {
                Console.WriteLine("Not NEC Model");
            }

        }

        public static void CheckNECSign()
        {
            string patten = @"B3CD3740.V2_\d.\d.\d.\d_x64__md25j3s46526j";
            Regex rgx = new Regex(patten, RegexOptions.IgnoreCase);
            string folder = "";
            string windows_apps = @"C:\Program Files\WindowsApps";
            DirectoryInfo dirs = new DirectoryInfo(windows_apps);
            if (dirs.Exists)
            {
                foreach(DirectoryInfo diritem in dirs.GetDirectories())
                {
                    MatchCollection matches = rgx.Matches(diritem.FullName);
                    if(matches.Count > 0)
                    {
                        foreach(Match match in matches)
                        {
                            folder = windows_apps + "\\" + match.Value;
                        }
                    }
                    
                }
                
            }

            Console.WriteLine(folder);

            string sigcheck_tool = @"C:\software\tools\sigcheck.exe";
            string cmdStr = " -e " + "\"" + folder + "\"";
            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = sigcheck_tool;
            exep.StartInfo.Arguments = cmdStr;
            exep.StartInfo.CreateNoWindow = false;
            exep.StartInfo.UseShellExecute = false;
            exep.Start();
            exep.WaitForExit();


        }


        public static void CheckFolder()
        {
            string[] drives = Environment.GetLogicalDrives();
            //    Console.WriteLine("GetLogicalDrives: {0}", String.Join(", ", drives));

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach(DriveInfo drive in allDrives)
            {
                //Console.WriteLine(drive.Name + "----" + drive.DriveType);
                if (!drive.DriveType.Equals(DriveType.CDRom))
                {
                    if (drive.Name.Equals("C:\\") || drive.Name.Equals("D:\\"))
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(drive.Name);
                        // Console.WriteLine(drive);
                        DirectoryInfo[] dirs = directoryInfo.GetDirectories();
                        foreach (DirectoryInfo dir in dirs)
                        {
                            Console.WriteLine(dir.FullName);
                        }
                    }
                }

                
            }
        }
    }

    
}
