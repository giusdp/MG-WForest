using Microsoft.Xna.Framework;

namespace PiBa.UI.Interfaces
{
    public interface Constraint
    {
        bool Enforce(Rectangle containerSpace, Rectangle destinationSpace);
    }
}