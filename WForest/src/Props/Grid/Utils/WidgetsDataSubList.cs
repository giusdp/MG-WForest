namespace WForest.Props.Grid.Utils
{
    internal class WidgetsDataSubList
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; }
        public float Height { get; }
        public int FirstWidgetIndex { get; }
        public int LastWidgetIndex { get; }

        public WidgetsDataSubList(float width, float height, int firstWidgetIndex, int lastWidgetIndex)
        {
            Width = width;
            Height = height;
            FirstWidgetIndex = firstWidgetIndex;
            LastWidgetIndex = lastWidgetIndex;
        }
        
    } 
}