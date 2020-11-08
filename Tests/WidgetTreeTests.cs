using Microsoft.Xna.Framework;
using NUnit.Framework;
using PiBa.UI;
using PiBa.UI.Factories;

namespace PiBa.Tests
{
    [TestFixture]
    public class WidgetTreeTests
    {
        private WidgetTree t;

        [SetUp]
        public void SetUpBeforeEach()
        {
            t = new WidgetTree(WidgetFactory.CreateContainer(Rectangle.Empty));
        }

        [Test]
        public void AddConstraint_AddConstraintToList()
        {
            var c = ConstraintFactory.CreateCenterConstraint();
            t.AddConstraint(c);
            Assert.That(t.Constraints.Contains(c), Is.True);
        }

        [Test]
        public void AddConstraint_NullInput()
        {
            Assert.That(() => t.AddConstraint(null), Throws.ArgumentNullException);
        }

        [Test]
        public void AddChild_AddsWidgetChild()
        {
            t.AddChild(WidgetFactory.CreateContainer(Rectangle.Empty));
            Assert.That(t.Children, Is.Not.Empty);
        }

        [Test]
        public void AddChild_Null_ThrowsError()
        {
            Assert.That(() => t.AddChild(null), Throws.ArgumentNullException);
        }
        
        
    }
}