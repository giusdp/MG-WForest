using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PiBa.UI;

namespace PiBa
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private UI.UserInterface _userInterface;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _userInterface = new UI.UserInterface();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _userInterface.Update();
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var blankTexture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
            
            blankTexture.SetData(new[] {Color.White});
            _spriteBatch.Begin();
            _spriteBatch.Draw(blankTexture, Rectangle.Empty, Color.Aqua);
            _spriteBatch.End();
            
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}