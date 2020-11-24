using Microsoft.Xna.Framework;
using WForest.UI.Properties.Grid.JustifyProps;
using WForest.UI.Properties.Grid.Utils;

namespace WForest.UI.Properties.Grid.ItemProps
{
    public class ItemBase : IProperty
    {
        public int Priority { get; } = 3;

        public void ApplyOn(WidgetTree widgetNode)
        {
            ApplyUtils.ApplyIfThereAreChildren(widgetNode, $"{widgetNode.Data} has no children to item-base.",
                () =>
                {
                    var rowsAtBase =
                        JustifyEnd.PutAtEnd(widgetNode, GridHelper.WidgetHeight, (y, c) => new Point(c.Data.Space.X, y));
                    var colsAtBase =
                        JustifyEnd.PutAtEnd(widgetNode, GridHelper.WidgetWidth, (x, c) => new Point(x, c.Data.Space.Y));

                    if (ApplyUtils.TryExtractRows(widgetNode, out var rows))
                        rowsAtBase(rows);
                    else if (ApplyUtils.TryExtractColumns(widgetNode, out var cols))
                        colsAtBase(cols);
                });
        }
    }
}