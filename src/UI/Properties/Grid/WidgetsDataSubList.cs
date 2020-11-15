namespace PiBa.UI.Properties.Grid
{
    internal class WidgetsDataSubList
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; }
        public int Height { get; }
        public int FirstWidgetIndex { get; }
        public int LastWidgetIndex { get; }

        public WidgetsDataSubList(int width, int height, int firstWidgetIndex, int lastWidgetIndex)
        {
            Width = width;
            Height = height;
            FirstWidgetIndex = firstWidgetIndex;
            LastWidgetIndex = lastWidgetIndex;
        }
        
    } 
}