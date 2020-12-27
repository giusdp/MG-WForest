using System;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Serilog;
using WForest.UI;
using WForest.UI.Widgets.TextWidget;

namespace WForest
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private HUD _hud;
        private FontSystem _fontSystem;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        private bool _windowSizeIsBeingChanged = true;

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            _windowSizeIsBeingChanged = !_windowSizeIsBeingChanged;
            if (!_windowSizeIsBeingChanged) return;
            _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            _graphics.ApplyChanges();
            _hud?.Resize(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            AssetLoader.Initialize(Content);
            _fontSystem = FontSystemFactory.Create(GraphicsDevice, 1024, 1024);
            _fontSystem.AddFont(File.ReadAllBytes("Fonts/Comfortaa-Regular.ttf"));
            FontManager.Initialize(new Font(_fontSystem));
            var bold = FontSystemFactory.Create(GraphicsDevice, 1024, 1024);
            bold.AddFont(File.ReadAllBytes("Fonts/Comfortaa-Bold.ttf"));
            FontManager.RegisterFont("Comfortaa-Bold", new Font(bold));
            _hud = new HUD(0,0,_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _hud.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            _hud.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}