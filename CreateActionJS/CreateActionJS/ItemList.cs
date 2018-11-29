using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Automation;

namespace CreateActionJS
{
    class ItemList : ObservableCollection<string>
    {
        ObservableCollection<string> buttons;
        public ItemList()
        {
            buttons = new ObservableCollection<string>();
        }

        //private ObservableCollection<string> GetItemList(string appName)
        //{
            

        //    #region get theApp
        //    AutomationElement root = AutomationElement.RootElement;

        //    OrCondition or = new OrCondition(new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
        //                                                             new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane)
        //                                          );

        //    PropertyCondition propertyCondition = new PropertyCondition(AutomationElement.NameProperty, appName);
        //    AndCondition andCondition = new AndCondition(or, propertyCondition);
        //    AutomationElement theApp = root.FindFirst(TreeScope.Children, andCondition);
        //    //if (theApp == null)
        //    //{
        //    //    DispatcherHelperEx.InvokeOnMainWindow(() => { this.btn_getbuttonlist.IsEnabled = true; });
        //    //    return;
        //    //}
        //    #endregion

        //    #region get button
        //    if (bGetButtonList)
        //    {
        //        PropertyCondition button_condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button);
        //        AutomationElementCollection btnNames = theApp.FindAll(TreeScope.Descendants, button_condition);

        //        if (btnNames.Count == 0)
        //        {
        //            btnTextList = null;
        //            MessageBox.Show(theApp.Current.Name + " not contain button.");
        //        }
        //        else
        //        {
        //            if (btnTextList == null)
        //            {
        //                btnTextList = new List<string>();
        //            }

        //            foreach (AutomationElement element in btnNames)
        //            {
        //                string showButtonMsg;
        //                if (string.IsNullOrEmpty(element.Current.AutomationId) /*|| string.IsNullOrEmpty(element.Current.HelpText)*/)
        //                {
        //                    continue;
        //                }
        //                else
        //                {
        //                    showButtonMsg = element.Current.AutomationId + ":" + element.Current.HelpText;
        //                    showButtonMsg = Regex.Replace(showButtonMsg, @"\t|\n|\r", "");
        //                }

        //                btnTextList.Add(showButtonMsg);

        //            }

        //        }
        //    }
        //    #endregion

        //    #region get edit box
        //    if (bGetEditBoxList)
        //    {
        //        PropertyCondition edit_condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit);
        //        AutomationElementCollection editLists = theApp.FindAll(TreeScope.Descendants, edit_condition);
        //        if (editLists.Count == 0)
        //        {
        //            MessageBox.Show(theApp.Current.Name + "not contain edit control type.");
        //        }
        //        else
        //        {
        //            if (btnTextList == null)
        //            {
        //                btnTextList = new List<string>();
        //            }

        //            foreach (AutomationElement element in editLists)
        //            {
        //                string showEditMsg;
        //                if (string.IsNullOrEmpty(element.Current.AutomationId))
        //                {
        //                    continue;
        //                }
        //                else
        //                {
        //                    showEditMsg = element.Current.AutomationId + ":" + element.Current.HelpText;
        //                    showEditMsg = Regex.Replace(showEditMsg, @"\t|\n|\r", "");
        //                }

        //                btnTextList.Add(showEditMsg);

        //            }
        //        }
        //    }
        //    #endregion

        //    string processName = "";
        //    Process process = Process.GetProcessById(theApp.Current.ProcessId);
        //    processName = process.ProcessName;


        //    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
        //    {
        //        this.ItemList.ItemsSource = btnTextList;
        //        this.btn_getbuttonlist.IsEnabled = true;
        //        this.ProcessName.Text = processName;
        //        this.ProcessId.Text = theApp.Current.ProcessId.ToString();
        //    });



        //}

    }
}
