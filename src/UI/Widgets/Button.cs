using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiBa.Rendering;

namespace PiBa.UI.Widgets
{
    public class Button : Widget
    {
        private Sprite NormalButton;
        private Sprite HoverButton;
        private Sprite PressedButton;
        public Button(Rectangle space, Sprite normalButton, Sprite hoverButton, Sprite pressedButton) : base(space)
        {
            NormalButton = normalButton;
            HoverButton = hoverButton;
            PressedButton = pressedButton;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            Console.WriteLine("Drawing button");
        }
    }
}