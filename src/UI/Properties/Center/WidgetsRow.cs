using System;
using System.Collections.Generic;

namespace PiBa.UI.Properties.Center
{
    internal class WidgetsRow
    {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; }
            public int FirstWidgetIndex { get; }
            public int LastWidgetIndex { get; }

            public WidgetsRow(int height, int firstWidgetIndex, int lastWidgetIndex)
            {
                Height = height;
                FirstWidgetIndex = firstWidgetIndex;
                LastWidgetIndex = lastWidgetIndex;
            }
            
            public static void OffsetHeightsPerRow(List<WidgetsRow> rows)
            {
                if (rows.Count <= 1) return;

                if (rows.Count % 2 == 0)
                    OffsetEvenNumberOfRows(rows);
                else
                    OffsetOddNumberOfRows(rows);
            }
            
            private static void OffsetEvenNumberOfRows(List<WidgetsRow> rows)
            {
                var middleRow = rows.Count / 2;

                for (var i = 0; i < rows.Count; i++)
                {
                    var heightOffset = rows[i].Height / 2;
                    var difference = Math.Abs(middleRow - i);
                    if (i < middleRow)
                        rows[i].Y -= heightOffset * difference;
                    else
                        rows[i].Y += heightOffset * (difference + 1);
                }
            }
        
            private static void OffsetOddNumberOfRows(List<WidgetsRow> rows)
            {
                var middleRow = rows.Count / 2;
                for (var i = 0; i < rows.Count; i++)
                {
                    var heightOffset = rows[i].Height;
                    var difference = Math.Abs(middleRow - i);
                    if (i <= middleRow)
                        rows[i].Y -= heightOffset * difference;
                    else
                        rows[i].Y += heightOffset * difference;
                }
            }

    }
}