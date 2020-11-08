using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiBa.UI.Widgets
{
    public class ImageButton : Widget
    {
        public Texture2D NormalButton { get; set; }
        public Texture2D HoverButton { get; set; }
        public Texture2D PressedButton { get; set; }

        public ImageButton(Texture2D normalButton) : base(Rectangle.Empty)
        {
            NormalButton = normalButton ??
                           throw new ArgumentNullException($"{GetType()} Button sprite cannot be null!");
            Space = new Rectangle(0, 0, normalButton.Width, normalButton.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(NormalButton, new Vector2(Space.X, Space.Y), Color.White);
        }
    }
}