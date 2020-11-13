using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;
using Serilog;

namespace PiBa.UI.Properties.Center
{
    public class Center : IProperty
    {
        public void ApplyOn(WidgetTree widgetNode)
        {
            var widget = widgetNode.Data;

            if (widgetNode.Children.Count == 0)
            {
                Log.Warning(
                    $"{widget} has no children to center.");
                return;
            }

            var rows = BuildRowsWithWidgets(widgetNode);

            WidgetsRow.OffsetHeightsPerRow(rows);
            
            CenterAllChildrenInRows(rows, widgetNode.Children);
        }

        private static List<WidgetsRow> BuildRowsWithWidgets(WidgetTree widget)
        {
            var rows = new List<WidgetsRow>();
            var previousIndex = 0;

            var done = false;
            while (!done)
            {
                var (maxH, firstIndexOnNewRow) =
                    SumChildrenWidthsTilFit(widget.Children, previousIndex, widget.Data.Space.Size.X);
                
                var maxV = GetMaxHeightInChildrenSubList(widget.Children, previousIndex, firstIndexOnNewRow);

                var row = new WidgetsRow(maxV, previousIndex,
                    firstIndexOnNewRow < 0 ? widget.Children.Count : firstIndexOnNewRow);

                var (x, y) = GetCoordsToCenterInSpace(widget.Data.Space, new Point(maxH, maxV));
                row.X = x;
                row.Y = y;
                rows.Add(row);
                previousIndex = firstIndexOnNewRow;
                done = firstIndexOnNewRow == -1;
            }
            return rows;
        }

        private static void CenterAllChildrenInRows(List<WidgetsRow> rows, IReadOnlyList<Tree<Widget>> children)
        {
            rows.ForEach(row =>
            {
                var xRow = row.X;
                for (var i = row.FirstWidgetIndex; i < row.LastWidgetIndex; i++)
                {
                    var childWidget = children[i].Data;
                    CenterChildWidget(childWidget, xRow, row.Y, row.Height);

                    xRow += childWidget.Space.Size.X;
                }
            });
        }

        private static void CenterChildWidget(Widget child, int x, int y, int maxV)
        {
            if (child.Space.Size.Y != maxV) y += maxV / 2 - child.Space.Height / 2;
            child.Space = new Rectangle(new Point(x, y), child.Space.Size);
        }

        private static (int, int) SumChildrenWidthsTilFit(List<Tree<Widget>> children, int startIndex, int parentHSize)
        {
            var acc = 0;
            var indexOnNewRow = -1;

            for (var i = startIndex; i < children.Count; i++)
            {
                if (acc + children[i].Data.Space.Size.X > parentHSize)
                {
                    indexOnNewRow = i;
                    break;
                }

                acc += children[i].Data.Space.Size.X;
            }

            return (acc, indexOnNewRow);
        }

        private static int GetMaxHeightInChildrenSubList(List<Tree<Widget>> children, int from, int until) =>
            children.GetRange(from, until < 0 ? children.Count - from : until - from).Max(w => w.Data.Space.Size.Y);

        public static Point GetCoordsToCenterInSpace(Rectangle parent, Point size)
        {
            var (x, y, width, height) = parent;
            var (w, h) = size;
            return new Point(
                (int) ((width + x) / 2f - w / 2f),
                (int) ((height + y) / 2f - h / 2f)
            );
        }
    }
}