using Microsoft.Xna.Framework.Graphics;
using WForest.Rendering.DrawableAdapters;

namespace WForest.UI.Widgets.BuiltIn
{
    /// <summary>
    /// Widget that displays Texture2Ds based on hovering and pressed states which can be used as a button.
    /// </summary>
    public class ImageButton : TouchableWidget
    {
        public ImageButton(Texture2D normal, Texture2D? hover = null, Texture2D? press = null)
            : base(
                new Image(normal),
                hover is not null ? new Image(hover) : null,
                press is not null ? new Image(press) : null
            )
        {}
    }
}