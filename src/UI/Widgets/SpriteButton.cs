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

        public SpriteButton(Sprite normalButton) 
            : base(new Rectangle(0, 0, normalButton.Texture2D.Width, normalButton.Texture2D.Height))
        {
            NormalButton = normalButton;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Console.WriteLine("Drawing button");
        }
    }
}