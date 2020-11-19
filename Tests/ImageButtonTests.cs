using NUnit.Framework;
using WForest.UI.Widgets;

namespace WForest.Tests
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