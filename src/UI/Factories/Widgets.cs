using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Widgets;

namespace WForest.UI.Factories
{
    public static class Widgets
    {
    
        public static Widget Block(int width, int height) => new Block(new Rectangle(0,0, width, height));
        public static Widget ImageButton(string image) =>
            new ImageButton(AssetLoader.Load<Texture2D>(image));

        public static Widget Container() => new Container(Rectangle.Empty);
        public static Widget Container(Rectangle space) => new Container(space);

        public static Widget Container(Point size) => new Container(new Rectangle(new Point(0, 0), size));

        public static Widget Container(int width, int height) =>
            new Container(new Rectangle(0, 0, width, height));
    }
}