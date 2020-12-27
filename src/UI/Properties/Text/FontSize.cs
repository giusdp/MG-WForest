using System;
using Microsoft.Xna.Framework;
using Serilog;
using WForest.Exceptions;
using WForest.UI.Utils;

namespace WForest.UI.Properties.Text
{
    public class FontSize : Property
    {
        internal override int Priority { get; } = 1;
        
        private readonly int _size;

        public FontSize(int size)
        {
            _size = size;
        }

        internal override void ApplyOn(WidgetTree.WidgetTree widgetNode)
        {
            if (widgetNode.Data is Widgets.TextWidget.Text text)
            {
                if (text.FontSize == _size) return;
                text.FontSize = _size >= 0 ? _size : throw new ArgumentException("FontSize cannot be negative.");
                var (x,y,_,_) = text.Space;
                var (w, h) = text.Font.MeasureText(text.TextString, _size);
                WidgetsSpaceHelper.UpdateSpace(widgetNode, new Rectangle(x,y, (int) w,(int) h));
            }
            else
            {
                Log.Error(
                    $"FontSize property is only applicable to a Text Widget. Instead it has received a {widgetNode}");
                throw new IncompatibleWidgetException("Property only applicable to a Text Widget.");
            }
        }
    }
}