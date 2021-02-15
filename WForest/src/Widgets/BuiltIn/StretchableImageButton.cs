using WForest.Rendering.DrawableAdapters;

namespace WForest.Widgets.BuiltIn
{
    public class StretchableImageButton : TouchableWidget
    {
        public StretchableImageButton(float width, float height, StretchableImage normal, StretchableImage? hover = null, StretchableImage? press = null) 
            : base(normal, hover, press)
        {
        }
    }
}