using NUnit.Framework;
using WForest.Exceptions;
using WForest.Factories;
using WForest.Props.Props.Grid.JustifyProps;
using WForest.Utilities;
using WForest.Widgets.Interfaces;

namespace Tests.PropTests
{
    [TestFixture]
    public class JustifyAroundTests
    {
        private JustifyAround _justifyAround;
        private IWidget _root;

        public JustifyAroundTests()
        {
            _justifyAround = new JustifyAround();
            _root = WidgetFactory.Container(new RectangleF(0, 0, 1280, 720));
        }

        [SetUp]
        public void BeforeEach()
        {
            _justifyAround = new JustifyAround();
            _root = WidgetFactory.Container(new RectangleF(0, 0, 1280, 720));
        }

        [Test]
        public void ZeroChild_NothingHappens()
        {
            Assert.That(() => _justifyAround.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void ApplyOn_WidgetWithoutRowOrCol_ThrowsExc()
        {
            var child = WidgetFactory.Container(new RectangleF(0, 0, 130, 120));
            _root.AddChild(child);

            _root.AddChild(WidgetFactory.Container(new RectangleF(0, 0, 120, 110)));

            Assert.That(() => _justifyAround.ApplyOn(_root), Throws.TypeOf<IncompatibleWidgetException>());
        }

        [Test]
        public void RowWithOneChild_PutsAtCenter()
        {
            var child = WidgetFactory.Container(new RectangleF(0, 0, 130, 120));
            _root.AddChild(child);
            _root.WithProp(PropFactory.Row());
            _root.WithProp(_justifyAround);

            TreeVisitor.ApplyPropsOnTree(_root);
            var expected = new RectangleF(575, 0, 130, 120);

            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ColWithOneChild_PutsAtCenter()
        {
            var child = WidgetFactory.Container(new RectangleF(0, 0, 130, 120));
            _root.AddChild(child);
            _root.WithProp(PropFactory.Column());
            _root.WithProp(_justifyAround);

            TreeVisitor.ApplyPropsOnTree(_root);
            var expected = new RectangleF(0, 300, 130, 120);

            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void RowWithTwoW_SpaceAround()
        {
            var child = WidgetFactory.Container(new RectangleF(0, 0, 130, 120));
            var child1 = WidgetFactory.Container(new RectangleF(0, 0, 120, 110));
            _root.AddChild(child);
            _root.AddChild(child1);

            _root.WithProp(PropFactory.Row());
            _root.WithProp(_justifyAround);

            TreeVisitor.ApplyPropsOnTree(_root);

            var exp = new RectangleF(343, 0, 130, 120);
            var exp1 = new RectangleF(817, 0, 120, 110);

            Assert.That(child.Space, Is.EqualTo(exp));
            Assert.That(child1.Space, Is.EqualTo(exp1));
        }
    }
}