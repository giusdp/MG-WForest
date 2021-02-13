using System;
using Microsoft.Xna.Framework;
using Serilog;
using WForest.Exceptions;
using WForest.UI.Props.Interfaces;
using WForest.UI.Utils;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities.Text;

namespace WForest.UI.Props.Text
{
    /// <summary>
    /// Property only applicable on Text Widget, it changes the font the widget.
    /// </summary>
    public class FontFamily : IApplicableProp
    {
        private readonly string _name;

        public FontFamily(string name)
        {
            _name = name;
        }

        /// <inheritdoc/>
        public int Priority { get; set; }

        /// <inherit/>
        public event EventHandler? Applied;

        /// <inheritdoc/>
        public bool ApplicationDone { get; set; }

        /// <summary>
        /// Gets the font from the FontStore, with the name passed to FontFamily constructor and assigns it to the TextWidget.
        /// Then the new space required by the widget is calculated and updated.
        /// </summary>
        /// <param name="widget"></param>
        /// <exception cref="IncompatibleWidgetException"></exception>
        public void ApplyOn(IWidget widget)
        {
            ApplicationDone = false;
            if (widget is Widgets.BuiltIn.Text text)
            {
                text.Font = FontStore.GetFont(_name);
            }
            else
            {
                Log.Error("FontFamily property is only applicable to a Text Widget, it was instead applied to: {W}",
                    widget.ToString());
                throw new IncompatibleWidgetException("Property only applicable to a Text Widget");
            }

            ApplicationDone = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}