using Microsoft.Xna.Framework;

namespace PiBa.UI.Constraints
{
    public class Center : IConstraint
    {
        private Point Size { get; }

        public Center(Point size)
        {
            Size = size;
        }

        public Rectangle Enforce(Rectangle parentSpace)
        {
            return new Rectangle(location: GetCenterLocation(parentSpace), Size);
        }

        private Point GetCenterLocation(Rectangle parent)
        {
            var (x, y, width, height) = parent;
            return new Point(
                (int) ((width - x) / 2f - Size.X / 2f),
                (int) ((height - y) / 2f - Size.Y / 2f)
            );
        }
    }
}