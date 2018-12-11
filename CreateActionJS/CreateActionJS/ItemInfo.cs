using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CreateActionJS
{
    class ItemInfo : INotifyPropertyChanged
    {
        private string itemName;
        private string automationId;
        private string helpText;

        public ItemInfo(string name, string id, string text)
        {
            this.itemName = name;
            this.automationId = id;
            this.helpText = text;
        }

        public ItemInfo()
        {

        }

        public string ItemName
        {
            get
            {
                return itemName;
            }

            set
            {
                if (itemName != value)
                {
                    itemName = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("ItemName"));
                }
            }
        }

        public string AutomationId
        {
            get
            {
                return automationId;
            }

            set
            {
                if (automationId != value)
                {
                    automationId = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("AutomationId"));
                }
            }
        }

        public string HelpText
        {
            get
            {
                return helpText;
            }

            set
            {
                if (helpText != value)
                {
                    helpText = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("HelpText"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
