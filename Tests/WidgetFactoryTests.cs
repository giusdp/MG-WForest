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
        [Test]
        public void CreateButton_FactoryInitialized_ReturnsButton()
        {
            var button = WidgetFactory.CreateButton();
            Assert.That(button, Is.Not.Null);
        }
    }
}