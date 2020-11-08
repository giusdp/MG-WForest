using System;
using System.Linq;
using NUnit.Framework;
using PiBa.Utilities;
using PiBa.Utilities.Collections;

namespace PiBa.Tests
{
    [TestFixture]
    public class TreeVisitorTests
    {
        private Tree<int> _tree;

        [SetUp]
        public void SetUpBeforeEach()
        {
           _tree = new Tree<int>(1);
            _tree.Children.Add(new Tree<int>(2));
            _tree.Children.Add(new Tree<int>(10));
            _tree.Children.First().Children.Add(new Tree<int>(20)); 
        }
        
        [Test]
        public void ApplyToTree_NullArgs_ThrowsError()
        {
            Assert.That(() => TreeVisitor<int>.ApplyToTree(null, tree => { }), Throws.ArgumentNullException);
            Assert.That(() => TreeVisitor<int>.ApplyToTree(new Tree<int>(0), null), Throws.ArgumentNullException);
        }

        [Test]
        public void ApplyToTree_TakesAnAction_AppliesItToAllNodes()
        {
            var count = 0;
            TreeVisitor<int>.ApplyToTree(_tree, node => count += node.Data );
            Assert.That(count, Is.EqualTo(33));
        }

        [Test]
        public void GetLowestNodeThatHolds_TruePredicateForThirdNode_ReturnsSome()
        {
            var res = TreeVisitor<int>.GetLowestNodeThatHolds(_tree, t => t.Data > 9 && t.Data < 11);
            var b = res.TryGetValue(out var r);
            Assert.That(r.Data, Is.EqualTo(10));
            Assert.That(b, Is.True);
        }

        [Test]
        public void GetLowestNodeThatHolds_TruePredicateForLeaf_ReturnsSome()
        {
           var res = TreeVisitor<int>.GetLowestNodeThatHolds(_tree, t => t.Data >= 10);
            var b = res.TryGetValue(out var r);
            Assert.That(b, Is.True); 
            Assert.That(r.Data, Is.EqualTo(20));
        }
        
        [Test]
        public void GetLowestNodeThatHolds_FalsePredicate_ReturnsNone()
        {
            var res = TreeVisitor<int>.GetLowestNodeThatHolds(_tree, t => t.Data > 30);
            var b = res.TryGetValue(out var r);
            Assert.That(b, Is.False); 
        }

        [Test]
        public void GetLowestNodeThatHolds_Null_Throws()
        {
            Assert.That(() => TreeVisitor<int>.GetLowestNodeThatHolds(null, tree => true), Throws.ArgumentNullException);
            Assert.That(() => TreeVisitor<int>.GetLowestNodeThatHolds(_tree, null), Throws.ArgumentNullException);
        }
    }
}