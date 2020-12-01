using Microsoft.Xna.Framework;
using WForest.UI.Properties.Grid.Utils;
using WForest.UI.Utils;

namespace WForest.UI.Properties.Grid
{
    public class Flex : Property
    {
        internal override void ApplyOn(WidgetTree widgetNode)
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
    }
}