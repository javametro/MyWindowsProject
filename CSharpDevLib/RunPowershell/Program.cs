using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunPowershell
{
    class Program
    {
        static void Main(string[] args)
        {
            string powershellName = @"powershell.exe";
            Program.LaunchPowershell(powershellName);
            return;
        }

        public static void LaunchPowershell(string powershellName)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.FileName = powershellName;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.Verb = "runas";
            startInfo.CreateNoWindow = false;
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            Process p = Process.Start(startInfo);
            p.StandardInput.WriteLine("set-executionpolicy remotesigned");
            p.StandardInput.WriteLine("y");
            p.StandardInput.WriteLine(@"cd c:\");
            p.StandardInput.WriteLine(@".\DisableAutoplay.ps1");
            p.StandardInput.AutoFlush = true;
            p.Close();
        }
    }
}
