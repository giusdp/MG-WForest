using System;
using PiBa.UI.Interfaces;

namespace PiBa.UI
{
    public class WidgetTree
    {
        public Widget Root { get; set; }
        public WidgetTree[] Children { get; set; }

        public WidgetTree(Widget root)
        {
            Root = root;
            Children = Array.Empty<WidgetTree>();
        }
    }
}