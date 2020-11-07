using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PiBa.Exceptions;

namespace PiBa
{
    public static class AssetLoader
    {
        private static ContentManager _content;

        public static void Initialize(ContentManager content)
        {
             _content = content;
        }

        public static T Load<T>(string name)
        {
            if (_content == null)
                throw new AssetLoaderNotInitializedException("Tried to use the asset loader but it is not initialized yet.");
            return _content.Load<T>(name);
        }
    }
}