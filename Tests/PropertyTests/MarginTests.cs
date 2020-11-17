using Microsoft.Xna.Framework;
using NUnit.Framework;
using PiBa.UI;
using PiBa.UI.Factories;
using PiBa.UI.Properties.Margins;

namespace PiBa.Tests.PropertyTests
{
    [TestFixture]
    public class MarginPropertyTests
    {
        private WidgetTree _root;

        [SetUp]
        public void BeforeEach()
        {
            _root = new WidgetTree(WidgetFactory.CreateContainer(new Rectangle(0, 0, 1280, 720)));
        }

        [Test]
        public void ApplyOn_OneWidget_TotalSpaceOccupiedIsUpdatedWithMargin()
        {
            var margin = new Margin(3);
            var widget = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(Point.Zero, new Point(120, 120))));

            var expected = new Rectangle(0, 0, 126, 126);
            margin.ApplyOn(widget);

            Assert.That(widget.Data.TotalSpaceOccupied, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_3MarginWidgetInRow_SpaceOf3FromBorders()
        {
            var margin = new Margin(3);
            var widget = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(Point.Zero, new Point(120, 120))));

            var expected = new Rectangle(3, 3, 120, 120);
            margin.ApplyOn(widget);

            Assert.That(widget.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_WidgetInRow_SeparatesFromOtherWidget()
        {
            var marginWidgetExpected = new Rectangle(10, 10, 120, 120);
            var secondWidgetExpected = new Rectangle(140, 0, 120, 120);
            
            var margin = new Margin(10);
            var widget = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(Point.Zero, new Point(120, 120))));
            var secondWidget = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(Point.Zero, new Point(120, 120))));

            margin.ApplyOn(widget);

            PropertyFactory.Row().ApplyOn(_root);
            Assert.That(widget.Data.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Data.Space, Is.EqualTo(secondWidgetExpected));
        }
        
        [Test]
                public void ApplyOn_RowOfWidgets_OffsetsRowNotInternally()
                {
                    var marginWidgetExpected = new Rectangle(10, 10, 120, 120);
                    var secondWidgetExpected = new Rectangle(130, 10, 120, 120);
                    
                    var margin = new Margin(10);
                    var widget = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(Point.Zero, new Point(120, 120))));
                    var secondWidget = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(Point.Zero, new Point(120, 120))));


                    margin.ApplyOn(_root);
                    PropertyFactory.Row().ApplyOn(_root);
                    Assert.That(widget.Data.Space, Is.EqualTo(marginWidgetExpected));
                    Assert.That(secondWidget.Data.Space, Is.EqualTo(secondWidgetExpected));
                }
    }
}