using System;
using System.Collections.Generic;
using System.Linq;
using WForest.Utilities;
using WForest.Utilities.WidgetUtils;
using WForest.Widgets.Interfaces;

namespace WForest.UI.Props.Grid.Utils
{
    internal static class CenterHelper
    {
        #region Public API

        public static void ItemCenterVertical(IWidget widgetTree, List<WidgetsDataSubList> wLists)
        {
            var children = widgetTree.Children;
            var totalHeight = wLists.Sum(l => l.Height);
            var startY = CenterCoord(widgetTree.Space.Y, widgetTree.Space.Height,
                totalHeight);
            var accY = widgetTree.Margins.Top / 2;
            wLists.ForEach(l =>
            {
                l.Y = startY + accY;
                accY += l.Height;
            });
            wLists.ForEach(l =>
            {
                for (var i = l.FirstWidgetIndex; i < l.LastWidgetIndex; i++)
                {
                    var ith = children.ElementAt(i);
                    var y = l.Y;
                    var (x, _, w, h) = ith.Space;
                    if (Math.Abs(ith.Space.Height - l.Height) > 0.01f)
                        y = CenterCoord(l.Y, l.Height, h);

                    WidgetsSpaceHelper.UpdateSpace(ith, new RectangleF(x, y, w, h));
                }
            });
        }

        public static void ItemCenterHorizontal(IWidget wTree, List<WidgetsDataSubList> wLists)
        {
            var children = wTree.Children;
            var totalWidth = wLists.Sum(l => l.Width);
            var startX = CenterCoord(wTree.Space.X, wTree.Space.Width, totalWidth);
            var accX = wTree.Margins.Left / 2;
            wLists.ForEach(l =>
            {
                l.X = startX + accX;
                accX += l.Width;
            });
            wLists.ForEach(l =>
            {
                for (var i = l.FirstWidgetIndex; i < l.LastWidgetIndex; i++)
                {
                    var ith = children.ElementAt(i);
                    var x = l.X;
                    var (_, y, w, h) = ith.Space;
                    if (Math.Abs(ith.Space.Width - l.Width) > 0.01f)
                        x = CenterCoord(l.X, l.Width, w);

                    WidgetsSpaceHelper.UpdateSpace(ith, new RectangleF(x, y, w, h));
                }
            });
        }

        public static void JustifyCenterByRow(IWidget wTree, List<WidgetsDataSubList> rows)
            => CenterByRow(wTree, rows, (r, x) =>
            {
                r.X = x;
                r.Y = wTree.Space.Y;
            });

        public static void JustifyCenterByColumn(IWidget wTree, List<WidgetsDataSubList> columns)
            => CenterByColumn(wTree, columns, (c, y) =>
            {
                c.X = wTree.Space.X;
                c.Y = y;
            });

        #endregion

        #region Backend

        private static void CenterByRow(IWidget wTree, List<WidgetsDataSubList> rows,
            Action<WidgetsDataSubList, float> setPosition)
        {
            var maxWidth = rows.Max(r => r.Width);
            var x = CenterCoord(wTree.Space.X, wTree.Space.Width, maxWidth);
            x += wTree.Margins.Left / 2.0f;
            rows.ForEach(r => setPosition(r, x));

            OffsetBySize(rows, (r, i) => r.Y += i, w => w.Height);
            CenterChildrenHorizontally(wTree, rows);
        }

        private static void CenterByColumn(IWidget wTree, List<WidgetsDataSubList> columns,
            Action<WidgetsDataSubList, float> setPosition)
        {
            var maxHeight = columns.Max(r => r.Height);
            var y = CenterCoord(wTree.Space.Y, wTree.Space.Height, maxHeight);
            y += wTree.Margins.Top / 2.0f;
            columns.ForEach(r => setPosition(r, y));

            OffsetBySize(columns, (r, i) => r.X += i, w => w.Width);
            CenterChildrenVertically(wTree, columns);
        }

        private static float CenterCoord(float start, float end, float sizeToCenter) =>
            start / 2 + (end + start) / 2 - sizeToCenter / 2;

        private static void OffsetBySize(List<WidgetsDataSubList> lists, Action<WidgetsDataSubList, float> updatePos,
            Func<WidgetsDataSubList, float> getSize)
        {
            if (lists.Count <= 1) return;

            float acc = 0;
            foreach (var l in lists)
            {
                updatePos(l, acc);
                acc += getSize(l);
            }
        }

        private static void CenterChildrenHorizontally(IWidget tree, List<WidgetsDataSubList> lists)
        {
            var children = tree.Children;
            lists.ForEach(w =>
            {
                var xRow = w.X;
                for (var i = w.FirstWidgetIndex; i < w.LastWidgetIndex; i++)
                {
                    var ith = children.ElementAt(i);
                    xRow += ith.Margins.Left;
                    var wY = w.Y + ith.Margins.Top;
                    WidgetsSpaceHelper.UpdateSpace(ith, new RectangleF(xRow, wY,
                        ith.Space.Width, ith.Space.Height));
                    xRow += ith.Space.Width + ith.Margins.Right;
                }
            });
        }

        private static void CenterChildrenVertically(IWidget tree, List<WidgetsDataSubList> lists)
        {
            var children = tree.Children;
            lists.ForEach(w =>
            {
                var yAcc = w.Y;
                for (var i = w.FirstWidgetIndex; i < w.LastWidgetIndex; i++)
                {
                    var ith = children.ElementAt(i);
                    var wX = w.X + ith.Margins.Left;
                    yAcc += ith.Margins.Top;
                    WidgetsSpaceHelper.UpdateSpace(ith, new RectangleF(wX, yAcc,
                        ith.Space.Width, ith.Space.Height));
                    yAcc += ith.Space.Height + ith.Margins.Bottom;
                }
            });
        }

        #endregion
    }
}