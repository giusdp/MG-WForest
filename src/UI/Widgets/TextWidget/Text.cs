using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.UI.Widgets.TextWidget
{
    public class Text : Widget
    {
        private string _text;
        
        public DynamicSpriteFont Font { get; set; }
        public int Size { get; set; }
        public Text(string text, DynamicSpriteFont font, int size = 12) : base(Rectangle.Empty)
        {
            Font = font;
            Size = size;
            _text = text;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
           spriteBatch.DrawString(Font, _text, new Vector2(Space.Location.X, Space.Location.Y), Color.White); 
        }
    }
}