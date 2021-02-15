using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RectangleF = WForest.Utilities.RectangleF;

namespace WForest.Rendering.DrawableAdapters
{
    public class StretchableImage : Drawable
    {
        public RectangleF[] NinePatchRects;

        private RectangleF _renderRect;

        public StretchableImage(Texture2D texture, int widthIncrease, int heightIncrease) : base(texture)
        {
            _renderRect = new RectangleF(0, 0, texture.Width + widthIncrease, texture.Height + heightIncrease);
            NinePatchRects = GenerateNinePatches(_renderRect, widthIncrease, heightIncrease);
        }

        /// <summary>
        /// Generates nine patch rectangles based on the given space and the how much to stretch it
        /// with the widthIncrease and heightIncrease parameters.
        /// Returns the nine patch array with 9 rectangles.
        /// </summary>
        /// <param name="space"></param>
        /// <param name="widthIncrease"></param>
        /// <param name="heightIncrease"></param>
        /// <returns></returns>
        public static RectangleF[] GenerateNinePatches(RectangleF space, float widthIncrease, float heightIncrease)
        {
            RectangleF[] ninePatchesArray = new RectangleF[9];

            var centerWidth = space.Width - widthIncrease;
            var centerHeight = space.Height - heightIncrease;

            var sidePatchWidth = widthIncrease / 2;
            var sidePatchHeight = heightIncrease / 2;

            var leftX = space.X;
            var middleX = leftX + sidePatchWidth;
            var rightX = middleX + centerWidth;

            var topY = space.Y;
            var middleY = topY + sidePatchHeight;
            var bottomY = middleY + centerHeight;

            ninePatchesArray[0] = new RectangleF(leftX, topY, sidePatchWidth, sidePatchHeight); // top-left
            ninePatchesArray[1] = new RectangleF(middleX, topY, centerWidth, sidePatchHeight); // top-center
            ninePatchesArray[2] = new RectangleF(rightX, topY, sidePatchWidth, sidePatchHeight); // top-right

            ninePatchesArray[3] = new RectangleF(leftX, middleY, sidePatchWidth, centerHeight); // middle-left
            ninePatchesArray[4] = new RectangleF(middleX, middleY, centerWidth, centerHeight); // middle-center
            ninePatchesArray[5] = new RectangleF(rightX, middleY, sidePatchWidth, centerHeight); // middle-right

            ninePatchesArray[6] = new RectangleF(leftX, bottomY, sidePatchWidth, sidePatchHeight); // bottom-left
            ninePatchesArray[7] = new RectangleF(middleX, bottomY, centerWidth, sidePatchHeight); // bottom-center
            ninePatchesArray[8] = new RectangleF(rightX, bottomY, sidePatchWidth, sidePatchHeight); // bottom-right

            return ninePatchesArray;
        }

        public override void Draw(IRenderer renderer, RectangleF space, Color color)
        {
            if (Math.Abs(_renderRect.Height - space.Height) > .001f ||
                Math.Abs(_renderRect.Width - space.Width) > .001f)
            {
                _renderRect.Width = space.Width;
                _renderRect.Height = space.Height;
                NinePatchRects = GenerateNinePatches(_renderRect, _renderRect.Width, _renderRect.Height);
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