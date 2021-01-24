using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Factories;
using WForest.UI.Props.Grid;
using WForest.UI.Props.Grid.StretchingProps;
using WForest.UI.Widgets;
using static WForest.Tests.Utils.HelperMethods;

namespace WForest.Tests.PropTests
{
    [TestFixture]
    public class FlexTests
    {
        private Flex _flex;
        private IWidget _root;

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
        public void ApplyOn_NoChildren_NothingHappens()
        {
            Assert.That(() => _flex.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void ApplyOn_WidgetWithoutRowOrCol_NothingHappens()
        {
            var child = WidgetFactory.Container(new Rectangle(0, 0, 120, 120));

            _root.AddChild(child);
            ApplyRow();

            var expected = new Rectangle(0, 0, 120, 120);

            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void FlexRowContainer_AsBigAsChild()
        {
            _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 120, 120)));

            ApplyRow();

            var expected = new Rectangle(0, 0, 120, 120);

            Assert.That(_root.Space, Is.EqualTo(expected));
        }

        [Test]
        public void FlexRowContainerWithTwoChildren_AsBigAsRow()
        {
            _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 120, 120)));
            _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 120, 120)));

            ApplyRow();

            var expected = new Rectangle(0, 0, 240, 120);

            Assert.That(_root.Space, Is.EqualTo(expected));
        }

        [Test]
        public void FlexColContainerWithTwoChildren_AsBigAsCol()
        {
            _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 120, 120)));
            _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 120, 120)));

            ApplyCol();

            var expected = new Rectangle(0, 0, 120, 240);

            Assert.That(_root.Space, Is.EqualTo(expected));
        }

        [Test]
        public void FlexContainerWithSize_IgnoresSizeAndStartsFromZero()
        {
            _root = WidgetFactory.Container(400, 300);
            _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 120, 60)));
            _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 120, 90)));

            _root.WithProp(PropertyFactory.Flex());
            ApplyRow();

            var expected = new Rectangle(0, 0, 240, 90);

            Assert.That(_root.Space, Is.EqualTo(expected));
        }
    }
}