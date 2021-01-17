using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Properties.Grid.Utils;
using WForest.UI.Utils;
using WForest.UI.WidgetTrees;

namespace WForest.UI.Properties.Grid
{
    /// <summary>
    /// Property to make the space of the widget flexible. It stays as little as possible and grows to take the minimum
    /// space required to accomodate the children.
    /// </summary>
    public class Flex : Property
    {
        internal Flex()
        {
        }

        /// <summary>
        /// It gets the children of the widget and expands the space enough to accomodate them.
        /// </summary>
        /// <param name="widgetNode"></param>
        public override void ApplyOn(WidgetTree widgetNode)
        {
            IncreaseSpaceWithChildren(widgetNode);

            var (x, y, _, _) = widgetNode.Data.Space;

            var maybeRow = ApplyUtils.ExtractRowProp(widgetNode);
            if (maybeRow.TryGetValue(out var row))
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
            else
            {
                var maybeCol = ApplyUtils.ExtractColumnProp(widgetNode);
                if (maybeCol.TryGetValue(out var col))
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
        }

        private static void IncreaseSpaceWithChildren(WidgetTree widgetNode)
        {
            int w = 0, h = 0;
            widgetNode.Children.ForEach(c =>
            {
                var (x, y, _, _) = widgetNode.Data.Space;
                var (_, _, cw, ch) = c.Data.Space;
                w += cw;
                h += ch;
                if (widgetNode.IsRoot == false)
                {
                    var (_, _, parentW, parentH) = widgetNode.Parent!.Data.Space;
                    if (w > parentW) w -= cw;
                    if (h > parentH) h -= ch;
                }

                WidgetsSpaceHelper.UpdateSpace(widgetNode, new Rectangle(x, y, w, h));
            });
        }
    }
}