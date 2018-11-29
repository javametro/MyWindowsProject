using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Collections.ObjectModel;

namespace CreateActionJS
{
    class Items : ViewModelBase
    {
        public ObservableCollection<string> items;

        public ObservableCollection<string> ItemGroup
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

        public static ObservableCollection<string> GetButtons(string appName, int type)
        {
            AutomationElement root = AutomationElement.RootElement;
            OrCondition or = new OrCondition(new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
                            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
            PropertyCondition propertyCondition = new PropertyCondition(AutomationElement.NameProperty, appName);
            AndCondition andCondition = new AndCondition(or, propertyCondition);
            AutomationElement theApp = root.FindFirst(TreeScope.Children, andCondition);

            if (type == 0)
            {
                ObservableCollection<string> button_data = new ObservableCollection<string>();
                PropertyCondition button_condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button);
                AutomationElementCollection btnNames = theApp.FindAll(TreeScope.Descendants, button_condition);

                foreach (AutomationElement item in btnNames)
                {
                    button_data.Add(item.Current.AutomationId);
                }

                return button_data;
            }

            if(type == 1)
            {
                ObservableCollection<string> edit_data = new ObservableCollection<string>();
                PropertyCondition edit_condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit);
                AutomationElementCollection editLists = theApp.FindAll(TreeScope.Descendants, edit_condition);

                foreach (AutomationElement item in editLists)
                {
                    edit_data.Add(item.Current.AutomationId);
                }

                return edit_data;
            }

            return null;

        }
    }
}
