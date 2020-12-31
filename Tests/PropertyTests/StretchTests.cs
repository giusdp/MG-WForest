using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI.Factories;
using WForest.UI.WidgetTree;

namespace WForest.Tests.PropertyTests
{
    [TestFixture]
    public class StretchTests
    {
        private WidgetTree _root;

        [Test]
        public void StretchContainer_AsBigAsParent()
        {
            _root = new WidgetTree(Widgets.Container(400, 401));
            var c = _root.AddChild(Widgets.Container());
            c.AddProperty(PropertyFactory.Stretch());
            c.ApplyProperties();
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(0, 0, 400, 401)));
        }

        [Test]
        public void StretchRowContainer_OnlyWidthAsParent()
        {
            _root = new WidgetTree(Widgets.Container(400, 401));
            var c = _root.AddChild(Widgets.Container());
            c.AddProperty(PropertyFactory.Stretch());
            c.AddProperty(PropertyFactory.Row());
            c.ApplyProperties();
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(0, 0, 400, 0)));
        }

        [Test]
        public void StretchRowContainer_HeightAsRows()
        {
            _root = new WidgetTree(Widgets.Container(400, 401));
            var c = _root.AddChild(Widgets.Container(40, 55));
            c.AddChild(Widgets.Container(40, 95));
            c.AddProperty(PropertyFactory.Stretch());
            c.AddProperty(PropertyFactory.Row());
            c.ApplyProperties();
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(0, 0, 400, 95)));
        }

        [Test]
        public void StretchColumnContainer_StretchHeightOnly()
        {
            _root = new WidgetTree(Widgets.Container(400, 401));
            var c = _root.AddChild(Widgets.Container(41, 55));
            c.AddChild(Widgets.Container(42, 95));
            c.AddProperty(PropertyFactory.Stretch());
            c.AddProperty(PropertyFactory.Column());
            c.ApplyProperties();
            Assert.That(c.Data.Space, Is.EqualTo(new Rectangle(0, 0, 42, 401)));
        }
    }
}