using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Threading;

namespace BackupMyMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<DirectoryInfo> folder_collection;
        private ObservableCollection<UsbDriveInfo> usb_collection;
        private UsbDriveInfo targetDrive;
        public MainWindow()
        {
            InitializeComponent();
          
            folder_collection = new ObservableCollection<DirectoryInfo>();
            usb_collection = new ObservableCollection<UsbDriveInfo>();
            ShowConnectedUsbVolume();
            this.folder_list.ItemsSource = folder_collection;
            this.usb_volumes.ItemsSource = usb_collection;
        }

        public void ShowConnectedUsbVolume()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            
            foreach(DriveInfo drive in allDrives)
            {
                if (drive.DriveType.Equals(DriveType.Removable) || drive.DriveType.Equals(DriveType.Fixed))
                {
                    UsbDriveInfo usb = new UsbDriveInfo();
                    usb.Volume = drive.Name;
                    usb_collection.Add(usb);
                }
                
            }
        }
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            for(int i=0; i<this.folder_list.Items.Count; i++)
            {
                string sourceDir = ((DirectoryInfo)this.folder_list.Items[i]).Folder_Name;
                Thread filecopyThread = new Thread(new ThreadStart(delegate { FileOperationHelper.CopyFiles(sourceDir, this.targetDrive.Volume); }));
                filecopyThread.Start();
            }
            
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo();
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = @"C:\Users";
            dialog.IsFolderPicker = true;
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                di.Folder_Name = dialog.FileName;
                folder_collection.Add(di);
            }
        }

        private void usb_volumes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.targetDrive = (UsbDriveInfo)this.usb_volumes.SelectedItem;
        }

        
    }
}
