using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BackupMyMachine
{
    class UsbDriveInfo : INotifyPropertyChanged
    {
        private string volume;
        public string Volume
        {
            get
            {
                return volume;
            }

            set
            {
                if(volume != value)
                {
                    volume = value;
                }

                PropertyChanged(this, new PropertyChangedEventArgs("Volume"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
