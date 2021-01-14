using System;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Exceptions;
using WForest.Factories;
using WForest.Tests.Utils;
using WForest.UI.Properties.Text;
using WForest.UI.Widgets.TextWidget;
using WForest.UI.WidgetTrees;

namespace WForest.Tests.PropertyTests.TextPropsTests
{
    [TestFixture]
    public class FontFamilyTests
    {
        [OneTimeSetUp]
        public void Before()
        {
            FontStore.Initialize(new FakeFont());
            FontStore.RegisterFont("ff", new FakeFont());
        }

        [Test]
        public void ApplyOn_NotTextWidget_ThrowsException()
        {
            var font = new FontFamily("ff");
            var tree = new WidgetTree(WidgetFactory.Container(0, 0));
            Assert.That(() => font.ApplyOn(tree), Throws.TypeOf<IncompatibleWidgetException>());
        }

        [Test]
        public void ApplyOn_TextWidget_ChangesSizeOfFont()
        {
            var anotherFont = new FakeFont();
            FontStore.RegisterFont("ff1", anotherFont);
            var font = new FontFamily("ff1");
            var testWidget = (Text) WidgetFactory.Text("Test string");
            var tree = new WidgetTree(testWidget);
            font.ApplyOn(tree);
            Assert.That(testWidget.Font, Is.EqualTo(anotherFont));
        }

        [Test]
        public void ApplyOn_RecalculatesSizeOfText()
        {
            var font = new FontFamily("ff");

            var testWidget = (Text) WidgetFactory.Text("Test string");
            var tree = new WidgetTree(testWidget);
            Assert.That(testWidget.Space.Size, Is.EqualTo(new Point(0, 0)));

            ((FakeFont)FontStore.GetFont("ff")).MeasureTextResult = (1, 1); 
            font.ApplyOn(tree);
            Assert.That(testWidget.Space.Size, Is.EqualTo(new Point(1, 1)));
        }
    }
}