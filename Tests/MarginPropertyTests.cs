using Microsoft.Xna.Framework;
using NUnit.Framework;
using PiBa.UI;
using PiBa.UI.Factories;
using PiBa.UI.Properties.Margins;

namespace PiBa.Tests
{
    [TestFixture]
    public class MarginPropertyTests
    {
        private WidgetTree _root;
        [SetUp]
        public void BeforeEach()
        {
            _root = new WidgetTree(WidgetFactory.CreateContainer(new Rectangle(0, 0, 1280, 720)));
        }
        
        // [Test]
        // public void ApplyOn_TwoWidget_MarginSeparatesThem()
        // {
        //     var margin = new Margin(3);
        //     var widget = _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(Point.Zero, new Point(120, 120))));
        //     var secondWidget= _root.AddChild(WidgetFactory.CreateContainer(new Rectangle(Point.Zero, new Point(120, 120))));
        //     
        //     var expected = new Rectangle(3, 3, 120, 120);
        //     margin.ApplyOn(widget);
        //     
        //     Assert.That(secondWidget.Data.Space, Is.EqualTo(expected));
        // } 
    }
}