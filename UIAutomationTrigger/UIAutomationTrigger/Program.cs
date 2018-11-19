using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace UIAutomationTrigger
{
    class Program
    {
        static void Main(string[] args)
        {
            Process p = Process.Start(@"C:\Windows\System32\calc.exe");
            Thread.Sleep(2000);

            AutomationElement desktop = AutomationElement.RootElement;
            AutomationElement calcframe = desktop.FindFirst(TreeScope.Descendants | TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Calculator"));
            AutomationElement sevenbtn = calcframe.FindFirst(TreeScope.Descendants | TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "7"));

            InvokePattern ivkp = (InvokePattern)sevenbtn.GetCachedPattern(InvokePattern.Pattern);
            ivkp.Invoke();
        }
    }

    public class FindElement
    {
        static public AutomationElement FindElementByConditionInTimes(AutomationElement aeParent, TreeScope ts, Condition cdt, int nTimes)
        {
            int nCount = nTimes * 1000;
            AutomationElement aeToFind = aeParent.FindFirst(ts, cdt);
            while((aeToFind == null) && (nCount > 0))
            {
                Thread.Sleep(1000);
                nCount = nCount - 1000;
                aeToFind = aeParent.FindFirst(TreeScope.Descendants, cdt);
                if(aeToFind != null)
                {
                    return aeToFind;
                }
            }

            return aeToFind;
        }

        static public AutomationElement FindElementByAndConditionInTimes(AutomationElement aeParent, TreeScope ts, Condition acdt, int nTimes)
        {
            int nCount = nTimes * 1000;
            AutomationElement aeToFind = aeParent.FindFirst(ts, acdt);
            while ((aeToFind == null) && (nCount > 0))
            {
                Thread.Sleep(1000);
                nCount = nCount - 1000;
                aeToFind = aeParent.FindFirst(TreeScope.Descendants, acdt);
                if (aeToFind != null)
                {
                    return aeToFind;
                }
            }

            return aeToFind;
        }

        static public AutomationElement FindElementByOrConditionInTimes(AutomationElement aeParent, TreeScope ts, Condition ocdt, int nTimes)
        {
            int nCount = nTimes * 1000;
            AutomationElement aeToFind = aeParent.FindFirst(ts, ocdt);
            while ((aeToFind == null) && (nCount > 0))
            {
                Thread.Sleep(1000);
                nCount = nCount - 1000;
                aeToFind = aeParent.FindFirst(TreeScope.Descendants, ocdt);
                if (aeToFind != null)
                {
                    return aeToFind;
                }
            }

            return aeToFind;
        }

        public static AutomationElement GetMainWindow()
        {
            AutomationElement dtop = AutomationElement.RootElement;
            AutomationElement mainwnd = FindElement.FindElementByConditionInTimes(dtop, TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Notepad"), 5);
            return mainwnd;
        }

        public static AutomationElement GetMenuBtn(AutomationElement mainwnd)
        {
            AutomationElement menubtn = FindElement.FindElementByConditionInTimes(mainwnd, TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Menu"), 5);
            return menubtn;
        }

        public static AutomationElement GetSaveWins(AutomationElement mainwnd)
        {
            var andcondition = new AndCondition(new PropertyCondition(AutomationElement.NameProperty, "Save"), new PropertyCondition(AutomationElement.ClassNameProperty, "Window"));
            AutomationElement saveWins = FindElement.FindElementByAndConditionInTimes(mainwnd, TreeScope.Children, andcondition, 5);
            return saveWins;
        }




    }
}
