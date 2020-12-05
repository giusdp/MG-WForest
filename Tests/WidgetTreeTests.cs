using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI.Factories;
using WForest.UI.WidgetTree;

namespace WForest.Tests
{
    [TestFixture]
    public class WidgetTreeTests
    {
        private WidgetTree _t;

        [SetUp]
        public void SetUpBeforeEach()
        {
            _t = new WidgetTree(Widgets.Container(Rectangle.Empty));
        }

        [Test]
        public void AddProperty_AddPropertyToList()
        {
            var c = Properties.JustifyCenter();
            _t.AddProperty(c);
            Assert.That(_t.Properties.Contains(c), Is.True);
        }

        [Test]
        public void AddProperty_NullInput()
        {
            Assert.That(() => _t.AddProperty(null), Throws.ArgumentNullException);
        }

        [Test]
        public void AddChild_AddsWidgetChild()
        {
            _t.AddChild(Widgets.Container(Rectangle.Empty));
            Assert.That(_t.Children, Is.Not.Empty);
        }

        [Test]
        public void AddChild_Null_ThrowsError()
        {
            Assert.That(() => _t.AddChild(null), Throws.ArgumentNullException);
        }

        [Test]
        public void AddChild_ChildPositionRelativeToParent()
        {
            _t.Data.Space = new Rectangle(new Point(100, 150), _t.Data.Space.Size);
           var c = _t.AddChild(Widgets.Container(Rectangle.Empty));
            Assert.That(c.Data.Space.Location, Is.EqualTo(_t.Data.Space.Location)); 
        }
    }
}