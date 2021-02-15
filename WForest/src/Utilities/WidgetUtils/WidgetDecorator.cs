using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WForest.Interactions;
using WForest.Props.Interfaces;
using WForest.Rendering;
using WForest.Utilities.Collections;
using WForest.Widgets.Interfaces;

namespace WForest.Utilities.WidgetUtils
{
    public abstract class WidgetDecorator : IWidget
    {
        private readonly IWidget _widget;

        protected WidgetDecorator(IWidget widget)
        {
            _widget = widget;
        }

        /// <inheritdoc/>
        public RectangleF Space
        {
            get => _widget.Space;
            set => _widget.Space = value;
        }

        /// <inheritdoc/>
        public MarginValues Margins
        {
            get => _widget.Margins;
            set => _widget.Margins = value;
        }

        /// <inheritdoc/>
        public Color Color
        {
            get => _widget.Color;
            set => _widget.Color = value;
        }

        /// <inheritdoc/>
        public ICollection<Action<IRenderer>> PostDrawActions => _widget.PostDrawActions;

        /// <inheritdoc/>
        public PropCollection Props => _widget.Props;

        /// <inheritdoc/>
        public IPropHolder WithProp(IProp prop) => _widget.WithProp(prop);

        /// <inheritdoc/>
        public void ApplyProps() => _widget.ApplyProps();

        /// <inheritdoc/>
        public Interaction CurrentInteraction
        {
            get => _widget.CurrentInteraction;
            set => _widget.CurrentInteraction = value;
        }

        /// <inheritdoc/>
        public virtual void Draw(IRenderer renderer) => _widget.Draw(renderer);
        
        /// <inheritdoc/>
        public IWidget? Parent
        {
            get => _widget.Parent;
            set => _widget.Parent = value;
        }

        /// <inheritdoc/>
        public ICollection<IWidget> Children => _widget.Children;

        /// <inheritdoc/>
        public IWidget AddChild(IWidget widget) => _widget.AddChild(widget);
    }
}