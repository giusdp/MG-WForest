using Microsoft.Xna.Framework;
using Serilog;

namespace WForest.UI.Properties.Grid
{
    public class JustifyEnd : IProperty
    {
        public int Priority { get; } = 2;

        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0)
            {
                Log.Warning($"{widgetNode.Data} has no children to justify-end.");
                return;
            }

            if (GridHelper.TryExtractRows(widgetNode, out var rows))
            {
                rows.ForEach(r =>
                {
                    // width of parent: widgetNode.Data.Space.Width
                    var xAcc = widgetNode.Data.Space.Width;
                    for (var i = r.FirstWidgetIndex; i < r.LastWidgetIndex; i++)
                    {
                        var childSpace = widgetNode.Children[i].Data.Space;
                        xAcc -= childSpace.Width;
                        ((WidgetTree) widgetNode.Children[i]).UpdateSpace(
                            new Rectangle(new Point(xAcc, childSpace.Y), childSpace.Size)
                            );
                    }
                });
            }
            else if (GridHelper.TryExtractColumns(widgetNode, out var cols))
            {
            }
        }
    }
}