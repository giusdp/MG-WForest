using System;
using PiBa.UI.Interfaces;

namespace PiBa.UI
{
    public class WidgetTree
    {
        public IWidget Root { get; private set; }
        public WidgetTree[] Children { get; set; }

        public WidgetTree(IWidget root)
        {
            Root = root;
            Children = Array.Empty<WidgetTree>();
        }
    }
}