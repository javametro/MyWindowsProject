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
    }
}
