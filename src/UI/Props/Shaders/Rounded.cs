﻿using Microsoft.Xna.Framework.Graphics;
using WForest.Exceptions;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities.Collections;

namespace WForest.UI.Props.Shaders
{
    internal class Rounded : Prop 
    {
        private int Radius { get; }
        private Effect Effect { get; }

        internal Rounded(int radius)
        {
            if (radius < 0) throw new InvalidRadiusException("Radius cannot be a negative number.");
            Radius = radius;
            Effect = ShaderDb.Rounded;
        }

        public override void ApplyOn(IWidget widget){}

       internal void ApplyParameters(int width, int height, int radius)
       {
           Effect.Parameters["Width"].SetValue(width);
           Effect.Parameters["Height"].SetValue(height);
           Effect.Parameters["Radius"].SetValue(radius);
           
       }
    }
}