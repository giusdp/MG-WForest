using Moq;
using NUnit.Framework;
using WForest.Factories;
using WForest.Props.Interfaces;
using WForest.Utilities;
using WForest.Widgets;
using WForest.Widgets.Interfaces;

namespace Tests
{
    [TestFixture]
    public class WidgetTests
    {
        private IWidget _widget = new Widget(RectangleF.Empty);

        [SetUp]
        public void BeforeEach()
        {
            _widget = new Widget(RectangleF.Empty);
        }

        [Test]
        public void WithProp_Prop_AddsToPropList()
        {
            _widget.WithProp(Mock.Of<IProp>());
            Assert.That(_widget.Props, Is.Not.Empty);
        }

        [Test]
        public void AddChild_ValidWidget_AddsToChildren()
        {
            _widget.AddChild(new Widget(RectangleF.Empty));
            Assert.That(_widget.Children, Is.Not.Empty);
        }

        [Test]
        public void AddChild_Itself_Throws()
        {
            Assert.That(() => _widget.AddChild(_widget), Throws.ArgumentException);
        }

        [Test]
        public void AddChild_ValidWidget_ChildHasThisParent()
        {
            var w = new Widget(RectangleF.Empty);
            _widget.AddChild(w);
            Assert.That(w.Parent, Is.EqualTo(_widget));
        }
        
        [Test]
        public void SimpleContainer_StartsAtSizeZero()
        {
            var container = WidgetFactory.Container();
            Assert.That(container.Space, Is.EqualTo(RectangleF.Empty));
        }
    }
}