using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using WForest.Factories;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets;
using WForest.UI.Widgets.BuiltIn;
using WForest.UI.Widgets.Interfaces;

namespace WForest.Tests
{
    [TestFixture]
    public class WidgetTests
    {
        private IWidget _widget;

        [SetUp]
        public void BeforeEach()
        {
            _widget = new Widget(Rectangle.Empty);
        }
        [Test]
        public void WithProp_Null_Throws()
        {
            Assert.That(()=>_widget.WithProp(null), Throws.ArgumentNullException);
            
        }

        [Test]
        public void WithProp_Prop_AddsToPropList()
        {
            _widget.WithProp(Mock.Of<IProp>());
            Assert.That(_widget.Props, Is.Not.Empty);
        }

        [Test]
        public void AddChild_Null_Throws()
        {
            Assert.That(() => _widget.AddChild(null), Throws.ArgumentNullException);
        }

        [Test]
        public void AddChild_ValidWidget_AddsToChildren()
        {
            _widget.AddChild(new Widget(Rectangle.Empty));
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
            var w = new Widget(Rectangle.Empty);
            _widget.AddChild(w);
            Assert.That(w.Parent, Is.EqualTo(_widget));
        }
        
        [Test]
        public void SimpleContainer_StartsAtSizeZero()
        {
            var container = WidgetFactory.Container();
            Assert.That(container.Space, Is.EqualTo(Rectangle.Empty));
        }

        [Test]
        public void SpriteButton_NullSprite_ThrowsError()
        {
            Assert.That(() => new ImageButton(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Text_NullString_Throws()
        {
            Assert.That(() => new Text(null), Throws.ArgumentNullException);
        }
    }
}