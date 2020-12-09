using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.Utilities;

namespace WForest.UI.Widgets
{
    public class Block : Widget
    {

        Texture2D _texture;
        public Block(Rectangle space) : base(space)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _texture ??= spriteBatch.CreateTexture(Color);
            spriteBatch.Draw(_texture, Space, Color.White);
            base.Draw(spriteBatch);
        }
    }
}