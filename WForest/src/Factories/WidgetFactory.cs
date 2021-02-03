using Microsoft.Xna.Framework.Graphics;
using WForest.UI.Widgets.BuiltIn;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

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
        public static IWidget Block(int width, int height) => new Block(new RectangleF(0, 0, width, height));

        /// <summary>
        /// Creates a <see cref="ImageButton"/> widget with a NormalButtonTexture.
        /// </summary>
        /// <param name="texture2D"></param>
        /// <returns></returns>
        public static IWidget ImageButton(Texture2D texture2D) => new ImageButton(texture2D);

        /// <summary>
        /// Creates a <see cref="Container()"/> widget with position and size of (0,0).
        /// </summary>
        /// <returns></returns>
        public static IWidget Container() => new Container(RectangleF.Empty);

        /// <summary>
        ///  Creates a <see cref="Container()"/> widget with a defined space.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static IWidget Container(float x, float y, float w, float h) =>
            new Container(new RectangleF(x, y, w, h));

        /// <summary>
        ///  Creates a <see cref="Container()"/> widget with a defined space using a <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static IWidget Container(RectangleF space) => new Container(space);

        /// <summary>
        /// Creates a <see cref="Container()"/> with position of (0,0) and a defined size.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static IWidget Container(float width, float height) =>
            new Container(new RectangleF(0, 0, width, height));

        /// <summary>
        /// Creates a <see cref="Text"/> widget with a text string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IWidget Text(string text) => new Text(text);
    }
}