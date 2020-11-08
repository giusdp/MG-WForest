using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI.Constraints
{
    public interface IConstraint
    {
        bool isSatisfied { get; }
        void EnforceOn(WidgetTree widgetNode);
    }
}