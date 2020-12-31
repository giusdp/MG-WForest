using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI.Factories;
using WForest.UI.Properties.Grid.Column;
using WForest.UI.WidgetTree;

namespace WForest.Tests.PropertyTests
{
    [TestFixture]
    public class ColumnPropertyTests
    {
        private Column _column;
        private WidgetTree _root;

        [SetUp]
        public void BeforeEach()
        {
            _column = new Column();
            _root = new WidgetTree(Widgets.Container(new Rectangle(0, 0, 1280, 720)));
        }

        [Test]
        public void ApplyOn_NoChildren_NothingHappens()
        {
            Assert.That(() => _column.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void ApplyOn_OneWidget_PositionIsUnchanged()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));
        
            var expected = new Rectangle(0, 0, 120, 120);
            _column.ApplyOn(_root);
            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }
        
        [Test]
        public void ApplyOn_TwoWidgets_OneAboveOther()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));
            var secondChild = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));
        
            var expectedLocFirst = new Rectangle(0, 0, 120, 120);
            var expectedLocSecond = new Rectangle(0, 120, 120, 120);
        
            _column.ApplyOn(_root);
            Assert.That(child.Data.Space, Is.EqualTo(expectedLocFirst));
            Assert.That(secondChild.Data.Space, Is.EqualTo(expectedLocSecond));
        }
        
        [Test]
        public void ApplyOn_WidgetsPassVerticalLimit_GoOnNewColumn()
        {
            var acts = new[]
            {
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 640, 640))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 70))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 160, 120))),
            };
        
            var expects = new[]
            {
                new Rectangle(0, 0, 640, 640),
                new Rectangle(0, 640, 120, 70),
                new Rectangle(640, 0, 160, 120),
            };
        
            _column.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }
        
        [Test]
        public void ApplyOn_ThreeColsWithSameWidgets_CreatesThreeCols()
        {
            var acts = new[]
            {
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
            };
        
            var expects = new[]
            {
                new Rectangle(0, 0, 120, 330),
                new Rectangle(0, 330, 120, 330),
                new Rectangle(120, 0, 120, 330),
                new Rectangle(120, 330, 120, 330),
                new Rectangle(240, 0, 120, 330),
                new Rectangle(240, 330, 120, 330),
            };
        
            _column.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }
    }
}