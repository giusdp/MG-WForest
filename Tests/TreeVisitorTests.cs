using System.Linq;
using NUnit.Framework;
using PiBa.Utilities;
using PiBa.Utilities.Collections;

namespace PiBa.Tests
{
    [TestFixture]
    public class TreeVisitorTests
    {
        [Test]
        public void ApplyToTree_NullArgs_ThrowsError()
        {
            Assert.That(() => TreeVisitor<int>.ApplyToTree(null, tree => { }), Throws.ArgumentNullException);
            Assert.That(() => TreeVisitor<int>.ApplyToTree(new Tree<int>(0), null), Throws.ArgumentNullException);
        }

        [Test]
        public void ApplyToTree_TakesAnAction_AppliesItToAllNodes()
        {
            var tree = new Tree<int>(1);
            tree.Children.Add(new Tree<int>(2));
            tree.Children.Add(new Tree<int>(10));
            tree.Children.First().Children.Add(new Tree<int>(20));
            var count = 0;
            TreeVisitor<int>.ApplyToTree(tree, node => count += node.Data );
            Assert.That(count, Is.EqualTo(33));
        }
    }
}