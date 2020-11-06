using NUnit.Framework;
using PiBa.UI;
using PiBa.UI.Factories;
using PiBa.UI.Widgets;
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
            var child = tree.AddChild(2);
            tree.AddChild(10);
            child.AddChild(20);
            var count = 0;
            TreeVisitor<int>.ApplyToTree(tree, node => count += node.Data );
            Assert.That(count, Is.EqualTo(33));
        }
    }
}