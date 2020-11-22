using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI;
using WForest.UI.Factories;
using WForest.UI.Properties.Grid.Center;
using WForest.UI.Properties.Grid.Column;
using WForest.UI.Properties.Grid.Row;

namespace WForest.Tests.PropertyTests
{
    [TestFixture]
    public class ItemCenterTests
    {
        private ItemCenter _itemCenter;
        private WidgetTree _root;

        [SetUp]
        public void BeforeEach()
        {
            _itemCenter = new ItemCenter();
            _root = new WidgetTree(Widgets.Container(new Rectangle(0, 0, 1280, 720)));
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
            Assert.That(() => _itemCenter.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void OnARow_PutsChildrenCenteredVertically()
        {
            var c = _root.AddChild(Widgets.Container(20, 20));
            ApplyRow();
            _itemCenter.ApplyOn(_root);
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(0, 350, 20, 20)));
        }

        [Test]
        public void OnACol_PutsChildrenCenteredHorizontally()
        {
            var c = _root.AddChild(Widgets.Container(20, 20));
            ApplyCol();
            _itemCenter.ApplyOn(_root);
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(630, 0, 20, 20)));
        }

        [Test]
        public void OnACenteredRow_CentersCorrectly()
        {
            var c = _root.AddChild(Widgets.Container(20, 20));
            ApplyRow();
            Properties.JustifyCenter().ApplyOn(_root);
            _itemCenter.ApplyOn(_root);
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(630, 350, 20, 20)));
        }
    }
}