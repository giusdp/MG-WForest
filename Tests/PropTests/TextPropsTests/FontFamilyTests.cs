using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using WForest.Exceptions;
using WForest.Factories;
using WForest.UI.Props.Text;
using WForest.UI.Widgets.BuiltIn;
using WForest.Utilities.Text;

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
        public void ApplyOn_NotTextWidget_ThrowsException()
        {
            var font = new FontFamily("ff");
            var tree =  WidgetFactory.Container(0, 0);
            Assert.That(() => font.ApplyOn(tree), Throws.TypeOf<IncompatibleWidgetException>());
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
        public void ApplyOn_RecalculatesSizeOfText()
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
    }
}