using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Rendering.Drawables;
using WForest.Utilities;


namespace Tests
{
    [TestFixture]
    public class NinePatchTests
    {
        [Test]
        public void GetNinePatches_ShouldDivideSpaceWithPatchesSizes()
        {
            // arrange
            RectangleF[] ar;
            var left = 12;
            var right = 12;
            var top = 12;
            var bottom = 12;
            var size = new Vector2(600, 120);

            var expected = new[]
            {
                new RectangleF(0, 0, 12, 12),
                new RectangleF(12, 0, 576, 12),
                new RectangleF(588, 0, 12, 12),

                new RectangleF(0, 12, 12, 96),
                new RectangleF(12, 12, 576, 96),
                new RectangleF(588, 12, 12, 96),

                new RectangleF(0, 108, 12, 12),
                new RectangleF(12, 108, 576, 12),
                new RectangleF(588, 108, 12, 12),
            };
            // act
            ar = NinePatch.BuildNinePatches(size, left, right, top, bottom);

            // assert
            for (var i = 0; i < ar.Length; i++)
                Assert.That(ar[i], Is.EqualTo(expected[i]));
        }

        [Test]
        public void GetNinePatches_WithExternalPatchesCoveringVector_ShouldDivide()
        {
            // arrange
            RectangleF[] ar;
            var left = 32;
            var right = 32;
            var top = 6;
            var bottom = 6;
            var size = new Vector2(64, 12);

            var expected = new[]
            {
                new RectangleF(0, 0, 32, 6),
                new RectangleF(32, 0, 0, 6),
                new RectangleF(32, 0, 32, 6),

                new RectangleF(0, 6, 32, 0),
                new RectangleF(32, 6, 0, 0),
                new RectangleF(32, 6, 32, 0),

                new RectangleF(0, 6, 32, 6),
                new RectangleF(32, 6, 0, 6),
                new RectangleF(32, 6, 32, 6),
            };
            // act
            ar = NinePatch.BuildNinePatches(size, left, right, top, bottom);

            // assert
            for (var i = 0; i < ar.Length; i++)
                Assert.That(ar[i], Is.EqualTo(expected[i]));
        }

        // [Test]
        // public void GetNinePatches_WithSizeLessThanPatches_ShouldThrow()
        // {
        //     // arrange
        //     var left = 32;
        //     var right = 32;
        //     var top = 6;
        //     var bottom = 6;
        //     var size = new Vector2(63, 11);
        //     
        //     Assert.That(() => NinePatch.GetNinePatches(size, left,right,top,bottom), Throws.ArgumentException);
        // }
    }
}