using System;
using Microsoft.Xna.Framework.Graphics;
using WForest.Exceptions;
using WForest.Utilities;
using WForest.Utilities.Collections;

namespace WForest.UI.Properties
{
    public class Rounded : Property
    {
        private readonly int _radius;
        private Effect _effect;

        internal Rounded(int radius)
        {
            if (radius < 0) throw new InvalidRadiusException("Radius cannot be a negative number.");
            _radius = radius;
            _effect = ShaderDb.Rounded;
        }

        internal override void ApplyOn(WidgetTree widgetNode)
        {
            // _effect.Parameters["Width"].SetValue(Space.Width);
            // _effect.Parameters["Height"].SetValue(Space.Height);
            // _effect.Parameters["Radius"].SetValue(65);

            throw new NotImplementedException();
        }
    }
}