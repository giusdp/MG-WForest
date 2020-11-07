using Microsoft.Xna.Framework.Graphics;

namespace PiBa.Rendering
{
    public class Sprite
    {
        public Texture2D Texture { get; }

        public Sprite(string image)
        {
            Texture = AssetLoader.Load<Texture2D>(image);

        }
    }
}