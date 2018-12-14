using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupMyMachine
{
    class DirectoryInfo : INotifyPropertyChanged
    {
        private string folder_name;
        public string Folder_Name
        {
            get
            {
                return folder_name;
            }

            set
            {
                if(folder_name != value)
                {
                    folder_name = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Folder_Name"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
