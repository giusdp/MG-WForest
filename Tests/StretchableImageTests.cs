using NUnit.Framework;
using WForest.Rendering.DrawableAdapters;
using WForest.Utilities;

namespace Tests
{
    [TestFixture]
    public class StretchableImageTests
    {
        [Test]
        public void GenerateNinePatches_ShouldDivideSpaceWithSizes()
        {
            // arrange
            RectangleF[] ar;
            var width = 24;
            var height = 24;

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
            ar = StretchableImage.GenerateNinePatches(new RectangleF(0, 0, 600, 120), width, height);

            // assert
            for (var i = 0; i < ar.Length; i++)
                Assert.That(ar[i], Is.EqualTo(expected[i]));
        }

        // todo test with a stretched space less than normal space
    }
}