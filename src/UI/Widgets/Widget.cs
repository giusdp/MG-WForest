using System.Collections.Generic;
using PiBa.UI.Constraints;
using PiBa.Utilities.Collections;

namespace PiBa.UI.Widgets
{
    public class Widget
    {
        public Props Props { get; }
        public List<IConstraint> Constraints { get; }

        protected Widget(Props props) 
        {
            Props = props;
            Constraints = new List<IConstraint>();
        }
 
        public void AddConstraint(IConstraint constraint) => Constraints.Add(constraint);
        
    }
}