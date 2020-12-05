using Microsoft.Xna.Framework.Graphics;
using WForest.Exceptions;
using WForest.Utilities;

namespace WForest.UI.Properties.Shaders
{
    public class Rounded : Shader 
    {
        private readonly int _radius;
        private readonly Effect _effect;

        internal Rounded(int radius)
        {
            if (radius < 0) throw new InvalidRadiusException("Radius cannot be a negative number.");
            _radius = radius;
            _effect = ShaderDb.Rounded;
        }

        internal override void ApplyOn(WidgetTree widgetNode)
        {
        }
    }
}