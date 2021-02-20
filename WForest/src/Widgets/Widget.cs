using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WForest.Interactions;
using WForest.Rendering;
using WForest.Utilities;
using WForest.Utilities.Collections;
using WForest.Utilities.WidgetUtils;
using WForest.Widgets.Interfaces;

namespace WForest.Widgets
{
    /// <summary>
    /// Concrete base class for the IWidget interface.
    /// </summary>
    public abstract class Widget : IWidget
    {
        ///<inheritdoc/> 
        public Interaction CurrentInteraction { get; set; }

        ///<inheritdoc/> 
        public PropCollection Props { get; }

        /// <summary>
        /// Creates a Widget without a parent. The widget will be a root of a new tree.
        /// </summary>
        /// <param name="space"></param>
        public Widget(RectangleF space)
        {
            Space = space;
            Margins = new MarginValues();
            Color = Color.White;
            Props = new PropCollection();
            Children = new List<IWidget>();
            CurrentInteraction = Interaction.Untouched;
            PostDrawActions = new List<Action<IRenderer>>();
        }

        #region Rendering

        ///<inheritdoc/> 
        public RectangleF Space { get; set; }

        ///<inheritdoc/> 
        public MarginValues Margins { get; set; }

        ///<inheritdoc/> 
        public Color Color { get; set; }

        ///<inheritdoc/> 
        public ICollection<Action<IRenderer>> PostDrawActions { get; }

        ///<inheritdoc/> 
        public virtual void Draw(IRenderer renderer)
        {
            foreach (var c in Children) c.Draw(renderer);
            foreach (var postDraw in PostDrawActions) postDraw(renderer);
        }

        #endregion

        #region Widget Tree

        ///<inheritdoc/> 
        public IWidget? Parent { get; set; }

        ///<inheritdoc/> 
        public ICollection<IWidget> Children { get; }

        /// <inheritdoc/>
        public IWidget AddChild(IWidget widget)
        {
            if (widget == null) throw new ArgumentNullException(nameof(widget));
            if (widget == this) throw new ArgumentException("Widgets cannot add themselves as their own children");
            WidgetSpaceHelper.UpdateSpace(widget,
                new RectangleF(Space.X, Space.Y, widget.Space.Width, widget.Space.Height));
            widget.Parent = this;
            Children.Add(widget);
            return widget;
        }

        #endregion
    }
}