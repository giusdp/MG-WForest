using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI.Factories;
using WForest.UI.Widgets;
using WForest.UI.Widgets.TextWidget;

namespace WForest.Tests
{
    [TestFixture]
    public class WidgetTests
    {
        [Test]
        public void SimpleContainer_StartsAtSizeZero()
        {
            var container = Widgets.Container();
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