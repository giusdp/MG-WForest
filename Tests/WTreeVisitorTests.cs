using NUnit.Framework;
using PiBa.UI;
using PiBa.UI.Interfaces;

namespace PiBa.Tests
{
    [TestFixture]
    public class WTreeVisitorTests
    {
        [Test]
        public void ShouldVisitTreeWithOneNode()
        {
            var widgetTree = new WidgetTree(new Container());
            int numberOfNodes = 0;
            WTreeVisitor.ApplyToTree(widgetTree, widget => { numberOfNodes += 1; });
            Assert.That(numberOfNodes, Is.EqualTo(1));
        }

        [Test]
        public void ShouldVisitTreeWithNNodes()
        {
            var widgetTree = new WidgetTree(new Container());
            var subTree = new WidgetTree(new Container());
            subTree.Children = new []{new WidgetTree(new Container())};
            widgetTree.Children = new WidgetTree[]{new WidgetTree(new Container()), subTree };
            int numberOfNodes = 0;
            WTreeVisitor.ApplyToTree(widgetTree, widget => { numberOfNodes += 1; });
            Assert.That(numberOfNodes, Is.EqualTo(4)); 
        }

        [Test]
        public void ShouldThrowArgumentExceptionWithNull()
        {
            Assert.That(() => WTreeVisitor.ApplyToTree(null, w => {}), Throws.ArgumentNullException);
        }
    }

    public class Container : Widget
    {
        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void Draw()
        {
            throw new System.NotImplementedException();
        }
    }
}