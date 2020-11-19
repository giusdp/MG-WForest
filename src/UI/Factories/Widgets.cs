using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiBa.UI.Widgets;

namespace PiBa.UI.Factories
{
    public static class Widgets
    {
        public static ImageButton CreateImageButton(string image) =>
            new ImageButton(AssetLoader.Load<Texture2D>(image));


        public static Container CreateContainer(Rectangle space) => new Container(space);

        public static Container CreateContainer(Point size) => new Container(new Rectangle(new Point(0, 0), size));

        public static Container CreateContainer(int width, int height) =>
            new Container(new Rectangle(0, 0, width, height));
    }
}