using System;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Properties.Grid.Utils;
using WForest.UI.Utils;

namespace WForest.UI.Properties.Grid
{
    public class Flex : Property
    {
        internal override void ApplyOn(WidgetTree.WidgetTree widgetNode)
        {
            IncreaseSpaceWithChildren(widgetNode);


            var (x, y, _, _) = widgetNode.Data.Space;
            if (ApplyUtils.TryExtractRow(widgetNode, out var row))
            {
                row.Applied += (sender, args) =>
                {
                    WidgetsSpaceHelper.UpdateSpace(widgetNode,
                        new Rectangle(
                            x,
                            y,
                            row.Rows.Max(r => r.Width),
                            row.Rows.Sum(r => r.Height)));
                };
            }
            else if (ApplyUtils.TryExtractColumn(widgetNode, out var col))
                col.Applied += (sender, args) =>
                {
                    WidgetsSpaceHelper.UpdateSpace(widgetNode,
                        new Rectangle(
                            x, 
                            y, 
                            col.Columns.Sum(c => c.Width), 
                            col.Columns.Sum(c => c.Height)));
                };
        }

        private static void IncreaseSpaceWithChildren(WidgetTree.WidgetTree widgetNode)
        {
            int w = 0, h = 0;
            widgetNode.Children.ForEach(c =>
            {
                var (x, y, _, _) = widgetNode.Data.Space;
                var (_, _, cw, ch) = c.Data.Space;
                w += cw;
                h += ch;
                if (!widgetNode.IsRoot)
                {
                    var (_, _, parentW, parentH) = widgetNode.Parent.Data.Space;
                    if (w > parentW) w -= cw;
                    if (h > parentH) h -= ch;
                }

                WidgetsSpaceHelper.UpdateSpace(widgetNode, new Rectangle(x, y, w, h));
            });
        }
    }
}