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
        private MouseDevice(){}
        private static MouseDevice _mouseDevice;

        public static MouseDevice Instance => _mouseDevice ??= new MouseDevice();

        public Point GetPointedLocation()
        {
            return Mouse.GetState().Position;
        }
    }
}