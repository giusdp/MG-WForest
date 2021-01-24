using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;

namespace WForest.UI.Widgets
{
    public interface IWidget : IEnumerable<IWidget>, IPropHolder
    {
        #region Shared State

        /// <summary>
        /// The Space used by the widget.
        /// </summary>
        Rectangle Space { get; set; }

        public MarginValues Margins { get; set; }

        public Rectangle TotalSpaceOccupied =>
            new Rectangle(
                Space.X - Margins.Left,
                Space.Y - Margins.Top,
                Space.Width + Margins.Left + Margins.Right,
                Space.Height + Margins.Top + Margins.Bottom
            );

        #endregion

        #region Widget Tree

        IWidget? Parent { get; set; }
        public ICollection<IWidget> Children { get; }

        void AddChild(IWidget widget)
        {
            if (widget == null) throw new ArgumentNullException(nameof(widget));
            if (widget == this) throw new ArgumentException("Widgets cannot add themselves as their children");
            widget.Parent = this;
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

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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