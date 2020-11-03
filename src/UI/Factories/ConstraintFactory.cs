using Microsoft.Xna.Framework;
using PiBa.UI.Constraints;

namespace PiBa.UI.Factories
{
    public static class ConstraintFactory
    {
        public static IConstraint CreateCenterCostraint(Point widgetSize) => new Center(widgetSize);
       
    }
}