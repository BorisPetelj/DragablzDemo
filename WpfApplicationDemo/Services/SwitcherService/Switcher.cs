using C_Client.Servisi.SwitcherService;
using MahApps.Metro.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace C_Client.Servisi
{
    public class Switcher
    {
        public static string activePage;
        private static string activeWindow;
        private static Stack openedWindows = new Stack();
        private static Dictionary<string, myWindow> windows = new Dictionary<string, myWindow>();
        private static Dictionary<string, myPage> pages = new Dictionary<string, myPage>();
        
        public void registerWindow(Type windowClass, string transitionContentName = null)
        {
            try
            {
                windows.Add(windowClass.Name, new myWindow(windowClass, transitionContentName));
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR");
            }
        }

        public void registerDialog(Type windowClass, string transitionContentName = null)
        {
            try
            {
                windows.Add(windowClass.Name, new myWindow(windowClass, transitionContentName, true));
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR");
            }
        }

        public void registerPage(string pageName, Type windowClass, Page page, TransitionType transitionType = TransitionType.Default)
        {
            pages.Add(pageName, new myPage(windowClass.Name, pageName, page, transitionType));
        }

        public static void navigate(string newPage, object windowParams = null, object pageParams = null) 
        {
            try
            {
                string newWindowName = pages[newPage].WindowName;
                bool isDialog = windows[newWindowName].IsDialog;

                if (activeWindow != newWindowName)
                {
                    Window tmpWindow = (Window)Activator.CreateInstance(windows[newWindowName].WindowClass);
                    if(!isDialog)
                        Application.Current.MainWindow = tmpWindow;
                    
                    if (openedWindows.Count > 0 && !isDialog)
                    {
                        ((Window)openedWindows.Pop()).Close();
                    }
                    
                    openedWindows.Push(tmpWindow);
                }
                
                Window newPageWindow = (Window)openedWindows.Peek();
                TransitioningContentControl transitionContent = newPageWindow.FindName(windows[newWindowName].TransitionContentName) as TransitioningContentControl;

                if (windowParams != null)
                {
                    ISwitchParams newPageWindowParams = newPageWindow as ISwitchParams;
                    if (newPageWindowParams != null)
                    {
                        newPageWindowParams.onLoad(windowParams);
                    }
                    else
                    {
                        throw new NotImplementedException("ISwitchParams not implemented");
                    }
                }

                if (pageParams != null)
                {
                    ISwitchParams newPageParams = pages[newPage].Page as ISwitchParams;
                    if (newPageParams != null)
                    {
                        newPageParams.onLoad(windowParams);
                    }
                    else
                    {
                        throw new NotImplementedException("ISwitchParams not implemented");
                    }
                }

                if (transitionContent != null)
                {
                    transitionContent.Transition = pages[newPage].TransitionType;
                    transitionContent.DataContext = pages[newPage].Page.DataContext;
                    transitionContent.Content = pages[newPage].Page.Content;
                    activePage = newPage;
                }
                else
                {
                    newPageWindow.Content = pages[newPage].Page.Content;
                    newPageWindow.DataContext = pages[newPage].Page.DataContext;
                    activePage = newPage;
                }

                if (activeWindow != newWindowName)
                {
                    activeWindow = newWindowName;
                    if (isDialog)
                    {
                        newPageWindow.ShowDialog(); //blocking
                        openedWindows.Pop();
                        activeWindow = "HeaderWindow";
                    }
                    else
                    {
                        newPageWindow.Show();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private class myPage
        {
            string windowName;
            string pageName;
            Page page;
            TransitionType transitionType;

            public string WindowName
            {
                get { return windowName; }
                set { windowName = value; }
            }

            public string PageName
            {
                get { return pageName; }
                set { pageName = value; }
            }

            public Page Page
            {
                get { return page; }
                set { page = value; }
            }

            public TransitionType TransitionType
            {
                get { return transitionType; }
                set { transitionType = value; }
            }

            public myPage(string windowName, string pageName, Page page, TransitionType transitionType = TransitionType.Default)
            {
                this.windowName = windowName;
                this.pageName = pageName;
                this.page = page;
                this.transitionType = transitionType;
            }
        }

        private class myWindow
        {
            Type windowClass;
            string transitionContentName;
            private bool isDialog;

            public Type WindowClass
            {
                get { return windowClass; }
                set { windowClass = value; } 
            }

            public string TransitionContentName
            {
                get { return transitionContentName; }
                set { transitionContentName = value; }
            }

            public bool IsDialog
            {
                get { return isDialog; }
                set { isDialog = value; } 
            }

            public myWindow(Type windowClass, string transitionContentName, bool isDialog = false)
            {
                this.windowClass = windowClass;
                this.transitionContentName = transitionContentName;
                this.IsDialog = isDialog;
            }
        }
    }
}