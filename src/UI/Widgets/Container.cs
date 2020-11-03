using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PiBa.UI.Constraints;
using PiBa.UI.Props;
using PiBa.UI.Widgets.Interfaces;

namespace PiBa.UI.Widgets
{
    public class Container : IWidget
    {
        public List<IProp> Props { get; }
        public List<IConstraint> Constraints { get; }

        public Container()
        {
            Props = new List<IProp>();
            Constraints = new List<IConstraint>();
        }
    }
}