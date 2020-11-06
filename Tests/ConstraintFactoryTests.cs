using NUnit.Framework;
using PiBa.UI.Constraints;
using PiBa.UI.Factories;

namespace PiBa.Tests
{
    [TestFixture]
    public class ConstraintFactoryTests
    {
        [Test]
        public void CreateCenterConstraint_ReturnsCenterConstraint()
        {
            var c = ConstraintFactory.CreateCenterConstraint();
            Assert.That(c, Is.TypeOf<Center>());
        }
    }
}