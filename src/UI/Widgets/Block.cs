using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.Utilities;

namespace WForest.UI.Widgets
{
    public class Block : Widget
    {
        public Block(Rectangle space) : base(space)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(spriteBatch.BlankTexture(), Space, Color.Black);
            base.Draw(spriteBatch);
        }
    }
}