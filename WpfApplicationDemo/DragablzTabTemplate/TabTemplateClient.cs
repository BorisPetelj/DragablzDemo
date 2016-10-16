using Dragablz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace C_Client.Pregledi.Meni.DragablzTabTemplate
{
    public class TabTemplateClient : IInterTabClient
    {
        public INewTabHost<Window> GetNewHost(IInterTabClient interTabClient, object partition, TabablzControl source)
        {
            var view = new TemplateWindow();
            view.DataContext = source.DataContext;
            return new NewTabHost<Window>(view, view.TabablzControl);
        }

        public TabEmptiedResponse TabEmptiedHandler(TabablzControl tabControl, Window window)
        {
            return TabEmptiedResponse.CloseWindowOrLayoutBranch;
        }
    }
}
