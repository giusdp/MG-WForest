using NUnit.Framework;
using WForest.Factories;
using WForest.Props.Grid;
using WForest.Props.Grid.ItemProps;
using WForest.Props.Grid.JustifyProps;
using WForest.Props.Interfaces;
using WForest.Utilities;
using WForest.Widgets.Interfaces;
using static Tests.Utils.HelperMethods;

namespace Tests.PropTests
{
    [TestFixture]
    public class ItemCenterTests
    {
        private ItemCenter _itemCenter;
        private IWidget _root;

        public ItemCenterTests()
        {
            _itemCenter = new ItemCenter();
            _root = WidgetFactory.Container(new RectangleF(0, 0, 1280, 720));
        }

        [SetUp]
        public void BeforeEach()
        {
            _itemCenter = new ItemCenter();
            _root = WidgetFactory.Container(new RectangleF(0, 0, 1280, 720));
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
            Assert.That(() => _itemCenter.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void OnARow_PutsChildrenCenteredVertically()
        {
            var c = WidgetFactory.Container(20, 20);
            _root.AddChild(c);
            ApplyRow();
            _itemCenter.ApplyOn(_root);
            Assert.That(c.Space, Is.EqualTo(new RectangleF(0, 350, 20, 20)));
        }

        [Test]
        public void OnACol_PutsChildrenCenteredHorizontally()
        {
            var c = WidgetFactory.Container(20, 20);
            _root.AddChild(c);
            ApplyCol();
            _itemCenter.ApplyOn(_root);
            Assert.That(c.Space, Is.EqualTo(new RectangleF(630, 0, 20, 20)));
        }

        [Test]
        public void OnACenteredRow_CentersCorrectly()
        {
            var c = WidgetFactory.Container(20, 20);
            _root.AddChild(c);
            ApplyRow();
            ((IApplicableProp) PropFactory.JustifyCenter()).ApplyOn(_root);
            _itemCenter.ApplyOn(_root);
            Assert.That(c.Space, Is.EqualTo(new RectangleF(630, 350, 20, 20)));
        }

        [Test]
        public void OnACenteredRowWithThreeWidgetsOfDiffSizes_CentersCorrectly()
        {
            var c = WidgetFactory.Container(20, 20);
            var c1 = WidgetFactory.Container(30, 40);
            var c2 = WidgetFactory.Container(20, 30);
            _root.AddChild(c);
            _root.AddChild(c1);
            _root.AddChild(c2);
            ApplyRow();
            ((IApplicableProp) PropFactory.JustifyCenter()).ApplyOn(_root);
            _itemCenter.ApplyOn(_root);
            Assert.That(c.Space, Is.EqualTo(new RectangleF(605, 350, 20, 20)));
            Assert.That(c1.Space, Is.EqualTo(new RectangleF(625, 340, 30, 40)));
            Assert.That(c2.Space, Is.EqualTo(new RectangleF(655, 345, 20, 30)));
        }

        [Test]
        public void OnMultipleCenteredRows_Centers()
        {
            var c = WidgetFactory.Container(1120, 20);
            var c1 = WidgetFactory.Container(100, 40);
            var c2 = WidgetFactory.Container(120, 30);
            _root.AddChild(c);
            _root.AddChild(c1);
            _root.AddChild(c2);

            ApplyRow();
            ((IApplicableProp) PropFactory.JustifyCenter()).ApplyOn(_root);
            _itemCenter.ApplyOn(_root);
            Assert.That(c.Space, Is.EqualTo(new RectangleF(30, 335, 1120, 20)));
            Assert.That(c1.Space, Is.EqualTo(new RectangleF(1150, 325, 100, 40)));
            Assert.That(c2.Space, Is.EqualTo(new RectangleF(30, 365, 120, 30)));
        }

        [Test]
        public void OnACenteredColWithThreeWidgetsOfDiffSizes_CentersCorrectly()
        {
            var c = WidgetFactory.Container(20, 20);
            var c1 = WidgetFactory.Container(30, 40);
            var c2 = WidgetFactory.Container(20, 30);
            _root.AddChild(c);
            _root.AddChild(c1);
            _root.AddChild(c2);

            ApplyCol();
            _root.WithProp(new JustifyCenter());
            _root.WithProp(new ItemCenter());
            TreeVisitor.ApplyPropsOnTree(_root);
            Assert.That(c.Space, Is.EqualTo(new RectangleF(630, 315, 20, 20)));
            Assert.That(c1.Space, Is.EqualTo(new RectangleF(625, 335, 30, 40)));
            Assert.That(c2.Space, Is.EqualTo(new RectangleF(630, 375, 20, 30)));
        }

        [Test]
        public void OnMultipleCenteredCols_Centers()
        {
            var c = WidgetFactory.Container(1120, 20);
            var c1 = WidgetFactory.Container(100, 40);
            var c2 = WidgetFactory.Container(120, 30);
            _root.AddChild(c);
            _root.AddChild(c1);
            _root.AddChild(c2);

            ApplyCol();
            ((IApplicableProp) PropFactory.JustifyCenter()).ApplyOn(_root);
            _itemCenter.ApplyOn(_root);
            Assert.That(c.Space, Is.EqualTo(new RectangleF(80, 315, 1120, 20)));
            Assert.That(c1.Space, Is.EqualTo(new RectangleF(590, 335, 100, 40)));
            Assert.That(c2.Space, Is.EqualTo(new RectangleF(580, 375, 120, 30)));
        }
    }
}