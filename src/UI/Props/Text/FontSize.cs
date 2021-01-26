using System;
using Microsoft.Xna.Framework;
using Serilog;
using WForest.Exceptions;
using WForest.UI.Props.Interfaces;
using WForest.UI.Utils;
using WForest.UI.Widgets.Interfaces;

namespace WForest.UI.Props.Text
{
    /// <summary>
    /// Property only applicable on Text Widget, it changes the size of the font of the widget.
    /// </summary>
    public class FontSize : IApplicableProp
    {
        private readonly int _size;

        internal FontSize(int size)
        {
            _size = size;
        }

        public int Priority { get; set; }
        public event EventHandler? Applied;

        /// <summary>
        /// Applies font size change on Text Widget. It assigns the new size to the FontSize field of the widget and
        /// measures (and updates) the new space taken by the text.
        /// </summary>
        /// <param name="widget"></param>
        /// <exception cref="IncompatibleWidgetException"></exception>
        public void ApplyOn(IWidget widget)
        {
            if (widget is Widgets.BuiltIn.Text text)
            {
                if (text.FontSize == _size) return;
                text.FontSize = _size >= 0 ? _size : throw new ArgumentException("FontSize cannot be negative.");
                var (x,y,_,_) = text.Space;
                var (w, h) = text.Font.MeasureText(text.TextString, _size);
                WidgetsSpaceHelper.UpdateSpace(widget, new Rectangle(x,y, (int) w,(int) h));
            }
            else
            {
                Log.Error("FontSize property is only applicable to a Text Widget. Instead it has received a {W}", widget);
                throw new IncompatibleWidgetException("Property only applicable to a Text Widget.");
            }
        }
    }
}