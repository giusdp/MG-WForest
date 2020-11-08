using NUnit.Framework;
using PiBa.UI.Widgets;

namespace PiBa.Tests
{
    [TestFixture]
    public class SpriteButtonTests
    {
        [Test]
        public void SpriteButton_NullSprite_ThrowsError()
        {
            Assert.That(() => new ImageButton(null), Throws.ArgumentNullException);
        }
    }
}