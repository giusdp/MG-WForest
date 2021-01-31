using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WForest.Rendering;
using WForest.UI.Interactions;
using WForest.UI.Widgets.Interfaces;
using WForest.Utilities;
using WForest.Utilities.Collections;

namespace WForest.UI.Widgets
{
    /// <summary>
    /// Concrete base class for the IWidget interface.
    /// </summary>
    public class Widget : IWidget
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
        public List<Action<IRenderer>> PostDrawActions { get; }

        ///<inheritdoc/> 
        public virtual void Draw(IRenderer renderer)
        {
        }

        #endregion

        #region Widget Tree

        ///<inheritdoc/> 
        public IWidget? Parent { get; set; }

        ///<inheritdoc/> 
        public ICollection<IWidget> Children { get; }

        #endregion
    }
}