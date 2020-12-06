using NUnit.Framework;
using WForest.UI.Properties;
using WForest.UI.Properties.Shaders;

namespace WForest.Tests.PropertyTests
{
    [TestFixture]
    public class RoundedTests
    {
        [Test]
        public void CreateRounded_WithNegativeValue_Throws()
        {
           Assert.That(() => new Rounded(-1), Throws.Exception); 
        }
        
        
    }
}