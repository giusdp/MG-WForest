using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using WForest.Factories;
using WForest.Props.Text;
using WForest.Utilities;
using WForest.Utilities.Text;
using WForest.Widgets.BuiltIn;

namespace Tests.PropTests.TextPropsTests
{
    [TestFixture]
    public class FontFamilyTests
    {
        [OneTimeSetUp]
        public void Before()
        {
            FontStore.DefaultFont = new Mock<Font>(null).Object;
            FontStore.RegisterFont("ff", new Mock<Font>(null).Object);
        }

        [Test]
        public void ApplyOn_TextWidget_ChangesSizeOfFont()
        {
            var anotherFont = new Mock<Font>(null).Object;
            FontStore.RegisterFont("ff1", anotherFont);

            var font = new FontFamily("ff1");
            var testWidget = (Text) WidgetFactory.Text("Test string");

            font.ApplyOn(testWidget);

            Assert.That(testWidget.Font, Is.EqualTo(anotherFont));
        }

        [Test]
        public void ApplyOn_TextWidget_RecalculatesSizeOfText()
        {
            var mockedFont = new Mock<Font>(null);
            mockedFont.Setup(f => f.MeasureText(It.IsAny<string>(), It.IsAny<int>())).Returns((1, 1));
            FontStore.RegisterFont("mockF", mockedFont.Object);

            var font = new FontFamily("mockF");
            var testWidget = (Text) WidgetFactory.Text("Test string");
            Assert.That(testWidget.Space.Size, Is.EqualTo(new Vector2(0, 0)));

            font.ApplyOn(testWidget);
            Assert.That(testWidget.Space.Size, Is.EqualTo(new Vector2(1, 1)));
        }

        [Test]
        public void ApplyOn_NotTextWidget_CascadesToChildren()
        {
            // arrange
            var root = WidgetFactory.Container();
            var testWidget = (Text) WidgetFactory.Text("Test string");
            root.AddChild(testWidget);
            var mockedFont = new Mock<Font>(null);
            FontStore.RegisterFont("mockF2", mockedFont.Object);
            var font = new FontFamily("mockF2");

            // act
            root.WithProp(font);
            TreeVisitor.ApplyPropsOnTree(root);

            // assert
            Assert.That(testWidget.Font, Is.EqualTo(mockedFont.Object));
        }

        [Test]
        public void ApplyOn_WidgetWithTextChildren_DoesNotOverrideTextChildWithFontFamilyProp()
        {
            // arrange
            var root = WidgetFactory.Container();
            var testWidget = WidgetFactory.Text("Test string");
            root.AddChild(testWidget);
            
            FontStore.RegisterFont("mockF3", new Mock<Font>(null).Object);
            var font = new FontFamily("mockF3");
            
            var mockedFont = new Mock<Font>(null);
            FontStore.RegisterFont("mockF4", mockedFont.Object);
            var fontChild = new FontFamily("mockF4");
            
            // act
            root.WithProp(font);
            testWidget.WithProp(fontChild);
            TreeVisitor.ApplyPropsOnTree(root);

            // assert
            Assert.That(((Text)testWidget).Font, Is.EqualTo(mockedFont.Object));
        }
    }
}