using System;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Interfaces
{
    /// <summary>
    /// Props that are applicable on a widget to change it or to add some data to it.
    /// </summary>
    public interface IApplicableProp : IProp
    {
        /// <summary>
        /// Priority of application on widget. It's used to sort the order of applications.
        /// </summary>
        int Priority { get; set; }

        /// <summary>
        /// Event fired when this property finished applying on a widget.
        /// </summary>
        event EventHandler? Applied;

        /// <summary>
        /// Boolean to easily check if the prop was applied to the widget.
        /// </summary>
        bool IsApplied { get; set; }

        /// <summary>
        /// Apply the prop on a widget.
        /// </summary>
        /// <param name="widget"></param>
        void ApplyOn(IWidget widget);
    }
}