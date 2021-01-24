using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Factories;
using WForest.UI.Widgets.Interfaces;
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
            _root!.WithProp(PropertyFactory.HorizontalStretch());
            ApplyProps(_root);
            Assert.That(_root.Space, Is.EqualTo(new Rectangle(0, 0, 400, 401)));
        }

        [Test]
        public void HorizontalStretch_OnContainer_WidthAsBigAsParent()
        {
            IWidget c = WidgetFactory.Container();
            _root!.AddChild(c);
            c.WithProp(PropertyFactory.HorizontalStretch());
            ApplyProps(c);
            Assert.That(c.Space, Is.EqualTo(new Rectangle(0, 0, 400, 0)));
        }

        [Test]
        public void VerticalStretch_OnRoot_DoesNothing()
        {
            _root!.WithProp(PropertyFactory.VerticalStretch());
            ApplyProps(_root);
            Assert.That(_root.Space, Is.EqualTo(new Rectangle(0, 0, 400, 401)));
        }

        [Test]
        public void VerticalStretch_OnContainer_HeightAsBigAsParent()
        {
            IWidget c = WidgetFactory.Container();
            _root!.AddChild(c);
            c.WithProp(PropertyFactory.VerticalStretch());
            ApplyProps(c);
            Assert.That(c.Space, Is.EqualTo(new Rectangle(0, 0, 0, 401)));
        }
    }
}