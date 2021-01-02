using System;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Exceptions;
using WForest.Factories;
using WForest.Tests.Utils;
using WForest.UI.Properties.Text;
using WForest.UI.Widgets.TextWidget;
using WForest.UI.WidgetTree;

namespace WForest.Tests.PropertyTests.TextPropsTests
{
    [TestFixture]
    public class FontFamilyTests
    {
        
        [Test]
        public void ApplyOn_NotTextWidget_ThrowsException()
        {
            var font = new FontFamily(new FakeFont());
            var tree = new WidgetTree(WidgetFactory.Container(0, 0));
            Assert.That(() => font.ApplyOn(tree), Throws.TypeOf<IncompatibleWidgetException>());
        }

        [Test]
        public void ApplyOn_TextWidget_ChangesSizeOfFont()
        {
            FontManager.Initialize(new FakeFont());
            var anotherFont = new FakeFont();
            var font = new FontFamily(anotherFont);
            var testWidget = (Text) WidgetFactory.Text("Test string");
            var tree = new WidgetTree(testWidget);
            font.ApplyOn(tree);
            Assert.That(testWidget.Font, Is.EqualTo(anotherFont));
        }
        
        [Test]
        public void ApplyOn_WithInvalidSize_Throws()
        {
            var font = new FontFamily(null);
            var tree = new WidgetTree(new Text("a"));
            Assert.That(() => font.ApplyOn(tree), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void ApplyOn_RecalculatesSizeOfText()
        {
                var ff = new FakeFont();
                FontManager.Initialize(new FakeFont());
                var font = new FontFamily(ff);
                var testWidget = (Text) WidgetFactory.Text("Test string");
                var tree = new WidgetTree(testWidget);
                Assert.That(testWidget.Space.Size, Is.EqualTo(new Point(0,0)));
                ff.MeasureTextResult = (1, 1);
                font.ApplyOn(tree);
                Assert.That(testWidget.Space.Size, Is.EqualTo(new Point(1,1))); 
        }
    }
}