using Microsoft.Xna.Framework;
using NUnit.Framework;
using PiBa.UI;
using PiBa.UI.Factories;
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
        public void ApplyOn_OneChildren_PutsItInCenter()
        {
            var center = new Center();
            var root = new WidgetTree(WidgetFactory.CreateContainer(new Rectangle(0, 0, 1280, 720)));
            var child = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 120, 120)));

            var expected = new Rectangle(580, 300, 120, 120);

            center.ApplyOn(root);
            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_NoChildren_NothingHappens()
        {
            var center = new Center();
            var root = new WidgetTree(WidgetFactory.CreateContainer(new Rectangle(0, 0, 1280, 720)));
            Assert.That(() => center.ApplyOn(root), Throws.Nothing);
        }

        [Test]
        public void ApplyOn_TwoIdenticalWidgets_CenteredOneAfterTheOther()
        {
            var center = new Center();
            var root = new WidgetTree(WidgetFactory.CreateContainer(new Rectangle(0, 0, 1280, 720)));
            var child = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 120, 120)));
            var secondChild = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 120, 120)));

            var firstChildExpectedLoc = new Rectangle(520, 300, 120, 120);
            var secondChildExpectedLoc = new Rectangle(640, 300, 120, 120);

            center.ApplyOn(root);
            Assert.That(child.Data.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Data.Space, Is.EqualTo(secondChildExpectedLoc));
        }

        [Test]
        public void ApplyOn_TwoDifferentWidgets_CenterVerticallyWithBiggest()
        {
           var center = new Center();
            var root = new WidgetTree(WidgetFactory.CreateContainer(new Rectangle(0, 0, 1280, 720)));
            
            var child = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var secondChild = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 120, 330)));

            var firstChildExpectedLoc = new Rectangle(470, 300, 220, 120);
            var secondChildExpectedLoc = new Rectangle(690, 195, 120, 330);

            center.ApplyOn(root);
            Assert.That(child.Data.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Data.Space, Is.EqualTo(secondChildExpectedLoc)); 
        }
    }
}