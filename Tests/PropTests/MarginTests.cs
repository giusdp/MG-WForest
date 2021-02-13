using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Factories;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;
using static Tests.Utils.HelperMethods;

namespace Tests.PropTests
{
    [TestFixture]
    public class MarginPropertyTests
    {
        private IWidget _root;
        private IWidget _widget;
        private IWidget _secondWidget;

        public MarginPropertyTests()
        {
            _root = new Widget(new RectangleF(0, 0, 1280, 720));
            _widget = new Widget(new RectangleF(Vector2.Zero, new Vector2(120, 120)));
            _secondWidget = new Widget(new RectangleF(Vector2.Zero, new Vector2(120, 120)));
        }

        [SetUp]
        public void BeforeEach()
        {
            _root = new Widget(new RectangleF(0, 0, 1280, 720));
            _widget = new Widget(new RectangleF(Vector2.Zero, new Vector2(120, 120)));
            _secondWidget = new Widget(new RectangleF(Vector2.Zero, new Vector2(120, 120)));
            _root.AddChild(_widget);
            _root.AddChild(_secondWidget);
        }

        [Test]
        public void ApplyOn_OneWidget_TotalSpaceOccupiedIsUpdatedWithMargin()
        {
            IApplicableProp margin = (IApplicableProp) PropFactory.Margin(3);

            var expected = new RectangleF(0, 0, 126, 126);
            margin.ApplyOn(_widget);

            Assert.That(_widget.TotalSpaceOccupied, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_3MarginWidgetInRow_SpaceOf3FromBorders()
        {
            var margin = PropFactory.Margin(3);

            var expected = new RectangleF(3, 3, 120, 120);
            ((IApplicableProp) margin).ApplyOn(_widget);

            Assert.That(_widget.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_WidgetInRow_SeparatesFromOtherWidget()
        {
            var marginWidgetExpected = new RectangleF(10, 10, 120, 120);
            var secondWidgetExpected = new RectangleF(140, 0, 120, 120);

            var margin = PropFactory.Margin(10);

            ((IApplicableProp) margin).ApplyOn(_widget);

            ((IApplicableProp) PropFactory.Row()).ApplyOn(_root);
            Assert.That(_widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(_secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void ApplyOn_RowOfWidgets_OffsetsRowNotInternally()
        {
            var marginWidgetExpected = new RectangleF(10, 10, 120, 120);
            var secondWidgetExpected = new RectangleF(130, 10, 120, 120);

            var margin = PropFactory.Margin(10);

            ((IApplicableProp) margin).ApplyOn(_root);
            ((IApplicableProp) PropFactory.Row()).ApplyOn(_root);
            Assert.That(_widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(_secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_MovesWidgetsInRow()
        {
            var marginWidgetExpected = new RectangleF(10, 0, 120, 120);
            var secondWidgetExpected = new RectangleF(130, 0, 120, 120);

            var margin = PropFactory.MarginLeft(10);

            ((IApplicableProp) margin).ApplyOn(_widget);
            ((IApplicableProp) PropFactory.Row()).ApplyOn(_root);
            Assert.That(_widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(_secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_SecondWidgetMovesSecondWidgetInRow()
        {
            var marginWidgetExpected = new RectangleF(0, 0, 120, 120);
            var secondWidgetExpected = new RectangleF(130, 0, 120, 120);

            var margin = PropFactory.MarginLeft(10);

            ((IApplicableProp) margin).ApplyOn(_secondWidget);
            ((IApplicableProp) PropFactory.Row()).ApplyOn(_root);
            Assert.That(_widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(_secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginRight_MovesSecondWidgetsInRow()
        {
            var marginWidgetExpected = new RectangleF(0, 0, 120, 120);
            var secondWidgetExpected = new RectangleF(130, 0, 120, 120);

            var margin = PropFactory.MarginRight(10);

            ((IApplicableProp) margin).ApplyOn(_widget);
            ((IApplicableProp) PropFactory.Row()).ApplyOn(_root);
            Assert.That(_widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(_secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginBottom_OnCenteredColFirstWidget_PushesDownSecondWidget()
        {
            var marginWidgetExpected = new RectangleF(0, 235, 120, 120);
            var secondWidgetExpected = new RectangleF(0, 365, 120, 120);

            var margin = PropFactory.MarginBottom(10);

            ((IApplicableProp) margin).ApplyOn(_widget);

            _root.WithProp(PropFactory.Column());
            _root.WithProp(PropFactory.JustifyCenter());

            ApplyProps(_root);

            Assert.That(_widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(_secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_OnWidgetInCenteredRow_PushesRow()
        {
            var marginWidgetExpected = new RectangleF(525, 0, 120, 120);
            var secondWidgetExpected = new RectangleF(645, 0, 120, 120);

            var margin = PropFactory.MarginLeft(10);

            ((IApplicableProp) margin).ApplyOn(_widget);

            _root.WithProp(PropFactory.Row());
            _root.WithProp(PropFactory.JustifyCenter());

            ApplyProps(_root);

            Assert.That(_widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(_secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_OnSecondWidgetInCenteredRow_PushesFirstWidget()
        {
            var marginWidgetExpected = new RectangleF(515, 0, 120, 120);
            var secondWidgetExpected = new RectangleF(645, 0, 120, 120);

            var margin = PropFactory.MarginLeft(10);

            ((IApplicableProp) margin).ApplyOn(_secondWidget);

            _root.WithProp(PropFactory.Row());
            _root.WithProp(PropFactory.JustifyCenter());

            ApplyProps(_root);

            Assert.That(_widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(_secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginTop_OnSecondWidgetInCenteredCol_PushesTopFirstWidget()
        {
            var marginWidgetExpected = new RectangleF(0, 235, 120, 120);
            var secondWidgetExpected = new RectangleF(0, 365, 120, 120);

            var margin = PropFactory.MarginTop(10);

            ((IApplicableProp) margin).ApplyOn(_secondWidget);

            _root.WithProp(PropFactory.Column());
            _root.WithProp(PropFactory.JustifyCenter());

            ApplyProps(_root);

            Assert.That(_widget.Space, Is.EqualTo(marginWidgetExpected));
            Assert.That(_secondWidget.Space, Is.EqualTo(secondWidgetExpected));
        }

        [Test]
        public void MarginLeft_OnWidgetInSecondCenteredRow_PushesSecondRow()
        {
            var w2Expected = new RectangleF(40, 120, 220, 120);
            var w3Expected = new RectangleF(260, 120, 220, 120);

            var margin = PropFactory.MarginLeft(10);

            _root = new Widget(new RectangleF(0, 0, 1280, 720));
            _root.AddChild(new Widget(new RectangleF(Vector2.Zero, new Vector2(1220, 120))));

            IWidget w2 = new Widget(new RectangleF(Vector2.Zero, new Vector2(220, 120)));
            var w3 = new Widget(new RectangleF(Vector2.Zero, new Vector2(220, 120)));
            _root.AddChild(w2);
            _root.AddChild(w3);

            ((IApplicableProp) margin).ApplyOn(w2);

            _root.WithProp(PropFactory.Row());
            _root.WithProp(PropFactory.JustifyCenter());

            ApplyProps(_root);

            Assert.That(w2.Space, Is.EqualTo(w2Expected));
            Assert.That(w3.Space, Is.EqualTo(w3Expected));
        }

        [Test]
        public void MultipleMargins_OnFirstWidgetSecondRow()
        {
            var w2Expected = new RectangleF(40, 130, 220, 120);
            var w3Expected = new RectangleF(260, 120, 220, 120);

            var margin = PropFactory.Margin(10, 0, 10, 0);

            _root = new Widget(new RectangleF(0, 0, 1280, 720));
            _root.AddChild(new Widget(new RectangleF(Vector2.Zero, new Vector2(1220, 120))));

            IWidget w2 = new Widget(new RectangleF(Vector2.Zero, new Vector2(220, 120)));
            var w3 = new Widget(new RectangleF(Vector2.Zero, new Vector2(220, 120)));
            _root.AddChild(w2);
            _root.AddChild(w3);

            ((IApplicableProp) margin).ApplyOn(w2);

            _root.WithProp(PropFactory.Row());
            _root.WithProp(PropFactory.JustifyCenter());

            ApplyProps(_root);

            Assert.That(w2.Space, Is.EqualTo(w2Expected));
            Assert.That(w3.Space, Is.EqualTo(w3Expected));
        }

        [Test]
        public void BottomRightMargin_OnFirstWidgetFirstRow() //todo last test
        {
            var w2Expected = new RectangleF(255, 0, 1000, 120);
            var w3Expected = new RectangleF(25, 130, 220, 120);

            _root = new Widget(new RectangleF(0, 0, 1280, 720));
            IWidget w1 = new Widget(new RectangleF(Vector2.Zero, new Vector2(220, 120)));
            _root.AddChild(w1);

            ((IApplicableProp) PropFactory.Margin(0, 10, 0, 10)).ApplyOn(w1);

            IWidget w2 = new Widget(new RectangleF(Vector2.Zero, new Vector2(1000, 120)));
            _root.AddChild(w2);

            IWidget w3 = new Widget(new RectangleF(Vector2.Zero, new Vector2(220, 120)));
            _root.AddChild(w3);
            _root.AddChild(WidgetFactory.Container(new RectangleF(Vector2.Zero, new Vector2(220, 120))));

            _root.WithProp(PropFactory.Row());
            _root.WithProp(PropFactory.JustifyCenter());

            ApplyProps(_root);

            Assert.That(w2.Space, Is.EqualTo(w2Expected));
            Assert.That(w3.Space, Is.EqualTo(w3Expected));
        }
    }
}