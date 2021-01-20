using System.IO;
using NUnit.Framework;
using WForest.Utilities.Collections;

namespace WForest.Tests
{
    [TestFixture]
    public class ShaderDbTests
    {
        [Test]
        public void LoadShader_NotInit_Throws()
        {
            Assert.That(() => ShaderDb.Rounded, Throws.ArgumentNullException);
        }

        // Reading from file system adds an external dependency (integration test not unit test anymore)
        // that makes the test fail outside my env where 
        // I have the file set up in the right directory.
        // TODO: Mock file system
        // [Test]
        // public void ReadShader_ReturnsByteArray()
        // {
        //     var res = ShaderDb.ReadShaderFromFile("Rounded");
        //     Assert.That(res, Is.Not.Null);
        // }

        [Test]
        public void ReadShader_WrongFile_ThrowsNotFound()
        {
            Assert.That(() => ShaderDb.ReadShaderFromFile("TestNotExisting"), Throws.TypeOf<FileNotFoundException>());
        }
    }
}