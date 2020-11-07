using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiBa.Rendering;

namespace PiBa.UI.Widgets
{
    public class SpriteButton : Widget
    {
        private Sprite NormalButton;
        private Sprite HoverButton;
        private Sprite PressedButton;

        public SpriteButton(Sprite normalButton) : base(Rectangle.Empty)
        {
            NormalButton = normalButton ??
                           throw new ArgumentNullException($"{GetType()} Button sprite cannot be null!");
            Space = new Rectangle(0, 0, normalButton.Texture.Width, normalButton.Texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(NormalButton.Texture, new Vector2(Space.X, Space.Y), Color.White);
        }
    }
}