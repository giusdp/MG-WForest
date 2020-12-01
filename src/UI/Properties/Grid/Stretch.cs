using Microsoft.Xna.Framework;
using WForest.UI.Utils;

namespace WForest.UI.Properties.Grid
{
    public class Stretch : Property
    {
        internal override void ApplyOn(WidgetTree widgetNode)
        {
            if (!widgetNode.IsRoot)
            {
                WidgetsSpaceHelper.UpdateSpace(widgetNode,
                    new Rectangle(widgetNode.Data.Space.Location, widgetNode.Parent.Data.Space.Size));
            }
        }
    }
}