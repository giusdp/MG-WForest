using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Widgets;

namespace WForest.UI.Factories
{
    public static class Widgets
    {
        public static ImageButton ImageButton(string image) =>
            new ImageButton(AssetLoader.Load<Texture2D>(image));


        public static Container Container(Rectangle space) => new Container(space);

        public static Container Container(Point size) => new Container(new Rectangle(new Point(0, 0), size));

        public static Container Container(int width, int height) =>
            new Container(new Rectangle(0, 0, width, height));
    }
}