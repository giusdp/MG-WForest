using WForest.Rendering;
using WForest.Rendering.DrawableAdapters;

namespace WForest.UI.Widgets.BuiltIn
{
    public class NinePatchButton : TouchableWidget
    {
        public NinePatchButton(NinePatch normal, NinePatch? hover = null, NinePatch? press = null) 
            : base(normal, hover, press)
        {
        }
    }
}