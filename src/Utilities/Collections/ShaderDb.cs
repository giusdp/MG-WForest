using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.Utilities.Collections
{
    internal static class ShaderDb
    {
        internal static GraphicsDevice GraphicsDevice { get; set; }

        private static readonly Lazy<Effect> RoundedEffect = new Lazy<Effect>(LoadRoundedShader());

        internal static Effect Rounded
        {
            get
            {
                try
                {
                    return RoundedEffect.Value;
                }
                catch (Exception e)
                {
                    if (e.InnerException != null) throw e.InnerException;
                    throw;
                }
            }
        }

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
            if (!File.Exists(path)) throw new FileNotFoundException($"Shader {effectName} not found.");
            return File.ReadAllBytes(path);
        }
        
        internal static void Dispose()
        {
            GraphicsDevice = null;
        }
    }
}