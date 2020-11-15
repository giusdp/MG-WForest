using Microsoft.Xna.Framework;
using NUnit.Framework;
using PiBa.UI;
using PiBa.UI.Factories;
using PiBa.UI.Properties.Grid.Column;

namespace PiBa.Tests.PropertyTests
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
            _root = new WidgetTree(WidgetFactory.CreateContainer(new Rectangle(0, 0, 1280, 720)));
        }

        [Test]
        public void ApplyOn_NoChildren_NothingHappens()
        {
            Assert.That(() => _column.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void ApplyOn_OneWidget_PositionIsUnchanged()
        {
            var child = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 120, 120)));
        
            var expected = new Rectangle(0, 0, 120, 120);
            _column.ApplyOn(_root);
            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }
        
        [Test]
        public void ApplyOn_TwoWidgets_GoSideBySide()
        {
            var child = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 120, 120)));
            var secondChild = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 120, 120)));
        
            var expectedLocFirst = new Rectangle(0, 0, 120, 120);
            var expectedLocSecond = new Rectangle(0, 120, 120, 120);
        
            _column.ApplyOn(_root);
            Assert.That(child.Data.Space, Is.EqualTo(expectedLocFirst));
            Assert.That(secondChild.Data.Space, Is.EqualTo(expectedLocSecond));
        }
        
        //     [Test]
        //     public void ApplyOn_WidgetsPassHorizontalLimit_GoOnNewRow()
        //     {
        //         var acts = new[]
        //         {
        //             _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 640, 120))),
        //             _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 320, 120))),
        //             _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 160, 120))),
        //             _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 80, 120))),
        //             _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 128, 64))),
        //         };
        //
        //         var expects = new[]
        //         {
        //             new Rectangle(0, 0, 640, 120),
        //             new Rectangle(640, 0, 320, 120),
        //             new Rectangle(960, 0, 160, 120),
        //             new Rectangle(1120, 0, 80, 120),
        //             new Rectangle(0, 120, 128, 64),
        //         };
        //
        //         _column.ApplyOn(_root);
        //         for (var i = 0; i < acts.Length; i++)
        //         {
        //             Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
        //         }
        //     }
        //     [Test]
        //     public void ApplyOn_WidgetsWithDifferentHeights_RowGetsMaxHeight()
        //     {
        //         var acts = new[]
        //         {
        //             _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 640, 120))),
        //             _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 600, 320))),
        //             _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(0, 0, 128, 64))),
        //         };
        //
        //         var expects = new[]
        //         {
        //             new Rectangle(0, 0, 640, 120),
        //             new Rectangle(640, 0, 600, 320),
        //             new Rectangle(0, 320, 128, 64),
        //         };
        //
        //         _column.ApplyOn(_root);
        //         for (var i = 0; i < acts.Length; i++)
        //         {
        //             Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
        //         }
        //     }
    }
}