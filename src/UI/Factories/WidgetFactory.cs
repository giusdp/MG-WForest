using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiBa.UI.Widgets;

namespace PiBa.UI.Factories
{
    public static class WidgetFactory
    {
        public static ImageButton CreateImageButton(string image)
        {
            return new ImageButton(AssetLoader.Load<Texture2D>(image));
        }

        public static Container CreateContainer(Rectangle space)
        {
            return new Container(space);
        }

    }
}