using Moq;
using NUnit.Framework;
using WForest.Exceptions;
using WForest.Utilities.Text;

namespace Tests
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
            Mock<Font> mockedFont = new Mock<Font>(null);
            FontStore.DefaultFont = mockedFont.Object;
            Assert.That(() => FontStore.GetFont("test"), Throws.TypeOf<FontNotFoundException>());
        }

        [Test]
        public void UseFontManager_NotInitialized_ThrowsException()
        {
            Assert.That(() => FontStore.RegisterFont("test", new Mock<Font>(null).Object),
                Throws.TypeOf<FontNotFoundException>());
        }

        [Test]
        public void AccessDefaultFont_NotInitialized_ThrowsException()
        {
           Assert.That(() => FontStore.DefaultFont,Throws.TypeOf<FontNotFoundException>()); 
        }
    }
}