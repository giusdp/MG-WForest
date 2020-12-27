using System;
using Microsoft.Xna.Framework;
using Serilog;
using WForest.Exceptions;
using WForest.UI.Utils;
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
                var (x,y,_,_) = text.Space;
                var (w, h) = text.Font.MeasureText(text.TextString, text.FontSize);
                WidgetsSpaceHelper.UpdateSpace(widgetNode, new Rectangle(x,y, (int) w,(int) h));
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