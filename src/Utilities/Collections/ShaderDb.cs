using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.Utilities.Collections
{
    public static class ShaderDb
    {
        internal static GraphicsDevice GraphicsDevice { get; set; }
        
        private static readonly Lazy<Effect> RoundedEffect = new Lazy<Effect>(LoadRoundedShader());
        internal static Effect Rounded => RoundedEffect.Value;

        private static Effect LoadRoundedShader() =>
            new Effect(GraphicsDevice, File.ReadAllBytes("Shaders/Rounded.fx"));
    }
}