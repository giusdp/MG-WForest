using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Factories;
using WForest.UI.Props;
using WForest.UI.Props.Grid;
using WForest.UI.Widgets;

namespace WForest.Tests.PropTests
{
    [TestFixture]
    public class ColumnPropertyTests
    {
        private Column _column;
        private IWidget _root;

        [SetUp]
        public void BeforeEach()
        {
            _column = new Column();
            _root = WidgetFactory.Container(new Rectangle(0, 0, 1280, 720));
        }

        [Test]
        public void ApplyOn_NoChildren_NothingHappens()
        {
            Assert.That(() => _column.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void ApplyOn_OneWidget_PositionIsUnchanged()
        {
            var child = WidgetFactory.Container(new Rectangle(0, 0, 120, 120));
            _root.AddChild(child);

            var expected = new Rectangle(0, 0, 120, 120);
            _column.ApplyOn(_root);
            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_TwoWidgets_OneAboveOther()
        {
            var child = WidgetFactory.Container(new Rectangle(0, 0, 120, 120));
            var secondChild = WidgetFactory.Container(new Rectangle(0, 0, 120, 120));
            _root.AddChild(child);
            _root.AddChild(secondChild);
            var expectedLocFirst = new Rectangle(0, 0, 120, 120);
            var expectedLocSecond = new Rectangle(0, 120, 120, 120);

            _column.ApplyOn(_root);
            Assert.That(child.Space, Is.EqualTo(expectedLocFirst));
            Assert.That(secondChild.Space, Is.EqualTo(expectedLocSecond));
        }

        [Test]
        public void ApplyOn_WidgetsPassVerticalLimit_GoOnNewColumn()
        {
            var acts = new[]
            {
               WidgetFactory.Container(new Rectangle(0, 0, 640, 640)),
               WidgetFactory.Container(new Rectangle(0, 0, 120, 70)),
               WidgetFactory.Container(new Rectangle(0, 0, 160, 120)),
            };

            var expects = new[]
            {
                new Rectangle(0, 0, 640, 640),
                new Rectangle(0, 640, 120, 70),
                new Rectangle(640, 0, 160, 120),
            };

            foreach (var widget in acts) _root.AddChild(widget);
            _column.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_ThreeColsWithSameWidgets_CreatesThreeCols()
        {
            var acts = new[]
            {
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
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
            
            foreach (var widget in acts) _root.AddChild(widget);

            _column.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Space, Is.EqualTo(expects[i]));
            }
        }
    }
}