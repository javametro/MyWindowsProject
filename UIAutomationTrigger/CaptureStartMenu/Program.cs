using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using UIAutomationClient;
using System.Windows.Forms;

namespace CaptureStartMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.WriteLine("Argument not Correct");
                return;
            }

            string path = args[0];

            IUIAutomation uia = new CUIAutomation();
            IUIAutomationElement ui_root = uia.GetRootElement();
            IUIAutomationPropertyCondition taskbar = uia.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "タスク バー") as IUIAutomationPropertyCondition;
            IUIAutomationElement uia_taskbar = ui_root.FindFirst(TreeScope.TreeScope_Subtree, taskbar);
            if(uia_taskbar != null)
            {
                IUIAutomationPropertyCondition start = uia.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "スタート") as IUIAutomationPropertyCondition;
                IUIAutomationElement uia_start = uia_taskbar.FindFirst(TreeScope.TreeScope_Subtree, start);
                if(uia_start != null)
                {
                    IUIAutomationInvokePattern start_invoke_pattern = uia_start.GetCurrentPattern(UIA_PatternIds.UIA_InvokePatternId) as IUIAutomationInvokePattern;
                    if(start_invoke_pattern != null)
                    {
                        start_invoke_pattern.Invoke();
                        Thread.Sleep(2000);

                        IUIAutomationPropertyCondition start2 = uia.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "スタート") as IUIAutomationPropertyCondition;
                        IUIAutomationElement uia_start2 = ui_root.FindFirst(TreeScope.TreeScope_Children, start2);
                        if(uia_start2 != null)
                        {
                            IUIAutomationPropertyCondition scrollbar = uia.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "ピン留めしたタイル") as IUIAutomationPropertyCondition;
                            IUIAutomationElement uia_scrollbar = uia_start2.FindFirst(TreeScope.TreeScope_Subtree, scrollbar);
                            if(uia_scrollbar != null)
                            {
                                IUIAutomationScrollPattern pinned_tiles_scroll_pattern = uia_scrollbar.GetCurrentPattern(UIA_PatternIds.UIA_ScrollPatternId) as IUIAutomationScrollPattern;
                                if(pinned_tiles_scroll_pattern != null)
                                {
                                    Program.CaptureScreen(path);
                                    pinned_tiles_scroll_pattern.Scroll(ScrollAmount.ScrollAmount_NoAmount, ScrollAmount.ScrollAmount_LargeIncrement);
                                    Program.CaptureScreen(path);
                                }
                            }
                        }
                    }
                }
            }
        }
        
        private static void CaptureScreen(string path)
        {
            Image myImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(myImage);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            IntPtr dc1 = g.GetHdc();
            g.ReleaseHdc(dc1);

            Random objRand = new Random();
            string pic_name = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".jpg";
            myImage.Save(path + pic_name);
        }
    }
}
