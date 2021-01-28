using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NUnit.Framework;
using WForest.Utilities;

namespace WForest.UI.Widgets.Interfaces
{
    public interface IRenderData
    {
        /// <summary>
        /// The Space used by the widget.
        /// </summary>
        Rectangle Space { get; set; }

        public MarginValues Margins { get; set; }

        Color Color { get; set; }

        public Rectangle TotalSpaceOccupied =>
            new Rectangle(
                Space.X - Margins.Left,
                Space.Y - Margins.Top,
                Space.Width + Margins.Left + Margins.Right,
                Space.Height + Margins.Top + Margins.Bottom
            );

        public List<Action<SpriteBatch>> PostDrawActions { get; }
        void Draw(SpriteBatch spriteBatch);
    }
}