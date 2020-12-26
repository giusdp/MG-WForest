using System;
using Serilog;
using WForest.Exceptions;
using WForest.UI.Widgets.TextWidget;

namespace WForest.UI.Properties.Text
{
    public class FontFamily : Property
    {
        private readonly Font _font;
        public FontFamily(Font font)
        {
            _font = font;
        }

        internal override void ApplyOn(WidgetTree.WidgetTree widgetNode)
        {
            if (widgetNode.Data is Widgets.TextWidget.Text text)
            {
                text.Font = _font ?? throw new ArgumentException("Font cannot be null.");
            }
            else
            {
                Log.Error(
                    $"FontFamily property is only applicable to a Text Widget. Instead it has received a {widgetNode}");
                throw new IncompatibleWidgetException("Property only applicable to a Text Widget.");
            }
        }
    }
}