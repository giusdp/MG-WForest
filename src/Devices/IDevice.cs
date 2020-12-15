using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WForest.Devices
{
    public interface IDevice
    {
        Point GetPointedLocation();
    }

    public class MouseDevice : IDevice
    {
        public Point GetPointedLocation()
        {
            return Mouse.GetState().Position;
        }
    }
}