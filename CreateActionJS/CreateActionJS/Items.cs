using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomationClient;
using System.Collections.ObjectModel;
using System.Windows.Automation;

namespace CreateActionJS
{
    class Items : ViewModelBase
    {
        public ObservableCollection<ItemInfo> items;

        public ObservableCollection<ItemInfo> ItemGroup
        {
            get
            {
                return items;
            }

            set
            {
                if(items != value)
                {
                    items = value;
                    RaisePropertyChange("buttons");
                }
            }
        }

        public Items()
        {
            
        }

        public static ObservableCollection<ItemInfo> GetButtons(string appName, int type)
        {
            ObservableCollection<ItemInfo> buttonList = new ObservableCollection<ItemInfo>();
            AutomationElement root = AutomationElement.RootElement;
            OrCondition or = new OrCondition(new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
                            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
            PropertyCondition propertyCondition = new PropertyCondition(AutomationElement.NameProperty, appName);
            AndCondition andCondition = new AndCondition(or, propertyCondition);
            AutomationElementCollection theAppGroup = root.FindAll(System.Windows.Automation.TreeScope.Children, andCondition);
            AutomationElement theApp = null;
            foreach(AutomationElement item in theAppGroup)
            {
                if (item.Current.Name.Equals(appName))
                {
                    theApp = item;
                }
            }
            if(theApp == null)
            {
                return null;
            }

            PropertyCondition button_condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button);
            AutomationElementCollection btnNames = theApp.FindAll(System.Windows.Automation.TreeScope.Descendants, button_condition);

            
            foreach (AutomationElement item in btnNames)
            {
                ItemInfo itemInfo = new ItemInfo();
                itemInfo.ItemName = item.Current.Name;
                itemInfo.AutomationId = item.Current.AutomationId;
                itemInfo.HelpText = item.Current.HelpText;
                buttonList.Add(itemInfo);
            }
            return buttonList;
        }

        public static ObservableCollection<ItemInfo> GetEdit(string appName, int type)
        {
            ObservableCollection<ItemInfo> editList = new ObservableCollection<ItemInfo>();
            AutomationElement root = AutomationElement.RootElement;
            OrCondition or = new OrCondition(new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
                            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
            PropertyCondition propertyCondition = new PropertyCondition(AutomationElement.NameProperty, appName);
            AndCondition andCondition = new AndCondition(or, propertyCondition);
            AutomationElementCollection theAppGroup = root.FindAll(System.Windows.Automation.TreeScope.Children, andCondition);
            AutomationElement theApp = null;
            foreach (AutomationElement item in theAppGroup)
            {
                if (item.Current.Name.Equals(appName))
                {
                    theApp = item;
                }
            }
            if (theApp == null)
            {
                return null;
            }

            PropertyCondition button_condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit);
            AutomationElementCollection btnNames = theApp.FindAll(System.Windows.Automation.TreeScope.Descendants, button_condition);


            foreach (AutomationElement item in btnNames)
            {
                ItemInfo itemInfo = new ItemInfo();
                itemInfo.ItemName = item.Current.Name;
                itemInfo.AutomationId = item.Current.AutomationId;
                itemInfo.HelpText = item.Current.HelpText;
                editList.Add(itemInfo);
            }
            return editList;
        }
    }
}
