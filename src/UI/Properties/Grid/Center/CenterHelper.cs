using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI.Properties.Grid.Center
{
    internal static class CenterHandler
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

            OffsetBySize(rows, (r, i) => r.Y += i, GridHelper.GetSubListHeight);
            CenterChildrenHorizontally(wTree, rows, GridHelper.GetWidgetWidth);
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

            OffsetBySize(columns, (r, i) => r.X += i, GridHelper.GetSubListWidth);

            CenterChildrenVertically(wTree, columns, GridHelper.GetWidgetHeight);
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


        private static void CenterChildrenHorizontally(WidgetTree tree, List<WidgetsDataSubList> rows,
            Func<Tree<Widget>, int> getSize)
        {
            var children = tree.Children;
            rows.ForEach(row =>
            {
                var xRow = row.X;
                for (var i = row.FirstWidgetIndex; i < row.LastWidgetIndex; i++)
                {
                    xRow += children[i].Data.Margin.Left;
                    children[i].Data.Space = new Rectangle(new Point(xRow, row.Y), children[i].Data.Space.Size);
                    xRow += getSize(children[i]) ;
                }
            });
        }

        private static void CenterChildrenVertically(WidgetTree tree, List<WidgetsDataSubList> columns,
            Func<Tree<Widget>, int> getSize)
        {
            var children = tree.Children;
            columns.ForEach(row =>
            {
                var yAcc = row.Y;
                for (var i = row.FirstWidgetIndex; i < row.LastWidgetIndex; i++)
                {
                    yAcc += children[i].Data.Margin.Top; 
                    children[i].Data.Space = new Rectangle(new Point(row.X, yAcc), children[i].Data.Space.Size);
                    yAcc += getSize(children[i]);
                }
            });
        }
    }
}