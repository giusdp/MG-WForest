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

            OffsetBySize(rows, GridHelper.GetSubListHeight);
            CenterChildrenHorizontally(rows, wTree);
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

            OffsetBySize(columns, GridHelper.GetSubListWidth);
            
            CenterChildrenVertically(wTree.Children, columns);
        }

        private static (int, int) GetCenterCoords(Rectangle space, int maxWidth,
            int maxHeight)
        {
            var (x, y, width, height) = space;
            var centerX = (int) ((width + x) / 2f - maxWidth / 2f);
            var centerY = (int) ((height + y) / 2f - maxHeight / 2f);
            return (centerX, centerY);
        }

        private static void OffsetBySize(List<WidgetsDataSubList> rows, Func<WidgetsDataSubList, int> getSize)
        {
            if (rows.Count <= 1) return;

            var acc = getSize(rows[0]);
            for (var i = 1; i < rows.Count; i++)
            {
                rows[i].Y += acc;
                acc += getSize(rows[i]);
            }
        }
        

        private static void CenterChildrenHorizontally(List<WidgetsDataSubList> rows, WidgetTree tree)
        {
            var children = tree.Children;
            rows.ForEach(row =>
            {
                var xRow = row.X;
                for (var i = row.FirstWidgetIndex; i < row.LastWidgetIndex; i++)
                {
                    var child = children[i].Data;
                    child.Space = new Rectangle(new Point(xRow, row.Y), child.Space.Size);
                    xRow += child.Space.Width;
                }
            });
            
        }

        private static void CenterChildrenVertically(List<Tree<Widget>> children, List<WidgetsDataSubList> columns)
        {
            columns.ForEach(row =>
            {
                var yAcc = row.Y;
                for (var i = row.FirstWidgetIndex; i < row.LastWidgetIndex; i++)
                {
                    children[i].Data.Space = new Rectangle(new Point(row.X, yAcc), children[i].Data.Space.Size);
                    yAcc += children[i].Data.Space.Height;
                }
            });
        }
    }
}