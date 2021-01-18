using Microsoft.Xna.Framework;
using WForest.UI.Utils;
using WForest.UI.WidgetTrees;

namespace WForest.UI.Properties.Grid.StretchingProps
{
    public class HorizontalStretch : Property
    {
        public override void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.IsRoot) return;
            var (x, y, _, h) = widgetNode.Data.Space;
            WidgetsSpaceHelper.UpdateSpace(widgetNode,
                new Rectangle(x, y, widgetNode.Parent!.Data.Space.Width, h));
        }
    }
}