using Microsoft.Xna.Framework;
using NUnit.Framework;
using PiBa.UI.Widgets;

namespace PiBa.Tests
{
    [TestFixture]
    public class WidgetTests
    {
        [Test]
        public void IsHovered_LocationInside_ReturnsTrue()
        {
            var w= new Widget(new Rectangle(0,0,540,540));
            var b = w.IsHovered(new Point(332, 43));
            Assert.That(b, Is.True);
        }

        [Test]
        public void IsHovered_NotInside_ReturnsFalse()
        {
            var w= new Widget(new Rectangle(0,0,540,540));
            var b = w.IsHovered(new Point(332, 678));
            Assert.That(b, Is.False);
        }
    }
}