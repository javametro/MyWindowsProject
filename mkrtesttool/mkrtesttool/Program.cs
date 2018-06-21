using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace mkrtesttool
{
    class Program
    {
        static void Main(string[] args)
        {
            string value = "";
            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey store_content_modifier = hklm.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Store\\");
            value = (string)store_content_modifier.GetValue("StoreContentModifier");

            if(value.CompareTo("Lenovo5_NECPC1") == 0)
            {
                Console.WriteLine("Consumer Model");
            }
            else if (value.CompareTo("Lenovo6_NECPC2") == 0)
            {
                Console.WriteLine("Commercial Model");
            }
            else
            {
                Console.WriteLine("Not NEC Model");
            }
        }
    }
}
