using System;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Props.Grid.Utils;
using WForest.UI.Props.Interfaces;
using WForest.UI.Utils;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props.Grid.StretchingProps
{
    /// <summary>
    /// Property to make the space of the widget flexible. It stays as little as possible and grows to take the minimum
    /// space required to accomodate the children.
    /// </summary>
    public class Flex : IApplicableProp
    {
        internal Flex()
        {
        }

        public int Priority { get; set; }
        public event EventHandler? Applied;

        /// <summary>
        /// Gets the children of the widget and expands the space enough to accomodate them,
        /// depending on if the parent is Row or Column.
        /// </summary>
        /// <param name="widget"></param>
        public void ApplyOn(IWidget widget)
        {
            IncreaseSpaceWithChildren(widget);

            var (x, y, _, _) = widget.Space;

            var maybeRow = ApplyUtils.ExtractRowProp(widget);
            if (maybeRow.TryGetValue(out var row))
            {
                row.Applied += (sender, args) =>
                {
                    WidgetsSpaceHelper.UpdateSpace(widget,
                        new Rectangle(
                            x,
                            y,
                            row.Rows.Max(r => r.Width),
                            row.Rows.Sum(r => r.Height)));
                };
            }
            else
            {
                var maybeCol = ApplyUtils.ExtractColumnProp(widget);
                if (maybeCol.TryGetValue(out var col))
                    col.Applied += (sender, args) =>
                    {
                        WidgetsSpaceHelper.UpdateSpace(widget,
                            new Rectangle(
                                x,
                                y,
                                col.Columns.Sum(c => c.Width),
                                col.Columns.Sum(c => c.Height)));
                    };
            }

            OnApplied();
        }

        protected virtual void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);

        private static void IncreaseSpaceWithChildren(IWidget widgetNode)
        {
            int w = 0, h = 0;
            foreach (var c in widgetNode.Children)
            {
                var (x, y, _, _) = widgetNode.Space;
                var (_, _, cw, ch) = c.Space;
                w += cw;
                h += ch;
                if (widgetNode.IsRoot == false)
                {
                    var (_, _, parentW, parentH) = widgetNode.Parent!.Space;
                    if (w > parentW) w -= cw;
                    if (h > parentH) h -= ch;
                }

                WidgetsSpaceHelper.UpdateSpace(widgetNode, new Rectangle(x, y, w, h));
            }
        }
    }
}