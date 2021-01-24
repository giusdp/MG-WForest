using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.Factories;
using WForest.UI.Props.Grid;
using WForest.UI.Props.Grid.JustifyProps;
using WForest.UI.Widgets.Interfaces;
using static WForest.Tests.Utils.HelperMethods;

namespace WForest.Tests.PropTests
{
    [TestFixture]
    public class CenterPropertyTests
    {
        private JustifyCenter _justifyCenter;
        private IWidget _root;
        private IWidget child;

        [SetUp]
        public void BeforeEach()
        {
            _justifyCenter = new JustifyCenter();
            _root = WidgetFactory.Container(new Rectangle(0, 0, 1280, 720));
            child = WidgetFactory.Container(new Rectangle(0, 0, 120, 120));
        }

        private void ApplyRow()
        {
            _root.WithProp(new Row());
            ApplyProps(_root);
        }

        private void ApplyCol()
        {
            _root.WithProp(new Column());
            ApplyProps(_root);
        }

        [Test]
        public void ApplyOn_NoChildren_NothingHappens()
        {
            Assert.That(() => _justifyCenter.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void ApplyOn_WidgetWithoutRowOrCol_NothingHappens()
        {
            _root.AddChild(child);
            _justifyCenter.ApplyOn(_root);

            var expected = new Rectangle(0, 0, 120, 120);

            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_OneChildInARow_PutsItInCenter()
        {
            _root.AddChild(child);
            ApplyRow();

            _justifyCenter.ApplyOn(_root);

            var expected = new Rectangle(580, 0, 120, 120);

            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_OneChildInAColumn_PutsItInCenter()
        {
            _root.AddChild(child);
            ApplyCol();

            _justifyCenter.ApplyOn(_root);

            var expected = new Rectangle(0, 300, 120, 120);

            Assert.That(child.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_TwoIdenticalWidgetsInColumn()
        {
            _root.AddChild(child);
            var secondChild = WidgetFactory.Container(new Rectangle(0, 0, 120, 120));
            _root.AddChild(secondChild);

            var firstChildExpectedLoc = new Rectangle(0, 240, 120, 120);
            var secondChildExpectedLoc = new Rectangle(0, 360, 120, 120);

            ApplyCol();
            _justifyCenter.ApplyOn(_root);

            Assert.That(child.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Space, Is.EqualTo(secondChildExpectedLoc));
        }

        [Test]
        public void ApplyOn_TwoIdenticalWidgetsInRow()
        {
            _root.AddChild(child);
            var secondChild = WidgetFactory.Container(new Rectangle(0, 0, 120, 120));
            _root.AddChild(secondChild);
            var firstChildExpectedLoc = new Rectangle(520, 0, 120, 120);
            var secondChildExpectedLoc = new Rectangle(640, 0, 120, 120);

            ApplyRow();
            _justifyCenter.ApplyOn(_root);

            Assert.That(child.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Space, Is.EqualTo(secondChildExpectedLoc));
        }

        [Test]
        public void ApplyOn_RowWithTwoSizesWidgets_CentersOnlyRowsNotInternally()
        {
            child.Space = new Rectangle(0, 0, 220, 120); 
            _root.AddChild(child);
            var secondChild = WidgetFactory.Container(new Rectangle(0, 0, 120, 330));
            _root.AddChild(secondChild);
            var firstChildExpectedLoc = new Rectangle(470, 0, 220, 120);
            var secondChildExpectedLoc = new Rectangle(690, 0, 120, 330);

            ApplyRow();
            _justifyCenter.ApplyOn(_root);
            Assert.That(child.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Space, Is.EqualTo(secondChildExpectedLoc));
        }

        [Test]
        public void ApplyOn_ColumnWithTwoSizesWidgets_CentersOnlyColumnsNotInternally()
        {
            _root.AddChild(child);
            var secondChild = WidgetFactory.Container(new Rectangle(0, 0, 220, 330));
            _root.AddChild(secondChild);
            var firstChildExpectedLoc = new Rectangle(0, 135, 120, 120);
            var secondChildExpectedLoc = new Rectangle(0, 255, 220, 330);

            ApplyCol();
            _justifyCenter.ApplyOn(_root);
            Assert.That(child.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Space, Is.EqualTo(secondChildExpectedLoc));
        }

        [Test]
        public void ApplyOn_RowsWidgetPassMaxWidth_OneWidgetOnBeginningOfSecondRow()
        {
            var acts = new[]
            {
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120))
            };

            var expects = new[]
            {
                new Rectangle(90, 0, 220, 120),
                new Rectangle(310, 0, 220, 120),
                new Rectangle(530, 0, 220, 120),
                new Rectangle(750, 0, 220, 120),
                new Rectangle(970, 0, 220, 120),
                new Rectangle(90, 120, 220, 120)
            };
            foreach (var widget in acts)
            {
                _root.AddChild(widget);
            }

            ApplyRow();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_ColumnWidgetPassMaxHeight_OneWidgetOnBeginningOfSecondColumn()
        {
            var acts = new[]
            {
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330))
            };

            var expects = new[]
            {
                new Rectangle(0, 30, 120, 330),
                new Rectangle(0, 360, 120, 330),
                new Rectangle(120, 30, 120, 330),
            };
            foreach (var widget in acts)
            {
                _root.AddChild(widget);
            }

            ApplyCol();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_ThreeRowsOfWidgets_CorrectlyCentered()
        {
            var acts = new[]
            {
                // First Row
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),

                // Second Row
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),

                // Third Row
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120))
            };

            var expects = new[]
            {
                new Rectangle(90, 0, 220, 120),
                new Rectangle(310, 0, 220, 120),
                new Rectangle(530, 0, 220, 120),
                new Rectangle(750, 0, 220, 120),
                new Rectangle(970, 0, 220, 120),
                new Rectangle(90, 120, 220, 120),
                new Rectangle(310, 120, 220, 120),
                new Rectangle(530, 120, 220, 120),
                new Rectangle(750, 120, 220, 120),
                new Rectangle(970, 120, 220, 120),
                new Rectangle(90, 240, 220, 120),
                new Rectangle(310, 240, 220, 120),
                new Rectangle(530, 240, 220, 120),
                new Rectangle(750, 240, 220, 120),
                new Rectangle(970, 240, 220, 120)
            };
            foreach (var widget in acts)
            {
                _root.AddChild(widget);
            }

            ApplyRow();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_ThreeColumnsOfWidgets_CorrectlyCentered()
        {
            var acts = new[]
            {
                // First Col
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),

                // Second Col
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),

                // Third Col
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
            };

            var expects = new[]
            {
                new Rectangle(0, 30, 120, 330),
                new Rectangle(0, 360, 120, 330),
                new Rectangle(120, 30, 120, 330),
                new Rectangle(120, 360, 120, 330),
                new Rectangle(240, 30, 120, 330),
                new Rectangle(240, 360, 120, 330),
            };
foreach (var widget in acts)
            {
                _root.AddChild(widget);
            }
            ApplyCol();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_ThreeRowsPlusOneWidget_TheLastOnBeginningOfRow()
        {
            var acts = new[]
            {
                // First Row
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),

                // Second Row
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),

                // Third Row
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),

                // The one more widget
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120))
            };

            var expects = new[]
            {
                new Rectangle(90, 0, 220, 120), // first row
                new Rectangle(310, 0, 220, 120),
                new Rectangle(530, 0, 220, 120),
                new Rectangle(750, 0, 220, 120),
                new Rectangle(970, 0, 220, 120),
                new Rectangle(90, 120, 220, 120), // second row
                new Rectangle(310, 120, 220, 120),
                new Rectangle(530, 120, 220, 120),
                new Rectangle(750, 120, 220, 120),
                new Rectangle(970, 120, 220, 120),
                new Rectangle(90, 240, 220, 120), // third row
                new Rectangle(310, 240, 220, 120),
                new Rectangle(530, 240, 220, 120),
                new Rectangle(750, 240, 220, 120),
                new Rectangle(970, 240, 220, 120),

                new Rectangle(90, 360, 220, 120) // new widget
            };
foreach (var widget in acts)
            {
                _root.AddChild(widget);
            }
            ApplyRow();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_ThreeColumnsPlusOneWidget_TheLastOnBeginningOfColumn()
        {
            var acts = new[]
            {
                // First Col
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),

                // Second Col
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),

                // Third Col
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),

                // The one more widget
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120))
            };

            var expects = new[]
            {
                new Rectangle(0, 30, 120, 330),
                new Rectangle(0, 360, 120, 330),
                new Rectangle(120, 30, 120, 330),
                new Rectangle(120, 360, 120, 330),
                new Rectangle(240, 30, 120, 330),
                new Rectangle(240, 360, 120, 330),
                new Rectangle(360, 30, 220, 120)
            };
foreach (var widget in acts)
            {
                _root.AddChild(widget);
            }
            ApplyCol();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_TwoRowsWithDifferentHeights()
        {
            var acts = new[]
            {
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
            };

            var expects = new[]
            {
                new Rectangle(30, 0, 220, 120),
                new Rectangle(250, 0, 120, 330),
                new Rectangle(370, 0, 220, 120),
                new Rectangle(590, 0, 220, 120),
                new Rectangle(810, 0, 220, 120),
                new Rectangle(1030, 0, 220, 120),
                new Rectangle(30, 330, 220, 120),
            };
foreach (var widget in acts)
            {
                _root.AddChild(widget);
            }
            ApplyRow();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_TwoColumnsWithDifferentWidths()
        {
            var acts = new[]
            {
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 120, 330)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),

                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
                WidgetFactory.Container(new Rectangle(0, 0, 220, 120)),
            };

            var expects = new[]
            {
                new Rectangle(0, 15, 220, 120),
                new Rectangle(0, 135, 120, 330),
                new Rectangle(0, 465, 220, 120),
                new Rectangle(0, 585, 220, 120),

                new Rectangle(220, 15, 220, 120),
                new Rectangle(220, 135, 220, 120),
                new Rectangle(220, 255, 220, 120),
            };
foreach (var widget in acts)
            {
                _root.AddChild(widget);
            }
            ApplyCol();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_RowWithColWithWidgets_TotallyCentered()
        {
            IWidget col = WidgetFactory.Container(200, 600);
            _root.AddChild(col);
            var innerChild = WidgetFactory.Container(100, 300);
            col.AddChild(innerChild);

            col.WithProp(PropertyFactory.Column());
            ApplyProps(col);
            ApplyRow();
            _justifyCenter.ApplyOn(_root);
            _justifyCenter.ApplyOn(col);


            Assert.That(col.Space, Is.EqualTo(new Rectangle(540, 0, 200, 600)));
            Assert.That(innerChild.Space, Is.EqualTo(new Rectangle(540, 150, 100, 300)));
        }
    }
}