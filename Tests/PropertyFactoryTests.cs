using NUnit.Framework;
using WForest.Factories;
using WForest.Props;
using WForest.Props.Actions;
using WForest.Props.Grid.JustifyProps;

namespace Tests
{
    [TestFixture]
    public class PropertyFactoryTests
    {
        [Test]
        public void CreateCenterProperty_ReturnsCenterProperty()
        {
            var p = PropFactory.JustifyCenter();
            Assert.That(p, Is.TypeOf<JustifyCenter>());
        }
        
        [Test]
        public void CreateOnPressProperty_ThrowsOnNull()
        {
            Assert.That(() => PropFactory.OnPress(null!), Throws.ArgumentNullException);
        }

        [Test]
        public void CreateOnPressProperty_ReturnsOnPressProp()
        {
            var p = PropFactory.OnPress(() => { });
            Assert.That(p, Is.TypeOf<OnPress>());
        }
        
        [Test]
        public void CreateOnReleaseProperty_ThrowsOnNull()
        {
            Assert.That(() => PropFactory.OnRelease(null!), Throws.ArgumentNullException);
        }

        [Test]
        public void CreateOnReleaseProperty_ReturnsOnReleaseProp()
        {
            var p = PropFactory.OnRelease(() => { });
            Assert.That(p, Is.TypeOf<OnRelease>());
        }
        
        [Test]
        public void CreateOnEnterPressProperty_ThrowsOnNull()
        {
            Assert.That(() => PropFactory.OnEnter(null!), Throws.ArgumentNullException);
        }
        
        [Test]
        public void CreateOnEnterProperty_ReturnsOnEnterProp()
        {
            var p = PropFactory.OnEnter(() => { });
            Assert.That(p, Is.TypeOf<OnEnter>());
        }
        
        [Test]
        public void CreateOnExitProperty_ThrowsOnNull()
        {
            Assert.That(() => PropFactory.OnExit(null!), Throws.ArgumentNullException);
        }
        
        [Test]
        public void CreateOnExitProperty_ReturnsOnExitProp()
        {
            var p = PropFactory.OnExit(() => { });
            Assert.That(p, Is.TypeOf<OnExit>());
        }

        [Test]
        public void CreateMarginProperty_ReturnsMarginProp()
        {
            var m = PropFactory.Margin(2);
            Assert.That(m, Is.TypeOf<Margin>());
        }
    }
}