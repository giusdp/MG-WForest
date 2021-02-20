using WForest.Interactions;

namespace WForest.Widgets.Interfaces
{
    public interface IInteractive
    {
        /// <summary>
        /// The current interaction of this widget with the input device.
        /// By default it can be:
        /// - Untouched
        /// - Entered
        /// - Exited
        /// - Pressed
        /// - Released
        /// </summary>
        public Interaction CurrentInteraction { get; set; }
    }
}