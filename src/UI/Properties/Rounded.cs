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

        internal Rounded(int radius)
        {
            if (radius < 0) throw new InvalidRadiusException("Radius cannot be a negative number.");
            _radius = radius;
        }

        internal override void ApplyOn(WidgetTree widgetNode)
        {
            widgetNode.Data.Effect = ShaderDb.Rounded; 

            throw new NotImplementedException();
        }
    }
}