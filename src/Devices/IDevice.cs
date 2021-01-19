using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WForest.Devices
{
    /// <summary>
    /// Interface to abstract over input devices such as mouse and controllers.
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Get the (X,Y) coordinates on the screen pointed by the device.
        /// </summary>
        /// <returns>A Point that holds the pointed coordinates on the game window.</returns>
        Point GetPointedLocation();

        /// <summary>
        /// Returns true if the device interact button was just pressed.
        /// </summary>
        /// <returns></returns>
        bool IsPressed();

        /// <summary>
        /// Returns true if the device interact button was previously pressed and is held down.
        /// </summary>
        /// <returns></returns>
        bool IsHeld();

        /// <summary>
        /// Returns true if the device interact button was previously pressed and was just released.
        /// </summary>
        /// <returns></returns>
        bool IsReleased();

        internal void Reset();
    }

    /// <summary>
    /// Device concrete class to handle Mouse location.
    /// </summary>
    public class MouseDevice : IDevice
    {
        private MouseDevice()
        {
        }

        private static MouseDevice? _mouseDevice;

        private bool _isLeftButtonPressed;

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

        /// <summary>
        /// Returns true if the left mouse button was just clicked. 
        /// </summary>
        /// <returns></returns>
        public bool IsPressed()
        {
            var isBtnPressed = Mouse.GetState().LeftButton == ButtonState.Pressed; 
            var b= isBtnPressed && !_isLeftButtonPressed;
            _isLeftButtonPressed = isBtnPressed;
            return b;
        }

        /// <summary>
        /// Returns true if the left mouse button is being held down.
        /// </summary>
        /// <returns></returns>
        public bool IsHeld()
        {
            return _isLeftButtonPressed && Mouse.GetState().LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Returns true if the left mouse button was just released.
        /// </summary>
        /// <returns></returns>
        public bool IsReleased()
        {
            var b = Mouse.GetState().LeftButton == ButtonState.Released && _isLeftButtonPressed;
            _isLeftButtonPressed = false;
            return b;
        }

        void IDevice.Reset()
        {
            _isLeftButtonPressed = false;
        }
    }
}