using Microsoft.Xna.Framework.Graphics;
using PiBa.Exceptions;

namespace PiBa
{
    public static class GraphicsInfo
    {
        private static GraphicsDevice _graphicsDevice;

        public static void Initialize(GraphicsDevice device)
        {
            _graphicsDevice = device;
        }

        public static GraphicsDevice GetGraphicsDevice()
        {
            if (_graphicsDevice == null)
                throw new GraphicsInfoNotInitializedException("Tried to get device but window is not initialized.");
            return _graphicsDevice;
        }
    }
}