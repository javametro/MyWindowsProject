using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UIAutomationClient;

namespace InstallFromStore
{
    class Program
    {
        static void Main(string[] args)
        {
            IUIAutomation uia = Program.GetUIAClentInstance();
            IUIAutomationElement ui_root = Program.GetRootElement(uia);
            

            //SearchBox Action
            //string searchbox_window = "検索";
            //IUIAutomationElement uia_searchboxwindow = Program.GetElementFromNamePropertyId(uia, ui_root, searchbox_window, TreeScope.TreeScope_Children);
            //if(uia_searchboxwindow != null)
            //{
            //    Console.WriteLine("hello");
            //    return;
            //}

            string taskbar = "タスク バー";
            IUIAutomationElement uia_taskbar = Program.GetElementFromNamePropertyId(uia, ui_root, taskbar, TreeScope.TreeScope_Children);
            if (uia_taskbar != null)
            {
                string start = "スタート";
                IUIAutomationElement uia_start = Program.GetElementFromNamePropertyId(uia, uia_taskbar, start, TreeScope.TreeScope_Children);
                if (uia_start != null)
                {
                    IUIAutomationInvokePattern start_invoke_pattern = uia_start.GetCurrentPattern(UIA_PatternIds.UIA_InvokePatternId) as IUIAutomationInvokePattern;
                    if (start_invoke_pattern != null)
                    {
                        start_invoke_pattern.Invoke();
                        Thread.Sleep(2000);

                        string start_window = "スタート";
                        IUIAutomationElement uia_startwindow = Program.GetElementFromNamePropertyId(uia, ui_root, start_window, TreeScope.TreeScope_Children);
                        if (uia_startwindow != null)
                        {
                            string search_window_text = "検索";
                            IUIAutomationElement uia_searchwindow = Program.GetElementFromNamePropertyId(uia, uia_startwindow, search_window_text, TreeScope.TreeScope_Children);
                        }
                        

                        IUIAutomationElement uia_start2 = Program.GetElementFromNamePropertyId(uia, uia_start, start, TreeScope.TreeScope_Subtree);
                        if (uia_start2 != null)
                        {
                            string search_box = "検索ボックス";
                            IUIAutomationElement uia_searchbox = Program.GetElementFromNamePropertyId(uia, uia_taskbar, search_box, TreeScope.TreeScope_Subtree);
                            if (uia_searchbox != null)
                            {
                                IUIAutomationTextPattern searchbox_invoke_pattern = uia_searchbox.GetCurrentPattern(UIA_PatternIds.UIA_TextPatternId) as IUIAutomationTextPattern;
                                if (searchbox_invoke_pattern != null)
                                {
                                    searchbox_invoke_pattern.GetSelection();
                                    Thread.Sleep(2000);
                                }
                            }

                            IUIAutomationPropertyCondition scrollbar = uia.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "ピン留めしたタイル") as IUIAutomationPropertyCondition;
                            IUIAutomationElement uia_scrollbar = uia_start2.FindFirst(TreeScope.TreeScope_Subtree, scrollbar);
                            if (uia_scrollbar != null)
                            {
                                //IUIAutomationScrollPattern pinned_tiles_scroll_pattern = uia_scrollbar.GetCurrentPattern(UIA_PatternIds.UIA_ScrollPatternId) as IUIAutomationScrollPattern;
                                //if (pinned_tiles_scroll_pattern != null)
                                //{
                                //    Program.CaptureScreen(path);
                                //    pinned_tiles_scroll_pattern.Scroll(ScrollAmount.ScrollAmount_NoAmount, ScrollAmount.ScrollAmount_LargeIncrement);
                                //    Program.CaptureScreen(path);
                                //}
                            }
                        }
                    }
                }
            }

        }

        private static IUIAutomation GetUIAClentInstance()
        {
            return new CUIAutomation();
        }

        private static IUIAutomationElement GetRootElement(IUIAutomation instance) {
            IUIAutomationElement ui_root = instance.GetRootElement();
            return ui_root;
        }

        private static IUIAutomationElement GetElementFromNamePropertyId(IUIAutomation instance, IUIAutomationElement parent, string nameProperty, TreeScope treeScope)
        {
            
            IUIAutomationPropertyCondition taskbar = instance.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, nameProperty) as IUIAutomationPropertyCondition;
            IUIAutomationElement element = parent.FindFirst(treeScope, taskbar);
            return element;
        }
    }
}
