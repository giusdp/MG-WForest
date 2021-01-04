using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Properties.Grid.Utils;
using WForest.UI.Utils;
using WForest.UI.WidgetTrees;

namespace WForest.UI.Properties.Grid
{
    public class Stretch : Property
    {
        internal Stretch(){}

        public override void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.IsRoot) return;
            WidgetsSpaceHelper.UpdateSpace(widgetNode,
                new Rectangle(widgetNode.Data.Space.Location, widgetNode.Parent.Data.Space.Size));

            if (ApplyUtils.TryExtractRow(widgetNode, out var row))
                row.Applied += (sender, _) =>
                    WidgetsSpaceHelper.UpdateSpace(widgetNode,
                        new Rectangle(
                            widgetNode.Data.Space.Location,
                            new Point(widgetNode.Parent.Data.Space.Width, row.Rows.Sum(r => r.Height))));
            else if (ApplyUtils.TryExtractColumn(widgetNode, out var col))
                col.Applied += (sender, _) =>
                    WidgetsSpaceHelper.UpdateSpace(widgetNode,
                        new Rectangle(
                            widgetNode.Data.Space.Location,
                            new Point(col.Columns.Sum(c => c.Width), widgetNode.Parent.Data.Space.Height)));
        }
    }
}