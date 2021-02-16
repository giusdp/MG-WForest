using WForest.Rendering.DrawableAdapters;

namespace WForest.Widgets.BuiltIn
{
    /// <summary>
    /// Widget that displays Texture2Ds based on hovering and pressed states which can be used as a button.
    /// </summary>
    public class Button : TouchableWidget
    {
        public Button(Drawable normal, Drawable? hover = null, Drawable? press = null) : base(normal, hover, press)
        {}
    }
}