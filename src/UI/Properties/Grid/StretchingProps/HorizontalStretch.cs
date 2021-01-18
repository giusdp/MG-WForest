using Microsoft.Xna.Framework;
using WForest.UI.Utils;
using WForest.UI.WidgetTrees;

namespace WForest.UI.Properties.Grid.StretchingProps
{
    /// <summary>
    /// Property to increase the width of the widget til the width of the parent.
    /// If the widget is the root, it does nothing since it has no parent.
    /// </summary>
    public class HorizontalStretch : Property
    {
        /// <summary>
        /// It gets the width of the parent (if it has one) and replaces the widget's width with it, then updates the spaces of its children.
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.IsRoot) return;
            var (x, y, _, h) = widgetNode.Data.Space;
            WidgetsSpaceHelper.UpdateSpace(widgetNode,
                new Rectangle(x, y, widgetNode.Parent!.Data.Space.Width, h));
        }
    }
}