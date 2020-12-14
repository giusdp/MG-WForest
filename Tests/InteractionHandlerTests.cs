using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI.Factories;
using WForest.UI.WidgetTree;
using WForest.Utilities;

namespace WForest.Tests
{
    [TestFixture]
    public class InteractionHelperTests
    {
        [Test]
        public void IsHovered_LocationInside_ReturnsTrue()
        {
            var v = new WidgetTreeVisitor();
            var w = new WidgetTree(Widgets.Container(new Rectangle(0,0,540,540)));
            var b = WidgetInteractionHandler.GetHoveredWidget(w, new Point(332, 43)) is Maybe<WidgetTree>.Some;
            Assert.That(b, Is.True);
        }

        [Test]
        public void IsHovered_NotInside_ReturnsFalse()
        {
            var v = new WidgetTreeVisitor();
            var w= new WidgetTree(Widgets.Container(new Rectangle(0,0,540,540)));
            var b = WidgetInteractionHandler.GetHoveredWidget(w, new Point(332, 678)) is Maybe<WidgetTree>.Some;
            Assert.That(b, Is.False);
        }
    }
}