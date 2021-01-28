using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Exceptions;
using WForest.Factories;
using WForest.UI.Props.Grid.ItemProps;
using WForest.UI.Widgets.Interfaces;
using static WForest.Tests.Utils.HelperMethods;

namespace WForest.Tests.PropTests
{
    [TestFixture]
    public class ItemBaseTests
    {
        private ItemBase _itemBase;
        private IWidget _root;
        private IWidget child;

        [SetUp]
        public void BeforeEach()
        {
            _itemBase = new ItemBase();
            _root = WidgetFactory.Container(new Rectangle(0, 0, 1280, 720));
            child = WidgetFactory.Container(20, 20);
            _root.AddChild(child);
        }

        [Test]
        public void ApplyOn_NoRowOrCol_ThrowsIncompatibleWidget()
        {
            Assert.That(() => _itemBase.ApplyOn(_root), Throws.TypeOf<IncompatibleWidgetException>());
        }

        [Test]
        public void OnRow_PutChildAtBottom()
        {
            _root.WithProp(PropertyFactory.Row());
            _root.WithProp(PropertyFactory.ItemBase());
            ApplyProps(_root);
            Assert.That(child.Space, Is.EqualTo(new Rectangle(0, 700, 20, 20)));
        }

        [Test]
        public void OnCol_PutChildAtRight()
        {
            _root.WithProp(PropertyFactory.Column());
            _root.WithProp(PropertyFactory.ItemBase());
            ApplyProps(_root);
            Assert.That(child.Space, Is.EqualTo(new Rectangle(1260, 0, 20, 20)));
        }

        [Test]
        public void OnRowWithJustifyEnd_PutsInLowerRightCorner()
        {
            _root.WithProp(PropertyFactory.Row());
            _root.WithProp(PropertyFactory.JustifyEnd());
            _root.WithProp(PropertyFactory.ItemBase());
            ApplyProps(_root);
            Assert.That(child.Space, Is.EqualTo(new Rectangle(1260, 700, 20, 20)));
        }

        [Test]
        public void OnColWithJustifyEnd_PutsInLowerRightCorner()
        {
            _root.WithProp(PropertyFactory.Column());
            _root.WithProp(PropertyFactory.JustifyEnd());
            _root.WithProp(PropertyFactory.ItemBase());
            ApplyProps(_root);
            Assert.That(child.Space, Is.EqualTo(new Rectangle(1260, 700, 20, 20)));
        }

        [Test]
        public void OnRowWithJustifyEndTwoWidgets_DoesNotFuckItUp()
        {
            var c1 = WidgetFactory.Container(30, 40);
            _root.AddChild(c1);
            _root.WithProp(PropertyFactory.Row());
            _root.WithProp(PropertyFactory.JustifyEnd());
            _root.WithProp(PropertyFactory.ItemBase());
            ApplyProps(_root);
            Assert.That(child.Space, Is.EqualTo(new Rectangle(1230, 700, 20, 20)));
            Assert.That(c1.Space, Is.EqualTo(new Rectangle(1250, 680, 30, 40)));
        }

        [Test]
        public void OnMultipleRows_OffsetsCorrectly()
        {
            _root = WidgetFactory.Container(new Rectangle(0, 0, 1280, 720));
            var c = WidgetFactory.Container(800, 20);
            var c1 = WidgetFactory.Container(300, 40);
            var c2 = WidgetFactory.Container(300, 40);
            _root.AddChild(c);
            _root.AddChild(c1);
            _root.AddChild(c2);
            _root.WithProp(PropertyFactory.Row());
            _root.WithProp(PropertyFactory.JustifyEnd());
            _root.WithProp(PropertyFactory.ItemBase());
            ApplyProps(_root);
            Assert.That(c.Space, Is.EqualTo(new Rectangle(180, 660, 800, 20)));
            Assert.That(c1.Space, Is.EqualTo(new Rectangle(980, 640, 300, 40)));
            Assert.That(c2.Space, Is.EqualTo(new Rectangle(980, 680, 300, 40)));
        }
    }
}