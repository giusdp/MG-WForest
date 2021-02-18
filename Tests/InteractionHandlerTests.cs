using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using WForest.Devices;
using WForest.Factories;
using WForest.Interactions;
using WForest.Utilities;
using WForest.Widgets;
using WForest.Widgets.Interfaces;

namespace Tests
{
    [TestFixture]
    public class InteractionHandlerTests
    {
        private IWidget _widgetTree;
        private UserInteractionHandler _handler;
        private Mock<IDevice> _device;

        private Mock<InteractionUpdater> _intRun;

        public InteractionHandlerTests()
        {
            _widgetTree = new Widget(new RectangleF(0, 0, 540, 260));
            _device = new Mock<IDevice>();
            _intRun = new Mock<InteractionUpdater>();
            _handler = new UserInteractionHandler(_device.Object, _intRun.Object);
        }

        [SetUp]
        public void BeforeEach()
        {
            _widgetTree = new Widget(new RectangleF(0, 0, 540, 260));
            _device = new Mock<IDevice>();
            _intRun = new Mock<InteractionUpdater>();
            _handler = new UserInteractionHandler(_device.Object, _intRun.Object);
        }

        [Test]
        public void GetHoveredWidget_LocationInside_ReturnsWidget()
        {
            var b = UserInteractionHandler.GetHoveredWidget(_widgetTree,
                new Vector2(332, 43)) is Maybe<IWidget>.Some;
            Assert.That(b, Is.True);
        }

        [Test]
        public void GetHoveredWidget_NotInside_ReturnsNone()
        {
            var b = UserInteractionHandler.GetHoveredWidget(_widgetTree, new Vector2(332, 678)) is Maybe<IWidget>.Some;
            Assert.That(b, Is.False);
        }

        [Test]
        public void GetHoveredWidget_HoverChildInWidget_ReturnsChild()
        {
            var child = new Widget(new RectangleF(0, 0, 50, 60));
            _widgetTree.AddChild(child);
            var hovered = UserInteractionHandler.GetHoveredWidget(_widgetTree, new Vector2(32, 8));
            var b = hovered.TryGetValue(out var value);
            Assert.That(b, Is.True);
            Assert.That(value, Is.EqualTo(child));
        }

        [Test]
        public void CurrentInteraction_OnStateChange_Changes()
        {
            _device.Setup(de => de.GetPointedLocation()).Returns(new Vector2(1, 4));
            _handler.Device = _device.Object;
            _handler.UpdateAndGenerateTransitions(_widgetTree);
            _intRun.Verify(r => r.NextState(_widgetTree, Interaction.Entered), Times.Once);
        }


        [Test]
        public void Enter_WidgetFirstTime_RunsEnteredInteraction()
        {
            _handler.UpdateAndGenerateTransitions(_widgetTree);
            _intRun.Verify(r => r.NextState(_widgetTree, Interaction.Entered), Times.Once);
        }

        [Test]
        public void EnterWidget_WithOldWidgetEntered_NewChangesToEnter()
        {
            _handler.UpdateAndGenerateTransitions(WidgetFactory.Container(1, 1));

            _handler.UpdateAndGenerateTransitions(_widgetTree);
            _intRun.Verify(r => r.NextState(_widgetTree, Interaction.Entered), Times.Once);
        }

//         var watch = System.Diagnostics.Stopwatch.StartNew();
// // the code that you want to measure comes here
// watch.Stop();
// var elapsedMs = watch.ElapsedMilliseconds;

        [Test]
        public void EnterAnotherWidget_WithOldWidgetEntered_RunsExitOnOld()
        {
            _device.Setup(d => d.GetPointedLocation()).Returns(Vector2.Zero);
            _handler.Device = _device.Object;

            var w = WidgetFactory.Container(1, 1);
            _handler.UpdateAndGenerateTransitions(w);
            _handler.UpdateAndGenerateTransitions(WidgetFactory.Container(1, 1));

            _intRun.Verify(r => r.NextState(w, Interaction.Exited), Times.Once);
        }

        [Test]
        public void EnterAnotherWidget_WithOldWidgetEntered_OldGoesUntouched()
        {
            _handler.UpdateAndGenerateTransitions(_widgetTree);
            _handler.UpdateAndGenerateTransitions(WidgetFactory.Container());
            Assert.That(_widgetTree.CurrentInteraction, Is.EqualTo(Interaction.Untouched));
        }

        [Test]
        public void HoveringNothing_WithOldWidget_OldRunsExit()
        {
            var w = WidgetFactory.Container(20, 20);
            _handler.UpdateAndGenerateTransitions(w);
            _device.Setup(d => d.GetPointedLocation()).Returns(new Vector2(-1, -1));
            _handler.UpdateAndGenerateTransitions(w);

            _intRun.Verify(r => r.NextState(w, Interaction.Exited), Times.Once);
        }

        [Test]
        public void Press_OnHoveredWidget_RunsPressInteraction()
        {
            // First enter the widget
            _handler.UpdateAndGenerateTransitions(_widgetTree);
            // Then press the interaction button
            _device.Setup(device => device.IsPressed()).Returns(true);
            _handler.Device = _device.Object;
            // Update to press widget
            _handler.UpdateAndGenerateTransitions(_widgetTree);

            _intRun.Verify(r => r.NextState(_widgetTree, Interaction.Pressed), Times.Once);
        }

        [Test]
        public void Press_OnNonEnteredWidget_EntersAndPresses()
        {
            // Press the interaction button
            _device.Setup(device => device.IsPressed()).Returns(true);
            _handler.Device = _device.Object;
            // Update to press widget
            _handler.UpdateAndGenerateTransitions(_widgetTree);

            _intRun.Verify(r => r.NextState(_widgetTree, Interaction.Pressed), Times.Once);
        }

        [Test]
        public void Release_OnSamePressedWidget_RunsReleaseInteraction()
        {
            // First enter the widget
            _handler.UpdateAndGenerateTransitions(_widgetTree);
            // Then press the interaction button
            _device.Setup(device => device.IsPressed()).Returns(true);
            _handler.Device = _device.Object;
            // Update to press widget
            _handler.UpdateAndGenerateTransitions(_widgetTree);
            //Then release the button
            _device.Setup(device => device.IsPressed()).Returns(false);
            _device.Setup(device => device.IsReleased()).Returns(true);
            _handler.UpdateAndGenerateTransitions(_widgetTree);

            _intRun.Verify(r => r.NextState(_widgetTree, Interaction.Released), Times.Once);
        }

        [Test]
        public void PressOnWidget_ThenEnterAnother_OldDoesNotRelease()
        {
            // Press the interaction button
            _device.Setup(device => device.IsPressed()).Returns(true);
            _handler.Device = _device.Object;
            // Press widget
            _handler.UpdateAndGenerateTransitions(_widgetTree);

            _device.Setup(device => device.IsPressed()).Returns(false);
            _device.Setup(device => device.IsHeld()).Returns(true);
            _handler.Device = _device.Object;

            // Move while pressing to another widget
            _handler.UpdateAndGenerateTransitions(WidgetFactory.Container());

            _intRun.Verify(r => r.NextState(_widgetTree, Interaction.Released), Times.Never);
        }

        #region Integration with InteractionUpdater

        [Test]
        public void HoveringNothing_WithAPreviousWidget_PreviousGoesUntouched()
        {
            _handler.UpdateAndGenerateTransitions(_widgetTree);
            _handler.UpdateAndGenerateTransitions(WidgetFactory.Container());
            Assert.That(_widgetTree.CurrentInteraction, Is.EqualTo(Interaction.Untouched));
        }

        [Test]
        public void PressOnWidget_ThenEnterAnother_OldStaysPressed()
        {
            // Press the interaction button
            _device.Setup(device => device.IsPressed()).Returns(true);
            _handler.Device = _device.Object;
            _handler.Updater = new InteractionUpdater();

            // Press widget
            _handler.UpdateAndGenerateTransitions(_widgetTree);

            _device.Setup(device => device.IsPressed()).Returns(false);
            _device.Setup(device => device.IsHeld()).Returns(true);
            _handler.Device = _device.Object;
            var w = WidgetFactory.Container();
            // Move while pressing to another widget
            _handler.UpdateAndGenerateTransitions(w);

            Assert.That(_widgetTree.CurrentInteraction, Is.EqualTo(Interaction.Pressed));
        }

        [Test]
        public void PressOnWidget_ThenEnterAnother_OldDoesGenerateOnExitCommands()
        {
            // Press the interaction button
            _device.Setup(device => device.IsPressed()).Returns(true);
            _handler.Device = _device.Object;
            _handler.Updater = new InteractionUpdater();
            // Press widget
            _handler.UpdateAndGenerateTransitions(_widgetTree);

            _device.Setup(device => device.IsPressed()).Returns(false);
            _device.Setup(device => device.IsHeld()).Returns(true);
            _handler.Device = _device.Object;

            // Move while pressing to another widget
            var l = _handler.UpdateAndGenerateTransitions(WidgetFactory.Container());

            Assert.That(l, Is.Empty);
        }

        [Test]
        public void PressOnWidget_ThenEnterAnother_NewDoesNotGetPressed()
        {
            // Press the interaction button
            _device.Setup(device => device.IsPressed()).Returns(true);
            _handler.Device = _device.Object;
            _handler.Updater = new InteractionUpdater();
            // Press widget
            _handler.UpdateAndGenerateTransitions(_widgetTree);

            var w = WidgetFactory.Container(30, 30);
            // Move while pressing to another widget
            _handler.UpdateAndGenerateTransitions(w);

            Assert.That(w.CurrentInteraction, Is.Not.EqualTo(Interaction.Pressed));
        }

        [Test]
        public void PressOnWidget_ThenEnterAnotherAndRelease_NoReleaseCommandsAreGenerated()
        {
            var thenEntered = WidgetFactory.Container(30, 30);


            _device.Setup(device => device.IsPressed()).Returns(true);
            _handler.Device = _device.Object;
            _handler.Updater = new InteractionUpdater();

            // Press firstPressed
            _handler.UpdateAndGenerateTransitions(_widgetTree);

            // Move to thenEntered while pressing
            _handler.UpdateAndGenerateTransitions(thenEntered);

            _device.Setup(device => device.IsPressed()).Returns(false);
            _device.Setup(d => d.IsReleased()).Returns(true);
            _handler.Device = _device.Object;

            var l = _handler.UpdateAndGenerateTransitions(thenEntered);

            Assert.That(l, Is.Empty);
        }

        #endregion
    }
}