using Microsoft.Xna.Framework;
using NUnit.Framework;
using PiBa.UI;
using PiBa.UI.Properties.Margins;
using PiBa.UI.Widgets;

namespace PiBa.Tests
{
    [TestFixture]
    public class MarginPropertyTests
    {
        [Test]
        public void ApplyOn()
        {
            var margin = new Margin(3);
            var root = new WidgetTree(new Widget(new Rectangle(0, 0, 1280, 720)));
            var widget = new Container(new Rectangle(Point.Zero, new Point(120, 120)));
            var widgetNode =  root.AddChild(widget);
            var expected = new Rectangle(580, 300, 120, 120);
            margin.ApplyOn(widgetNode);
            Assert.That(widgetNode.Data.Space, Is.EqualTo(expected));
        } 
    }
}