using System;
using Microsoft.Xna.Framework.Graphics;

namespace PiBa.Rendering
{
    public class Sprite
    {
        public Texture2D Texture2D { get; }

        public Sprite(string image)
        {
            Texture2D = Texture2D.FromFile(GraphicsInfo.GetGraphicsDevice(), image);
        }
    }
}