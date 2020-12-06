using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI;
using WForest.UI.Factories;
using WForest.UI.Properties.Grid.JustifyProps;
using WForest.UI.WidgetTree;

namespace WForest.Tests.PropertyTests
{
    [TestFixture]
    public class JustifyAroundTests
    {
        private JustifyAround _justifyAround;
        private WidgetTree _root;

        [SetUp]
        public void BeforeEach()
        {
            _justifyAround = new JustifyAround();
            _root = new WidgetTree(Widgets.Container(new Rectangle(0, 0, 1280, 720)));
        }

        [Test]
        public void ZeroChild_NothingHappens()
        {
            Assert.That(() => _justifyAround.ApplyOn(_root), Throws.Nothing);
        } 
        
        [Test]
        public void WidgetWithoutRowOrCol_NothingHappens()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 130, 120)));
            _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 110)));

            _justifyAround.ApplyOn(_root);

            var expected = new Rectangle(0, 0, 130, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void RowWithOneChild_NothingHappens()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 130, 120)));

            _root.AddProperty(Properties.Row());
            _root.AddProperty(_justifyAround);

            _root.ApplyProperties();
            var expected = new Rectangle(0, 0, 130, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void RowWithTwoW_SpaceAround()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 130, 120)));
            var child1 = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 110)));

            _root.AddProperty(Properties.Row());
            _root.AddProperty(_justifyAround);

            _root.ApplyProperties();

            var exp = new Rectangle(343, 0, 130, 120);
            var exp1 = new Rectangle(817, 0, 120, 110);

            Assert.That(child.Data.Space, Is.EqualTo(exp));
            Assert.That(child1.Data.Space, Is.EqualTo(exp1));
        }
    }
}