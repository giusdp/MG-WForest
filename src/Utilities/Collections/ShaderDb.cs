using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Serilog;

namespace WForest.Utilities.Collections
{
    internal static class ShaderDb
    {
        internal static GraphicsDevice GraphicsDevice { get; set; }

        private static readonly Lazy<Effect> RoundedEffect = new Lazy<Effect>(LoadRoundedShader);

        internal static Effect Rounded => RoundedEffect.Value;

        private static Effect LoadRoundedShader()
        {
            if (GraphicsDevice == null)
                throw new ArgumentNullException(nameof(GraphicsDevice),
                    "GraphicsDevice was null when loading Rounded Effect");
            return new Effect(GraphicsDevice, ReadShaderFromFile("Rounded"));
        }

        internal static byte[] ReadShaderFromFile(string effectName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $@"Content/Shaders/{effectName}.fx");
            Log.Debug($"Reading shader {effectName} at location {path}");
            if (!File.Exists(path)) throw new FileNotFoundException($"Shader {effectName} not found.");
            return File.ReadAllBytes(path);
        }
    }
}