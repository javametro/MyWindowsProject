using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace CreateActionJS
{
    class AppsList : ObservableCollection<string>
    {
        public ObservableCollection<string> datas;

        public AppsList()
        {
            datas = new ObservableCollection<string>();
            AutomationElementCollection appsData = GetItem();
            foreach (AutomationElement item in appsData)
            {
                if (!string.IsNullOrEmpty(item.Current.Name))
                {
                    datas.Add(item.Current.Name);
                }
                
            }
        }

        private AutomationElementCollection GetItem()
        {
            AutomationElement root = AutomationElement.RootElement;
            OrCondition or = new OrCondition(new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window), new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
            AutomationElementCollection apps = root.FindAll(TreeScope.Children, or);
            return apps;
        }
    }
}
