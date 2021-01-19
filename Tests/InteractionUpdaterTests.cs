using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using WForest.Devices;
using WForest.Factories;
using WForest.UI.Widgets;
using WForest.UI.Widgets.Interactions;
using WForest.UI.WidgetTrees;
using WForest.Utilities;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace WForest.Tests
{
    [TestFixture]
    public class InteractionUpdaterTests
    {
        private WidgetTree _widgetTree;
        private WidgetInteractionUpdater _updater;

        [SetUp]
        public void BeforeEach()
        {
            _widgetTree = new WidgetTree(WidgetFactory.Container(new Rectangle(0, 0, 540, 260)));
            _updater = new WidgetInteractionUpdater(new Mock<IDevice>().Object);
        }

        [Test]
        public void IsHovered_LocationInside_ReturnsTrue()
        {
            var b = WidgetInteractionUpdater.GetHoveredWidget(_widgetTree,
                new Point(332, 43)) is Maybe<WidgetTree>.Some;
            Assert.That(b, Is.True);
        }

        [Test]
        public void IsHovered_NotInside_ReturnsFalse()
        {
            var b = WidgetInteractionUpdater.GetHoveredWidget(_widgetTree, new Point(332, 678)) is Maybe<WidgetTree>
                .Some;
            Assert.That(b, Is.False);
        }

        [Test]
        public void EnteringWidget_FirstTime_GoesIntoEnteredInteraction()
        {
            _updater.Update(Maybe.Some(_widgetTree));
            Assert.That(_widgetTree.Data.CurrentInteraction(), Is.EqualTo(Interaction.Entered));
        }

        [Test]
        public void HoveringNothing_WithNoPreviousWidget_DoesNothing()
        {
            Assert.That(() => _updater.Update(Maybe.None), Throws.Nothing);
        }

        [Test]
        public void HoveringAnotherWidget_WithPreviousWidgetEntered_RunsExitInteraction()
        {
            Mock<Widget> w = new Mock<Widget>(Rectangle.Empty);
            _updater.Update(Maybe.Some(new WidgetTree(w.Object)));
            _updater.Update(Maybe.Some(new WidgetTree(WidgetFactory.Container())));
            w.Verify(widget => widget.ChangeInteraction(Interaction.Exited), Times.Once);
        }

        [Test]
        public void HoveringAnotherWidget_WithAPreviousWidgetEntered_PreviousGoesUntouched()
        {
            _updater.Update(Maybe.Some(_widgetTree));
            _updater.Update(Maybe.Some(new WidgetTree(WidgetFactory.Container())));
            Assert.That(_widgetTree.Data.CurrentInteraction(), Is.EqualTo(Interaction.Untouched));
        }

        [Test]
        public void HoveringNothing_WithAPreviousWidget_PreviousRunsExit()
        {
            Mock<Widget> w = new Mock<Widget>(Rectangle.Empty);
            _updater.Update(Maybe.Some(new WidgetTree(w.Object)));
            _updater.Update(Maybe.None);
            w.Verify(widget => widget.ChangeInteraction(Interaction.Exited), Times.Once);
        }

        [Test]
        public void HoveringNothing_WithAPreviousWidget_PreviousGoesUntouched()
        {
            _updater.Update(Maybe.Some(_widgetTree));
            _updater.Update(Maybe.None);
            Assert.That(_widgetTree.Data.CurrentInteraction(), Is.EqualTo(Interaction.Untouched));
        }

        [Test]
        public void PressingOnHoveredWidget_RunsPressInteraction()
        {
            Mock<IDevice> mockedDevice = new Mock<IDevice>();
            Mock<Widget> mockedWidget = new Mock<Widget>(Rectangle.Empty);
            var wTree = new WidgetTree(mockedWidget.Object);
            
            // First enter the widget
            _updater.Update(Maybe.Some(wTree));
            // Then press the interaction button
            mockedDevice.Setup(device => device.IsPressed()).Returns(true);
            // Update to press widget
            _updater.Update(Maybe.Some(wTree));
            
            mockedWidget.Verify(w => w.ChangeInteraction(Interaction.Entered));
            mockedWidget.Verify(widget => widget.ChangeInteraction(Interaction.Pressed));
        }
    }
}