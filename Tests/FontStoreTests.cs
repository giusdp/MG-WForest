using NUnit.Framework;
using WForest.Exceptions;
using WForest.Tests.Utils;
using WForest.Utilities.Text;

namespace WForest.Tests
{

    [TestFixture]
    public class FontStoreTests
    {
        [SetUp]
        public void BeforeEach()
        {
            FontStore.DefaultFont = null!;
        }
        [Test]
        public void GetFont_NoFont_ThrowsFontNotFound()
        {
            FontStore.Initialize(new FakeFont());
            Assert.That(() => FontStore.GetFont("test"), Throws.TypeOf<FontNotFoundException>());
        }

        [Test]
        public void UseFontManager_NotInitialized_ThrowsException()
        {
            Assert.That(() => FontStore.RegisterFont("test", new FakeFont()),
                Throws.TypeOf<FontStoreNotInitializedException>());
        }

        [Test]
        public void AccessDefaultFont_NotInitialized_ThrowsException()
        {
           Assert.That(() => FontStore.DefaultFont,Throws.TypeOf<FontStoreNotInitializedException>()); 
        }
    }
}