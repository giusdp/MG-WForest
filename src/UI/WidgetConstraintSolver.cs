using System;
using PiBa.UI.Widgets.Interfaces;
using PiBa.Utilities.Collections;

namespace PiBa.UI
{
    public static class WidgetConstraintSolver
    {
        public static bool EnforceConstraints(Tree<IWidget> widgetTree)
        {
            ApplyToWidgetTree(widgetTree, widget =>
            {
                widget.Constraints.ForEach(constraint => constraint.Enforce());
            });
            return false;
        }

        private static void ApplyToWidgetTree(Tree<IWidget> tree, Action<IWidget> action)
        {
            foreach (var t in tree) action(t.Data);

        }
    }
}