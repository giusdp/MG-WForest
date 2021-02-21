using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using WForest.Factories;
using WForest.Props.Props.Text;
using WForest.Utilities;
using WForest.Utilities.Text;
using WForest.Widgets.BuiltIn;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Tests.PropTests.TextPropsTests
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
        public void ApplyOn_TextWidget_ChangesSizeOfFont()
        {
            var size = new FontSize(14);
            var testWidget = (Text) WidgetFactory.Text("Test string");
            size.ApplyOn(testWidget);
            Assert.That(testWidget.FontSize, Is.EqualTo(14));
        }

        [Test]
        public void ApplyOn_WithInvalidSize_Throws()
        {
            var size = new FontSize(-1);
            var t = new Text("a");
            Assert.That(() => size.ApplyOn(t), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void ApplyOn_RecalculatesSizeOfWidget()
        {
            var mockFont = new Mock<Font>(null);
            mockFont.Setup(f => f.MeasureText(It.IsAny<string>(), It.IsAny<int>())).Returns((0, 0));
            FontStore.DefaultFont = mockFont.Object;

            var testWidget = (Text) WidgetFactory.Text("Test string");
            Assert.That(testWidget.Space.Size, Is.EqualTo(new Vector2(0, 0)));

            mockFont.Setup(f => f.MeasureText(It.IsAny<string>(), It.IsAny<int>())).Returns((1, 1));
            FontStore.DefaultFont = mockFont.Object;
            var size = new FontSize(14);
            size.ApplyOn(testWidget);
            Assert.That(testWidget.Space.Size, Is.EqualTo(new Vector2(1, 1)));
        }

        [Test]
        public void ApplyOn_NotTextWidget_CascadesToChildren()
        {
            // arrange
            var root = WidgetFactory.Container();
            var testWidget = (Text) WidgetFactory.Text("Test string");
            root.AddChild(testWidget);
            var size = 14;
            // act
            root.WithProp(new FontSize(size));
            TreeVisitor.ApplyPropsOnTree(root);

            // assert
            Assert.That(testWidget.FontSize, Is.EqualTo(size));
        }

        [Test]
        public void ApplyOn_WidgetWithTextChildren_DoesNotOverrideTextChildWithFontFamilyProp()
        {
            // arrange
            var root = WidgetFactory.Text("Root string");
            var testWidget = WidgetFactory.Text("Test string");
            root.AddChild(testWidget);

            var childSize = 15;
            var fontsize = new FontSize(14);
            var fontSizeChild = new FontSize(childSize);

            // act
            root.WithProp(fontsize);
            testWidget.WithProp(fontSizeChild);
            TreeVisitor.ApplyPropsOnTree(root);

            // assert
            Assert.That(((Text) testWidget).FontSize, Is.EqualTo(childSize));
        }
    }
}