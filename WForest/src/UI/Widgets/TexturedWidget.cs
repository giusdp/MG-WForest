using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

namespace WForest.UI.Widgets
{
    public class TexturedWidget : Widget
    {
        protected Texture2D? NormalTexture;

        /// <summary>
        /// Texture2D to use when the widget is hovered.
        /// </summary>
        protected Texture2D? HoverTexture;

        /// <summary>
        /// Texture to use when the widget is pressed.
        /// </summary>
        protected Texture2D? PressTexture;

        protected TexturedWidget(RectangleF space) : base(space)
        {
        }
    }
}