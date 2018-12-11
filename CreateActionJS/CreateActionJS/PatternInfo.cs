using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace CreateActionJS
{
    class PatternInfo : INotifyPropertyChanged
    {
        private string patternName;
        private string PatternName
        {
            get
            {
                return patternName;
            }

            set
            {
                if(patternName != value)
                {
                    patternName = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("PatternName"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public static ObservableCollection<PatternInfo> GetPatterns(string controlName)
        {
            ObservableCollection<ItemInfo> patternList = new ObservableCollection<ItemInfo>();
            AutomationElement root = AutomationElement.RootElement;
            OrCondition or = new OrCondition(new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
                            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane));
            PropertyCondition propertyCondition = new PropertyCondition(AutomationElement.NameProperty, appName);
            AndCondition andCondition = new AndCondition(or, propertyCondition);
            AutomationElementCollection theAppGroup = root.FindAll(System.Windows.Automation.TreeScope.Children, andCondition);
            AutomationElement theApp = null;
            foreach (AutomationElement item in theAppGroup)
            {
                if (item.Current.Name.Equals(controlName))
                {
                    theApp = item;
                }
            }
            if (theApp == null)
            {
                return null;
            }

            return patternList;
        }
    }

    
}
