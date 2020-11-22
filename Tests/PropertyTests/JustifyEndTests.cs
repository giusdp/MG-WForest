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
        public void ApplyOn_OneChildrenInARow_PutsItAtTheEnd()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));

            _root.AddProperty(Properties.Row());
            _root.ApplyProperties();

            _justifyEnd.ApplyOn(_root);

            var expected = new Rectangle(1160, 0, 120, 120);
            
            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }
    }
}