using Microsoft.Xna.Framework.Graphics;
using PiBa.UI.Widgets;
using PiBa.Utilities;
using PiBa.Utilities.Collections;

namespace PiBa.UI
{
    public static class WidgetTreeVisitor
    {
        public static void DrawTree(Tree<Widget> widgetTree, SpriteBatch spriteBatch)
        {
            TreeVisitor<Widget>.ApplyToTree(widgetTree, w => w.Data.Draw(spriteBatch));
        }
        
        public static void EnforceConstraints(Tree<Widget> widgetTree, ConstraintsDict constraints)
        {
            TreeVisitor<Widget>.ApplyToTree(widgetTree, nodeWidget =>
               constraints.GetConstraintsOf(nodeWidget).ForEach(c => c.EnforceOn(nodeWidget))
            );
        }
    }
}