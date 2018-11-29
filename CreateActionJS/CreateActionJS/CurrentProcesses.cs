using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CreateActionJS
{
    class CurrentProcesses : ObservableCollection<string>
    {
        public ObservableCollection<string> appsMainProcess;

        public CurrentProcesses()
        {
            appsMainProcess = new ObservableCollection<string>();
            Process[] processArray = Process.GetProcesses();
            foreach(Process item in processArray)
            {
                appsMainProcess.Add(item.ProcessName);
            }
        }

        
    }
}
