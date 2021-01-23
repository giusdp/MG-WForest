using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Props;

namespace WForest.UI.Widgets
{
    public interface IWidget : IEnumerable<IWidget>
    {
        /// <summary>
        /// The Space used by the widget.
        /// </summary>
        Rectangle Space { get; set; }

        Rectangle TotalSpaceOccupied() => Space;

        #region Props

        ICollection<Prop> Props { get; }

        void WithProp(Prop prop)
        {
            if (prop == null) throw new ArgumentNullException(nameof(prop));
            Props.Add(prop);
        }

        void ApplyProps()
        {
            foreach (var prop in Props)
                prop.Apply(this);
        }

        #endregion

        #region Widget Tree

        IWidget? Parent { get; }
        public ICollection<IWidget> Children { get; }

        void AddChild(IWidget widget)
        {
            if (widget == null) throw new ArgumentNullException(nameof(widget));
            if (widget == this) throw new ArgumentException("Widgets cannot add themselves as their children");
            Children.Add(widget);
        }

        /// <summary>
        /// Check if it's root by checking if Parent is null.
        /// </summary>
        public bool IsRoot => Parent == null;

        /// <summary>
        /// Check if it's leaf by checking if Children.Count to 0.
        /// </summary>
        public bool IsLeaf => Children.Count == 0;

        #endregion

        #region Enumerator

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Enumerator to cycle through the tree.
        /// </summary>
        /// <returns></returns>
        IEnumerator<IWidget> IEnumerable<IWidget>.GetEnumerator()
        {
            var stack = new Stack<IWidget>();
            stack.Push(this);
            while (stack.Count > 0)
            {
                var next = stack.Pop();
                yield return next;

                if (next.IsLeaf) continue;
                foreach (var c in next.Children.Reverse())
                    stack.Push(c);
            }
        }

        #endregion
    }
}