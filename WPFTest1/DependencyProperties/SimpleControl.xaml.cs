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

namespace DependencyProperties
{
    /// <summary>
    /// Interaction logic for SimpleControl.xaml
    /// </summary>
    public partial class SimpleControl : UserControl
    {
        public SimpleControl()
        {
            InitializeComponent();
        }

        //public int MyProperty {
        //       get { return (int)GetValue(MyProperty); }
        //       set { SetValue(MyProperty, value); }
        //}

        //public static readonly DependencyProperty MyPropertyProperty = DependencyProperty.Register("MyProperty", typeof(int), typeof(SimpleControl), new UIPropertyMetadata(0));

        public int YearPublished {
               get { return (int)GetValue(YearPublishedProperty); }
                set { SetValue(YearPublishedProperty, value); }
        }

        public static readonly DependencyProperty YearPublishedProperty = DependencyProperty.Register("YearPublished", typeof(int), typeof(SimpleControl), new UIPropertyMetadata(2000));
    }
}
