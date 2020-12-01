using System;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Properties.Grid.Utils;
using WForest.UI.Utils;

namespace WForest.UI.Properties.Grid
{
    public class Flex : Property
    {
        internal override void ApplyOn(WidgetTree widgetNode)
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

            // if (ApplyUtils.TryExtractRows(widgetNode, out var rows))
            // {
            //     rows.ForEach(l =>
            //     {
            //         var (x, y, w, h) = widgetNode.Data.Space;
            //         var newWidth = w + l.Width;
            //         var newHeight = h + l.Height;
            //         if (!widgetNode.IsRoot)
            //         {
            //             var (_, _, parentW, parentH) = widgetNode.Parent.Data.Space;
            //             if (newWidth > parentW) newWidth -= l.Width;
            //             if (newHeight > parentH) newHeight -= l.Height;
            //         }
            //
            //         WidgetsSpaceHelper.UpdateSpace(widgetNode, new Rectangle(x, y, newWidth, newHeight));
            //     });
            // }
            // else if (ApplyUtils.TryExtractColumns(widgetNode, out var cols))
            // {
            // }
        }

        private void IncreaseSpaceWithChildren(WidgetTree widgetNode)
        {
            widgetNode.Children.ForEach(c =>
            {
                var (x, y, w, h) = widgetNode.Data.Space;
                var (_, _, cw, ch) = c.Data.Space;
                var newWidth = w + cw;
                var newHeight = h + ch;
                if (!widgetNode.IsRoot)
                {
                    var (_, _, parentW, parentH) = widgetNode.Parent.Data.Space;
                    if (newWidth > parentW) newWidth -= cw;
                    if (newHeight > parentH) newHeight -= ch;
                }

                WidgetsSpaceHelper.UpdateSpace(widgetNode, new Rectangle(x, y, newWidth, newHeight));
            });
        }
    }
}