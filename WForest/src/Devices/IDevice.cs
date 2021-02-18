using Microsoft.Xna.Framework;

namespace WForest.Devices
{
    /// <summary>
    /// Interface to abstract over input devices such as mouse and controllers.
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Updates the state of the device.
        /// </summary>
        void Update();

        /// <summary>
        /// Get the (X,Y) coordinates on the screen pointed by the device.
        /// </summary>
        /// <returns>A Vector2 that holds the pointed coordinates on the game window.</returns>
        Vector2 GetPointedLocation();

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
    }
}