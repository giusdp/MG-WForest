using NUnit.Framework;
using WForest.UI.Widgets;
using WForest.UI.Widgets.TextWidget;

namespace WForest.Tests
{
    [TestFixture]
    public class WidgetTests
    {
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