using Microsoft.Xna.Framework;
using WForest.Rendering;
using WForest.Rendering.DrawableAdapters;
using WForest.Utilities;

namespace WForest.Widgets.BuiltIn
{
    /// <summary>
    /// Basic UI block, it's just a customizable colored rectangle.
    /// </summary>
    public class Block : Widget
    {
        private Drawable? _block;
        public Block(RectangleF space) : base(space) { }

        /// <summary>
        /// Draws a colored rectangle (white if not set with the Color prop) to cover the Space of the widget.
        /// </summary>
        /// <param name="renderer"></param>
        public override void Draw(IRenderer renderer)
        {
            _block ??= new Image(renderer.CreateTexture(Color));
            renderer.Draw(_block, Space, Color.White);
            base.Draw(renderer);
        }
    }
}