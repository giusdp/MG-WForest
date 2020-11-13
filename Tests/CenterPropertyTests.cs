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
        private Center _center;
        private WidgetTree _root;
        [SetUp]
        public void BeforeEach()
        {
            _center = new Center();
            _root = new WidgetTree(WidgetFactory.CreateContainer(new Rectangle(0, 0, 1280, 720)));
        }
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
            var child = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 120, 120)));

            var expected = new Rectangle(580, 300, 120, 120);

            _center.ApplyOn(_root);
            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_NoChildren_NothingHappens()
        {
            Assert.That(() => _center.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void ApplyOn_TwoIdenticalWidgets_CenteredOneAfterTheOther()
        {
            var child = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 120, 120)));
            var secondChild = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 120, 120)));

            var firstChildExpectedLoc = new Rectangle(520, 300, 120, 120);
            var secondChildExpectedLoc = new Rectangle(640, 300, 120, 120);

            _center.ApplyOn(_root);
            Assert.That(child.Data.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Data.Space, Is.EqualTo(secondChildExpectedLoc));
        }

        [Test]
        public void ApplyOn_TwoDifferentWidgets_CenterVerticallyWithBiggest()
        {
            var child = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var secondChild = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 120, 330)));

            var firstChildExpectedLoc = new Rectangle(470, 300, 220, 120);
            var secondChildExpectedLoc = new Rectangle(690, 195, 120, 330);

            _center.ApplyOn(_root);
            Assert.That(child.Data.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Data.Space, Is.EqualTo(secondChildExpectedLoc));
        }

        [Test]
        public void ApplyOn_WidgetPassHorizontalSize_ViolatingWidgetOnNewRow()
        {
            var acts = new[]
            {
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)))
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

            _center.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_ThreeRowsOfWidgets_CorrectlyCentered()
        {
            var acts = new[]
            {
                // First Row
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),

                // Second Row
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),

                // Third Row
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)))
            };

            var expects = new[]
            {
                new Rectangle(90, 180, 220, 120),
                new Rectangle(310, 180, 220, 120),
                new Rectangle(530, 180, 220, 120),
                new Rectangle(750, 180, 220, 120),
                new Rectangle(970, 180, 220, 120),
                new Rectangle(90, 300, 220, 120),
                new Rectangle(310, 300, 220, 120),
                new Rectangle(530, 300, 220, 120),
                new Rectangle(750, 300, 220, 120),
                new Rectangle(970, 300, 220, 120),
                new Rectangle(90, 420, 220, 120),
                new Rectangle(310, 420, 220, 120),
                new Rectangle(530, 420, 220, 120),
                new Rectangle(750, 420, 220, 120),
                new Rectangle(970, 420, 220, 120)
            };
            
            _center.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_ThreeRowsPlusOneWidget_TheLastCenteredOnNewRow()
        {
            var acts = new[]
            {
                // First Row
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),

                // Second Row
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),

                // Third Row
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                
                // The one more widget
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120)))

            };

            var expects = new[]
            {
                new Rectangle(90, 120, 220, 120), // first row
                new Rectangle(310, 120, 220, 120),
                new Rectangle(530, 120, 220, 120),
                new Rectangle(750, 120, 220, 120),
                new Rectangle(970, 120, 220, 120),
                new Rectangle(90, 240, 220, 120), // second row
                new Rectangle(310, 240, 220, 120),
                new Rectangle(530, 240, 220, 120),
                new Rectangle(750, 240, 220, 120),
                new Rectangle(970, 240, 220, 120),
                new Rectangle(90, 360, 220, 120), // third row
                new Rectangle(310, 360, 220, 120),
                new Rectangle(530, 360, 220, 120),
                new Rectangle(750, 360, 220, 120),
                new Rectangle(970, 360, 220, 120),
                
                new Rectangle(530, 480, 220, 120) // new widget
            };
            
            _center.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_TwoRowsWithDifferentHeights()
        {
            var acts = new[]
            {
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 220, 120))),

            };

            var expects = new[]
            {
                new Rectangle(16, 120, 220, 120),
                new Rectangle(236, 30, 120, 330),
                new Rectangle(356, 240, 220, 120),
                new Rectangle(576, 240, 220, 120),
                new Rectangle(796, 240, 220, 120),
                new Rectangle(924, 360, 220, 120),
                new Rectangle(1144, 360, 220, 120),
            };

            _center.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }
    }
}