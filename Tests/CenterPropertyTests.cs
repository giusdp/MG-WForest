using Microsoft.Xna.Framework;
using NUnit.Framework;
using PiBa.UI;
using PiBa.UI.Factories;
using PiBa.UI.Properties.Center;

namespace PiBa.Tests
{
    [TestFixture]
    public class CenterPropertyTests
    {
        [Test]
        public void GetLocationToCenterElementInRect_ReturnsPoint()
        {
            var point = Center.GetCoordsToCenterInSpace(Rectangle.Empty, Point.Zero);
            Assert.That(point, Is.InstanceOf<Point>());
        }

        [Test]
        public void GetLocationToCenterElementInRect_ReturnsPointToCenterInRect()
        {
            var expected = new Point(580, 300);
            var result = Center.GetCoordsToCenterInSpace(new Rectangle(0, 0, 1280, 720), new Point(120, 120));
            Assert.That(result, Is.EqualTo(expected));

            expected = new Point(270, 505);
            result = Center.GetCoordsToCenterInSpace(new Rectangle(200, 50, 640, 1080), new Point(300, 120));
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

        [Test]
        public void ApplyOn_WidgetPassHorizontalSize_ViolatingWidgetOnNewRow()
        {
            var center = new Center();
            var root = new WidgetTree(WidgetFactory.CreateContainer(new Rectangle(0, 0, 1280, 720)));

            var acts = new[]
            {
                root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)))
            };

            var expects = new[]
            {
                new Rectangle(90, 240, 220, 120),
                new Rectangle(310, 240, 220, 120),
                new Rectangle(530, 240, 220, 120),
                new Rectangle(750, 240, 220, 120),
                new Rectangle(970, 240, 220, 120),
                new Rectangle(530, 360, 220, 120)
            };

            center.ApplyOn(root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_ThreeRowsOfWidgets_CorrectlyCentered()
        {
            var center = new Center();
            var root = new WidgetTree(WidgetFactory.CreateContainer(new Rectangle(0, 0, 1280, 720)));

            // First Row
            var w1r1 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var w2r1 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var w3r1 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var w4r1 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var w5r1 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));

            // Second Row
            var w1r2 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var w2r2 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var w3r2 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var w4r2 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var w5r2 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));

            // Third Row
            var w1r3 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var w2r3 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var w3r3 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var w4r3 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var w5r3 = root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));

            var w1r1Expected = new Rectangle(90, 180, 220, 120);
            var w2r1Expected = new Rectangle(310, 180, 220, 120);
            var w3r1Expected = new Rectangle(530, 180, 220, 120);
            var w4r1Expected = new Rectangle(750, 180, 220, 120);
            var w5r1Expected = new Rectangle(970, 180, 220, 120);

            var w1r2Expected = new Rectangle(90, 300, 220, 120);
            var w2r2Expected = new Rectangle(310, 300, 220, 120);
            var w3r2Expected = new Rectangle(530, 300, 220, 120);
            var w4r2Expected = new Rectangle(750, 300, 220, 120);
            var w5r2Expected = new Rectangle(970, 300, 220, 120);

            var w1r3Expected = new Rectangle(90, 420, 220, 120);
            var w2r3Expected = new Rectangle(310, 420, 220, 120);
            var w3r3Expected = new Rectangle(530, 420, 220, 120);
            var w4r3Expected = new Rectangle(750, 420, 220, 120);
            var w5r3Expected = new Rectangle(970, 420, 220, 120);

            center.ApplyOn(root);
            Assert.That(w1r1.Data.Space, Is.EqualTo(w1r1Expected));
            Assert.That(w2r1.Data.Space, Is.EqualTo(w2r1Expected));
            Assert.That(w3r1.Data.Space, Is.EqualTo(w3r1Expected));
            Assert.That(w4r1.Data.Space, Is.EqualTo(w4r1Expected));
            Assert.That(w5r1.Data.Space, Is.EqualTo(w5r1Expected));
            Assert.That(w1r2.Data.Space, Is.EqualTo(w1r2Expected));
            Assert.That(w2r2.Data.Space, Is.EqualTo(w2r2Expected));
            Assert.That(w3r2.Data.Space, Is.EqualTo(w3r2Expected));
            Assert.That(w4r2.Data.Space, Is.EqualTo(w4r2Expected));
            Assert.That(w5r2.Data.Space, Is.EqualTo(w5r2Expected));
            Assert.That(w1r3.Data.Space, Is.EqualTo(w1r3Expected));
            Assert.That(w2r3.Data.Space, Is.EqualTo(w2r3Expected));
            Assert.That(w3r3.Data.Space, Is.EqualTo(w3r3Expected));
            Assert.That(w4r3.Data.Space, Is.EqualTo(w4r3Expected));
            Assert.That(w5r3.Data.Space, Is.EqualTo(w5r3Expected));
        }
    }
}