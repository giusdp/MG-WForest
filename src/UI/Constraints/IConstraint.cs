using Microsoft.Xna.Framework;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI.Constraints
{
    public interface IConstraint
    {
        void EnforceOn(Tree<Widget> widgetNode);
    }
}