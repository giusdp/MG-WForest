using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Widgets;

namespace WForest.Factories
{
    /// <summary>
    /// Static class to create Widgets.
    /// </summary>
    public static class WidgetFactory
    {
        /// <summary>
        /// Creates a <see cref="Block"/> widget with custom size.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>Block Widget</returns>
        public static Widget Block(int width, int height) => new Block(new Rectangle(0, 0, width, height));
        
        /// <summary>
        /// Creates a <see cref="ImageButton"/> widget with a NormalButtonTexture.
        /// </summary>
        /// <param name="texture2D"></param>
        /// <returns></returns>
        public static Widget ImageButton(Texture2D texture2D) => new ImageButton(texture2D);

        /// <summary>
        /// Creates a <see cref="Container()"/> widget with position and size of (0,0).
        /// </summary>
        /// <returns></returns>
        public static Widget Container() => new Container(Rectangle.Empty);
        
        /// <summary>
        /// Creates a <see cref="Container()"/> widget with a defined space.
        /// </summary>
        /// <param name="space"></param>
        /// <returns></returns>
        public static Widget Container(Rectangle space) => new Container(space);

        /// <summary>
        /// Creates a <see cref="Container()"/> with position of (0,0) and a defined size.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Widget Container(int width, int height) =>
            new Container(new Rectangle(0, 0, width, height));

        /// <summary>
        /// Creates a <see cref="Text"/> widget with a text string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Widget Text(string text) => new Text(text);
    }
}