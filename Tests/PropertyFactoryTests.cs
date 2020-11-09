using NUnit.Framework;
using PiBa.UI.Factories;
using PiBa.UI.Properties;

namespace PiBa.Tests
{
    [TestFixture]
    public class ConstraintFactoryTests
    {
        [Test]
        public void CreateCenterProperty_ReturnsCenterProperty()
        {
            var c = PropertyFactory.CreateCenterProperty();
            Assert.That(c, Is.TypeOf<Center>());
        }
    }
}