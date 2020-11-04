using System;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI
{
    public static class WidgetConstraintSolver
    {
        public static bool EnforceConstraints(Tree<Widget> widgetTree, ConstraintsMap constraints)
        {
            ApplyToWidgetTree(widgetTree, nodeWidget =>
            {
                var widgetConstraints = constraints.GetConstraintsOf(nodeWidget);
                widgetConstraints.ForEach(c => c.EnforceOn(nodeWidget));
                Console.WriteLine($"Is Root? {nodeWidget.IsRoot} Space: {nodeWidget.Data.Space}");
            } );
            return false;
        }

        private static void ApplyToWidgetTree(Tree<Widget> tree, Action<Tree<Widget>> action)
        {
            foreach (var node in tree) action(node);

        }
    }
}