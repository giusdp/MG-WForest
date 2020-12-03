using System;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.Utilities
{
    public static class ShaderDb
    {
        private static readonly Lazy<Effect> RoundedEffect = new Lazy<Effect>(AssetLoader.Load<Effect>("Shaders/Rounded"));
        public static Effect Rounded => RoundedEffect.Value;
    }
}