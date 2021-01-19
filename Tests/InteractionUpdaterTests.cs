using System;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using WForest.Factories;
using WForest.Tests.Utils;
using WForest.UI.Widgets;
using WForest.UI.Widgets.Interactions;
using WForest.UI.WidgetTrees;
using WForest.Utilities;

namespace WForest.Tests
{
    [TestFixture]
    public class InteractionUpdaterTests
    {
        private WidgetTree _widgetTree;
        private FakeDevice _fakeDevice;
        private WidgetInteractionUpdater _updater;

        [SetUp]
        public void BeforeEach()
        {
            _widgetTree = new WidgetTree(WidgetFactory.Container(new Rectangle(0, 0, 540, 260)));
            _fakeDevice = new FakeDevice();
            _updater = new WidgetInteractionUpdater(_fakeDevice);
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
        public void HoveringAnotherWidget_WithPreviousWidgetEntered_RunsExitInteraction()
        {
            Mock<Widget> w = new Mock<Widget>(Rectangle.Empty);
            _updater.Update(Maybe.Some(new WidgetTree(w.Object)));
            _updater.Update(Maybe.Some(new WidgetTree(WidgetFactory.Container())));
            w.Verify(widget => widget.ChangeInteraction(Interaction.Exited), Times.Once);
        }
    }
}