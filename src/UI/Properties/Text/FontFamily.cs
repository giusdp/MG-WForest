using System;
using Microsoft.Xna.Framework;
using Serilog;
using WForest.Exceptions;
using WForest.UI.Utils;
using WForest.UI.WidgetTrees;
using WForest.Utilities.Text;

namespace WForest.UI.Properties.Text
{
    /// <summary>
    /// Property only applicable on Text Widget, it changes the font the widget.
    /// </summary>
    public class FontFamily : Property
    {
        private readonly string _name;

        internal FontFamily(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Gets the font from the FontStore, with the name passed to FontFamily constructor and assigns it to the TextWidget.
        /// Then the new space required by the widget is calculated and updated.
        /// </summary>
        /// <param name="widgetNode"></param>
        /// <exception cref="IncompatibleWidgetException"></exception>
        public override void ApplyOn(WidgetTree widgetNode)
        {
            if (widgetNode.Data is Widgets.Text text)
            {
                text.Font = FontStore.GetFont(_name);
                var (x, y, _, _) = text.Space;
                var (w, h) = text.Font.MeasureText(text.TextString, text.FontSize);
                WidgetsSpaceHelper.UpdateSpace(widgetNode, new Rectangle(x, y, w, h));
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