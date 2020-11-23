using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI;
using WForest.UI.Factories;
using WForest.UI.Properties.Grid;

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
            _root = new WidgetTree(Widgets.Container(new Rectangle(0, 0, 1280, 720)));
        }

        [Test]
        public void NoChildren_NothingHappens()
        {
            Assert.That(() => _justifyEnd.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void WidgetWithoutRowOrCol_NothingHappens()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));

            _justifyEnd.ApplyOn(_root);

            var expected = new Rectangle(0, 0, 120, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void OneChildInARow_PutsItAtTheEnd()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));

            _root.AddProperty(Properties.Row());
            _root.ApplyProperties();

            _justifyEnd.ApplyOn(_root);

            var expected = new Rectangle(1160, 0, 120, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void OneChildInAColumn_PutsItOnTheBottom()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));

            _root.AddProperty(Properties.Column());
            _root.ApplyProperties();

            _justifyEnd.ApplyOn(_root);

            var expected = new Rectangle(0, 600, 120, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ItemCenterRow_VerticallyCenteredAndAtTheRight()
        {
            var expected = new Rectangle(1160, 300, 120, 120);
            
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));
            _root.AddProperty(Properties.Row());
            _root.AddProperty(Properties.ItemCenter());
            _root.AddProperty(_justifyEnd);
        
            _root.ApplyProperties();
            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }
    }
}