using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI;
using WForest.UI.Factories;
using WForest.UI.Properties.Grid.Column;
using WForest.UI.Properties.Grid.JustifyProps;
using WForest.UI.Properties.Grid.Row;

namespace WForest.Tests.PropertyTests
{
    [TestFixture]
    public class CenterPropertyTests
    {
        private JustifyCenter _justifyCenter;
        private WidgetTree _root;

        [SetUp]
        public void BeforeEach()
        {
            _justifyCenter = new JustifyCenter();
            _root = new WidgetTree(Widgets.Container(new Rectangle(0, 0, 1280, 720)));
        }

        private void ApplyRow()
        {
            _root.AddProperty(new Row());
            _root.ApplyProperties();
        }

        private void ApplyCol()
        {
            _root.AddProperty(new Column());
            _root.ApplyProperties();
        }

        [Test]
        public void ApplyOn_NoChildren_NothingHappens()
        {
            Assert.That(() => _justifyCenter.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void ApplyOn_WidgetWithoutRowOrCol_NothingHappens()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));

            _justifyCenter.ApplyOn(_root);

            var expected = new Rectangle(0, 0, 120, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_OneChildInARow_PutsItInCenter()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));

            ApplyRow();

            _justifyCenter.ApplyOn(_root);

            var expected = new Rectangle(580, 0, 120, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_OneChildInAColumn_PutsItInCenter()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));

            ApplyCol();

            _justifyCenter.ApplyOn(_root);

            var expected = new Rectangle(0, 300, 120, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_TwoIdenticalWidgetsInColumn()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));
            var secondChild = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));

            var firstChildExpectedLoc = new Rectangle(0, 240, 120, 120);
            var secondChildExpectedLoc = new Rectangle(0, 360, 120, 120);

            ApplyCol();
            _justifyCenter.ApplyOn(_root);

            Assert.That(child.Data.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Data.Space, Is.EqualTo(secondChildExpectedLoc));
        }

        [Test]
        public void ApplyOn_TwoIdenticalWidgetsInRow()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));
            var secondChild = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 120)));

            var firstChildExpectedLoc = new Rectangle(520, 0, 120, 120);
            var secondChildExpectedLoc = new Rectangle(640, 0, 120, 120);

            ApplyRow();
            _justifyCenter.ApplyOn(_root);

            Assert.That(child.Data.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Data.Space, Is.EqualTo(secondChildExpectedLoc));
        }

        [Test]
        public void ApplyOn_RowWithTwoSizesWidgets_CentersOnlyRowsNotInternally()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120)));
            var secondChild = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330)));

            var firstChildExpectedLoc = new Rectangle(470, 0, 220, 120);
            var secondChildExpectedLoc = new Rectangle(690, 0, 120, 330);

            ApplyRow();
            _justifyCenter.ApplyOn(_root);
            Assert.That(child.Data.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Data.Space, Is.EqualTo(secondChildExpectedLoc));
        }

        [Test]
        public void ApplyOn_ColumnWithTwoSizesWidgets_CentersOnlyColumnsNotInternally()
        {
            var child = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120)));
            var secondChild = _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330)));

            var firstChildExpectedLoc = new Rectangle(0, 135, 220, 120);
            var secondChildExpectedLoc = new Rectangle(0, 255, 120, 330);

            ApplyCol();
            _justifyCenter.ApplyOn(_root);
            Assert.That(child.Data.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Data.Space, Is.EqualTo(secondChildExpectedLoc));
        }

        [Test]
        public void ApplyOn_RowsWidgetPassMaxWidth_OneWidgetOnBeginningOfSecondRow()
        {
            var acts = new[]
            {
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120)))
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

            ApplyRow();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_ColumnWidgetPassMaxHeight_OneWidgetOnBeginningOfSecondColumn()
        {
            var acts = new[]
            {
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330)))
            };

            var expects = new[]
            {
                new Rectangle(0, 30, 120, 330),
                new Rectangle(0, 360, 120, 330),
                new Rectangle(120, 30, 120, 330),
            };

            ApplyCol();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_ThreeRowsOfWidgets_CorrectlyCentered()
        {
            var acts = new[]
            {
                // First Row
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),

                // Second Row
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),

                // Third Row
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120)))
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

            ApplyRow();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_ThreeColumnsOfWidgets_CorrectlyCentered()
        {
            var acts = new[]
            {
                // First Col
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),

                // Second Col
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),

                // Third Col
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
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

            ApplyCol();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_ThreeRowsPlusOneWidget_TheLastOnBeginningOfRow()
        {
            var acts = new[]
            {
                // First Row
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),

                // Second Row
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),

                // Third Row
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),

                // The one more widget
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120)))
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

            ApplyRow();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_ThreeColumnsPlusOneWidget_TheLastOnBeginningOfColumn()
        {
            var acts = new[]
            {
                // First Col
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),

                // Second Col
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),

                // Third Col
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),

                // The one more widget
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120)))
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

            ApplyCol();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_TwoRowsWithDifferentHeights()
        {
            var acts = new[]
            {
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
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

            ApplyRow();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_TwoColumnsWithDifferentWidths()
        {
            var acts = new[]
            {
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),

                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.Container(new Rectangle(0, 0, 220, 120))),
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

            ApplyCol();
            _justifyCenter.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }

        [Test]
        public void ApplyOn_RowWithColWithWidgets_TotallyCentered()
        {
            var col = _root.AddChild(Widgets.Container(200, 600));
            var innerChild = col.AddChild(Widgets.Container(100, 300));

            col.AddProperty(Properties.Column());
            col.ApplyProperties();
            ApplyRow();
            _justifyCenter.ApplyOn(_root);
            _justifyCenter.ApplyOn(col);


            Assert.That(col.Data.Space, Is.EqualTo(new Rectangle(540,0,200,600)));
            Assert.That(innerChild.Data.Space, Is.EqualTo(new Rectangle(540,150,100,300)));
        }
    }
}