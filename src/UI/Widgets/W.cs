using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WForest.UI.Props;

namespace WForest.UI.Widgets
{
    public class W : IWidget
    {
        /// <summary>
        /// Creates a Widget without a parent. The widget will be a root of a new tree.
        /// </summary>
        /// <param name="space"></param>
        public W(Rectangle space)
        {
            Space = space;
            Props = new List<Prop>();
            Children = new List<IWidget>();
        }

        /// <summary>
        /// Creates a Widget with a parent. The widget will be a node or a leaf.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="space"></param>
        public W(IWidget parent, Rectangle space)
        {
            Space = space;
            Parent = parent;
            Props = new List<Prop>();
            Children = new List<IWidget>();
        }

        public Rectangle Space { get; set; }
        public ICollection<Prop> Props { get; }

        public IWidget? Parent { get; }
        public ICollection<IWidget> Children { get; }
    }
}