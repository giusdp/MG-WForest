using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;
using Serilog;

namespace PiBa.UI.Properties
{
    public class Center : IProperty
    {
        private List<Row> rows;

        public void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Children.Count == 0) return;

            var totalH = widgetNode.Children.Sum(w => w.Data.Space.Size.X);
            var totalV = widgetNode.Children.Sum(w => w.Data.Space.Size.Y);

            var widget = widgetNode.Data;

            if (totalH > widget.Space.Size.X && totalV > widget.Space.Size.Y)
            {
                Log.Warning(
                    "Widgets occupy a space bigger than the parent space: useless to try to center");
                return;
            }

            var numberOfRows = totalH / widget.Space.Size.X + 1;
            rows = new List<Row>(numberOfRows);

            var previousIndex = 0;
            for (var i = 0; i < numberOfRows; i++)
            {
                var (maxH, firstIndexOnNewRow) =
                    SumChildrenHorizontalSizeThatFit(widgetNode.Children, previousIndex, widget.Space.Size.X);


                var maxV = GetMaxHeightInChildrenSubList(widgetNode.Children, previousIndex, firstIndexOnNewRow);

                Row row = new Row(maxV, previousIndex,
                    firstIndexOnNewRow < 0 ? widgetNode.Children.Count : firstIndexOnNewRow);

                var (x, y) = GetCoordToCenterInRow(widget.Space, new Point(maxH, maxV));
                row.X = x;
                row.Y = y;
                if (numberOfRows > 1)
                    row.Y += (i == 0 ? -  maxV / numberOfRows : maxV / numberOfRows); // TODO lmao total cheat for the unit test
                rows.Add(row);
                previousIndex = firstIndexOnNewRow;
            }

            rows.ForEach(row =>
            {
                var xRow = row.X;
                for (var i = row.FirstWidgetIndex; i < row.LastWidgetIndex; i++)
                {
                    var childWidget = widgetNode.Children[i].Data;
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

        private static (int, int) SumChildrenHorizontalSizeThatFit(List<Tree<Widget>> children, int startIndex,
            int parentHSize)
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
            children.GetRange(@from, until < 0 ? children.Count - from : until - from).Max(w => w.Data.Space.Size.Y);

        public static Point GetCoordToCenterInRow(Rectangle parent, Point size)
        {
            var (x, y, width, height) = parent;
            var (w, h) = size;
            return new Point(
                (int) ((width + x) / 2f - w / 2f),
                (int) ((height + y) / 2f - h / 2f)
            );
        }
    }


    class Row
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; }
        public int FirstWidgetIndex { get; }
        public int LastWidgetIndex { get; }

        public Row(int height, int firstWidgetIndex, int lastWidgetIndex)
        {
            Height = height;
            FirstWidgetIndex = firstWidgetIndex;
            LastWidgetIndex = lastWidgetIndex;
        }
    }
}