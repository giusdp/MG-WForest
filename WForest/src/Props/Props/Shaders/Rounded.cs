using Microsoft.Xna.Framework.Graphics;
using WForest.Exceptions;
using WForest.Props.Interfaces;
using WForest.Utilities.Collections;

namespace WForest.Props.Props.Shaders
{
    public class Rounded : IProp 
    {
        private int Radius { get; }
        private Effect Effect { get; }

        public Rounded(int radius)
        {
            if (radius < 0) throw new InvalidRadiusException("Radius cannot be a negative number.");
            Radius = radius;
            Effect = ShaderDb.Rounded;
        }

       internal void ApplyParameters(int width, int height, int radius)
       {
           Effect.Parameters["Width"].SetValue(width);
           Effect.Parameters["Height"].SetValue(height);
           Effect.Parameters["Radius"].SetValue(radius);
           
       }
    }
}