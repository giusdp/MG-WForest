using NUnit.Framework;
using WForest.Props.Props.Shaders;

namespace Tests.PropTests
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