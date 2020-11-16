using NUnit.Framework;
using PiBa.UI.Factories;
using PiBa.UI.Properties.Actions;
using PiBa.UI.Properties.Grid.Center;
using PiBa.UI.Properties.Margins;

namespace PiBa.Tests
{
    [TestFixture]
    public class PropertyFactoryTests
    {
        [Test]
        public void CreateCenterProperty_ReturnsCenterProperty()
        {
            var p = PropertyFactory.Center();
            Assert.That(p, Is.TypeOf<Center>());
        }
        
        [Test]
        public void CreateOnPressProperty_ThrowsOnNull()
        {
            Assert.That(() => PropertyFactory.OnPress(null), Throws.ArgumentNullException);
        }

        [Test]
        public void CreateOnPressProperty_ReturnsOnPressProp()
        {
            var p = PropertyFactory.OnPress(() => { });
            Assert.That(p, Is.TypeOf<OnPress>());
        }
        
        [Test]
        public void CreateOnReleaseProperty_ThrowsOnNull()
        {
            Assert.That(() => PropertyFactory.OnRelease(null), Throws.ArgumentNullException);
        }

        [Test]
        public void CreateOnReleaseProperty_ReturnsOnReleaseProp()
        {
            var p = PropertyFactory.OnRelease(() => { });
            Assert.That(p, Is.TypeOf<OnRelease>());
        }
        
        [Test]
        public void CreateOnEnterPressProperty_ThrowsOnNull()
        {
            Assert.That(() => PropertyFactory.OnEnter(null), Throws.ArgumentNullException);
        }
        
        [Test]
        public void CreateOnEnterProperty_ReturnsOnEnterProp()
        {
            var p = PropertyFactory.OnEnter(() => { });
            Assert.That(p, Is.TypeOf<OnEnter>());
        }
        
        [Test]
        public void CreateOnExitProperty_ThrowsOnNull()
        {
            Assert.That(() => PropertyFactory.OnExit(null), Throws.ArgumentNullException);
        }
        
        [Test]
        public void CreateOnExitProperty_ReturnsOnExitProp()
        {
            var p = PropertyFactory.OnExit(() => { });
            Assert.That(p, Is.TypeOf<OnExit>());
        }

        [Test]
        public void CreateMarginProperty_ReturnsMarginProp()
        {
            var m = PropertyFactory.Margin(2);
            Assert.That(m, Is.TypeOf<Margin>());
        }
    }
}