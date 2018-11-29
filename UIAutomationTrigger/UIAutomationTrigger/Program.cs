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
        #region InvokePattern helper
        /// <summary>
        /// Get InvokePattern
        /// </summary>
        /// <param name="element">AutomationElement instance</param>
        /// <returns>InvokePattern instance</returns>
        public static InvokePattern GetInvokePattern(AutomationElement element)
        {
            object currentPattern;
            if (!element.TryGetCurrentPattern(InvokePattern.Pattern, out currentPattern))
            {
                throw new Exception(string.Format("Element with AutomationId '{0}' and Name '{1}' does not support the InvokePattern.",
                    element.Current.AutomationId, element.Current.Name));
            }
            return currentPattern as InvokePattern;
        }
        #endregion

        static void Main(string[] args)
        {
            int length = args.Length;
            if(length != 2)
            {
                Console.WriteLine("******************************************************************");
                Console.WriteLine("Usage: UIAutomationTrigger.exe arg1=ProgramName arg2=AutomationId");
                Console.WriteLine("******************************************************************");
                return;
            }

            string appName = args[0];
            string automationid = args[1];
            Process[] process = Process.GetProcessesByName(appName);
            int pid = 0;
            foreach (Process processItem in process)
            {
                pid = processItem.Id;
            }

            AutomationElement item = FindElement.FindElementById(pid, automationid);
            InvokePattern currentPattern = GetInvokePattern(item);
            currentPattern.Invoke();
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

        public static AutomationElement FindWindowByProcessId(int processId)
        {
            AutomationElement targetWindow = null;
            int count = 0;
            try
            {
                Process p = Process.GetProcessById(processId);
                targetWindow = AutomationElement.FromHandle(p.MainWindowHandle);
                return targetWindow;
            }
            catch (Exception ex)
            {
                count++;
                StringBuilder sb = new StringBuilder();
                string message = sb.AppendLine(string.Format("Target window is not existing.try #{0}", count)).ToString();
                if (count > 5)
                {
                    throw new InvalidProgramException(message, ex);
                }
                else
                {
                    return FindWindowByProcessId(processId);
                }
            }
        }

        public static AutomationElement FindElementById(int processId, string automationId)
        {
            AutomationElement aeForm = FindWindowByProcessId(processId);
            AutomationElement tarFindElement = aeForm.FindFirst(TreeScope.Descendants,
            new PropertyCondition(AutomationElement.AutomationIdProperty, automationId));
            return tarFindElement;
        }
    }
}
