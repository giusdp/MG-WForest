using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RectangleF = WForest.Utilities.RectangleF;

namespace WForest.Rendering.DrawableAdapters
{
    public class NinePatch : Drawable
    {
        public RectangleF[] NinePatchRects;

        private int _left, _right, _top, _bottom;
        private RectangleF _renderRect;

        public NinePatch(Texture2D texture, int left, int right, int top, int bottom) : base(texture)
        {
            _left = left;
            _right = right;
            _top = top;
            _bottom = bottom;

            var r = texture.Bounds;
            _renderRect = new RectangleF(r.X, r.Y, r.Width, r.Height);
            NinePatchRects = GenerateNinePatches(_renderRect, left, right, top, bottom);
        }

        /// <summary>
        /// Generates nine patch rectangles each of equal size that cover the entire space given in input.
        /// Returns the nine patch array with 9 rectangles.
        /// </summary>
        /// <param name="stretchedSpace"></param>
        /// <returns></returns>
        public static RectangleF[] GenerateNinePatches(RectangleF stretchedSpace)
        {
            var patchWidth = stretchedSpace.Width / 3;
            var patchHeight = stretchedSpace.Height / 3;
            return GenerateNinePatches(stretchedSpace, patchWidth, patchWidth, patchHeight, patchHeight);
        }

        /// <summary>
        /// Generates nine patch rectangles based on the given sizes for the external patches.
        /// Returns the nine patch array with 9 rectangles.
        /// </summary>
        /// <param name="stretchedSpace"></param>
        /// <param name="leftWidth"></param>
        /// <param name="rightWidth"></param>
        /// <param name="topHeight"></param>
        /// <param name="bottomHeight"></param>
        /// <returns></returns>
        public static RectangleF[] GenerateNinePatches(RectangleF stretchedSpace, float leftWidth,
            float rightWidth, float topHeight, float bottomHeight)
        {
            RectangleF[] ninePatchesArray = new RectangleF[9];

            var centerWidth = stretchedSpace.Width - leftWidth - rightWidth;
            var centerHeight = stretchedSpace.Height - topHeight - bottomHeight;
            var leftX = stretchedSpace.X;
            var middleX = leftX + leftWidth;
            var rightX = middleX + centerWidth;
            var topY = stretchedSpace.Y;
            var middleY = topY + topHeight;
            var bottomY = middleY + centerHeight;

            ninePatchesArray[0] = new RectangleF(leftX, topY, leftWidth, topHeight); // top-left
            ninePatchesArray[1] = new RectangleF(middleX, topY, centerWidth, topHeight); // top-center
            ninePatchesArray[2] = new RectangleF(rightX, topY, rightWidth, topHeight); // top-right

            ninePatchesArray[3] = new RectangleF(leftX, middleY, leftWidth, centerHeight); // middle-left
            ninePatchesArray[4] = new RectangleF(middleX, middleY, centerWidth, centerHeight); // middle-center
            ninePatchesArray[5] = new RectangleF(rightX, middleY, rightWidth, centerHeight); // middle-right

            ninePatchesArray[6] = new RectangleF(leftX, bottomY, leftWidth, bottomHeight); // bottom-left
            ninePatchesArray[7] = new RectangleF(middleX, bottomY, centerWidth, bottomHeight); // bottom-center
            ninePatchesArray[8] = new RectangleF(rightX, bottomY, rightWidth, bottomHeight); // bottom-right

            return ninePatchesArray;
        }

        public override void Draw(IRenderer renderer, RectangleF space, Color color)
        {
            if (Math.Abs(_renderRect.Height - space.Height) > .001f || Math.Abs(_renderRect.Width - space.Width) > .001f)
            {
                _renderRect.Width = space.Width;
                _renderRect.Height = space.Height;
                NinePatchRects = GenerateNinePatches(_renderRect, _left, _right,  _top, _bottom);
            }
            for (var i = 0; i < 9; i++)
            {
                if (NinePatchRects[i].Width == 0 || NinePatchRects[i].Height == 0)
                    continue;

                var destRect = NinePatchRects[i];
                destRect.X += space.X;
                destRect.Y += space.Y;
                renderer.Draw(this, destRect, NinePatchRects[i], color);
            }
        }
    }
}