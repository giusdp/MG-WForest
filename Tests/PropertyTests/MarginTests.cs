using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Factories;
using WForest.UI.WidgetTrees;

namespace WForest.Tests.PropertyTests
{
    [TestFixture]
    public class MarginPropertyTests
    {
        private WidgetTree _root;

        [SetUp]
        public void BeforeEach()
        {
            _root = new WidgetTree(WidgetFactory.Container(new Rectangle(0, 0, 1280, 720)));
        }

        [Test]
        public void ApplyOn_OneWidget_TotalSpaceOccupiedIsUpdatedWithMargin()
        {
            var margin = PropertyFactory.Margin(3);
            var widget = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));

            var expected = new Rectangle(0, 0, 126, 126);
            margin.ApplyOn(widget);

            Assert.That(widget.Data.TotalSpaceOccupied, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_3MarginWidgetInRow_SpaceOf3FromBorders()
        {
            var margin = PropertyFactory.Margin(3);
            var widget = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));

            var expected = new Rectangle(3, 3, 120, 120);
            margin.ApplyOn(widget);

            Assert.That(widget.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_WidgetInRow_SeparatesFromOtherWidget()
        {
            var marginWidgetExpected = new Rectangle(10, 10, 120, 120);
            var secondWidgetExpected = new Rectangle(140, 0, 120, 120);

            var margin = PropertyFactory.Margin(10);
            var widget = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));
            var secondWidget =
                _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));

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

            var margin = PropertyFactory.Margin(10);
            var widget = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));
            var secondWidget =
                _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));


            margin.ApplyOn(_root);
            PropertyFactory.Row().ApplyOn(_root);
            Assert.That(widget.Data.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Data.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_MovesWidgetsInRow()
        {
            var marginWidgetExpected = new Rectangle(10, 0, 120, 120);
            var secondWidgetExpected = new Rectangle(130, 0, 120, 120);

            var margin = PropertyFactory.MarginLeft(10);
            var widget = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));
            var secondWidget =
                _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));


            margin.ApplyOn(widget);
            PropertyFactory.Row().ApplyOn(_root);
            Assert.That(widget.Data.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Data.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_SecondWidgetMovesSecondWidgetInRow()
        {
            var marginWidgetExpected = new Rectangle(0, 0, 120, 120);
            var secondWidgetExpected = new Rectangle(130, 0, 120, 120);

            var margin = PropertyFactory.MarginLeft(10);
            var widget = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));
            var secondWidget =
                _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));


            margin.ApplyOn(secondWidget);
            PropertyFactory.Row().ApplyOn(_root);
            Assert.That(widget.Data.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Data.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginRight_MovesSecondWidgetsInRow()
        {
            var marginWidgetExpected = new Rectangle(0, 0, 120, 120);
            var secondWidgetExpected = new Rectangle(130, 0, 120, 120);

            var margin = PropertyFactory.MarginRight(10);
            var widget = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));
            var secondWidget =
                _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));


            margin.ApplyOn(widget);
            PropertyFactory.Row().ApplyOn(_root);
            Assert.That(widget.Data.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Data.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginBottom_OnCenteredColFirstWidget_PushesDownSecondWidget()
        {
            var marginWidgetExpected = new Rectangle(0, 235, 120, 120);
            var secondWidgetExpected = new Rectangle(0, 365, 120, 120);

            var margin = PropertyFactory.MarginBottom(10);
            var widget = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));
            var secondWidget =
                _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));


            margin.ApplyOn(widget);

            _root.WithProperty(PropertyFactory.Column());
            _root.WithProperty(PropertyFactory.JustifyCenter());

            _root.ApplyProperties();

            Assert.That(widget.Data.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Data.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_OnWidgetInCenteredRow_PushesRow()
        {
            var marginWidgetExpected = new Rectangle(525, 0, 120, 120);
            var secondWidgetExpected = new Rectangle(645, 0, 120, 120);

            var margin = PropertyFactory.MarginLeft(10);
            var widget = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));
            var secondWidget =
                _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));


            margin.ApplyOn(widget);

            _root.WithProperty(PropertyFactory.Row());
            _root.WithProperty(PropertyFactory.JustifyCenter());

            _root.ApplyProperties();

            Assert.That(widget.Data.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Data.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_OnSecondWidgetInCenteredRow_PushesFirstWidget()
        {
            var marginWidgetExpected = new Rectangle(515, 0, 120, 120);
            var secondWidgetExpected = new Rectangle(645, 0, 120, 120);

            var margin = PropertyFactory.MarginLeft(10);
            var widget = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));
            var secondWidget =
                _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));


            margin.ApplyOn(secondWidget);

            _root.WithProperty(PropertyFactory.Row());
            _root.WithProperty(PropertyFactory.JustifyCenter());

            _root.ApplyProperties();

            Assert.That(widget.Data.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Data.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginTop_OnSecondWidgetInCenteredCol_PushesTopFirstWidget()
        {
            var marginWidgetExpected = new Rectangle(0, 235, 120, 120);
            var secondWidgetExpected = new Rectangle(0, 365, 120, 120);

            var margin = PropertyFactory.MarginTop(10);
            var widget = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));
            var secondWidget =
                _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(120, 120))));


            margin.ApplyOn(secondWidget);

            _root.WithProperty(PropertyFactory.Column());
            _root.WithProperty(PropertyFactory.JustifyCenter());

            _root.ApplyProperties();

            Assert.That(widget.Data.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Data.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_OnWidgetInSecondCenteredRow_PushesSecondRow()
        {
            var w2Expected = new Rectangle(40, 120, 220, 120);
            var w3Expected = new Rectangle(260, 120, 220, 120);

            var margin = PropertyFactory.MarginLeft(10);
            _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(1220, 120))));

            var w2 = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(220, 120))));
            var w3 = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(220, 120))));

            margin.ApplyOn(w2);

            _root.WithProperty(PropertyFactory.Row());
            _root.WithProperty(PropertyFactory.JustifyCenter());

            _root.ApplyProperties();

            Assert.That(w2.Data.Space, Is.EqualTo(w2Expected));
            Assert.That(w3.Data.Space, Is.EqualTo(w3Expected));
        }

        [Test]
        public void MultipleMargins_OnFirstWidgetSecondRow()
        {
            var w2Expected = new Rectangle(40, 130, 220, 120);
            var w3Expected = new Rectangle(260, 120, 220, 120);

            var margin = PropertyFactory.Margin(10, 0, 10, 0);


            _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(1220, 120))));

            var w2 = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(220, 120))));
            var w3 = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(220, 120))));

            margin.ApplyOn(w2);

            _root.WithProperty(PropertyFactory.Row());
            _root.WithProperty(PropertyFactory.JustifyCenter());

            _root.ApplyProperties();

            Assert.That(w2.Data.Space, Is.EqualTo(w2Expected));
            Assert.That(w3.Data.Space, Is.EqualTo(w3Expected));
        }

        [Test]
        public void BottomRightMargin_OnFirstWidgetFirstRow() //todo last test
        {
            var w2Expected = new Rectangle(255, 0, 1000, 120);
            var w3Expected = new Rectangle(25, 130, 220, 120);

            var w1 = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(220, 120))));
            PropertyFactory.Margin(0, 10, 0, 10).ApplyOn(w1);
            
            var w2 = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(1000, 120))));

            var w3 = _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(220, 120))));
            _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(220, 120))));

            _root.WithProperty(PropertyFactory.Row());
            _root.WithProperty(PropertyFactory.JustifyCenter());

            _root.ApplyProperties();

            Assert.That(w2.Data.Space, Is.EqualTo(w2Expected));
            Assert.That(w3.Data.Space, Is.EqualTo(w3Expected));
        }
    }
}