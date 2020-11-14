using System;
using System.Collections.Generic;
using System.Linq;

namespace PiBa.UI.Properties.Center
{
    internal class WidgetsRow
    {
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; }
            public int Height { get; private set; }
            public int FirstWidgetIndex { get; }
            public int LastWidgetIndex { get; }

            public WidgetsRow(int width, int height, int firstWidgetIndex, int lastWidgetIndex)
            {
                Width = width;
                Height = height;
                FirstWidgetIndex = firstWidgetIndex;
                LastWidgetIndex = lastWidgetIndex;
            }
            
            public static void OffsetHeightsPerRow(List<WidgetsRow> rows)
            {
                if (rows.Count <= 1) return;

                var acc = rows[0].Height;
                for (var i = 1; i < rows.Count; i++)
                {
                    rows[i].Y += acc;
                    acc += rows[i].Height;
                }
            }
    }
}