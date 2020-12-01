using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI;
using WForest.UI.Factories;

namespace WForest.Tests
{
    [TestFixture]
    public class ContainerTests
    {
        [Test]
        public void NewContainer_StartsAtSizeZero()
        {
            var container = Widgets.Container();
            Assert.That(container.Space, Is.EqualTo(Rectangle.Empty));
        }

        // [Test]
        // public void ContainerWithChild_AsBigAsChild()
        // {
        //     var container = Widgets.Container();
        //     var tree = new WidgetTree(container);
        //     tree.AddChild(Widgets.Container(50, 60));
        //     
        //     tree.ApplyProperties();
        //     Assert.That(container.Space.Size, Is.EqualTo(new Point(50, 60)));
        // }
    }
}