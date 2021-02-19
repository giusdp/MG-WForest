using NUnit.Framework;
using WForest.Exceptions;
using WForest.Factories;
using WForest.Props.Grid.JustifyProps;
using WForest.Utilities;
using WForest.Widgets.Interfaces;

namespace Tests.PropTests
{
    [TestFixture]
    public class SpaceBetweenTests
    {
        private JustifyBetween _justifyBetween;
        private IWidget _root;

        public SpaceBetweenTests()
        {
            _justifyBetween = new JustifyBetween();
            _root = WidgetFactory.Container(new RectangleF(0, 0, 1280, 720));
        }

        [SetUp]
        public void BeforeEach()
        {
            _justifyBetween = new JustifyBetween();
            _root = WidgetFactory.Container(new RectangleF(0, 0, 1280, 720));
        }

        [Test]
        public void ZeroChild_NothingHappens()
        {
            Assert.That(() => _justifyBetween.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void WidgetWithoutRowOrCol_ThrowsExc()
        {
            var child = WidgetFactory.Container(new RectangleF(0, 0, 130, 120));
            _root.AddChild(child);
            _root.AddChild(WidgetFactory.Container(new RectangleF(0, 0, 120, 110)));

            Assert.That(() => _justifyBetween.ApplyOn(_root), Throws.TypeOf<IncompatibleWidgetException>());
        }

        [Test]
        public void RowWithOneChild_NothingHappens()
        {
            var child = WidgetFactory.Container(new RectangleF(0, 0, 130, 120));
            _root.AddChild(child);

            _root.WithProp(PropFactory.Row());
            _root.WithProp(_justifyBetween);

            TreeVisitor.ApplyPropsOnTree(_root);
            var expected = new RectangleF(0, 0, 130, 120);

            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void RowWithTwoW_MaximizesSpaceBetween()
        {
            var child = WidgetFactory.Container(new RectangleF(0, 0, 130, 120));
            var child1 = WidgetFactory.Container(new RectangleF(0, 0, 120, 110));
            _root.AddChild(child);
            _root.AddChild(child1);

            _root.WithProp(PropFactory.Row());
            _root.WithProp(_justifyBetween);

            TreeVisitor.ApplyPropsOnTree(_root);

            var exp = new RectangleF(0, 0, 130, 120);
            var exp1 = new RectangleF(1160, 0, 120, 110);

            Assert.That(child.Space, Is.EqualTo(exp));
            Assert.That(child1.Space, Is.EqualTo(exp1));
        }

        [Test]
        public void RowWithThreeW_MaximizesSpaceBetween()
        {
            var child = WidgetFactory.Container(new RectangleF(0, 0, 130, 120));
            var child1 = WidgetFactory.Container(new RectangleF(0, 0, 120, 110));
            var child2 = WidgetFactory.Container(new RectangleF(0, 0, 140, 110));
            _root.AddChild(child);
            _root.AddChild(child1);
            _root.AddChild(child2);

            _root.WithProp(PropFactory.Row());
            _root.WithProp(_justifyBetween);

            TreeVisitor.ApplyPropsOnTree(_root);

            var exp = new RectangleF(0, 0, 130, 120);
            var exp1 = new RectangleF(575, 0, 120, 110);
            var exp2 = new RectangleF(1140, 0, 140, 110);

            Assert.That(child.Space, Is.EqualTo(exp));
            Assert.That(child1.Space, Is.EqualTo(exp1));
            Assert.That(child2.Space, Is.EqualTo(exp2));
        }

        [Test]
        public void RowWithFiveW_MaximizesSpaceBetween()
        {
            var child = WidgetFactory.Container(new RectangleF(0, 0, 130, 120));
            var child1 = WidgetFactory.Container(new RectangleF(0, 0, 120, 110));
            var child2 = WidgetFactory.Container(new RectangleF(0, 0, 140, 110));
            var child3 = WidgetFactory.Container(new RectangleF(0, 0, 90, 120));
            var child4 = WidgetFactory.Container(new RectangleF(0, 0, 100, 20));

            _root.AddChild(child);
            _root.AddChild(child1);
            _root.AddChild(child2);
            _root.AddChild(child3);
            _root.AddChild(child4);

            _root.WithProp(PropFactory.Row());
            _root.WithProp(_justifyBetween);

            TreeVisitor.ApplyPropsOnTree(_root);

            var exp = new RectangleF(0, 0, 130, 120);
            var exp1 = new RectangleF(305, 0, 120, 110);
            var exp2 = new RectangleF(600, 0, 140, 110);
            var exp3 = new RectangleF(915, 0, 90, 120);
            var exp4 = new RectangleF(1180, 0, 100, 20);

            Assert.That(child.Space, Is.EqualTo(exp));
            Assert.That(child1.Space, Is.EqualTo(exp1));
            Assert.That(child2.Space, Is.EqualTo(exp2));
            Assert.That(child3.Space, Is.EqualTo(exp3));
            Assert.That(child4.Space, Is.EqualTo(exp4));
        }

        [Test]
        public void RowWithFourW_MaximizesSpaceBetween()
        {
            var child = WidgetFactory.Container(new RectangleF(0, 0, 130, 120));
            var child1 = WidgetFactory.Container(new RectangleF(0, 0, 120, 110));
            var child2 = WidgetFactory.Container(new RectangleF(0, 0, 200, 200));
            var child3 = WidgetFactory.Container(new RectangleF(0, 0, 90, 110));

            _root.AddChild(child);
            _root.AddChild(child1);
            _root.AddChild(child2);
            _root.AddChild(child3);
            _root.WithProp(PropFactory.Row());
            _root.WithProp(_justifyBetween);

            TreeVisitor.ApplyPropsOnTree(_root);

            var exp = new RectangleF(0, 0, 130, 120);
            var exp1 = new RectangleF(377, 0, 120, 110);
            var exp2 = new RectangleF(743, 0, 200, 200);
            var exp3 = new RectangleF(1190, 0, 90, 110);

            Assert.That(child.Space, Is.EqualTo(exp));
            Assert.That(child1.Space, Is.EqualTo(exp1));
            Assert.That(child2.Space, Is.EqualTo(exp2));
            Assert.That(child3.Space, Is.EqualTo(exp3));
        }
    }
}