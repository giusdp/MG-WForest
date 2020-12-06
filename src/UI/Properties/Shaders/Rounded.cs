using Microsoft.Xna.Framework.Graphics;
using WForest.Exceptions;
using WForest.Utilities;

namespace WForest.UI.Properties.Shaders
{
    public class Rounded : Property 
    {
        public int Radius { get; }
        private Effect Effect { get; }

        internal Rounded(int radius)
        {
            if (radius < 0) throw new InvalidRadiusException("Radius cannot be a negative number.");
            Radius = radius;
            Effect = ShaderDb.Rounded;
        }

       internal override void ApplyOn(WidgetTree.WidgetTree widgetNode){}

       public void ApplyParameters(int width, int height, int radius)
       {
           Effect.Parameters["Width"].SetValue(width);
           Effect.Parameters["Height"].SetValue(height);
           Effect.Parameters["Radius"].SetValue(radius);
           
       }
    }
}