using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.Rendering;
using WForest.Utilities;

namespace WForest.UI.Widgets.BuiltIn
{
    /// <summary>
    /// Basic UI block, it's just a customizable colored rectangle.
    /// </summary>
    public class Block : Widget
    {
        private Texture2D? _texture;
        public Block( RectangleF space) : base(space) { }

        /// <summary>
        /// Draws a colored rectangle (white if not set with the Color prop) to cover the Space of the widget.
        /// </summary>
        /// <param name="renderer"></param>
        public override void Draw(IRenderer renderer)
        {
            _texture ??= renderer.CreateTexture(Color);
            renderer.Draw(_texture, Space, Color.White);
            base.Draw(renderer);
        }
    }
}