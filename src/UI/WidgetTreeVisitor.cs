using System;
using Microsoft.Xna.Framework.Graphics;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI
{
    public static class WidgetTreeVisitor
    {
        public static void DrawTree(Tree<Widget> widgetTree, SpriteBatch spriteBatch)
        {
            ApplyToWidgetTree(widgetTree, w => w.Data.Draw(spriteBatch));
        }
        
        public static void EnforceConstraints(Tree<Widget> widgetTree, ConstraintsMap constraints)
        {
            ApplyToWidgetTree(widgetTree, nodeWidget =>
               constraints.GetConstraintsOf(nodeWidget).ForEach(c => c.EnforceOn(nodeWidget))
            );
        }

        private static void ApplyToWidgetTree(Tree<Widget> tree, Action<Tree<Widget>> action)
        {
            foreach (var node in tree) action(node);
        }
    }
}