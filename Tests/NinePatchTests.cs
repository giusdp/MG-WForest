using NUnit.Framework;
using WForest.Rendering;
using WForest.Rendering.DrawableAdapters;
using WForest.Utilities;

namespace Tests
{
    [TestFixture]
    public class NinePatchTests
    {
        [Test]
        public void GenerateNinePatches_ShouldDivideSpaceEqually()
        {
            // arrange
            RectangleF[] ar;
            var expected = new[]
            {
                new RectangleF(0, 0, 200, 40),
                new RectangleF(200, 0, 200, 40),
                new RectangleF(400, 0, 200, 40),
                new RectangleF(0, 40, 200, 40),
                new RectangleF(200, 40, 200, 40),
                new RectangleF(400, 40, 200, 40),
                new RectangleF(0, 80, 200, 40),
                new RectangleF(200, 80, 200, 40),
                new RectangleF(400, 80, 200, 40),
            };
            // act
            ar = NinePatch.GenerateNinePatches(new RectangleF(0, 0, 600, 120));

            // assert
            for (var i = 0; i < ar.Length; i++)
                Assert.That(ar[i], Is.EqualTo(expected[i]));
        }

        [Test]
        public void GenerateNinePatches_WithSizes_ShouldDivideSpaceWithSizes()
        {
            // arrange
            RectangleF[] ar;
            var left = 12;
            var right = 12;
            var top = 12;
            var bottom = 12;

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
            ar = NinePatch.GenerateNinePatches(new RectangleF(0, 0, 600, 120), left, right, top, bottom);

            // assert
            for (var i = 0; i < ar.Length; i++)
                Assert.That(ar[i], Is.EqualTo(expected[i]));
        }

        [Test]
        public void GenerateNinePatches_WithDifferentCustomSizes_ShouldDivideSpaceWithDifferentSizes()
        {
            // arrange
            RectangleF[] ar;
            var left = 12;
            var right = 12;
            var top = 12;
            var bottom = 12;

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
            ar = NinePatch.GenerateNinePatches(new RectangleF(0, 0, 600, 120), left, right, top, bottom);

            // assert
            for (var i = 0; i < ar.Length; i++)
                Assert.That(ar[i], Is.EqualTo(expected[i]));
        }
    }
}