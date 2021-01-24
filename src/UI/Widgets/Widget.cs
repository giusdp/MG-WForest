using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WForest.Utilities;
using WForest.Utilities.Collections;

namespace WForest.UI.Widgets
{
    public class Widget : IWidget
    {
        /// <summary>
        /// Creates a Widget without a parent. The widget will be a root of a new tree.
        /// </summary>
        /// <param name="space"></param>
        public Widget(Rectangle space)
        {
            Space = space;
            Margins = new MarginValues();
            Props = new PropCollection();
            Children = new List<IWidget>();
        }

        /// <summary>
        /// Creates a Widget with a parent. The widget will be a node or a leaf.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="space"></param>
        public Widget(IWidget parent, Rectangle space)
        {
            Space = space;
            Parent = parent;
            Margins = new MarginValues();
            Props = new PropCollection();
            Children = new List<IWidget>();
        }

        public Rectangle Space { get; set; }
        public MarginValues Margins { get; set; }

        public PropCollection Props { get; }

        public IWidget? Parent { get; set; }
        public ICollection<IWidget> Children { get; }
    }
}