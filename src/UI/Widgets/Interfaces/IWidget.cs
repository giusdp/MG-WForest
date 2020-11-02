using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PiBa.Interfaces;
using PiBa.UI.Interfaces;

namespace PiBa.UI.Widgets.Interfaces
{
    public interface IWidget : IGameObject
    {
        Rectangle ParentSpace { get; }
        Rectangle DestinationSpace { get; }
        List<Constraint> Constraints { get; }

        void AddConstraint(Constraint constraint)
        {
            Constraints.Add(constraint);
        }

    }
}