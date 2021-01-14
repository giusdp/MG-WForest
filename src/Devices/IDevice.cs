using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WForest.Devices
{
    /// <summary>
    /// Interface to abstract over input devices such as mouse and controllers.
    /// </summary>
    public interface IDevice
    {
        Point GetPointedLocation();
    }

    /// <summary>
    /// Device concrete class to handle Mouse location.
    /// </summary>
    public class MouseDevice : IDevice
    {
        private MouseDevice(){}
        private static MouseDevice? _mouseDevice;

        /// <summary>
        /// Get the singleton instance of the MouseDevice class.
        /// </summary>
        public static MouseDevice Instance => _mouseDevice ??= new MouseDevice();

        /// <summary>
        /// Get the (X,Y) coordinates of the mouse.
        /// </summary>
        /// <returns>A Point that holds the cursor coordinates on the game window.</returns>
        public Point GetPointedLocation()
        {
            return Mouse.GetState().Position;
        }
    }
}