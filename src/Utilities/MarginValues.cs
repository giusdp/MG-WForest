namespace WForest.Utilities
{
    /// <summary>
    /// Simple struct to hold the margin values of a widget.
    /// </summary>
    public readonly struct MarginValues
    {
        /// <summary>
        /// Constructor to create a Margin values struct with the left, right, top and bottom values.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
       public MarginValues(float left, float right, float top, float bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        /// <summary>
        /// The margin values.
        /// </summary>
        public readonly float Left, Right, Top, Bottom;
    }
}