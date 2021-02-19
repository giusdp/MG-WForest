using System;
using System.Linq;
using Serilog;
using WForest.Exceptions;
using WForest.Props.Interfaces;
using WForest.Utilities.Text;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Text
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
        public bool IsApplied { get; set; }

        /// <summary>
        /// Gets the font from the FontStore, with the name passed to FontFamily constructor and assigns it to the TextWidget.
        /// Then the new space required by the widget is calculated and updated.
        /// </summary>
        /// <param name="widget"></param>
        /// <exception cref="IncompatibleWidgetException"></exception>
        public void ApplyOn(IWidget widget)
        {
            IsApplied = false;

            var tail = widget.Skip(1).OfType<Widgets.BuiltIn.Text>().ToList();
            if (widget is Widgets.BuiltIn.Text text) text.Font = FontStore.GetFont(_name);
            if (tail.Count > 0)
            {
                foreach (var t in tail)
                {
                    var alreadyHasFontFamilyProp = t.Props.SafeGetByProp<FontFamily>().TryGetValue(out var l);
                    if (!alreadyHasFontFamilyProp || l.Count == 0) t.Font = FontStore.GetFont(_name);
                }
            }
            else
                Log.Warning(
                    "FontFamily was applied to a widget that is not a Text nor has any Text in its sub-tree");

            IsApplied = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}