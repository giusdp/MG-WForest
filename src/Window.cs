using Microsoft.Xna.Framework.Graphics;
using PiBa.Exceptions;

namespace PiBa
{
    public static class Window
    {
        private static GraphicsDevice _graphicsDevice;

        public static void Initialize(GraphicsDevice device)
        {
            _graphicsDevice = device;
        }

        public static GraphicsDevice GetGraphicsDevice()
        {
            if (_graphicsDevice == null)
                throw new WindowNotInitializedException("Tried to get device but window is not initialized.");
            return _graphicsDevice;
        }
    }
}