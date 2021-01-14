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
    public class FontSizeTests
    {
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
            FontStore.Initialize(new FakeFont());
            var size = new FontSize(14);
            var testWidget = (Text) WidgetFactory.Text("Test string");
            var tree = new WidgetTree(testWidget);
            size.ApplyOn(tree);
            Assert.That(testWidget.FontSize, Is.EqualTo(14));
        }

        [Test]
        public void ApplyOn_WithInvalidSize_Throws()
        {
            FontStore.Initialize(new FakeFont());
            var size = new FontSize(-1);
            var t = new Text("a");
            var tree = new WidgetTree(t);
            Assert.That(() => size.ApplyOn(tree), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void ApplyOn_RecalculatesSizeOfWidget()
        {
            var ff = new FakeFont();
            FontStore.Initialize(ff);
            var size = new FontSize(14);
            var testWidget = (Text) WidgetFactory.Text("Test string");
            var tree = new WidgetTree(testWidget);
            Assert.That(testWidget.Space.Size, Is.EqualTo(new Point(0,0)));
            ff.MeasureTextResult = (1, 1);
            size.ApplyOn(tree);
            Assert.That(testWidget.Space.Size, Is.EqualTo(new Point(1,1))); 
        }
    }
}