namespace PiBa.Utilities
{
    public readonly struct Margin
    {
       public Margin(int left, int right, int top, int bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public readonly int Left, Right, Top, Bottom; 
    }
}