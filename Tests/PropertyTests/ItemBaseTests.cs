using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI.Factories;
using WForest.UI.Properties.Grid.ItemProps;
using WForest.UI.WidgetTree;

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

        [Test]
        public void OnRow_PutChildAtBottom()
        {
            var c = _root.AddChild(Widgets.Container(20, 20)); 
            _root.AddProperty(PropertyFactory.Row()); 
            _root.AddProperty(PropertyFactory.ItemBase());
            _root.ApplyProperties();
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(0, 700, 20, 20)));
        }
        [Test]
        public void OnCol_PutChildAtRight()
        {
            var c = _root.AddChild(Widgets.Container(20, 20)); 
            _root.AddProperty(PropertyFactory.Column()); 
            _root.AddProperty(PropertyFactory.ItemBase());
            _root.ApplyProperties();
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(1260, 0, 20, 20)));
        }

        [Test]
        public void OnRowWithJustifyEnd_PutsInLowerRightCorner()
        {
            var c = _root.AddChild(Widgets.Container(20, 20)); 
            _root.AddProperty(PropertyFactory.Row());
            _root.AddProperty(PropertyFactory.JustifyEnd());
            _root.AddProperty(PropertyFactory.ItemBase());
            _root.ApplyProperties();
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(1260, 700, 20, 20)));
        }
        
        [Test]
        public void OnColWithJustifyEnd_PutsInLowerRightCorner()
        {
            var c = _root.AddChild(Widgets.Container(20, 20)); 
            _root.AddProperty(PropertyFactory.Column());
            _root.AddProperty(PropertyFactory.JustifyEnd());
            _root.AddProperty(PropertyFactory.ItemBase());
            _root.ApplyProperties();
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(1260, 700, 20, 20)));
        }

        [Test]
        public void OnRowWithJustifyEndTwoWidgets_DoesNotFuckItUp()
        {
            var c = _root.AddChild(Widgets.Container(20, 20));
            var c1= _root.AddChild(Widgets.Container(30, 40));
            _root.AddProperty(PropertyFactory.Row());
            _root.AddProperty(PropertyFactory.JustifyEnd());
            _root.AddProperty(PropertyFactory.ItemBase());
            _root.ApplyProperties();
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(1230, 700, 20, 20)));
            Assert.That(c1.Data.Space, Is.EqualTo(new Rectangle(1250, 680, 30, 40)));
        }

        [Test]
        public void OnMultipleRows_OffsetsCorrectly()
        {
           var c = _root.AddChild(Widgets.Container(800, 20));
            var c1= _root.AddChild(Widgets.Container(300, 40));
            var c2= _root.AddChild(Widgets.Container(300, 40));
            _root.AddProperty(PropertyFactory.Row());
            _root.AddProperty(PropertyFactory.JustifyEnd());
            _root.AddProperty(PropertyFactory.ItemBase());
            _root.ApplyProperties();
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(180, 660, 800, 20)));
            Assert.That(c1.Data.Space, Is.EqualTo(new Rectangle(980, 640, 300, 40)));
            Assert.That(c2.Data.Space, Is.EqualTo(new Rectangle(980, 680, 300, 40)));
        
        }
    }
}