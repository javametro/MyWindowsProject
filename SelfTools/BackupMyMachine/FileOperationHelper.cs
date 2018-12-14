using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BackupMyMachine
{
    class FileOperationHelper
    {
        private static string fileName = string.Empty;
        private static string destFile = string.Empty;
        private static string dirName = string.Empty;
        private static string destDir = string.Empty;
        public static void CopyFiles(string sourceDir, string destDir)
        {
            string[] files = System.IO.Directory.GetFiles(sourceDir);
            string[] dirs = System.IO.Directory.GetDirectories(sourceDir);

            foreach (string file in files)
            {
                System.IO.DirectoryInfo folder = Directory.GetParent(file);
                string folder_name = folder.Name;
                string target_folder = System.IO.Path.Combine(destDir, folder_name);
                if (!Directory.Exists(target_folder))
                {
                    Directory.CreateDirectory(target_folder);
                }
                
                fileName = System.IO.Path.GetFileName(file);
                destFile = System.IO.Path.Combine(destDir, folder_name, fileName);
                System.IO.File.Copy(file, destFile, true);
            }

            foreach(string dir in dirs)
            {
                string subDir = Path.Combine(destDir, dir);
                Directory.CreateDirectory(subDir);
                CopyFiles(dir, subDir);
            }
        }

    }
}
