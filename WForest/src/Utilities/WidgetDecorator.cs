using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WForest.Rendering;
using WForest.UI.Interactions;
using WForest.UI.Props.Interfaces;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities.Collections;

namespace WForest.Utilities
{
    public abstract class WidgetDecorator : IWidget
    {
        private readonly IWidget _widget;

        protected WidgetDecorator(IWidget widget)
        {
            _widget = widget;
        }

        public RectangleF Space
        {
            get => _widget.Space;
            set => _widget.Space = value;
        }

        public MarginValues Margins
        {
            get => _widget.Margins;
            set => _widget.Margins = value;
        }

        public Color Color
        {
            get => _widget.Color;
            set => _widget.Color = value;
        }

        public ICollection<Action<IRenderer>> PostDrawActions => _widget.PostDrawActions;

        public PropCollection Props => _widget.Props;

        /// <summary>
        /// Add a prop to the holder and return the updated holder so this method can be chained.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public IPropHolder WithProp(IProp prop) => _widget.WithProp(prop);

        public void ApplyProps() => _widget.ApplyProps();

        public Interaction CurrentInteraction
        {
            get => _widget.CurrentInteraction;
            set => _widget.CurrentInteraction = value;
        }

        public virtual void Draw(IRenderer renderer) => _widget.Draw(renderer);
        
        public IWidget? Parent
        {
            get => _widget.Parent;
            set => _widget.Parent = value;
        }

        public ICollection<IWidget> Children => _widget.Children;

        /// <summary>
        /// Add a widget as a child of this widget. It updates it's location relative to this widget location
        /// and the child's parent is set to this widget.
        /// </summary>
        /// <param name="widget"></param>
        /// <returns>The added child.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public IWidget AddChild(IWidget widget) => _widget.AddChild(widget);
    }
}