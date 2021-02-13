using NUnit.Framework;
using WForest.Factories;
using WForest.UI.Props.Grid;
using WForest.UI.Props.Grid.JustifyProps;
using WForest.UI.Props.Grid.StretchingProps;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;
using static Tests.Utils.HelperMethods;

namespace Tests.PropTests
{
    [TestFixture]
    public class StretchTests
    {
        private IWidget? _root;

        [SetUp]
        public void BeforeEach()
        {
            _root = WidgetFactory.Container(400, 402);
        }


        [Test]
        public void HorizontalStretch_OnRoot_DoesNothing()
        {
            _root!.WithProp(PropFactory.HorizontalStretch());
            ApplyProps(_root);
            Assert.That(_root.Space, Is.EqualTo(new RectangleF(0, 0, 400, 402)));
        }

        [Test]
        public void HorizontalStretch_OnContainer_WidthAsBigAsParent()
        {
            IWidget c = WidgetFactory.Container(100, 120);
            _root!.AddChild(c);
            c.WithProp(PropFactory.HorizontalStretch());
            ApplyProps(c);
            Assert.That(c.Space, Is.EqualTo(new RectangleF(0, 0, 400, 120)));
        }

        [Test]
        public void VerticalStretch_OnRoot_DoesNothing()
        {
            _root!.WithProp(PropFactory.VerticalStretch());
            ApplyProps(_root);
            Assert.That(_root.Space, Is.EqualTo(new RectangleF(0, 0, 400, 402)));
        }

        [Test]
        public void VerticalStretch_OnContainer_HeightAsBigAsParent()
        {
            IWidget c = WidgetFactory.Container(100, 120);
            _root!.AddChild(c);
            c.WithProp(PropFactory.VerticalStretch());
            TreeVisitor.ApplyPropsOnTree(_root);
            Assert.That(c.Space, Is.EqualTo(new RectangleF(0, 0, 100, 402)));
        }

        [Test]
        public void HorizontalStretch_OnRowWithSiblings_AsBigAsCanBeRespectingSiblings()
        {
            IWidget c = WidgetFactory.Container();
            _root!.AddChild(c);
            _root.WithProp(new Row());
            c.WithProp(PropFactory.HorizontalStretch());
            IWidget c1 = WidgetFactory.Container(100, 120);
            _root!.AddChild(c1);
            TreeVisitor.ApplyPropsOnTree(_root);
            Assert.That(c.Space, Is.EqualTo(new RectangleF(0, 0, 300, 0)));
        }

        [Test]
        public void VerticalStretch_OnColWithSiblings_AsBigAsCanBeRespectingSiblings()
        {
            IWidget c = WidgetFactory.Container();
            _root!.AddChild(c);
            _root.WithProp(new Column());
            c.WithProp(PropFactory.VerticalStretch());
            IWidget c1 = WidgetFactory.Container(100, 120);
            _root!.AddChild(c1);
            TreeVisitor.ApplyPropsOnTree(_root);
            Assert.That(c.Space, Is.EqualTo(new RectangleF(0, 0, 0, 282)));
        }


        [Test]
        public void VerticalStretch_OnBothChildrenAndParentCentered_DoesNotFuckUp()
        {
            _root!.WithProp(new Column());
            IWidget c1 = WidgetFactory.Container();
            IWidget c2 = WidgetFactory.Container();
            _root!.AddChild(c1);
            _root!.AddChild(c2);
            c1.WithProp(new VerticalStretch());
            c2.WithProp(new VerticalStretch());
            _root.WithProp(new JustifyCenter());

            TreeVisitor.ApplyPropsOnTree(_root);
            Assert.That(c1.Space, Is.EqualTo(new RectangleF(0, 0, 0, 201)));
            Assert.That(c2.Space, Is.EqualTo(new RectangleF(0, 201, 0, 201)));
        }

        [Test]
        public void VerticalStretch_OnTwoMarginChildren_ParentCentered()
        {
            _root!.Space = new RectangleF(10, 10, 380, 382);
            _root!.WithProp(new Column());
            IWidget c1 = WidgetFactory.Container();
            IWidget c2 = WidgetFactory.Container();
            _root!.AddChild(c1);
            _root!.AddChild(c2);
            c1.WithProp(new VerticalStretch());
            c2.WithProp(new VerticalStretch());
            _root.WithProp(new JustifyCenter());

            TreeVisitor.ApplyPropsOnTree(_root);
            Assert.That(c1.Space, Is.EqualTo(new RectangleF(10, 10, 0, 191)));
            Assert.That(c2.Space, Is.EqualTo(new RectangleF(10, 201, 0, 191)));
        }

        [Test]
        public void SingleChildWithBothTypeStretch_ShouldGetParentSize()
        {
            _root!.Space = new RectangleF(10, 10, 380, 382);
            _root!.WithProp(new Row());
            IWidget c1 = WidgetFactory.Container();

            _root!.AddChild(c1);
            c1.WithProp(new VerticalStretch());
            c1.WithProp(new HorizontalStretch());

            TreeVisitor.ApplyPropsOnTree(_root);
            Assert.That(c1.Space, Is.EqualTo(new RectangleF(10, 10, 380, 382)));
        }

        [Test]
        public void ChildWithBothStretchAndFixedSibling_ShouldTakeAsMuchSpaceWhileRespecting()
        {
            _root!.Space = new RectangleF(10, 10, 380, 382);
            _root!.WithProp(new Column());
            IWidget c1 = WidgetFactory.Container();
            IWidget c2 = WidgetFactory.Container(15, 82);
            _root!.AddChild(c1);
            _root!.AddChild(c2);
            c1.WithProp(new VerticalStretch());
            c1.WithProp(new HorizontalStretch());

            TreeVisitor.ApplyPropsOnTree(_root);
            Assert.That(c1.Space, Is.EqualTo(new RectangleF(10, 10, 380, 300)));
            Assert.That(c2.Space, Is.EqualTo(new RectangleF(10, 310, 15, 82)));
        }

        [Test]
        public void TwoChildrenWithBothStretch_ShouldDivideSpaceEqually()
        {
            _root!.Space = new RectangleF(10, 10, 380, 382);
            _root!.WithProp(new Row());
            IWidget c1 = WidgetFactory.Container();
            IWidget c2 = WidgetFactory.Container(15, 82);
            _root!.AddChild(c1);
            _root!.AddChild(c2);
            c1.WithProp(new VerticalStretch());
            c1.WithProp(new HorizontalStretch());
            c2.WithProp(new VerticalStretch());
            c2.WithProp(new HorizontalStretch());

            TreeVisitor.ApplyPropsOnTree(_root);
            Assert.That(c1.Space, Is.EqualTo(new RectangleF(10, 10, 190, 382)));
            Assert.That(c2.Space, Is.EqualTo(new RectangleF(200, 10, 190, 382)));
        }
    }
}