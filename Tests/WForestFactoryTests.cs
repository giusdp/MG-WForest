using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Exceptions;
using WForest.Factories;
using WForest.UI.WidgetTrees;
using WForest.Utilities.Collections;

namespace WForest.Tests
{
    [TestFixture]
    public class WForestFactoryTests
    {
        [Test]
        public void CreateTree_NotInit_Throws()
        {
            Assert.That(() => WForestFactory.CreateWTree(0, 0, 0, 0, new WidgetTree(null)),
                Throws.TypeOf<WForestNotInitializedException>());
        }

        [Test]
        public void CreateTree_NullWTree_Throws()
        {
            Assert.That(() => WForestFactory.CreateWTree(Rectangle.Empty, null), Throws.ArgumentNullException);
        }
    }
}