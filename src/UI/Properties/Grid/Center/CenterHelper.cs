using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace WForest.UI.Properties.Grid.Center
{
    internal static class CenterHelper
    {
        #region Public API

        public static void ItemCenterVertical(WidgetTree wTree, List<WidgetsDataSubList> wLists)
        {
            var y = CenterY(wTree.Data.Space, wLists.Max(l => l.Height));
            wLists.ForEach(l => l.Y = y);
            CenterChildrenVertically(wTree, wLists);
        }

        public static void ItemCenterHorizontal(WidgetTree wTree, List<WidgetsDataSubList> wLists)
        {
            var x = CenterX(wTree.Data.Space, wLists.Max(l => l.Width));
            wLists.ForEach(l => l.X = x);
            CenterChildrenHorizontally(wTree, wLists);
        }

        public static void JustifyCenterByRow(WidgetTree wTree, List<WidgetsDataSubList> rows)
            => CenterByRow(wTree, rows, (r, x) =>
            {
                r.X = x;
                r.Y = wTree.Data.Space.Y;
            });

        public static void JustifyCenterByColumn(WidgetTree wTree, List<WidgetsDataSubList> columns)
            => CenterByColumn(wTree, columns, (c, y) =>
            {
                c.X = wTree.Data.Space.X;
                c.Y = y;
            });

        #endregion

        #region Backend

        private static void CenterByRow(WidgetTree wTree, List<WidgetsDataSubList> rows,
            Action<WidgetsDataSubList, int> setPosition)
        {
            var maxWidth = rows.Max(r => r.Width);
            var x = CenterX(wTree.Data.Space, maxWidth);
            rows.ForEach(r => setPosition(r, x));

            OffsetBySize(rows, (r, i) => r.Y += i, w => w.Height);
            CenterChildrenHorizontally(wTree, rows);
        }

        private static void CenterByColumn(WidgetTree wTree, List<WidgetsDataSubList> columns,
            Action<WidgetsDataSubList, int> setPosition)
        {
            var maxHeight = columns.Max(r => r.Height);
            var y = CenterY(wTree.Data.Space, maxHeight);
            columns.ForEach(r => setPosition(r, y));

            OffsetBySize(columns, (r, i) => r.X += i, w => w.Width);
            CenterChildrenVertically(wTree, columns);
        }

        private static int CenterY(Rectangle space, int maxHeight)
        {
            var (_, y, _, height) = space;
            var centerY = (int) ((height + y) / 2f - maxHeight / 2f);
            return centerY;
        }

        private static int CenterX(Rectangle space, int maxWidth)
        {
            var (x, _, width, _) = space;
            var centerX = (int) ((width + x) / 2f - maxWidth / 2f);
            return centerX;
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
                    ((WidgetTree) children[i]).UpdateSpace(new Rectangle(new Point(xRow, wY),
                        children[i].Data.Space.Size));
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
                    var wX = row.X + children[i].Data.Margin.Left;
                    yAcc += children[i].Data.Margin.Top;
                    ((WidgetTree) children[i]).UpdateSpace(new Rectangle(new Point(wX, yAcc),
                        children[i].Data.Space.Size));
                    yAcc += children[i].Data.Space.Height + children[i].Data.Margin.Bottom;
                }
            });
        }

        #endregion
    }
}