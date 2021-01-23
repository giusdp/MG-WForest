using System;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using WForest.Exceptions;
using WForest.Factories;
using WForest.UI.Props.Text;
using WForest.UI.Widgets;
using WForest.UI.WidgetTrees;
using WForest.Utilities.Text;

namespace WForest.Tests.PropTests.TextPropsTests
{
    [TestFixture]
    public class FontSizeTests
    {

        [SetUp]
        public void BeforeEach()
        {
            FontStore.DefaultFont = new Mock<Font>(null).Object;
        }

        [Test]
        public void ApplyOn_NotTextWidget_ThrowsException()
        {
            var size = new FontSize(5);
            var tree = new WidgetTree(WidgetFactory.Container(0, 0));
            Assert.That(() => size.ApplyOn(tree), Throws.TypeOf<IncompatibleWidgetException>());
        }

        [Test]
        public void ApplyOn_TextWidget_ChangesSizeOfFont()
        {
            var size = new FontSize(14);
            var testWidget = (Text) WidgetFactory.Text("Test string");
            var tree = new WidgetTree(testWidget);
            size.ApplyOn(tree);
            Assert.That(testWidget.FontSize, Is.EqualTo(14));
        }

        [Test]
        public void ApplyOn_WithInvalidSize_Throws()
        {
            var size = new FontSize(-1);
            var t = new Text("a");
            var tree = new WidgetTree(t);
            Assert.That(() => size.ApplyOn(tree), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void ApplyOn_RecalculatesSizeOfWidget()
        {
            var mockFont = new Mock<Font>(null);
            FontStore.DefaultFont = mockFont.Object;
            
            var testWidget = (Text) WidgetFactory.Text("Test string");
            var tree = new WidgetTree(testWidget);
            Assert.That(testWidget.Space.Size, Is.EqualTo(new Point(0,0)));
            
            mockFont.Setup(f => f.MeasureText(It.IsAny<string>(), It.IsAny<int>())).Returns((1, 1));
            FontStore.DefaultFont = mockFont.Object;
            var size = new FontSize(14);
            size.ApplyOn(tree);
            Assert.That(testWidget.Space.Size, Is.EqualTo(new Point(1,1))); 
        }
    }
}