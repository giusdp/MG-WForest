using NUnit.Framework;
using WForest.Exceptions;
using WForest.Factories;
using WForest.Props.Grid;
using WForest.Props.Grid.StretchingProps;
using WForest.Utilities;
using WForest.Widgets.Interfaces;
using static Tests.Utils.HelperMethods;

namespace Tests.PropTests
{
    [TestFixture]
    public class FlexTests
    {
        private Flex _flex = new Flex();
        private IWidget _root = WidgetFactory.Container();

        [SetUp]
        public void BeforeEach()
        {
            _flex = new Flex();
            _root = WidgetFactory.Container();
            _root.WithProp(_flex);
        }

        private void ApplyRow()
        {
            _root.WithProp(new Row());
            ApplyProps(_root);
        }

        private void ApplyCol()
        {
            _root.WithProp(new Column());
            ApplyProps(_root);
        }

        [Test]
        public void ApplyOn_NoRowOrCol_ThrowsExc()
        {
            Assert.That(() => _flex.ApplyOn(_root), Throws.TypeOf<IncompatibleWidgetException>());
        }

        [Test]
        public void FlexRowContainer_AsBigAsChild()
        {
            _root.AddChild(WidgetFactory.Container(new RectangleF(0, 0, 120, 120)));

            ApplyRow();

            var expected = new RectangleF(0, 0, 120, 120);

            Assert.That(_root.Space, Is.EqualTo(expected));
        }

        [Test]
        public void FlexRowContainerWithTwoChildren_AsBigAsRow()
        {
            _root.AddChild(WidgetFactory.Container(new RectangleF(0, 0, 120, 120)));
            _root.AddChild(WidgetFactory.Container(new RectangleF(0, 0, 120, 120)));

            ApplyRow();

            var expected = new RectangleF(0, 0, 240, 120);

            Assert.That(_root.Space, Is.EqualTo(expected));
        }

        [Test]
        public void FlexColContainerWithTwoChildren_AsBigAsCol()
        {
            _root.AddChild(WidgetFactory.Container(new RectangleF(0, 0, 120, 120)));
            _root.AddChild(WidgetFactory.Container(new RectangleF(0, 0, 120, 120)));

            ApplyCol();

            var expected = new RectangleF(0, 0, 120, 240);

            Assert.That(_root.Space, Is.EqualTo(expected));
        }

        [Test]
        public void FlexContainerWithSize_IgnoresSizeAndStartsFromZero()
        {
            _root = WidgetFactory.Container(400, 300);
            _root.AddChild(WidgetFactory.Container(new RectangleF(0, 0, 120, 60)));
            _root.AddChild(WidgetFactory.Container(new RectangleF(0, 0, 120, 90)));

            _root.WithProp(PropFactory.Flex());
            ApplyRow();

            var expected = new RectangleF(0, 0, 240, 90);

            Assert.That(_root.Space, Is.EqualTo(expected));
        }
    }
}