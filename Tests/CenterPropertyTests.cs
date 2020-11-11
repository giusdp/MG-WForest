using Microsoft.Xna.Framework;
using NUnit.Framework;
using PiBa.UI;
using PiBa.UI.Properties;
using PiBa.UI.Widgets;

namespace PiBa.Tests
{
    [TestFixture]
    public class CenterPropertyTests
    {
        [Test]
        public void GetLocationToCenterElementInRect_ReturnsPoint()
        {
            var point = Center.GetLocationToCenterElementInRect(Rectangle.Empty, Point.Zero);
            Assert.That(point, Is.InstanceOf<Point>());
        }

        [Test]
        public void GetLocationToCenterElementInRect_ReturnsPointToCenterInRect()
        {
            var expected = new Point(580, 300);
            var result = Center.GetLocationToCenterElementInRect(new Rectangle(0, 0, 1280, 720), new Point(120, 120));
            Assert.That(result, Is.EqualTo(expected));
            
            expected = new Point(270, 505);
            result = Center.GetLocationToCenterElementInRect(new Rectangle(200, 50, 640, 1080), new Point(300, 120));
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_WidgetWithoutParent_WidgetNotModified()
        {
            var center = new Center();
            var widgetNode = new WidgetTree(new Widget(new Rectangle(0, 0, 1280, 720)));
            var expected = widgetNode.Data.Space;
            center.ApplyOn(widgetNode);
            Assert.That(widgetNode.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_ReturnsCenteredWidgetRelativeToParent()
        {
            var center = new Center();
            var root = new WidgetTree(new Widget(new Rectangle(0, 0, 1280, 720)));
            var widget = new Container(new Rectangle(Point.Zero, new Point(120, 120)));
            var widgetNode =  root.AddChild(widget);
            var expected = new Rectangle(580, 300, 120, 120);
            center.ApplyOn(widgetNode);
            Assert.That(widgetNode.Data.Space, Is.EqualTo(expected));
        }
    }
}