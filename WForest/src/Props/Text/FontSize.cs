using System;
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
        public bool ApplicationDone { get; set; }

        /// <summary>
        /// Applies font size change on Text Widget. It assigns the new size to the FontSize field of the widget and
        /// measures (and updates) the new space taken by the text.
        /// </summary>
        /// <param name="widget"></param>
        /// <exception cref="IncompatibleWidgetException"></exception>
        public void ApplyOn(IWidget widget)
        {
            ApplicationDone = false;
            if (widget is Widgets.BuiltIn.Text text)
            {
                if (text.FontSize == _size) return;
                text.FontSize = _size >= 0 ? _size : throw new ArgumentException("FontSize cannot be negative.");
            }
            else
            {
                Log.Error("FontSize property is only applicable to a Text Widget. Instead it has received a {W}",
                    widget.ToString());
                throw new IncompatibleWidgetException("Property only applicable to a Text Widget.");
            }

            ApplicationDone = true;
            OnApplied();
        }

        private void OnApplied() => Applied?.Invoke(this, EventArgs.Empty);
    }
}