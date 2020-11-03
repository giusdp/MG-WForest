using Microsoft.Xna.Framework;
using PiBa.UI.Constraints;

namespace PiBa.UI.Factories
{
    public static class ConstraintFactory
    {
        public static IConstraint CreateCenterConstraint() => new Center();
       
    }
}