using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Exceptions;
using WForest.Factories;
using WForest.UI.Widgets;

namespace WForest.Tests
{
    [TestFixture]
    public class WForestFactoryTests
    {
        [Test]
        public void CreateTree_NotInit_Throws()
        {
            Assert.That(() => WForestFactory.CreateWTree(new Widget(Rectangle.Empty)),
                Throws.TypeOf<WForestNotInitializedException>());
        }
    }
}