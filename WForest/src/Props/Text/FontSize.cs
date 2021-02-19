using System;
using System.Linq;
using Serilog;
using WForest.Exceptions;
using WForest.Props.Interfaces;
using WForest.Widgets.Interfaces;

namespace WForest.Props.Text
{
    /// <summary>
    /// Property only applicable on Text Widget, it changes the size of the font of the widget.
    /// </summary>
    public class FontSize : IApplicableProp
    {
        private readonly int _size;

        public FontSize(int size)
        {
            _size = size;
        }

        /// <inheritdoc/>
        public int Priority { get; set; }

        /// <inherit/>
        public event EventHandler? Applied;

        /// <inheritdoc/>
        public bool IsApplied { get; set; }

        /// <summary>
        /// Applies font size change on Text Widget. It assigns the new size to the FontSize field of the widget and
        /// measures (and updates) the new space taken by the text.
        /// </summary>
        /// <param name="widget"></param>
        /// <exception cref="IncompatibleWidgetException"></exception>
        public void ApplyOn(IWidget widget)
        {
            IsApplied = false;
            if (_size < 0) throw new ArgumentException("FontSize cannot be negative.");

            var tail = widget.Skip(1).OfType<Widgets.BuiltIn.Text>().ToList();
            var appliedToTexts = false;
            if (widget is Widgets.BuiltIn.Text text)
            {
                text.FontSize = _size;
                appliedToTexts = true;
            }

            if (tail.Count > 0)
            {
                foreach (var t in tail)
                {
                    var alreadyHasFontFamilyProp = t.Props.SafeGetByProp<FontSize>().TryGetValue(out var l);
                    if (!alreadyHasFontFamilyProp || l.Count == 0) t.FontSize = _size;
                }

                appliedToTexts = true;
            }

            if (!appliedToTexts)
                Log.Warning(
                    "FontSize was applied to a widget that is not a Text nor has any Text in its sub-tree");

            IsApplied = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}