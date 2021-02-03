using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.Rendering
{
    public interface IRenderer
    {
        GraphicsDevice GraphicsDevice { get; }

        #region Public draw methods

        public void Draw(Texture2D texture, Vector2 position);

        public void Draw(Texture2D texture, Vector2 position, Color color);

        public void Draw(Texture2D texture, Rectangle destinationRectangle);

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color);


        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color);

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color,
            SpriteEffects effects);

        public void Draw(
            Texture2D texture,
            Rectangle destinationRectangle,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            SpriteEffects effects,
            float layerDepth,
            float skewTopX, float skewBottomX, float skewLeftY, float skewRightY
        );


        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color);

        public void Draw(
            Texture2D texture,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            float scale,
            SpriteEffects effects,
            float layerDepth
        );

        public void Draw(
            Texture2D texture,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth
        );

        public void Draw(
            Texture2D texture,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth,
            float skewTopX, float skewBottomX, float skewLeftY, float skewRightY
        );


        public void Draw(
            Texture2D texture,
            Rectangle destinationRectangle,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            SpriteEffects effects,
            float layerDepth
        );

        #endregion

        #region Public DrawString methods

        void DrawString(
            SpriteFontBase font,
            string text,
            Vector2 position,
            Color color,
            float layerDepth = 0.0f);

        #endregion
    }
}