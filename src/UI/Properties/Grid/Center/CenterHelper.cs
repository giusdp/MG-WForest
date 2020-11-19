using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace WForest.UI.Properties.Grid.Center
{
    internal static class CenterHelper
    {
        public static void CenterByRow(WidgetTree wTree, List<WidgetsDataSubList> rows)
        {
            var maxWidth = rows.Max(r => r.Width);
            var totalHeight = rows.Sum(r => r.Height);
            var (x, y) = GetCenterCoords(wTree.Data.Space, maxWidth, totalHeight);

            rows.ForEach(r =>
            {
                r.X = x;
                r.Y = y;
            });

            OffsetBySize(rows, (r, i) => r.Y += i, w => w.Height);
            CenterChildrenHorizontally(wTree, rows);
        }

        public static void CenterByColumn(WidgetTree wTree, List<WidgetsDataSubList> columns)
        {
            var totalWidth = columns.Sum(r => r.Width);
            var maxHeight = columns.Max(r => r.Height);
            var (x, y) = GetCenterCoords(wTree.Data.Space, totalWidth, maxHeight);
            columns.ForEach(r =>
            {
                r.X = x;
                r.Y = y;
            });

            OffsetBySize(columns, (r, i) => r.X += i, w => w.Width);

            CenterChildrenVertically(wTree, columns);
        }

        private static (int, int) GetCenterCoords(Rectangle space, int maxWidth,
            int maxHeight)
        {
            var (x, y, width, height) = space;
            var centerX = (int) ((width + x) / 2f - maxWidth / 2f);
            var centerY = (int) ((height + y) / 2f - maxHeight / 2f);
            return (centerX, centerY);
        }

        private static void OffsetBySize(List<WidgetsDataSubList> lists, Action<WidgetsDataSubList, int> updatePos,
            Func<WidgetsDataSubList, int> getSize)
        {
            if (lists.Count <= 1) return;

            var acc = 0;
            foreach (var l in lists)
            {
                updatePos(l, acc);
                acc += getSize(l);
            }
        }


        private static void CenterChildrenHorizontally(WidgetTree tree, List<WidgetsDataSubList> rows)
        {
            var children = tree.Children;
            rows.ForEach(row =>
            {
                var xRow = row.X;
                for (var i = row.FirstWidgetIndex; i < row.LastWidgetIndex; i++)
                {
                    xRow += children[i].Data.Margin.Left;
                    var wY = row.Y + children[i].Data.Margin.Top;
                    children[i].Data.Space = new Rectangle(new Point(xRow, wY), children[i].Data.Space.Size);
                    xRow += children[i].Data.Space.Width + children[i].Data.Margin.Right;
                }
            });
        }

        private static void CenterChildrenVertically(WidgetTree tree, List<WidgetsDataSubList> columns)
        {
            var children = tree.Children;
            columns.ForEach(row =>
            {
                var yAcc = row.Y;
                for (var i = row.FirstWidgetIndex; i < row.LastWidgetIndex; i++)
                {
                    yAcc += children[i].Data.Margin.Top;
                    children[i].Data.Space = new Rectangle(new Point(row.X + children[i].Data.Margin.Left, yAcc), children[i].Data.Space.Size);
                    yAcc += children[i].Data.Space.Height + children[i].Data.Margin.Bottom;
                }
            });
        }
    }
}