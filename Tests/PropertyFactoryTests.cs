using NUnit.Framework;
using WForest.UI.Factories;
using WForest.UI.Properties.Actions;
using WForest.UI.Properties.Grid.Center;
using WForest.UI.Properties.Margins;

namespace WForest.Tests
{
    [TestFixture]
    public class PropertyFactoryTests
    {
        [Test]
        public void CreateCenterProperty_ReturnsCenterProperty()
        {
            var p = Properties.Center();
            Assert.That(p, Is.TypeOf<JustifyCenter>());
        }
        
        [Test]
        public void CreateOnPressProperty_ThrowsOnNull()
        {
            Assert.That(() => Properties.OnPress(null), Throws.ArgumentNullException);
        }

        [Test]
        public void CreateOnPressProperty_ReturnsOnPressProp()
        {
            var p = Properties.OnPress(() => { });
            Assert.That(p, Is.TypeOf<OnPress>());
        }
        
        [Test]
        public void CreateOnReleaseProperty_ThrowsOnNull()
        {
            Assert.That(() => Properties.OnRelease(null), Throws.ArgumentNullException);
        }

        [Test]
        public void CreateOnReleaseProperty_ReturnsOnReleaseProp()
        {
            var p = Properties.OnRelease(() => { });
            Assert.That(p, Is.TypeOf<OnRelease>());
        }
        
        [Test]
        public void CreateOnEnterPressProperty_ThrowsOnNull()
        {
            Assert.That(() => Properties.OnEnter(null), Throws.ArgumentNullException);
        }
        
        [Test]
        public void CreateOnEnterProperty_ReturnsOnEnterProp()
        {
            var p = Properties.OnEnter(() => { });
            Assert.That(p, Is.TypeOf<OnEnter>());
        }
        
        [Test]
        public void CreateOnExitProperty_ThrowsOnNull()
        {
            Assert.That(() => Properties.OnExit(null), Throws.ArgumentNullException);
        }
        
        [Test]
        public void CreateOnExitProperty_ReturnsOnExitProp()
        {
            var p = Properties.OnExit(() => { });
            Assert.That(p, Is.TypeOf<OnExit>());
        }

        [Test]
        public void CreateMarginProperty_ReturnsMarginProp()
        {
            var m = Properties.Margin(2);
            Assert.That(m, Is.TypeOf<Margin>());
        }
    }
}