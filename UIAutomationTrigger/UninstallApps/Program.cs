using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UninstallApps
{
    class Program
    {
        private string PackageId;
        
        static void Main(string[] args)
        {
            string testid = "29310804E5D5";
            bool bResult = Program.CheckInstall(testid);
            Console.WriteLine("The result is {0}", bResult);
            return;

            if(args.Length != 1)
            {
                Console.WriteLine("Invalid arguments.");
                return;
            }

            Program program = new Program();
            program.PackageId = args[0]; 


        }

        private static bool CheckInstall(string id)
        {
            RegistryKey rkCurrentUser = Registry.CurrentUser;
            const string subkey = @"Software\Classes\ActivatableClasses\Package";
            RegistryKey rkPackage = rkCurrentUser.OpenSubKey(subkey);
            string[] keys = rkPackage.GetSubKeyNames();
            rkPackage.Close();
            rkCurrentUser.Close();
            Regex rx = new Regex(id, RegexOptions.Compiled|RegexOptions.IgnoreCase);
            
            foreach(string item in keys)
            {
                MatchCollection matches = rx.Matches(item);
                if(matches.Count > 0)
                {
                    Console.WriteLine(id + "->" + item);
                    return true;
                }
                
            }
            return false;
        }
    }
}
