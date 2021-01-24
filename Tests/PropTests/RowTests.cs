using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Factories;
using WForest.UI.Props.Grid;
using WForest.UI.Widgets;

namespace WForest.Tests.PropTests
{
    [TestFixture]
    public class RowPropertyTests
    {
        private Row _row;
        private IWidget _root;

        [SetUp]
        public void BeforeEach()
        {
            _row = new Row();
            _root = WidgetFactory.Container(new Rectangle(0, 0, 1280, 720));
        }

        [Test]
        public void ApplyOn_NoChildren_NothingHappens()
        {
            Assert.That(() => _row.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void ApplyOn_OneWidget_PositionIsUnchanged()
        {
            IWidget child = WidgetFactory.Container(new Rectangle(0, 0, 120, 120));
            _root.AddChild(child);

            var expected = new Rectangle(0, 0, 120, 120);
            _row.ApplyOn(_root);
            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_TwoWidgets_GoSideBySide()
        {
            var child = WidgetFactory.Container(new Rectangle(0, 0, 120, 120));
            var secondChild = WidgetFactory.Container(new Rectangle(0, 0, 120, 120));
            _root.AddChild(child);
            _root.AddChild(secondChild);

            var expectedLocFirst = new Rectangle(0, 0, 120, 120);
            var expectedLocSecond = new Rectangle(120, 0, 120, 120);

            _row.ApplyOn(_root);
            Assert.That(child.Space, Is.EqualTo(expectedLocFirst));
            Assert.That(secondChild.Space, Is.EqualTo(expectedLocSecond));
        }

        [Test]
        public void ApplyOn_WidgetsPassHorizontalLimit_GoOnNewRow()
        {
            var acts = new[]
            {
                WidgetFactory.Container(new Rectangle(0, 0, 640, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 320, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 160, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 80, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 128, 64)),
            };

            var expects = new[]
            {
                new Rectangle(0, 0, 640, 120),
                new Rectangle(640, 0, 320, 120),
                new Rectangle(960, 0, 160, 120),
                new Rectangle(1120, 0, 80, 120),
                new Rectangle(0, 120, 128, 64),
            };

            foreach (var widget in acts)
            {
                _root.AddChild(widget);
            }

            _row.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_WidgetsWithDifferentHeights_RowGetsMaxHeight()
        {
            var acts = new[]
            {
                WidgetFactory.Container(new Rectangle(0, 0, 640, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 600, 320)),
                WidgetFactory.Container(new Rectangle(0, 0, 128, 64)),
            };

            var expects = new[]
            {
                new Rectangle(0, 0, 640, 120),
                new Rectangle(640, 0, 600, 320),
                new Rectangle(0, 320, 128, 64),
            };

            foreach (var widget in acts)
            {
                _root.AddChild(widget);
            }

            _row.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Space, Is.EqualTo(expects[i]));
            }
        }
    }
}