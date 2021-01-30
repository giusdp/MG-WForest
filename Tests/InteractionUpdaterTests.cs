using Microsoft.Xna.Framework;
using NUnit.Framework;
using WForest.UI.Interactions;
using WForest.UI.Widgets;
using WForest.Utilities;

namespace WForest.Tests
{
    [TestFixture]
    public class InteractionUpdaterTests
    {
        private InteractionUpdater _updater;
        private Widget _widget;

        [SetUp]
        public void BeforeEach()
        {
            _updater = new InteractionUpdater();
            _widget = new Widget(RectangleF.Empty);
        }

        #region Untouched Case

        [Test]
        public void UpdateTest_FromUntouchedToUntouched_NothingHappens()
        {
            _updater.NextState(_widget, Interaction.Untouched);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Untouched));
        }

        [Test]
        public void UpdateTest_FromUntouchedToEnter_GoToEntered()
        {
            _updater.NextState(_widget, Interaction.Entered);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Entered));
        }

        [Test]
        public void UpdateTest_FromUntouchedToExit_DoesNotChange()
        {
            _updater.NextState(_widget, Interaction.Exited);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Untouched));
        }

        [Test]
        public void UpdateTest_FromUntouchedToPress_GoToPress()
        {
            _updater.NextState(_widget, Interaction.Pressed);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Pressed));
        }

        [Test]
        public void UpdateTest_FromUntouchedToRelease_DoesNotChange()
        {
            _updater.NextState(_widget, Interaction.Released);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Untouched));
        }

        #endregion

        #region Entered Case

        [Test]
        public void UpdateTest_FromEnterToUntouched_GoToUntouched()
        {
            _widget.CurrentInteraction = Interaction.Entered;
            _updater.NextState(_widget, Interaction.Untouched);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Untouched));
        }

        [Test]
        public void UpdateTest_FromEnterToEnter_DoesNotChange()
        {
            _widget.CurrentInteraction = Interaction.Entered;
            _updater.NextState(_widget, Interaction.Entered);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Entered));
        }

        [Test]
        public void UpdateTest_FromEnterToExit_GoToUntouched()
        {
            _widget.CurrentInteraction = Interaction.Entered;
            _updater.NextState(_widget, Interaction.Exited);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Untouched));
        }

        [Test]
        public void UpdateTest_FromEnterToPress_GoToPressed()
        {
            _widget.CurrentInteraction = Interaction.Entered;
            _updater.NextState(_widget, Interaction.Pressed);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Pressed));
        }

        [Test]
        public void UpdateTest_FromEnterToReleased_GoToEntered()
        {
            _widget.CurrentInteraction = Interaction.Entered;
            _updater.NextState(_widget, Interaction.Released);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Entered));
        }

        #endregion

        #region Pressed Case

        [Test]
        public void UpdateTest_FromPressToUntouched_StayPressed()
        {
            _widget.CurrentInteraction = Interaction.Pressed;
            _updater.NextState(_widget, Interaction.Untouched);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Pressed));
        }

        [Test]
        public void UpdateTest_FromPressToEnter_StayPressed()
        {
            _widget.CurrentInteraction = Interaction.Pressed;
            _updater.NextState(_widget, Interaction.Entered);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Pressed));
        }

        [Test]
        public void UpdateTest_FromPressToExit_GoToUntouched()
        {
            _widget.CurrentInteraction = Interaction.Pressed;
            _updater.NextState(_widget, Interaction.Exited);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Untouched));
        }

        [Test]
        public void UpdateTest_FromPressToPress_StayPressed()
        {
            _widget.CurrentInteraction = Interaction.Pressed;
            _updater.NextState(_widget, Interaction.Pressed);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Pressed));
        }

        [Test]
        public void UpdateTest_FromPressToReleased_GoToEntered()
        {
            _widget.CurrentInteraction = Interaction.Pressed;
            _updater.NextState(_widget, Interaction.Released);
            Assert.That(_widget.CurrentInteraction, Is.EqualTo(Interaction.Entered));
        }

        #endregion
    }
}