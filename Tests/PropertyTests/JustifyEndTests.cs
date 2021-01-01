using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI.Factories;
using WForest.UI.Properties.Grid.JustifyProps;
using WForest.UI.WidgetTree;

namespace WForest.Tests.PropertyTests
{
    [TestFixture]
    public class JustifyEndTests
    {
        private JustifyEnd _justifyEnd;
        private WidgetTree _root;

        [SetUp]
        public void BeforeEach()
        {
            _justifyEnd = new JustifyEnd();
            _root = new WidgetTree(WidgetFactory.Container(new Rectangle(0, 0, 1280, 720)));
        }

        [Test]
        public void NoChildren_NothingHappens()
        {
            Assert.That(() => _justifyEnd.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void WidgetWithoutRowOrCol_NothingHappens()
        {
            var child = _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 120, 120)));

            _justifyEnd.ApplyOn(_root);

            var expected = new Rectangle(0, 0, 120, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void OneChildInARow_PutsItAtTheEnd()
        {
            var child = _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 120, 220)));

            _root.WithProperty(PropertyFactory.Row());
            _root.ApplyProperties();

            _justifyEnd.ApplyOn(_root);

            var expected = new Rectangle(1160, 0, 120, 220);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void OneChildInAColumn_PutsItOnTheBottom()
        {
            var child = _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 220, 120)));

            _root.WithProperty(PropertyFactory.Column());
            _root.WithProperty(_justifyEnd);
            _root.ApplyProperties();

            var expected = new Rectangle(0, 600, 220, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ItemCenterRow_VerticallyCenteredAndAtTheRight()
        {
            var expected = new Rectangle(1060, 300, 220, 120);
            
            var child = _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 220, 120)));
            _root.WithProperty(PropertyFactory.Row());
            _root.WithProperty(PropertyFactory.ItemCenter());
            _root.WithProperty(_justifyEnd);
        
            _root.ApplyProperties();
            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }
        
        [Test]
        public void MultipleItemsInColumn_AtTheBottom()
        {
            var e = new Rectangle(0, 40, 120, 120);
            var e1 = new Rectangle(0, 160, 220, 120);
            var e2 = new Rectangle(0, 280, 120, 320);
            var e3 = new Rectangle(0, 600, 120, 120);
            
            var c = _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 120, 120)));
            var c1 = _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 220, 120)));
            var c2 = _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 120, 320)));
            var c3 = _root.AddChild(WidgetFactory.Container(new Rectangle(0, 0, 120, 120)));
            _root.WithProperty(PropertyFactory.Column());
            _root.WithProperty(_justifyEnd);
        
            _root.ApplyProperties();
            Assert.That(c.Data.Space, Is.EqualTo(e));
            Assert.That(c1.Data.Space, Is.EqualTo(e1));
            Assert.That(c2.Data.Space, Is.EqualTo(e2));
            Assert.That(c3.Data.Space, Is.EqualTo(e3));
        }
    }
}