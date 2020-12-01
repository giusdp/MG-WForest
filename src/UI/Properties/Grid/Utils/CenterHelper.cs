using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Utils;

namespace WForest.UI.Properties.Grid.Utils
{
    internal static class CenterHelper
    {
        #region Public API

        public static void ItemCenterVertical(WidgetTree wTree, List<WidgetsDataSubList> wLists)
        {
            var children = wTree.Children;
            var totalHeight = wLists.Sum(l => l.Height);
            var startY = CenterCoord(wTree.Data.Space.Y, wTree.Data.Space.Height, totalHeight);
            var accY = 0;
            wLists.ForEach(l =>
            {
                l.Y = startY + accY;
                accY += l.Height;
            });
            wLists.ForEach(l =>
            {
                for (var i = l.FirstWidgetIndex; i < l.LastWidgetIndex; i++)
                {
                    var y = l.Y;
                    var (x, _, w, h) = children[i].Data.Space;
                    if (children[i].Data.Space.Height != l.Height)
                        y = CenterCoord(l.Y, l.Y + l.Height, h);

                    WidgetsSpaceHelper.UpdateSpace(children[i],new Rectangle(x, y, w, h));
                }
            });
        }

        public static void ItemCenterHorizontal(WidgetTree wTree, List<WidgetsDataSubList> wLists)
        {
            var children = wTree.Children;
            var totalWidth = wLists.Sum(l => l.Width);
            var startX = CenterCoord(wTree.Data.Space.X, wTree.Data.Space.Width, totalWidth);
            var accX = 0;
            wLists.ForEach(l =>
            {
                l.X = startX + accX;
                accX += l.Width;
            });
            wLists.ForEach(l =>
            {
                for (var i = l.FirstWidgetIndex; i < l.LastWidgetIndex; i++)
                {
                    var x = l.X;
                    var (_, y, w, h) = children[i].Data.Space;
                    if (children[i].Data.Space.Width != l.Width)
                        x = CenterCoord(l.X, l.X + l.Width, w);

                    WidgetsSpaceHelper.UpdateSpace(children[i],new Rectangle(x, y, w, h));
                }
            });
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
            var x = CenterCoord(wTree.Data.Space.X, wTree.Data.Space.Width, maxWidth);
            rows.ForEach(r => setPosition(r, x));

            OffsetBySize(rows, (r, i) => r.Y += i, w => w.Height);
            CenterChildrenHorizontally(wTree, rows);
        }

        private static void CenterByColumn(WidgetTree wTree, List<WidgetsDataSubList> columns,
            Action<WidgetsDataSubList, int> setPosition)
        {
            var maxHeight = columns.Max(r => r.Height);
            var y = CenterCoord(wTree.Data.Space.Y, wTree.Data.Space.Height, maxHeight);
            columns.ForEach(r => setPosition(r, y));

            OffsetBySize(columns, (r, i) => r.X += i, w => w.Width);
            CenterChildrenVertically(wTree, columns);
        }

        private static int CenterCoord(int start, int end, int sizeToCenter) => (end + start) / 2 - sizeToCenter / 2;

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

        private static void CenterChildrenHorizontally(WidgetTree tree, List<WidgetsDataSubList> lists)
        {
            var children = tree.Children;
            lists.ForEach(w =>
            {
                var xRow = w.X;
                for (var i = w.FirstWidgetIndex; i < w.LastWidgetIndex; i++)
                {
                    xRow += children[i].Data.Margin.Left;
                    var wY = w.Y + children[i].Data.Margin.Top;
                    WidgetsSpaceHelper.UpdateSpace(children[i], new Rectangle(new Point(xRow, wY),
                        children[i].Data.Space.Size));
                    xRow += children[i].Data.Space.Width + children[i].Data.Margin.Right;
                }
            });
        }

        private static void CenterChildrenVertically(WidgetTree tree, List<WidgetsDataSubList> lists)
        {
            var children = tree.Children;
            lists.ForEach(w =>
            {
                var yAcc = w.Y;
                for (var i = w.FirstWidgetIndex; i < w.LastWidgetIndex; i++)
                {
                    var wX = w.X + children[i].Data.Margin.Left;
                    yAcc += children[i].Data.Margin.Top;
                    WidgetsSpaceHelper.UpdateSpace(children[i],new Rectangle(new Point(wX, yAcc),
                        children[i].Data.Space.Size));
                    yAcc += children[i].Data.Space.Height + children[i].Data.Margin.Bottom;
                }
            });
        }

        #endregion
    }
}