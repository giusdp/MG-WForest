namespace WForest.Utilities
{
    public readonly struct MarginValues
    {
       public MarginValues(int left, int right, int top, int bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public readonly int Left, Right, Top, Bottom; 
    }
}