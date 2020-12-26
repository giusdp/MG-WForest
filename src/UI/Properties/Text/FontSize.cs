using System;
using Serilog;
using WForest.Exceptions;

namespace WForest.UI.Properties.Text
{
    public class FontSize : Property
    {
        private int _size;

        public FontSize(int size)
        {
            _size = size;
        }

        internal override void ApplyOn(WidgetTree.WidgetTree widgetNode)
        {
            if (widgetNode.Data is Widgets.TextWidget.Text text)
            {
                text.Font.Size = _size >= 0 ? _size : throw new ArgumentException("FontSize cannot be negative.");
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