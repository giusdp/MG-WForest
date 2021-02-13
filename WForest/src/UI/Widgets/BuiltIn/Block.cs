using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.Rendering;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

namespace WForest.UI.Widgets.BuiltIn
{
    /// <summary>
    /// Basic UI block, it's just a customizable colored rectangle.
    /// </summary>
    public class Block : TexturedWidget
    {
        public Block(RectangleF space) : base(space) { }

        /// <summary>
        /// Draws a colored rectangle (white if not set with the Color prop) to cover the Space of the widget.
        /// </summary>
        /// <param name="renderer"></param>
        public override void Draw(IRenderer renderer)
        {
            NormalTexture ??= renderer.CreateTexture(Color);
            renderer.Draw(NormalTexture, Space, Color.White);
            base.Draw(renderer);
        }
    }
}