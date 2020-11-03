using Microsoft.Xna.Framework;

namespace PiBa.UI.Constraints
{
    public interface IConstraint
    {
        Rectangle Enforce(Rectangle parentSpace);
    }
}