using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RectangleF = WForest.Utilities.RectangleF;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]

namespace WForest.Rendering.DrawableAdapters
{
    /// <summary>
    /// Nine patch drawable: keeps an array of 9 rectangles (patches) that cover the underlying texture
    /// so that the center rectangle can be stretched over the space of the widget while the external
    /// patches can be moved to the ends of the widget. The result is a texture that can be shrunk or enlarged.
    /// </summary>
    public class NinePatch : Drawable
    {
        private readonly RectangleF[] _ninePatchRects;

        private RectangleF[] _destRects;
        private Vector2 _imageSize;
        private readonly Func<Vector2, RectangleF[]> _generatePatches;

        public NinePatch(Texture2D texture, float leftPatchesWidth,
            float rightPatchesWidth, float topPatchesHeight, float bottomPatchesHeight) : base(texture)
        {
            _generatePatches = size =>
                GetNinePatches(size, leftPatchesWidth, rightPatchesWidth, topPatchesHeight, bottomPatchesHeight);

            _ninePatchRects = _generatePatches(new Vector2(Width, Height));
            _destRects = _ninePatchRects;
            _imageSize = new Vector2(texture.Width, texture.Height);
        }

        public override void Draw(IRenderer renderer, RectangleF space, Color color)
        {
            if (Math.Abs(_imageSize.Y - space.Height) > .001f ||
                Math.Abs(_imageSize.X - space.Width) > .001f)
            {
                _imageSize.X = space.Width;
                _imageSize.Y = space.Height;
                _destRects = _generatePatches(_imageSize);
            }

            for (var i = 0; i < 9; i++)
            {
                if (_ninePatchRects[i].Width == 0 || _ninePatchRects[i].Height == 0)
                    continue;

                var destRect = _destRects[i];
                destRect.X += space.X;
                destRect.Y += space.Y;
                renderer.Draw(this, destRect, _ninePatchRects[i], color);
            }
        }


        internal static RectangleF[] GetNinePatches(Vector2 size, float leftPatchesWidth,
            float rightPatchesWidth, float topPatchesHeight, float bottomPatchesHeight)
        {
            RectangleF[] ninePatchesArray = new RectangleF[9];

            var centerWidth = size.X - leftPatchesWidth - rightPatchesWidth;
            var centerHeight = size.Y - topPatchesHeight - bottomPatchesHeight;

            var leftX = 0;
            var middleX = leftPatchesWidth;
            var rightX = middleX + centerWidth;

            var topY = 0;
            var middleY = topPatchesHeight;
            var bottomY = middleY + centerHeight;

            ninePatchesArray[0] = new RectangleF(leftX, topY, leftPatchesWidth, topPatchesHeight); // top-left
            ninePatchesArray[1] = new RectangleF(middleX, topY, centerWidth, topPatchesHeight); // top-center
            ninePatchesArray[2] = new RectangleF(rightX, topY, rightPatchesWidth, topPatchesHeight); // top-right

            ninePatchesArray[3] = new RectangleF(leftX, middleY, leftPatchesWidth, centerHeight); // middle-left
            ninePatchesArray[4] = new RectangleF(middleX, middleY, centerWidth, centerHeight); // middle-center
            ninePatchesArray[5] = new RectangleF(rightX, middleY, rightPatchesWidth, centerHeight); // middle-right

            ninePatchesArray[6] = new RectangleF(leftX, bottomY, leftPatchesWidth, bottomPatchesHeight); // bottom-left
            ninePatchesArray[7] = new RectangleF(middleX, bottomY, centerWidth, bottomPatchesHeight); // bottom-center
            ninePatchesArray[8] =
                new RectangleF(rightX, bottomY, rightPatchesWidth, bottomPatchesHeight); // bottom-right

            return ninePatchesArray;
        }
    }
}