using System;
using System.Collections.Generic;
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
using System.Windows.Automation;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace CreateActionJS
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        private string                      appName;
        private string                      itemName;
        private AutomationElementCollection apps;
        private List<AutomationElement>     btnNameList;
        private List<string>                btnTextList;
        private List<string>                patternNameList;
        private AutomationElement           root;
        private int itemType;


        public enum ItemType
        {
            ButtonType,
            EditType
        }
       
        public MainWindow()
        {
            InitializeComponent();
            this.itemType = 0;

            AppsList applist = new AppsList();
            ObservableCollection<string> infos = applist.datas;
            this.appsList.ItemsSource = infos;

            //CurrentProcesses processList = new CurrentProcesses();
            //ObservableCollection<string> processSource = processList.appsMainProcess;
 
            //DispatcherHelperEx.MainWindowDispatcher = Application.Current.MainWindow.Dispatcher;
            //DispatcherHelperEx.InvokeOnMainWindow(() => {
            //    btnNameList = new List<AutomationElement>();
                
            //});
        }

        static SemaphoreSlim semaphore = new SemaphoreSlim(1);

        

        private void button_listBoxSelectChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (ItemList.SelectedItem != null)
            //{
            //    this.itemName = ItemList.SelectedItem.ToString();
            //    this.itemName = this.itemName.Split('-')[0].TrimEnd();
            //    Thread thread = new Thread(() => GetCurrentPattern(this.appName, itemName));
            //    thread.Start();
            //}
            //else {
            //    return;
            //}
            Items items = new Items();
            items.items = Items.GetButtons(this.appsList.SelectedItem.ToString(), this.itemType);
            ObservableCollection<string> infos = (ObservableCollection<string>)items.items;
            this.ItemList.ItemsSource = items.items;
        }

        private void GetCurrentPattern(string appName, string itemName)
        {
            patternNameList = new List<string>();
            #region get theApp
            root = AutomationElement.RootElement;
            OrCondition or = new OrCondition(new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window), new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
            PropertyCondition propertyCondition = new PropertyCondition(AutomationElement.NameProperty, this.appName);
            AndCondition andCondition = new AndCondition(or, propertyCondition);
            AutomationElement theApp = root.FindFirst(TreeScope.Children, andCondition);
            #endregion

            itemName = itemName.Split(':')[0].TrimEnd();
//            PropertyCondition button_condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button);
            PropertyCondition name_condition = new PropertyCondition(AutomationElement.AutomationIdProperty, itemName);
//            andCondition = new AndCondition(button_condition, name_condition);
            AutomationElement theItem = theApp.FindFirst(TreeScope.Descendants, name_condition);

            if(theItem == null)
            {
                return;
            }

            AutomationPattern[] envet = theItem.GetSupportedPatterns();
            foreach (AutomationPattern pattern in envet)
            {
                patternNameList.Add(pattern.ProgrammaticName);
            }

            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate () {
                this.patternList.ItemsSource = patternNameList;
                this.automation_id.Text = theItem.Current.AutomationId.ToString();
            });
        }

        private void UpdateButtonList(string appName)
        {
            if (appName == null)
            {
                MessageBox.Show("Can't find program");
                //DispatcherHelperEx.InvokeOnMainWindow(() => { this.btn_getbuttonlist.IsEnabled = false; });
                return;
            }

            DispatcherHelperEx.InvokeOnMainWindow(() =>
            {
                //this.btn_getbuttonlist.IsEnabled = false;
                this.ProcessId.Text = "";
                this.ProcessName.Text = "";
                this.automation_id.Text = "";
                if(btnTextList != null)
                {
                    this.btnTextList.Clear();
                }
                
            });

            //string processName = "";
            //Process process = Process.GetProcessById(theApp.Current.ProcessId);
            //processName = process.ProcessName;
            

            //this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate()
            //{
            //    this.ItemList.ItemsSource = btnTextList;
            //    //this.btn_getbuttonlist.IsEnabled = true;
            //    this.ProcessName.Text = processName;
            //    this.ProcessId.Text = theApp.Current.ProcessId.ToString();
            //});

            

        }

        private void CreateCase(object sender, RoutedEventArgs e)
        {

            StreamWriter sw = new StreamWriter("test.js");
            string one = "1";
            string two = "2";
            string three = "3";
            string four = "4";
            string five = "5";
            string six = "6";
            string seven = "7";
            string eight = "8";
            string nine = "9";
            string ten = "10";

            string text = string.Format(@"
            var funclib       = require('../../../funclib');
            var template      = require('../../../template');
            var constant      = require('../../../constant');
            module.exports    = new template.Batch({{ 
                                    descrition  :{0},
                                    category    : constant.Category.Common,
                                    flags       : [
                                        constant.Flag.Hardware.Battery,
                                        constant.Flag.OS.Win10
                                    ]
                                }})

                                    .process({1}, (the_case, param) => {{
                                        require({2}).run([{3}, '{4} {5}']);
                                        require({6}).run([{7}, '{8} {9}']);
                                    }});"
                            , one, two, three, four, five, six, seven, eight, nine, ten);



            MessageBox.Show(text);
            return;
            sw.Write(text);
            MessageBox.Show("Write Done");
            sw.Close();
        }

        private void OnClickShowTaskList(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("你想测试的进程是：" +  this.processlist.SelectedItem.ToString());
            this.selected_process.Text = this.processlist.SelectedItem.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            this.FileName.Text = openFileDialog.FileName;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.process_check_content.Visibility = Visibility.Visible;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.process_check_content.Visibility = Visibility.Hidden;
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            this.file_exist_content.Visibility = Visibility.Visible;
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            this.file_exist_content.Visibility = Visibility.Hidden;
        }

        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Unchecked_2(object sender, RoutedEventArgs e)
        {

        }

        private void ClearList()
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                this.btnTextList.Clear();
                this.ItemList.ItemsSource = null;
            });
        }

        private void appsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Items items = new Items();
            items.items = Items.GetButtons(this.appsList.SelectedItem.ToString(), this.itemType);
            ObservableCollection<string> infos = (ObservableCollection<string>)items.items;
            this.ItemList.ItemsSource = items.items;
        }

        private void button_Checked(object sender, RoutedEventArgs e)
        {
            this.itemType = 0;
            Items items = new Items();
            if (this.appsList.SelectedItem == null)
            {
                return;
            }
            items.items = Items.GetButtons(this.appsList.SelectedItem.ToString(), this.itemType);
            ObservableCollection<string> infos = (ObservableCollection<string>)items.items;
            this.ItemList.ItemsSource = items.items;
        }

        private void edit_Checked(object sender, RoutedEventArgs e)
        {
            this.itemType = 1;
            Items items = new Items();
            if (this.appsList.SelectedItem == null)
            {
                return;
            }
            items.items = Items.GetButtons(this.appsList.SelectedItem.ToString(), this.itemType);
            ObservableCollection<string> infos = (ObservableCollection<string>)items.items;
            this.ItemList.ItemsSource = items.items;
        }

        private void other_Checked(object sender, RoutedEventArgs e)
        {

        }
    }


    public class DispatcherHelperEx
    {
        public static Dispatcher MainWindowDispatcher { get; set; }

        /// <summary>
        /// Dispatch action on UI thread.
        /// </summary>
        /// <param name="doAction"></param>
        public static void InvokeOnMainWindow(Action doAction)
        {
            try
            {
                if (MainWindowDispatcher == null)
                {
                    return;
                }

                if (doAction == null)
                {
                    return;
                }

                MainWindowDispatcher.Invoke(doAction);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }

}
