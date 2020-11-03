using System.Collections.Generic;
using Microsoft.Xna.Framework;
using PiBa.Interfaces;
using PiBa.UI.Constraints;
using PiBa.UI.Props;

namespace PiBa.UI.Widgets.Interfaces
{
    public interface IWidget
    {
        List<IProp> Props { get; }
        List<IConstraint> Constraints { get; }

        void AddProp(IProp prop)
        {
            Props.Add(prop);
        }

        void AddConstraint(IConstraint constraint)
        {
            Constraints.Add(constraint);
        }
    }
}