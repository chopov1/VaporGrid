using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VaporGridCrossPlatform
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        GameStateManager conditionManager;

        Texture2D blank;

        Vector2 mouseCords;
        public Game1()
        {
            Window.Title = "VaporGrid";
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            conditionManager = new GameStateManager(this);
            Components.Add(conditionManager);
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            blank = this.Content.Load<Texture2D>("Blank");
            MouseCursor mc = MouseCursor.FromTexture2D(blank, 0, 0);
            Mouse.SetCursor(mc);
            mouseCords.X = GraphicsDevice.Viewport.Width / 2;
            mouseCords.Y = GraphicsDevice.Viewport.Height / 2;
            Mouse.SetPosition((int)mouseCords.X, (int)mouseCords.Y);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}