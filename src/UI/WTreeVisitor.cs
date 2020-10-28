using System;
using PiBa.Tests;
using PiBa.UI.Interfaces;

namespace PiBa.UI
{
    public static class WTreeVisitor
    {
        public static void ApplyToTree(WidgetTree widgetTree, Action<Widget> action)
        {
            if(widgetTree == null) throw new ArgumentNullException(nameof(widgetTree));
            action(widgetTree.Root);
            foreach (var tree in widgetTree.Children)
            {
                ApplyToTree(tree, action);
            }
        }
    }
}