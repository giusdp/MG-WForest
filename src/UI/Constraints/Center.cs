using Microsoft.Xna.Framework;
using PiBa.UI.Interfaces;

namespace PiBa.UI.Constraints
{
    public class Center : Constraint
    {
        public bool Enforce(Rectangle containerSpace, Rectangle destinationSpace)
        {
            return true;
        }
    }
}