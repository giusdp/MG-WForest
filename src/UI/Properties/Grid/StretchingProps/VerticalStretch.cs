using Microsoft.Xna.Framework;
using WForest.UI.Utils;
using WForest.UI.WidgetTrees;

namespace WForest.UI.Properties.Grid.StretchingProps
{
    public class VerticalStretch : Property
    {
        public override void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.IsRoot) return;
            var (x, y, w, _) = widgetNode.Data.Space;
            WidgetsSpaceHelper.UpdateSpace(widgetNode,
                new Rectangle(x, y, w, widgetNode.Parent!.Data.Space.Height));
        }
    }
}