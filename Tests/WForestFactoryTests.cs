using NUnit.Framework;
using WForest.Exceptions;
using WForest.Factories;
using WForest.UI.Widgets;
using WForest.Utilities;

namespace Tests
{
    [TestFixture]
    public class WForestFactoryTests
    {
        [Test]
        public void CreateTree_NotInit_Throws()
        {
            Assert.That(() => WForestFactory.CreateWTree(new Widget(RectangleF.Empty)),
                Throws.TypeOf<WForestNotInitializedException>());
        }
    }
}