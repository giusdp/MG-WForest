using System;
using System.Linq;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI.Widgets;
using WForest.UI;
using WForest.UI.Factories;
using WForest.UI.WidgetTreeHandlers;
using WForest.Utilities;
using WForest.Utilities.Collections;

namespace WForest.Tests
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
            Assert.That(() => TreeVisitor<int>.ApplyToTreeFromRoot(null, tree => { }), Throws.ArgumentNullException);
            Assert.That(() => TreeVisitor<int>.ApplyToTreeFromRoot(new Tree<int>(0), null), Throws.ArgumentNullException);
        }

        [Test]
        public void ApplyToTreeFromRoot_TakesAnAction_AppliesItToAllNodes()
        {
            var count = 0;
            TreeVisitor<int>.ApplyToTreeFromRoot(_tree, node => count += node.Data );
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
        
        [Test]
        public void IsHovered_LocationInside_ReturnsTrue()
        {
            var v = new WidgetTreeVisitor();
            var w = new WidgetTree(Widgets.Container(new Rectangle(0,0,540,540)));
            var b = v.CheckHovering(w, new Point(332, 43)) is Maybe<WidgetTree>.Some;
            Assert.That(b, Is.True);
        }

        [Test]
        public void IsHovered_NotInside_ReturnsFalse()
        {
            var v = new WidgetTreeVisitor();
            var w= new WidgetTree(Widgets.Container(new Rectangle(0,0,540,540)));
            var b = v.CheckHovering(w, new Point(332, 678)) is Maybe<WidgetTree>.Some;
            Assert.That(b, Is.False);
        }
    }
}