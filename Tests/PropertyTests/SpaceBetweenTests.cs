using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI.Factories;
using WForest.UI.Properties.Grid.JustifyProps;
using WForest.UI.WidgetTree;

namespace WForest.Tests.PropertyTests
{
    [TestFixture]
    public class SpaceBetweenTests
    {
        private JustifyBetween _justifyBetween;
        private WidgetTree _root;

        [SetUp]
        public void BeforeEach()
        {
            _justifyBetween = new JustifyBetween();
            _root = new WidgetTree(Widgets.Container(new Rectangle(0, 0, 1280, 720)));
        }

        [Test]
        public void ZeroChild_NothingHappens()
        {
            Assert.That(() => _justifyBetween.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void WidgetWithoutRowOrCol_NothingHappens()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 130, 120)));
            _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 110)));

            _justifyBetween.ApplyOn(_root);

            var expected = new Rectangle(0, 0, 130, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void RowWithOneChild_NothingHappens()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 130, 120)));

            _root.AddProperty(PropertyFactory.Row());
            _root.AddProperty(_justifyBetween);

            _root.ApplyProperties();
            var expected = new Rectangle(0, 0, 130, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void RowWithTwoW_MaximizesSpaceBetween()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 130, 120)));
            var child1 = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 110)));

            _root.AddProperty(PropertyFactory.Row());
            _root.AddProperty(_justifyBetween);

            _root.ApplyProperties();

            var exp = new Rectangle(0, 0, 130, 120);
            var exp1 = new Rectangle(1160, 0, 120, 110);

            Assert.That(child.Data.Space, Is.EqualTo(exp));
            Assert.That(child1.Data.Space, Is.EqualTo(exp1));
        }

        [Test]
        public void RowWithThreeW_MaximizesSpaceBetween()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 130, 120)));
            var child1 = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 110)));
            var child2 = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 140, 110)));

            _root.AddProperty(PropertyFactory.Row());
            _root.AddProperty(_justifyBetween);

            _root.ApplyProperties();

            var exp = new Rectangle(0, 0, 130, 120);
            var exp1 = new Rectangle(575, 0, 120, 110);
            var exp2 = new Rectangle(1140, 0, 140, 110);

            Assert.That(child.Data.Space, Is.EqualTo(exp));
            Assert.That(child1.Data.Space, Is.EqualTo(exp1));
            Assert.That(child2.Data.Space, Is.EqualTo(exp2));
        }

        [Test]
        public void RowWithFiveW_MaximizesSpaceBetween()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 130, 120)));
            var child1 = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 110)));
            var child2 = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 140, 110)));
            var child3 = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 90, 120)));
            var child4 = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 100, 20)));

            _root.AddProperty(PropertyFactory.Row());
            _root.AddProperty(_justifyBetween);

            _root.ApplyProperties();

            var exp = new Rectangle(0, 0, 130, 120);
            var exp1 = new Rectangle(305, 0, 120, 110);
            var exp2 = new Rectangle(600, 0, 140, 110);
            var exp3 = new Rectangle(915, 0, 90, 120);
            var exp4 = new Rectangle(1180, 0, 100, 20);

            Assert.That(child.Data.Space, Is.EqualTo(exp));
            Assert.That(child1.Data.Space, Is.EqualTo(exp1));
            Assert.That(child2.Data.Space, Is.EqualTo(exp2));
            Assert.That(child3.Data.Space, Is.EqualTo(exp3));
            Assert.That(child4.Data.Space, Is.EqualTo(exp4));
        }

        [Test]
        public void RowWithFourW_MaximizesSpaceBetween()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 130, 120)));
            var child1 = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 110)));
            var child2 = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 200, 200)));
            var child3 = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 90, 110)));

            _root.AddProperty(PropertyFactory.Row());
            _root.AddProperty(_justifyBetween);

            _root.ApplyProperties();

            var exp = new Rectangle(0, 0, 130, 120);
            var exp1 = new Rectangle(377, 0, 120, 110);
            var exp2 = new Rectangle(743, 0, 200, 200);
            var exp3 = new Rectangle(1190, 0, 90, 110);

            Assert.That(child.Data.Space, Is.EqualTo(exp));
            Assert.That(child1.Data.Space, Is.EqualTo(exp1));
            Assert.That(child2.Data.Space, Is.EqualTo(exp2));
            Assert.That(child3.Data.Space, Is.EqualTo(exp3));
        }
    }
}