using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PiBa.UI.Interfaces;
using PiBa.UI.Widgets.Interfaces;

namespace PiBa.UI.Widgets
{
    public class Container : IWidget
    {
        public Rectangle ParentSpace { get; }
        public Rectangle DestinationSpace { get; }
        public List<Constraint> Constraints { get; }

        public Container(Rectangle destinationSpace, Rectangle parentSpace)
        {
            ParentSpace = parentSpace;
            DestinationSpace = destinationSpace;
            Constraints = new List<Constraint>();
        }

        public void Update()
        {
            Constraints.ForEach(c => c.Enforce(ParentSpace, DestinationSpace));
        }

        public void Draw()
        {
            Console.WriteLine($"Draw {GetType()}");
        }
    }
}