using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PiBa.UI.Constraints;

namespace PiBa.UI.Widgets
{
    public class Container : Widget
    {
        public Container(Rectangle space) : base(space)
        {
        }

        public override string ToString() => $"Container Widget with a Space of {Space}";
    }
}