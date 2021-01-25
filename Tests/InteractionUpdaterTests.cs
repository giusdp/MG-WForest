using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using WForest.Devices;
using WForest.Factories;
using WForest.UI;
using WForest.UI.Interactions;
using WForest.UI.Widgets;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

namespace WForest.Tests
{
    [TestFixture]
    public class InteractionUpdaterTests
    {
        private IWidget _widgetTree;
        private InteractionUpdater _updater;
        private Mock<IDevice> _device;

        private Mock<InteractionRunner> intRun;

        [SetUp]
        public void BeforeEach()
        {
            _widgetTree = new Widget(new Rectangle(0, 0, 540, 260));
            _device = new Mock<IDevice>();
            intRun = new Mock<InteractionRunner>();
            _updater = new InteractionUpdater(_device.Object, intRun.Object);
        }

        [Test]
        public void GetHoveredWidget_LocationInside_ReturnsWidget()
        {
            var b = InteractionUpdater.GetHoveredWidget(_widgetTree,
                new Point(332, 43)) is Maybe<IWidget>.Some;
            Assert.That(b, Is.True);
        }

        [Test]
        public void GetHoveredWidget_NotInside_ReturnsNone()
        {
            var b = InteractionUpdater.GetHoveredWidget(_widgetTree, new Point(332, 678)) is Maybe<IWidget>.Some;
            Assert.That(b, Is.False);
        }

        [Test]
        public void GetHoveredWidget_HoverChildInWidget_ReturnsChild()
        {
            var child = new Widget(new Rectangle(0, 0, 50, 60));
            _widgetTree.AddChild(child);
            var hovered = InteractionUpdater.GetHoveredWidget(_widgetTree, new Point(32, 8));
            var b = hovered.TryGetValue(out var value);
            Assert.That(b, Is.True);
            Assert.That(value, Is.EqualTo(child));
        }

        [Test]
        public void CurrentInteraction_OnStateChange_Changes()
        {
            _device.Setup(de => de.GetPointedLocation()).Returns(new Point(1, 4));
            _updater.Device = _device.Object;
            _updater.Update(_widgetTree);
            intRun.Verify(r => r.ChangeState(_widgetTree, Interaction.Entered), Times.Once);
        }


        [Test]
        public void Enter_WidgetFirstTime_RunsEnteredInteraction()
        {
            _updater.Update(_widgetTree);
            intRun.Verify(r => r.ChangeState(_widgetTree, Interaction.Entered), Times.Once);
        }

        [Test]
        public void EnterWidget_WithOldWidgetEntered_NewChangesToEnter()
        {
            _updater.Update(WidgetFactory.Container(1, 1));

            _updater.Update(_widgetTree);
            intRun.Verify(r => r.ChangeState(_widgetTree, Interaction.Entered), Times.Once);
        }

//         var watch = System.Diagnostics.Stopwatch.StartNew();
// // the code that you want to measure comes here
// watch.Stop();
// var elapsedMs = watch.ElapsedMilliseconds;

        [Test]
        public void EnterAnotherWidget_WithOldWidgetEntered_RunsExitOnOld()
        {
            _device.Setup(d => d.GetPointedLocation()).Returns(Point.Zero);
            _updater.Device = _device.Object;

            var w = WidgetFactory.Container(1, 1);
            _updater.Update(w);
            _updater.Update(WidgetFactory.Container(1, 1));

            intRun.Verify(r => r.ChangeState(w, Interaction.Exited), Times.Once);
        }

        [Test]
        public void EnterAnotherWidget_WithOldWidgetEntered_OldGoesUntouched()
        {
            _updater.Update(_widgetTree);
            _updater.Update(WidgetFactory.Container());
            Assert.That(_widgetTree.CurrentInteraction, Is.EqualTo(Interaction.Untouched));
        }

        [Test]
        public void HoveringNothing_WithOldWidget_OldRunsExit()
        {
            var w = WidgetFactory.Container(20, 20);
            _updater.Update(w);
            _device.Setup(d => d.GetPointedLocation()).Returns(new Point(-1, -1));
            _updater.Update(w);

            intRun.Verify(r => r.ChangeState(w, Interaction.Exited), Times.Once);
        }

        [Test]
        public void Press_OnHoveredWidget_RunsPressInteraction()
        {
            // First enter the widget
            _updater.Update(_widgetTree);
            // Then press the interaction button
            _device.Setup(device => device.IsPressed()).Returns(true);
            _updater.Device = _device.Object;
            // Update to press widget
            _updater.Update(_widgetTree);

            intRun.Verify(r => r.ChangeState(_widgetTree, Interaction.Pressed), Times.Once);
        }

        [Test]
        public void Press_OnNonEnteredWidget_EntersAndPresses()
        {
            // Press the interaction button
            _device.Setup(device => device.IsPressed()).Returns(true);
            _updater.Device = _device.Object;
            // Update to press widget
            _updater.Update(_widgetTree);

            intRun.Verify(r => r.ChangeState(_widgetTree, Interaction.Pressed), Times.Once);
        }

        [Test]
        public void Release_OnSamePressedWidget_RunsReleaseInteraction()
        {
            // First enter the widget
            _updater.Update(_widgetTree);
            // Then press the interaction button
            _device.Setup(device => device.IsPressed()).Returns(true);
            _updater.Device = _device.Object;
            // Update to press widget
            _updater.Update(_widgetTree);
            //Then release the button
            _device.Setup(device => device.IsPressed()).Returns(false);
            _device.Setup(device => device.IsReleased()).Returns(true);
            _updater.Update(_widgetTree);

            intRun.Verify(r => r.ChangeState(_widgetTree, Interaction.Released), Times.Once);
        }

        [Test]
        public void PressOnWidget_ThenEnterAnother_OldDoesNotRelease()
        {
            // Press the interaction button
            _device.Setup(device => device.IsPressed()).Returns(true);
            _updater.Device = _device.Object;
            // Press widget
            _updater.Update(_widgetTree);

            _device.Setup(device => device.IsPressed()).Returns(false);
            _device.Setup(device => device.IsHeld()).Returns(true);
            _updater.Device = _device.Object;

            // Move while pressing to another widget
            _updater.Update(WidgetFactory.Container());

            intRun.Verify(r => r.ChangeState(_widgetTree, Interaction.Released), Times.Never);
        }

        #region Integration with InteractionRunner

// [Test]
        // public void HoveringNothing_WithAPreviousWidget_PreviousGoesUntouched()
        // {
        //     _updater.Update(Maybe.Some(_widgetTree));
        //     _updater.Update(Maybe.None);
        //     Assert.That(_widgetTree.CurrentInteraction, Is.EqualTo(Interaction.Untouched));
        // }
        // [Test]
        // public void PressOnWidget_ThenEnterAnother_OldStaysPressed()
        // {
        //     // Press the interaction button
        //     _device.Setup(device => device.IsPressed()).Returns(true);
        //     _updater.Device = _device.Object;
        //     // Press widget
        //     _updater.Update(_widgetTree);
        //
        //     _device.Setup(device => device.IsPressed()).Returns(false);
        //     _device.Setup(device => device.IsHeld()).Returns(true);
        //     _updater.Device = _device.Object;
        //     var w = WidgetFactory.Container();
        //     // Move while pressing to another widget
        //     _updater.Update(w);
        //
        //     Assert.That(_widgetTree.CurrentInteraction(), Is.EqualTo(Interaction.Pressed));
        // }

        // [Test]
        // public void PressOnWidget_ThenEnterAnother_OldDoesNotExit()
        // {
        //     // Press the interaction button
        //     _device.Setup(device => device.IsPressed()).Returns(true);
        //     _updater.Device = _device.Object;
        //     // Press widget
        //     _updater.Update(_widgetTree);
        //
        //     _device.Setup(device => device.IsPressed()).Returns(false);
        //     _device.Setup(device => device.IsHeld()).Returns(true);
        //     _updater.Device = _device.Object;
        //
        //     // Move while pressing to another widget
        //     _updater.Update(WidgetFactory.Container());
        //
        //     intRun.Verify(r=> r.ChangeState(_widgetTree, Interaction.Exited), Times.Never);
        // }
        //
        //     [Test]
        //     public void PressOnWidget_ThenEnterAnother_NewDoesNotGetPressed()
        //     {
        //         Mock<IDevice> mockedDevice = new Mock<IDevice>();
        //         Mock<Widget> mockedWidget = new Mock<Widget>(Rectangle.Empty);
        //         mockedWidget.Setup(w => w.CurrentInteraction()).Returns(Interaction.Pressed);
        //         var wTree = new W(mockedWidget.Object);
        //
        //         // Press the interaction button
        //         mockedDevice.Setup(device => device.IsPressed()).Returns(true);
        //         _updater.Device = mockedDevice.Object;
        //         // Press widget
        //         _updater.Update(Maybe.Some(wTree));
        //
        //         // Move while pressing to another widget
        //         _updater.Update(Maybe.Some(_widgetTree));
        //
        //         Assert.That(_widgetTree..CurrentInteraction(), Is.Not.EqualTo(Interaction.Pressed));
        //     }
        //
        //     [Test]
        //     public void PressOnWidget_ThenEnterAnotherAndRelease_NoRelease()
        //     {
        //         Mock<IDevice> mockedDevice = new Mock<IDevice>();
        //         Mock<Widget> firstPressed = new Mock<Widget>(Rectangle.Empty);
        //         Mock<Widget> thenEntered = new Mock<Widget>(Rectangle.Empty);
        //
        //         var wTree = new W(thenEntered.Object);
        //
        //         mockedDevice.Setup(device => device.IsPressed()).Returns(true);
        //         _updater.Device = mockedDevice.Object;
        //
        //         firstPressed.Setup(w => w.CurrentInteraction()).Returns(Interaction.Pressed);
        //
        //         // Press firstPressed
        //         _updater.Update(Maybe.Some(new W(firstPressed.Object)));
        //
        //         // Move to thenEntered while pressing
        //         _updater.Update(wTree);
        //
        //         mockedDevice.Setup(device => device.IsPressed()).Returns(false);
        //         mockedDevice.Setup(d => d.IsReleased()).Returns(true);
        //         _updater.Device = mockedDevice.Object;
        //
        //         _updater.Update(Maybe.Some(wTree));
        //
        //         firstPressed.Verify(w => w.ChangeInteraction(Interaction.Released), Times.Never);
        //         thenEntered.Verify(w => w.ChangeInteraction(Interaction.Released), Times.Never);
        //     }

        #endregion
    }
}