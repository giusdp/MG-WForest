using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WForest.Rendering
{
    public class DefaultRenderer : IRenderer
    {
        private readonly SpriteBatch _spriteBatch;
        public GraphicsDevice GraphicsDevice { get; }

        public DefaultRenderer(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            GraphicsDevice = graphicsDevice;
        }

        public void Draw(Texture2D texture, Vector2 position) => _spriteBatch.Draw(texture, position, Color.White);


        public void Draw(Texture2D texture, Vector2 position, Color color) =>
            _spriteBatch.Draw(texture, position, color);


        public void Draw(Texture2D texture, Rectangle destinationRectangle) =>
            _spriteBatch.Draw(texture, destinationRectangle, Color.White);


        public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color) =>
            _spriteBatch.Draw(texture, destinationRectangle, color);


        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
            => _spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color,
            SpriteEffects effects)
        {
            _spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color, 0, Vector2.Zero, effects, 0);
        }

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color,
            float rotation,
            SpriteEffects effects, float layerDepth, float skewTopX, float skewBottomX, float skewLeftY,
            float skewRightY)
        {
            throw new System.NotImplementedException("SpriteBatch does not support this kind of Draw!");
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
        {
            _spriteBatch.Draw(texture, position, sourceRectangle, color);
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation,
            Vector2 origin,
            float scale, SpriteEffects effects, float layerDepth)
        {
            _spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation,
            Vector2 origin,
            Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            _spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation,
            Vector2 origin,
            Vector2 scale, SpriteEffects effects, float layerDepth, float skewTopX, float skewBottomX, float skewLeftY,
            float skewRightY)
        {
            throw new System.NotImplementedException("SpriteBatch does not support this kind of Draw!");
        }

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color,
            float rotation,
            Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            _spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, effects,
                layerDepth);
        }

        public void DrawString(SpriteFontBase font, string text, Vector2 position, Color color, float layerDepth = 0)
        {
            _spriteBatch.DrawString(font, text, position, color, layerDepth);
        }
    }
}