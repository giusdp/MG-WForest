using System.Collections.Generic;
using PiBa.UI.Constraints;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI
{
    public class ConstraintsMap
    {
        private Dictionary<Tree<Widget>, List<IConstraint>> Map { get; }

        public ConstraintsMap()
        {
            Map = new Dictionary<Tree<Widget>, List<IConstraint>>();
        }

        public void Register(Tree<Widget> widget) => Map.Add(widget, new List<IConstraint>());

        public void AddConstraint(Tree<Widget> widget, IConstraint c) => Map[widget].Add(c);
        public List<IConstraint> GetConstraintsOf(Tree<Widget> widget) => Map[widget];
    }
}