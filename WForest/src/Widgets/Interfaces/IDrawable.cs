using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WForest.Rendering;
using WForest.Utilities;

namespace WForest.Widgets.Interfaces
{
    /// <summary>
    /// Interface for basic drawing.
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// The Space used by the widget.
        /// </summary>
        RectangleF Space { get; set; }

        /// <summary>
        /// The values for the left, right, top, bottom margins.
        /// </summary>
        MarginValues Margins { get; set; }

        /// <summary>
        /// The widget color.
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// Get the total space occupied by the widget, that is it's space + margins.
        /// </summary>
        RectangleF TotalSpaceOccupied =>
            new RectangleF(
                Space.X - Margins.Left,
                Space.Y - Margins.Top,
                Space.Width + Margins.Left + Margins.Right,
                Space.Height + Margins.Top + Margins.Bottom
            );

        /// <summary>
        /// Actions that can be run after drawing. The border prop adds a post draw action that adds the border.
        /// </summary>
        ICollection<Action<IRenderer>> PostDrawActions { get; }

        /// <summary>
        /// Draw the widget.
        /// </summary>
        /// <param name="renderer"></param>
        void Draw(IRenderer renderer);
    }
}