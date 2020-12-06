using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI;
using WForest.UI.Factories;
using WForest.UI.Properties.Grid;
using WForest.UI.Properties.Grid.Column;
using WForest.UI.Properties.Grid.Row;
using WForest.UI.WidgetTree;

namespace WForest.Tests.PropertyTests
{
    [TestFixture]
    public class FlexTests
    {
        private Flex _flex;
        private WidgetTree _root;

        [SetUp]
        public void BeforeEach()
        {
            _flex = new Flex();
            _root = new WidgetTree(Widgets.Container());
            _root.AddProperty(_flex);
        }

        private void ApplyRow()
        {
            _root.AddProperty(new Row());
            _root.ApplyProperties();
        }

        private void ApplyCol()
        {
            _root.AddProperty(new Column());
            _root.ApplyProperties();
        }

        [Test]
        public void ApplyOn_NoChildren_NothingHappens()
        {
            Assert.That(() => _flex.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void ApplyOn_WidgetWithoutRowOrCol_NothingHappens()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));

            ApplyRow();

            var expected = new Rectangle(0, 0, 120, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void FlexRowContainer_AsBigAsChild()
        {
            _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));

            ApplyRow();

            var expected = new Rectangle(0, 0, 120, 120);

            Assert.That(_root.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void FlexRowContainerWithTwoChildren_AsBigAsRow()
        {
            _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));
            _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));

            ApplyRow();

            var expected = new Rectangle(0, 0, 240, 120);

            Assert.That(_root.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void FlexColContainerWithTwoChildren_AsBigAsCol()
        {
            _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));
            _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));

            ApplyCol();

            var expected = new Rectangle(0, 0, 120, 240);

            Assert.That(_root.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void FlexContainerWithSize_IgnoresSizeAndStartsFromZero()
        {
            _root = new WidgetTree(Widgets.Container(400, 300));
            _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 60)));
            _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 90)));

            _root.AddProperty(Properties.Flex());
            ApplyRow();

            var expected = new Rectangle(0, 0, 240, 90);

            Assert.That(_root.Data.Space, Is.EqualTo(expected));
        }
    }
}