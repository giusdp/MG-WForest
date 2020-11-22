using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI;
using WForest.UI.Factories;
using WForest.UI.Properties.Grid.Column;
using WForest.UI.Properties.Grid.ItemCenter;
using WForest.UI.Properties.Grid.Row;

namespace WForest.Tests.PropertyTests
{
    [TestFixture]
    public class ItemCenterTests
    {
        private ItemCenter _itemcenter;
        private WidgetTree _root;

        [SetUp]
        public void BeforeEach()
        {
            _itemcenter = new ItemCenter();
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
            Assert.That(() => _itemcenter.ApplyOn(_root), Throws.Nothing);
        } 
    }
}