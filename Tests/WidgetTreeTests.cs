using Microsoft.Xna.Framework;
using NUnit.Framework;
using PiBa.UI;
using PiBa.UI.Factories;
using PiBa.UI.WidgetTreeHandlers;

namespace PiBa.Tests
{
    [TestFixture]
    public class WidgetTreeTests
    {
        private WidgetTree _t;

        [SetUp]
        public void SetUpBeforeEach()
        {
            _t = new WidgetTree(WidgetFactory.CreateContainer(Rectangle.Empty));
        }

        [Test]
        public void AddConstraint_AddConstraintToList()
        {
            var c = PropertyFactory.CreateCenterProperty();
            _t.AddProperty(c);
            Assert.That(_t.Properties.Contains(c), Is.True);
        }

        [Test]
        public void AddConstraint_NullInput()
        {
            Assert.That(() => _t.AddProperty(null), Throws.ArgumentNullException);
        }

        [Test]
        public void AddChild_AddsWidgetChild()
        {
            _t.AddChild(WidgetFactory.CreateContainer(Rectangle.Empty));
            Assert.That(_t.Children, Is.Not.Empty);
        }

        [Test]
        public void AddChild_Null_ThrowsError()
        {
            Assert.That(() => _t.AddChild(null), Throws.ArgumentNullException);
        }
    }
}