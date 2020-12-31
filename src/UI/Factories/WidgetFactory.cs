using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Widgets;
using WForest.UI.Widgets.TextWidget;

namespace WForest.UI.Factories
{
    public static class WidgetFactory
    {
        public static Widget Block(int width, int height) => new Block(new Rectangle(0, 0, width, height));
        public static Widget ImageButton(Texture2D texture2D) => new ImageButton(texture2D);

        public static Widget Container() => new Container(Rectangle.Empty);
        public static Widget Container(Rectangle space) => new Container(space);

        public static Widget Container(int width, int height) =>
            new Container(new Rectangle(0, 0, width, height));

        public static Widget Text(string text) => new Text(text);
    }
}