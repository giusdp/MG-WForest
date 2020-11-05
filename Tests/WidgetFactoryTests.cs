using Microsoft.Xna.Framework.Graphics;
using Moq;
using NUnit.Framework;
using PiBa.Exceptions;
using PiBa.Rendering;
using PiBa.UI.Factories;

namespace PiBa.Tests
{
    [TestFixture]
    public class WidgetFactoryTests
    {
        public void BeforeEach()
        {
        }
        
        [Test]
        public void CreateButton_NotInitialized_ThrowsError()
        {
            Assert.That(WidgetFactory.CreateButton, Throws.TypeOf<WindowNotInitializedException>());
        }

        [Test]
        public void CreateButton_FactoryInitialized_ReturnsButton()
        {
        }
    }
}