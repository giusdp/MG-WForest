using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiBa.UI.Widgets
{
    public class Widget
    {
        public Rectangle Space { get; set; }

        public Widget(Rectangle space)
        {
            Space = space;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual bool IsHovered()
        {
            
            return false;
        }
    }
}