using NUnit.Framework;
using WForest.Exceptions;
using WForest.Factories;
using WForest.Utilities;
using WForest.Widgets;

namespace Tests
{
    [TestFixture]
    public class WForestFactoryTests
    {
        [Test]
        public void CreateTree_NotInit_Throws()
        {
            Assert.That(() => WTreeFactory.CreateWTree(new Widget(RectangleF.Empty)),
                Throws.TypeOf<WForestNotInitializedException>());
        }
    }
}