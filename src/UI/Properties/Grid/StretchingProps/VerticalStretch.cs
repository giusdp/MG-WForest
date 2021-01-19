using Microsoft.Xna.Framework;
using WForest.UI.Utils;
using WForest.UI.WidgetTrees;

namespace WForest.UI.Properties.Grid.StretchingProps
{
    /// <summary>
    /// Property to increase the height of the widget til the height of the parent.
    /// If the widget is the root, it does nothing since it has no parent.
    /// </summary>
    public class VerticalStretch : Property
    {
        internal VerticalStretch(){}
        /// <summary>
        /// It gets the height of the parent (if it has one) and replaces the widget's height with it, then updates the spaces of its children.
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.IsRoot) return;
            var (x, y, w, _) = widgetNode.Data.Space;
            WidgetsSpaceHelper.UpdateSpace(widgetNode,
                new Rectangle(x, y, w, widgetNode.Parent!.Data.Space.Height));
        }
    }
}