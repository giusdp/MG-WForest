using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Factories;
using WForest.UI.WidgetTrees;

namespace WForest.Tests.PropTests
{
    [TestFixture]
    public class StretchTests
    {
        private WidgetTree? _root;

        [SetUp]
        public void BeforeEach()
        {
            _root = new WidgetTree(WidgetFactory.Container(400, 401));
        }

        [Test]
        public void HorizontalStretch_OnRoot_DoesNothing()
        {
            _root!.WithProperty(PropertyFactory.HorizontalStretch());
            _root.ApplyProperties();
            Assert.That(_root.Data.Space, Is.EqualTo(new Rectangle(0, 0, 400, 401)));
        }

        [Test]
        public void HorizontalStretch_OnContainer_WidthAsBigAsParent()
        {
            var c = _root!.AddChild(WidgetFactory.Container());
            c.WithProperty(PropertyFactory.HorizontalStretch());
            c.ApplyProperties();
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(0, 0, 400, 0)));
        }

        [Test]
        public void VerticalStretch_OnRoot_DoesNothing()
        {
            _root!.WithProperty(PropertyFactory.VerticalStretch());
            _root.ApplyProperties();
            Assert.That(_root.Data.Space, Is.EqualTo(new Rectangle(0, 0, 400, 401)));
        }

        [Test]
        public void VerticalStretch_OnContainer_HeightAsBigAsParent()
        {
            var c = _root!.AddChild(WidgetFactory.Container());
            c.WithProperty(PropertyFactory.VerticalStretch());
            c.ApplyProperties();
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(0, 0, 0, 401)));
        }
    }
}