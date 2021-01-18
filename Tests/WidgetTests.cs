using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Factories;
using WForest.UI.Widgets;

namespace WForest.Tests
{
    [TestFixture]
    public class WidgetTests
    {
        [Test]
        public void SimpleContainer_StartsAtSizeZero()
        {
            var container = WidgetFactory.Container();
            Assert.That(container.Space, Is.EqualTo(Rectangle.Empty));
        }

        [Test]
        public void SpriteButton_NullSprite_ThrowsError()
        {
            Assert.That(() => new ImageButton(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Text_NullString_Throws()
        {
            Assert.That(() => new Text(null), Throws.ArgumentNullException);
        }
    }
}