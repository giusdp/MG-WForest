using System.Linq;
using Moq;
using NUnit.Framework;
using WForest.Props.Interfaces;
using WForest.Utilities;
using WForest.Widgets.BuiltIn;
using WForest.Widgets.Interfaces;

namespace Tests
{
    [TestFixture]
    public class TreeVisitorTests
    {
        private IWidget _tree = new Container(RectangleF.Empty);

        [SetUp]
        public void SetUpBeforeEach()
        {
            _tree = new Container(RectangleF.Empty);
            _tree.Children.Add(new Container(new RectangleF(0, 0, 1, 1)));
            _tree.Children.Add(new Container(new RectangleF(0, 0, 10, 2)));
            _tree.Children.First().Children.Add(new Container(new RectangleF(0, 0, 20, 20)));
        }

        [Test]
        public void ApplyProps_WidgetTree_Applies()
        {
            IWidget root = new Container(RectangleF.Empty);
            Mock<IApplicableProp> mock = new Mock<IApplicableProp>();
            root.WithProp(mock.Object);

            TreeVisitor.ApplyPropsOnTree(root);

            mock.Verify(p => p.ApplyOn(It.IsAny<IWidget>()), Times.Once);
        }

        [Test]
        public void ApplyProps_Null_Throws()
        {
            Assert.That(() => TreeVisitor.ApplyPropsOnTree(null!), Throws.ArgumentNullException);
        }

        [Test]
        public void ApplyProps_Tree_StartsFromLeaf()
        {
            IWidget root = new Container(RectangleF.Empty);
            IWidget child = new Container(RectangleF.Empty);
            Mock<IApplicableProp> mockRootProp = new Mock<IApplicableProp>();
            Mock<IApplicableProp> mockChildProp = new Mock<IApplicableProp>();

            var lastCalled = 0;
            mockRootProp.Setup(p => p.ApplyOn(It.IsAny<IWidget>())).Callback(() => lastCalled = 1);
            mockChildProp.Setup(p => p.ApplyOn(It.IsAny<IWidget>())).Callback(() => lastCalled = 2);

            root.WithProp(mockRootProp.Object);
            child.WithProp(mockChildProp.Object);

            root.AddChild(child);

            TreeVisitor.ApplyPropsOnTree(root);

            mockRootProp.Verify(p => p.ApplyOn(It.IsAny<IWidget>()), Times.Once);
            mockChildProp.Verify(p => p.ApplyOn(It.IsAny<IWidget>()), Times.Once);
            Assert.That(lastCalled, Is.EqualTo(1));
        }

        [Test]
        public void ApplyProps_Tree_StartsFromRightLeaf()
        {
            IWidget root = new Container(RectangleF.Empty);
            IWidget child = new Container(RectangleF.Empty);
            IWidget rightChild = new Container(RectangleF.Empty);
            Mock<IApplicableProp> mockRootProp = new Mock<IApplicableProp>();
            Mock<IApplicableProp> mockLeftLeaf = new Mock<IApplicableProp>();
            Mock<IApplicableProp> mockRightLeaf = new Mock<IApplicableProp>();

            var firstCalled = 0;
            var lastCalled = 0;

            void SetVerification(int x)
            {
                if (firstCalled == 0) firstCalled = x;
                lastCalled = x;
            }

            mockRootProp.Setup(p => p.ApplyOn(It.IsAny<IWidget>()))
                .Callback(() => SetVerification(1));
            mockLeftLeaf.Setup(p => p.ApplyOn(It.IsAny<IWidget>()))
                .Callback(() => SetVerification(2));
            mockRightLeaf.Setup(p => p.ApplyOn(It.IsAny<IWidget>()))
                .Callback(() => SetVerification(3));

            root.WithProp(mockRootProp.Object);
            child.WithProp(mockLeftLeaf.Object);
            rightChild.WithProp(mockRightLeaf.Object);

            root.AddChild(child);
            root.AddChild(rightChild);

            TreeVisitor.ApplyPropsOnTree(root);

            mockRootProp.Verify(p => p.ApplyOn(It.IsAny<IWidget>()), Times.Once);
            mockLeftLeaf.Verify(p => p.ApplyOn(It.IsAny<IWidget>()), Times.Once);
            mockRightLeaf.Verify(p => p.ApplyOn(It.IsAny<IWidget>()), Times.Once);
            Assert.That(lastCalled, Is.EqualTo(1));
            Assert.That(firstCalled, Is.EqualTo(3));
        }

        [Test]
        public void ApplyToTreeLevelByLevel_NullArgs_ThrowsError()
        {
            Assert.That(() => TreeVisitor.ApplyToTreeLevelByLevel(null!, tree => { }),
                Throws.ArgumentNullException);
            Assert.That(() => TreeVisitor.ApplyToTreeLevelByLevel(new Container(RectangleF.Empty), null!),
                Throws.ArgumentNullException);
        }

        [Test]
        public void ApplyToTreLevelByLevel_TakesAnAction_AppliesItToAllNodes()
        {
            float count = 0;
            TreeVisitor.ApplyToTreeLevelByLevel(_tree, node => count += node.Sum(n => n.Space.Width));
            Assert.That(count, Is.EqualTo(31));
        }

        [Test]
        public void GetLowestNodeThatHolds_TruePredicateForThirdNode_ReturnsSome()
        {
            var res = TreeVisitor.GetLowestNodeThatHolds(_tree, w => w.Children.Reverse(),
                t => t.Space.Width > 9 && t.Space.Width < 11);
            var b = res.TryGetValue(out var r);
            Assert.That(r.Space.Width, Is.EqualTo(10));
            Assert.That(b, Is.True);
        }

        [Test]
        public void GetLowestNodeThatHolds_TruePredicateForLeaf_ReturnsSome()
        {
            var res = TreeVisitor.GetLowestNodeThatHolds(_tree, w => w.Children.Reverse(), t => t.Space.Width >= 10);
            var b = res.TryGetValue(out var r);
            Assert.That(b, Is.True);
            Assert.That(r.Space.Width, Is.EqualTo(20));
        }

        [Test]
        public void GetLowestNodeThatHolds_FalsePredicate_ReturnsNone()
        {
            var res = TreeVisitor.GetLowestNodeThatHolds(_tree, w => w.Children.Reverse(), t => t.Space.Width > 30);
            var b = res.TryGetValue(out _);
            Assert.That(b, Is.False);
        }

        [Test]
        public void GetLowestNodeThatHolds_Null_Throws()
        {
            Assert.That(() => TreeVisitor.GetLowestNodeThatHolds(null!, w => w.Children, tree => true),
                Throws.ArgumentNullException);
            Assert.That(() => TreeVisitor.GetLowestNodeThatHolds(_tree, null!, tree => true),
                Throws.ArgumentNullException);
            Assert.That(() => TreeVisitor.GetLowestNodeThatHolds(_tree, w => w.Children, null!),
                Throws.ArgumentNullException);
        }
    }
}