using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Factories;
using WForest.UI.Props.Grid.JustifyProps;
using WForest.UI.Widgets;
using static WForest.Tests.Utils.HelperMethods;

namespace WForest.Tests.PropTests
{
    [TestFixture]
    public class JustifyEndTests
    {
        private JustifyEnd _justifyEnd;
        private IWidget _root;

        [SetUp]
        public void BeforeEach()
        {
            _justifyEnd = new JustifyEnd();
            _root = WidgetFactory.Container(new Rectangle(0, 0, 1280, 720));
        }

        [Test]
        public void NoChildren_NothingHappens()
        {
            Assert.That(() => _justifyEnd.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void WidgetWithoutRowOrCol_NothingHappens()
        {
            var child = WidgetFactory.Container(new Rectangle(0, 0, 120, 120));
            _root.AddChild(child);

            _justifyEnd.ApplyOn(_root);

            var expected = new Rectangle(0, 0, 120, 120);

            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void OneChildInARow_PutsItAtTheEnd()
        {
            var child = WidgetFactory.Container(new Rectangle(0, 0, 120, 220));
            _root.AddChild(child);
            
            _root.WithProp(PropertyFactory.Row());
            ApplyProps(_root);

            _justifyEnd.ApplyOn(_root);

            var expected = new Rectangle(1160, 0, 120, 220);

            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void OneChildInAColumn_PutsItOnTheBottom()
        {
            var child = WidgetFactory.Container(new Rectangle(0, 0, 220, 120));
            _root.AddChild(child);

            _root.WithProp(PropertyFactory.Column());
            _root.WithProp(_justifyEnd);
            ApplyProps(_root);

            var expected = new Rectangle(0, 600, 220, 120);

            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ItemCenterRow_VerticallyCenteredAndAtTheRight()
        {
            var expected = new Rectangle(1060, 300, 220, 120);

            var child = WidgetFactory.Container(new Rectangle(0, 0, 220, 120));
            _root.AddChild(child);
            
            _root.WithProp(PropertyFactory.Row());
            _root.WithProp(PropertyFactory.ItemCenter());
            _root.WithProp(_justifyEnd);

            ApplyProps(_root);
            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void MultipleItemsInColumn_AtTheBottom()
        {
            var e = new Rectangle(0, 40, 120, 120);
            var e1 = new Rectangle(0, 160, 220, 120);
            var e2 = new Rectangle(0, 280, 120, 320);
            var e3 = new Rectangle(0, 600, 120, 120);

            var c =  WidgetFactory.Container(new Rectangle(0, 0, 120, 120));
            var c1 = WidgetFactory.Container(new Rectangle(0, 0, 220, 120));
            var c2 = WidgetFactory.Container(new Rectangle(0, 0, 120, 320));
            var c3 = WidgetFactory.Container(new Rectangle(0, 0, 120, 120));
            _root.AddChild(c);
            _root.AddChild(c1);
            _root.AddChild(c2);
            _root.AddChild(c3);
            _root.WithProp(PropertyFactory.Column());
            _root.WithProp(_justifyEnd);

            ApplyProps(_root);
            Assert.That(c.Space, Is.EqualTo(e));
            Assert.That(c1.Space, Is.EqualTo(e1));
            Assert.That(c2.Space, Is.EqualTo(e2));
            Assert.That(c3.Space, Is.EqualTo(e3));
        }
    }
}