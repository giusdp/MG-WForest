using NUnit.Framework;
using WForest.UI.Properties;

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