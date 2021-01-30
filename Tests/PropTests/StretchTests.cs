using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Factories;
using WForest.UI.Props.Grid;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;
using static WForest.Tests.Utils.HelperMethods;

namespace WForest.Tests.PropTests
{
    [TestFixture]
    public class StretchTests
    {
        private IWidget? _root;

        [SetUp]
        public void BeforeEach()
        {
            _root = WidgetFactory.Container(400, 401);
        }


        [Test]
        public void HorizontalStretch_OnRoot_DoesNothing()
        {
            _root!.WithProp(PropFactory.HorizontalStretch());
            ApplyProps(_root);
            Assert.That(_root.Space, Is.EqualTo(new Rectangle(0, 0, 400, 401)));
        }

        [Test]
        public void HorizontalStretch_OnContainer_WidthAsBigAsParent()
        {
            IWidget c = WidgetFactory.Container(100, 120);
            _root!.AddChild(c);
            c.WithProp(PropFactory.HorizontalStretch());
            ApplyProps(c);
            Assert.That(c.Space, Is.EqualTo(new Rectangle(0, 0, 400, 120)));
        }

        [Test]
        public void VerticalStretch_OnRoot_DoesNothing()
        {
            _root!.WithProp(PropFactory.VerticalStretch());
            ApplyProps(_root);
            Assert.That(_root.Space, Is.EqualTo(new Rectangle(0, 0, 400, 401)));
        }

        [Test]
        public void VerticalStretch_OnContainer_HeightAsBigAsParent()
        {
            IWidget c = WidgetFactory.Container(100, 120);
            _root!.AddChild(c);
            c.WithProp(PropFactory.VerticalStretch());
            TreeVisitor.ApplyPropsOnTree(_root);
            Assert.That(c.Space, Is.EqualTo(new Rectangle(0, 0, 100, 401)));
        }

        [Test]
        public void HorizontalStretch_OnRowWithSiblings_AsBigAsCanBeRespectingSiblings()
        {
            IWidget c = WidgetFactory.Container();
            _root!.AddChild(c);
            c.WithProp(PropFactory.HorizontalStretch());
            IWidget c1 = WidgetFactory.Container(100, 0);
            _root!.AddChild(c1);
            TreeVisitor.ApplyPropsOnTree(_root);
            Assert.That(c.Space, Is.EqualTo(new Rectangle(0, 0, 300, 0)));
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
            Assert.That(c.Space, Is.EqualTo(new Rectangle(0, 0, 0, 281)));
        }
    }
}