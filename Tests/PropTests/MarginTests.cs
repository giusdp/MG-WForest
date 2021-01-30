using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Factories;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets;
using WForest.UI.Widgets.Interfaces;
using static WForest.Tests.Utils.HelperMethods;

namespace WForest.Tests.PropTests
{
    [TestFixture]
    public class MarginPropertyTests
    {
        private IWidget _root;
        private IWidget widget;
        private IWidget secondWidget;

        [SetUp]
        public void BeforeEach()
        {
            _root = new Widget(new Rectangle(0, 0, 1280, 720));
            widget = new Widget(new Rectangle(Point.Zero, new Point(120, 120)));
            secondWidget = new Widget(new Rectangle(Point.Zero, new Point(120, 120)));
            _root.AddChild(widget);
            _root.AddChild(secondWidget);
        }

        [Test]
        public void ApplyOn_OneWidget_TotalSpaceOccupiedIsUpdatedWithMargin()
        {
            IApplicableProp margin = (IApplicableProp) PropFactory.Margin(3);

            var expected = new Rectangle(0, 0, 126, 126);
            margin.ApplyOn(widget);

            Assert.That(widget.TotalSpaceOccupied, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_3MarginWidgetInRow_SpaceOf3FromBorders()
        {
            var margin = PropFactory.Margin(3);

            var expected = new Rectangle(3, 3, 120, 120);
            ((IApplicableProp)margin).ApplyOn(widget);

            Assert.That(widget.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_WidgetInRow_SeparatesFromOtherWidget()
        {
            var marginWidgetExpected = new Rectangle(10, 10, 120, 120);
            var secondWidgetExpected = new Rectangle(140, 0, 120, 120);

            var margin = PropFactory.Margin(10);

            ((IApplicableProp)margin).ApplyOn(widget);

            ((IApplicableProp)PropFactory.Row()).ApplyOn(_root);
            Assert.That(widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void ApplyOn_RowOfWidgets_OffsetsRowNotInternally()
        {
            var marginWidgetExpected = new Rectangle(10, 10, 120, 120);
            var secondWidgetExpected = new Rectangle(130, 10, 120, 120);

            var margin = PropFactory.Margin(10);

            ((IApplicableProp)margin).ApplyOn(_root);
            ((IApplicableProp)PropFactory.Row()).ApplyOn(_root);
            Assert.That(widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_MovesWidgetsInRow()
        {
            var marginWidgetExpected = new Rectangle(10, 0, 120, 120);
            var secondWidgetExpected = new Rectangle(130, 0, 120, 120);

            var margin = PropFactory.MarginLeft(10);

            ((IApplicableProp)margin).ApplyOn(widget);
            ((IApplicableProp)PropFactory.Row()).ApplyOn(_root);
            Assert.That(widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_SecondWidgetMovesSecondWidgetInRow()
        {
            var marginWidgetExpected = new Rectangle(0, 0, 120, 120);
            var secondWidgetExpected = new Rectangle(130, 0, 120, 120);

            var margin = PropFactory.MarginLeft(10);

            ((IApplicableProp)margin).ApplyOn(secondWidget);
            ((IApplicableProp)PropFactory.Row()).ApplyOn(_root);
            Assert.That(widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginRight_MovesSecondWidgetsInRow()
        {
            var marginWidgetExpected = new Rectangle(0, 0, 120, 120);
            var secondWidgetExpected = new Rectangle(130, 0, 120, 120);

            var margin = PropFactory.MarginRight(10);

            ((IApplicableProp)margin).ApplyOn(widget);
            ((IApplicableProp)PropFactory.Row()).ApplyOn(_root);
            Assert.That(widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginBottom_OnCenteredColFirstWidget_PushesDownSecondWidget()
        {
            var marginWidgetExpected = new Rectangle(0, 235, 120, 120);
            var secondWidgetExpected = new Rectangle(0, 365, 120, 120);

            var margin = PropFactory.MarginBottom(10);

            ((IApplicableProp)margin).ApplyOn(widget);

            _root.WithProp(PropFactory.Column());
            _root.WithProp(PropFactory.JustifyCenter());

            ApplyProps(_root);

            Assert.That(widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_OnWidgetInCenteredRow_PushesRow()
        {
            var marginWidgetExpected = new Rectangle(525, 0, 120, 120);
            var secondWidgetExpected = new Rectangle(645, 0, 120, 120);

            var margin = PropFactory.MarginLeft(10);

            ((IApplicableProp)margin).ApplyOn(widget);

            _root.WithProp(PropFactory.Row());
            _root.WithProp(PropFactory.JustifyCenter());

            ApplyProps(_root);

            Assert.That(widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_OnSecondWidgetInCenteredRow_PushesFirstWidget()
        {
            var marginWidgetExpected = new Rectangle(515, 0, 120, 120);
            var secondWidgetExpected = new Rectangle(645, 0, 120, 120);

            var margin = PropFactory.MarginLeft(10);

            ((IApplicableProp)margin).ApplyOn(secondWidget);

            _root.WithProp(PropFactory.Row());
            _root.WithProp(PropFactory.JustifyCenter());

            ApplyProps(_root);

            Assert.That(widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginTop_OnSecondWidgetInCenteredCol_PushesTopFirstWidget()
        {
            var marginWidgetExpected = new Rectangle(0, 235, 120, 120);
            var secondWidgetExpected = new Rectangle(0, 365, 120, 120);

            var margin = PropFactory.MarginTop(10);

            ((IApplicableProp)margin).ApplyOn(secondWidget);

            _root.WithProp(PropFactory.Column());
            _root.WithProp(PropFactory.JustifyCenter());

            ApplyProps(_root);

            Assert.That(widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_OnWidgetInSecondCenteredRow_PushesSecondRow()
        {
            var w2Expected = new Rectangle(40, 120, 220, 120);
            var w3Expected = new Rectangle(260, 120, 220, 120);

            var margin = PropFactory.MarginLeft(10);

            _root = new Widget(new Rectangle(0, 0, 1280, 720));
            _root.AddChild(new Widget(new Rectangle(Point.Zero, new Point(1220, 120))));

            IWidget w2 = new Widget(new Rectangle(Point.Zero, new Point(220, 120)));
            var w3 = new Widget(new Rectangle(Point.Zero, new Point(220, 120)));
            _root.AddChild(w2);
            _root.AddChild(w3);

            ((IApplicableProp)margin).ApplyOn(w2);

            _root.WithProp(PropFactory.Row());
            _root.WithProp(PropFactory.JustifyCenter());

            ApplyProps(_root);

            Assert.That(w2.Space, Is.EqualTo(w2Expected));
            Assert.That(w3.Space, Is.EqualTo(w3Expected));
        }

        [Test]
        public void MultipleMargins_OnFirstWidgetSecondRow()
        {
            var w2Expected = new Rectangle(40, 130, 220, 120);
            var w3Expected = new Rectangle(260, 120, 220, 120);

            var margin = PropFactory.Margin(10, 0, 10, 0);

            _root = new Widget(new Rectangle(0, 0, 1280, 720));
            _root.AddChild(new Widget(new Rectangle(Point.Zero, new Point(1220, 120))));

            IWidget w2 = new Widget(new Rectangle(Point.Zero, new Point(220, 120)));
            var w3 = new Widget(new Rectangle(Point.Zero, new Point(220, 120)));
            _root.AddChild(w2);
            _root.AddChild(w3);

            ((IApplicableProp)margin).ApplyOn(w2);

            _root.WithProp(PropFactory.Row());
            _root.WithProp(PropFactory.JustifyCenter());

            ApplyProps(_root);

            Assert.That(w2.Space, Is.EqualTo(w2Expected));
            Assert.That(w3.Space, Is.EqualTo(w3Expected));
        }

        [Test]
        public void BottomRightMargin_OnFirstWidgetFirstRow() //todo last test
        {
            var w2Expected = new Rectangle(255, 0, 1000, 120);
            var w3Expected = new Rectangle(25, 130, 220, 120);

            _root = new Widget(new Rectangle(0, 0, 1280, 720));
            IWidget w1 = new Widget(new Rectangle(Point.Zero, new Point(220, 120)));
            _root.AddChild(w1);
            
            ((IApplicableProp)PropFactory.Margin(0, 10, 0, 10)).ApplyOn(w1);

            IWidget w2 = new Widget(new Rectangle(Point.Zero, new Point(1000, 120)));
            _root.AddChild(w2);

            IWidget w3 =new Widget(new Rectangle(Point.Zero, new Point(220, 120)));
            _root.AddChild(w3);
            _root.AddChild(WidgetFactory.Container(new Rectangle(Point.Zero, new Point(220, 120))));

            _root.WithProp(PropFactory.Row());
            _root.WithProp(PropFactory.JustifyCenter());

            ApplyProps(_root);

            Assert.That(w2.Space, Is.EqualTo(w2Expected));
            Assert.That(w3.Space, Is.EqualTo(w3Expected));
        }
    }
}