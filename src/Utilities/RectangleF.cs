using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;

namespace WForest.Utilities
{
    public struct RectangleF : IEquatable<RectangleF>
    {
        /// <summary>
        ///     The x-coordinate of the top-left corner position of this <see cref="RectangleF" />.
        /// </summary>
        [DataMember] public float X;

        /// <summary>
        ///     The y-coordinate of the top-left corner position of this <see cref="RectangleF" />.
        /// </summary>
        [DataMember] public float Y;

        /// <summary>
        ///     The width of this <see cref="RectangleF" />.
        /// </summary>
        [DataMember] public float Width;

        /// <summary>
        ///     The height of this <see cref="RectangleF" />.
        /// </summary>
        [DataMember] public float Height;

        /// <summary>
        ///     Gets the x-coordinate of the left edge of this <see cref="RectangleF" />.
        /// </summary>
        public float Left => X;

        /// <summary>
        ///     Gets the x-coordinate of the right edge of this <see cref="RectangleF" />.
        /// </summary>
        public float Right => X + Width;

        /// <summary>
        ///     Gets the y-coordinate of the top edge of this <see cref="RectangleF" />.
        /// </summary>
        public float Top => Y;

        /// <summary>
        ///     Gets the y-coordinate of the bottom edge of this <see cref="RectangleF" />.
        /// </summary>
        public float Bottom => Y + Height;

        public Vector2 Size => new Vector2(Width, Height);

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleF(Vector2 location, Vector2 size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.X;
            Height = size.Y;
        }

        public static RectangleF Empty => new RectangleF();

        public static implicit operator Rectangle(RectangleF r) =>
            new Rectangle((int) r.X, (int) r.Y, (int) r.Width, (int) r.Height);

        public bool Equals(RectangleF other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Width.Equals(other.Width) && Height.Equals(other.Height);
        }

        public void Deconstruct(out float x, out float y, out float width, out float height)
        {
            x = this.X;
            y = this.Y;
            width = this.Width;
            height = this.Height;
        }
    }
}