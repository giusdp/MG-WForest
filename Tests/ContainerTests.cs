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
        public void SimpleContainer_StartsAtSizeZero()
        {
            var container = Widgets.Container();
            Assert.That(container.Space, Is.EqualTo(Rectangle.Empty));
        }

    }
}