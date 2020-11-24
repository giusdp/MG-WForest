using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI;
using WForest.UI.Factories;
using WForest.UI.Properties.Grid.ItemProps;

namespace WForest.Tests.PropertyTests
{
    [TestFixture]
    public class ItemBaseTests
    {
        private ItemBase _itemBase;
        private WidgetTree _root;

        [SetUp]
        public void BeforeEach()
        {
            _itemBase = new ItemBase();
            _root = new WidgetTree(Widgets.Container(new Rectangle(0, 0, 1280, 720)));
        }

        [Test]
        public void ApplyOn_NoChildren_NothingHappens()
        {
            Assert.That(() => _itemBase.ApplyOn(_root), Throws.Nothing);
        }
    }
}