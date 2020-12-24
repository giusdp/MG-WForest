using System.Collections.Generic;
using System.IO;
using FontStashSharp;
using FontStashSharp.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using NUnit.Framework;
using WForest.Exceptions;
using WForest.UI.Widgets.TextWidget;

namespace WForest.Tests
{
    class FakeFont : Font
    {
        public FakeFont() : base(null)
        {
        }
    }

    [TestFixture]
    public class FontManagerTests
    {
        [SetUp]
        public void BeforeEach()
        {
            FontManager.DefaultFont = null;
        }
        [Test]
        public void GetFont_NoFont_ThrowsFontNotFound()
        {
            FontManager.Initialize(new FakeFont());
            Assert.That(() => FontManager.GetFont("test"), Throws.TypeOf<FontNotFoundException>());
        }

        [Test]
        public void UseFontManager_NotInitialized_ThrowsException()
        {
            Assert.That(() => FontManager.RegisterFont("test", new FakeFont()),
                Throws.TypeOf<FontManagerNotInitializedException>());
        }

        [Test]
        public void AccessDefaultFont_NotInitialized_ThrowsException()
        {
           Assert.That(() => FontManager.DefaultFont,Throws.TypeOf<FontManagerNotInitializedException>()); 
        }
    }
}