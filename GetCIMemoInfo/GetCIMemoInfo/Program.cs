using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;

// Get AppName, Version, Pin, InstallCommand
namespace GetCIMemoInfo
{
    class Program
    {
        //private string appName;
        //private string appVersion;
        //private string appUserModeId;
        //private string setupCommand;

        private const string OutputXmlPath = @"CIMemoInfo.xml";

        static void Main(string[] args)
        {
            //Get excel files
            string ciDir = @"C:\GithubRepository\MyWindowsProject\GetCIMemoInfo\CIMemo";
            string []files = Directory.GetFiles(ciDir);
            
            Program program = new Program();

            string[] infoArray = new string[4];

            program.CreateXmlTree(OutputXmlPath);

            foreach (string item in files)
            {
                Console.WriteLine("FileName:" + item);
                infoArray = program.GetCIMemoInfo(item);
                program.Create(OutputXmlPath, infoArray[0], infoArray[1], infoArray[2], infoArray[3]);
            }

            // string file = @"C:\GithubRepository\MyWindowsProject\GetCIMemoInfo\CIMemo\191Q_Alana_Beta_Audio1_01.xlsx";
            //infoArray = program.GetCIMemoInfo(file);

            //foreach(string str in infoArray)
            //{
            //    Console.WriteLine("tag:" + str);
            //}

            

            return;
            //foreach (string file in files)
            //{
            //    infoArray = program.GetCIMemoInfo(file);
            //    //Console.WriteLine(file);
            //}

            
            

            //Excel.Application xlApp = new Excel.Application();
            //Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\GithubRepository\MyWindowsProject\GetCIMemoInfo\GetCIMemoInfo\bin\Debug\191Q_Alana_Beta_SoftnaviLibrary_01.xlsx");
            //Excel._Worksheet xlWorkSheet = xlWorkbook.Sheets[1];
            //Excel.Range xlRange = xlWorkSheet.UsedRange;

            //string AppName = xlRange.Cells[12, 4].Value2.ToString();
            //string AppVersion = xlRange.Cells[12, 46].Value2.ToString();
            //string AppUserModeId = xlRange.Cells[12, 50].Value2.ToString();
            //string AppSetupMethod = xlRange.Cells[12, 33].Value2.ToString();

            //AppSetupMethod = AppSetupMethod.Replace('\n', ' ').Trim();

            //string setupMethodPattern = @"[^\d][a-z|A-Z|_|\\|\/|:|\.]+";
            //string setupMethodCommand = "";
            //foreach (Match match in Regex.Matches(AppSetupMethod, setupMethodPattern))
            //{
            //    setupMethodCommand += match.Value;
            //}

            //setupMethodCommand = setupMethodCommand.Trim();

            ////Console.WriteLine(setupMethodCommand);

            ////Console.WriteLine(AppName);
            ////Console.WriteLine(AppVersion);
            ////Console.WriteLine(AppUserModeId);
            ////Console.WriteLine(setupMethodCommand);

            //Program program = new Program();
            //program.CreateXmlTree(@"CIMemoInfo.xml", AppName, AppVersion, AppUserModeId, setupMethodCommand);

        }

        public void CreateXmlTree(string xmlPath) {

            XElement xElement = new XElement("CIMemoInfo", "");

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            XmlWriter xw = XmlWriter.Create(xmlPath, settings);
            xElement.Save(xw);
            xw.Flush();
            xw.Close();
        }

        public string[] GetCIMemoInfo(string fileName)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileName);
            Excel._Worksheet xlWorkSheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorkSheet.UsedRange;

            string AppName = xlRange.Cells[12, 4].Value2.ToString();
            string AppVersion = xlRange.Cells[12, 46].Value2.ToString();
            string AppUserModeId = xlRange.Cells[12, 50].Value2.ToString();
            string AppSetupMethod = xlRange.Cells[12, 33].Value2.ToString();

            AppSetupMethod = AppSetupMethod.Replace('\n', ' ').Trim();
            AppVersion = AppVersion.Replace('\n', ' ').Trim();
            string setupMethodPattern = @"[^\d][a-z|A-Z|_|\\|\/|:|\.]+";
            string setupMethodCommand = "";
            foreach (Match match in Regex.Matches(AppSetupMethod, setupMethodPattern))
            {
                setupMethodCommand += match.Value;
            }

            setupMethodCommand = setupMethodCommand.Trim();

            string[] infoArray = new string[4];
            infoArray[0] = AppName.Trim();
            infoArray[1] = AppVersion.Trim();
            infoArray[2] = AppUserModeId.Trim();
            infoArray[3] = setupMethodCommand.Trim();

            Console.WriteLine(fileName + ":" + "Name:" + infoArray[0] + "Version:" + infoArray[1] + "Id:" + infoArray[2] + "command:" + infoArray[3]);

            xlWorkbook.Close();
            xlApp.Quit();

            return infoArray;            
        }

        public void Create(string xmlPath, string appName, string appVersion, string appUserModeId, string setupCommand)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            var root = xmlDoc.DocumentElement;
            XmlElement appNameElement = xmlDoc.CreateElement("AppName", appName);
            root.AppendChild(appNameElement);

            XmlElement appVersionElement = xmlDoc.CreateElement("AppVersion");
            appVersionElement.InnerText = appVersion;

            XmlElement appUserModeIdElement = xmlDoc.CreateElement("AppUserModeId");
            appUserModeIdElement.InnerText = appUserModeId;

            XmlElement setupCommandElement = xmlDoc.CreateElement("SetupCommand");
            setupCommandElement.InnerText = setupCommand;
            //XmlNode appNameNode = xmlDoc.CreateNode(appName, /*appVersion*/"hello", "");
            // XmlNode appVersionNode = xmlDoc.CreateNode();

            // root.AppendChild(appNameElement);
            appNameElement.AppendChild(appVersionElement);
            appNameElement.AppendChild(appUserModeIdElement);
            appNameElement.AppendChild(setupCommandElement);

            xmlDoc.Save(xmlPath);
        }

    }
}
