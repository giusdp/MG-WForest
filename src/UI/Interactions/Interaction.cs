namespace WForest.UI.Interactions
{
    /// <summary>
    /// Enum for the interaction state a widget can get be in.
    /// </summary>
    public enum Interaction
    {
        /// <summary> The widget is in a neutral state. </summary>
        Untouched,

        /// <summary> The input device (e.g. mouse cursor) entered the widget space and is now in focus. </summary>
        Entered,

        /// <summary> The input device (e.g. mouse cursor) left the widget space and is now out of focus. </summary>
        Exited,

        /// <summary> The input device (e.g. mouse cursor) pressed and is pressing on the widget. </summary>
        Pressed,

        /// <summary> The input device (e.g. mouse cursor) released the pressed widget while in it's space. </summary>
        Released
    }
}