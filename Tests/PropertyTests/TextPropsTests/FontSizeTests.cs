using System;
using NUnit.Framework;
using WForest.Exceptions;
using WForest.Tests.Utils;
using WForest.UI.Factories;
using WForest.UI.Properties.Text;
using WForest.UI.Widgets.TextWidget;
using WForest.UI.WidgetTree;

namespace WForest.Tests.PropertyTests.TextPropsTests
{
    [TestFixture]
    public class FontSizeTests
    {
        [Test]
        public void ApplyOn_NotTextWidget_ThrowsException()
        {
            var size = new FontSize(5);
            var tree = new WidgetTree(Widgets.Container(0, 0));
            Assert.That(() => size.ApplyOn(tree), Throws.TypeOf<IncompatibleWidgetException>());
        }

        [Test]
        public void ApplyOn_TextWidget_ChangesSizeOfFont()
        {
            FontManager.Initialize(new FakeFont());
            var size = new FontSize(14);
            var testWidget = (Text) Widgets.Text("Test string");
            var tree = new WidgetTree(testWidget);
            size.ApplyOn(tree);
            Assert.That(testWidget.Font.Size, Is.EqualTo(14));
        }

        [Test]
        public void ApplyOn_WithInvalidSize_Throws()
        {
            var size = new FontSize(-1);
            var tree = new WidgetTree(new Text("a"));
            Assert.That(() => size.ApplyOn(tree), Throws.TypeOf<ArgumentException>());
        }
    }
}