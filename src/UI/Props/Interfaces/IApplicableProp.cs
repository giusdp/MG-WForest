using System;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props.Interfaces
{
    public interface IApplicableProp : IProp
    {
        /// <summary>
        /// Priority of application on widget. It's used to sort the order of applications.
        /// </summary>
        int Priority { get; set; }

        /// <summary>
        /// Event fired when this property finished applying on a widget.
        /// </summary>
        public event EventHandler? Applied;

        /// <summary>
        /// Apply the prop on a widget.
        /// </summary>
        /// <param name="widget"></param>
        public  void ApplyOn(IWidget widget);
    }
}