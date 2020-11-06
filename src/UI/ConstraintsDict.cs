using System;
using System.Collections.Generic;
using PiBa.Exceptions;
using PiBa.UI.Constraints;
using PiBa.UI.Widgets;
using PiBa.Utilities.Collections;

namespace PiBa.UI
{
    public class ConstraintsDict
    {
        private Dictionary<Tree<Widget>, List<IConstraint>> Dict { get; }

        public ConstraintsDict()
        {
            Dict = new Dictionary<Tree<Widget>, List<IConstraint>>();
        }

        public void Register(Tree<Widget> widget) => Dict.Add(widget, new List<IConstraint>());

        public List<IConstraint> AddConstraint(Tree<Widget> widget, IConstraint c) =>
            CheckAndRunF(widget, w =>
            {
                if (c == null) throw new ArgumentNullException(nameof(c));
                Dict[w].Add(c);
                return Dict[w];
            });


        public List<IConstraint> GetConstraintsOf(Tree<Widget> widget) => CheckAndRunF(widget, w => Dict[w]);


        private T CheckAndRunF<T>(Tree<Widget> widget, Func<Tree<Widget>, T> f)
        {
            if (widget == null) throw new ArgumentNullException(nameof(widget));
            if (!Dict.ContainsKey(widget))
                throw new WidgetNotRegisteredException($"{widget} was not registered in the constraints dictionary.");
            return f(widget);
        }
    }
}