using Microsoft.Xna.Framework;
using NUnit.Framework;
using PiBa.UI;
using PiBa.UI.Factories;
using PiBa.UI.Properties.Grid.Center;
using PiBa.UI.Properties.Grid.Column;
using PiBa.UI.Properties.Grid.Row;

namespace PiBa.Tests.PropertyTests
{
    [TestFixture]
    public class CenterPropertyTests
    {
        private Center _center;
        private WidgetTree _root;

        [SetUp]
        public void BeforeEach()
        {
            _center = new Center();
            _root = new WidgetTree(Widgets.CreateContainer(new Rectangle(0, 0, 1280, 720)));
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
            Assert.That(() => _center.ApplyOn(_root), Throws.Nothing);
        }

        [Test]
        public void ApplyOn_WidgetWithoutRowOrCol_NothingHappens()
        {
           var child = _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 120)));

            _center.ApplyOn(_root);

            var expected = new Rectangle(0, 0, 120, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected)); 
        }
        
        [Test]
        public void ApplyOn_OneChildrenInARow_PutsItInCenter()
        {
            var child = _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 120)));

            ApplyRow();

            _center.ApplyOn(_root);

            var expected = new Rectangle(580, 300, 120, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }
        
        [Test]
        public void ApplyOn_OneChildrenInAColumn_PutsItInCenter()
        {
            var child = _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 120)));

            ApplyCol();

            _center.ApplyOn(_root);

            var expected = new Rectangle(580, 300, 120, 120);

            Assert.That(child.Data.Space, Is.EqualTo(expected));
        }

        [Test]
        public void ApplyOn_TwoIdenticalWidgetsInColumn()
        {
            var child = _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 120)));
            var secondChild = _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 120)));
        
            var firstChildExpectedLoc = new Rectangle(580, 240, 120, 120);
            var secondChildExpectedLoc = new Rectangle(580, 360, 120, 120);
        
            ApplyCol();
            _center.ApplyOn(_root);
            
            Assert.That(child.Data.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Data.Space, Is.EqualTo(secondChildExpectedLoc));
        }
        
        [Test]
        public void ApplyOn_TwoIdenticalWidgetsInRow()
        {
            var child = _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 120)));
            var secondChild = _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 120)));

            var firstChildExpectedLoc = new Rectangle(520, 300, 120, 120);
            var secondChildExpectedLoc = new Rectangle(640, 300, 120, 120);

            ApplyRow();
            _center.ApplyOn(_root);
            
            Assert.That(child.Data.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Data.Space, Is.EqualTo(secondChildExpectedLoc));
        }

        [Test]
        public void ApplyOn_RowWithTwoSizesWidgets_CentersOnlyRowsNotInternally()
        {
            var child = _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var secondChild = _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330)));
        
            var firstChildExpectedLoc = new Rectangle(470, 195, 220, 120);
            var secondChildExpectedLoc = new Rectangle(690, 195, 120, 330);
        
            ApplyRow();
            _center.ApplyOn(_root);
            Assert.That(child.Data.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Data.Space, Is.EqualTo(secondChildExpectedLoc));
        }
        
        [Test]
        public void ApplyOn_ColumnWithTwoSizesWidgets_CentersOnlyColumnsNotInternally()
        {
            var child = _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120)));
            var secondChild = _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330)));
        
            var firstChildExpectedLoc = new Rectangle(530, 135, 220, 120);
            var secondChildExpectedLoc = new Rectangle(530, 255, 120, 330);
        
            ApplyCol();
            _center.ApplyOn(_root);
            Assert.That(child.Data.Space, Is.EqualTo(firstChildExpectedLoc));
            Assert.That(secondChild.Data.Space, Is.EqualTo(secondChildExpectedLoc));
        }
        
        [Test]
        public void ApplyOn_RowsWidgetPassMaxWidth_OneWidgetOnBeginningOfSecondRow()
        {
            var acts = new[]
            {
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120)))
            };
        
            var expects = new[]
            {
                new Rectangle(90, 240, 220, 120),
                new Rectangle(310, 240, 220, 120),
                new Rectangle(530, 240, 220, 120),
                new Rectangle(750, 240, 220, 120),
                new Rectangle(970, 240, 220, 120),
                new Rectangle(90, 360, 220, 120)
            };
        
            ApplyRow();
            _center.ApplyOn(_root);
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
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330)))
            };
        
            var expects = new[]
            {
                new Rectangle(520, 30, 120, 330),
                new Rectangle(520, 360, 120, 330),
                new Rectangle(640, 30, 120, 330),
            };
        
            ApplyCol();
            _center.ApplyOn(_root);
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
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
        
                // Second Row
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
        
                // Third Row
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120)))
            };
        
            var expects = new[]
            {
                new Rectangle(90, 180, 220, 120),
                new Rectangle(310, 180, 220, 120),
                new Rectangle(530, 180, 220, 120),
                new Rectangle(750, 180, 220, 120),
                new Rectangle(970, 180, 220, 120),
                new Rectangle(90, 300, 220, 120),
                new Rectangle(310, 300, 220, 120),
                new Rectangle(530, 300, 220, 120),
                new Rectangle(750, 300, 220, 120),
                new Rectangle(970, 300, 220, 120),
                new Rectangle(90, 420, 220, 120),
                new Rectangle(310, 420, 220, 120),
                new Rectangle(530, 420, 220, 120),
                new Rectangle(750, 420, 220, 120),
                new Rectangle(970, 420, 220, 120)
            };
        
            ApplyRow();
            _center.ApplyOn(_root);
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
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
        
                // Second Col
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
        
                // Third Col
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
            };
        
            var expects = new[]
            {
                new Rectangle(460, 30, 120, 330),
                new Rectangle(460, 360, 120, 330),
                new Rectangle(580, 30, 120, 330),
                new Rectangle(580, 360, 120, 330),
                new Rectangle(700, 30, 120, 330),
                new Rectangle(700, 360, 120, 330),
            };
        
            ApplyCol();
            _center.ApplyOn(_root);
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
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
        
                // Second Row
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
        
                // Third Row
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
        
                // The one more widget
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120)))
            };
        
            var expects = new[]
            {
                new Rectangle(90, 120, 220, 120), // first row
                new Rectangle(310, 120, 220, 120),
                new Rectangle(530, 120, 220, 120),
                new Rectangle(750, 120, 220, 120),
                new Rectangle(970, 120, 220, 120),
                new Rectangle(90, 240, 220, 120), // second row
                new Rectangle(310, 240, 220, 120),
                new Rectangle(530, 240, 220, 120),
                new Rectangle(750, 240, 220, 120),
                new Rectangle(970, 240, 220, 120),
                new Rectangle(90, 360, 220, 120), // third row
                new Rectangle(310, 360, 220, 120),
                new Rectangle(530, 360, 220, 120),
                new Rectangle(750, 360, 220, 120),
                new Rectangle(970, 360, 220, 120),
        
                new Rectangle(90, 480, 220, 120) // new widget
            };
        
            ApplyRow();
            _center.ApplyOn(_root);
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
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
        
                // Second Col
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
        
                // Third Col
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
                
                // The one more widget
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120)))
            };

            var expects = new[]
            {
                new Rectangle(350, 30, 120, 330),
                new Rectangle(350, 360, 120, 330),
                new Rectangle(470, 30, 120, 330),
                new Rectangle(470, 360, 120, 330),
                new Rectangle(590, 30, 120, 330),
                new Rectangle(590, 360, 120, 330),
                new Rectangle(710, 30, 220, 120)
            };
        
            ApplyCol();
            _center.ApplyOn(_root);
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
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
            };
        
            var expects = new[]
            {
                new Rectangle(30, 135, 220, 120),
                new Rectangle(250, 135, 120, 330),
                new Rectangle(370, 135, 220, 120),
                new Rectangle(590, 135, 220, 120),
                new Rectangle(810, 135, 220, 120),
                new Rectangle(1030, 135,  220, 120),
                new Rectangle(30, 465, 220, 120),
            };
            
            ApplyRow();
            _center.ApplyOn(_root);
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
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 120, 330))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
                _root.AddChild(Widgets.CreateContainer(new Rectangle(0, 0, 220, 120))),
            };
        
            var expects = new[]
            {
                new Rectangle(420, 15, 220, 120),
                new Rectangle(420, 135, 120, 330),
                new Rectangle(420, 465, 220, 120),
                new Rectangle(420, 585, 220, 120),
                
                new Rectangle(640, 15, 220, 120),
                new Rectangle(640, 135,  220, 120),
                new Rectangle(640, 255, 220, 120),
            };
            
            ApplyCol();
            _center.ApplyOn(_root);
            for (var i = 0; i < acts.Length; i++)
            {
                Assert.That(acts[i].Data.Space, Is.EqualTo(expects[i]));
            }
        }
    }
}