using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.Utilities;

namespace WForest.UI.Widgets.BuiltIn
{
    /// <summary>
    /// Basic UI block, it's just a customizable colored rectangle.
    /// </summary>
    public class Block : Widget
    {
        private Texture2D? _texture;
        internal Block(Rectangle space) : base(space) { }

        /// <summary>
        /// Draws a colored rectangle (white if not set with the Color prop) to cover the Space of the widget.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // _texture ??= spriteBatch.CreateTexture(Color);
            spriteBatch.Draw(_texture, Space, Color.White);
        }
    }
}