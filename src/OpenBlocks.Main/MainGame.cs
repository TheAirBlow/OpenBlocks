using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.TextureAtlases;
using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;

namespace OpenBlocks.Main
{
    public class MainGame : Game
    {
        public Texture2D CrosshairTexture;
        public Texture2D TextureAtlas;
        public Texture2D SteveTexture;
        public Texture2D LogoTexture;
        public SpriteFont MainFont;
        public byte[] RawMainFont;

        private readonly GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;
        private SpriteBatch _spriteBatch;

        internal GraphicsDeviceManager GetGraphics()
            => _graphics;
        
        internal SpriteBatch GetSpriteBatch()
            => _spriteBatch;

        internal ScreenManager GetScreenManager()
            => _screenManager;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Entrypoint.Logger.Information("[MainGame] Initializing...");
            // 1 tick is 25 milliseconds
            TargetElapsedTime = TimeSpan.FromSeconds(1 / 40.0f);
            IsFixedTimeStep = true;
            Window.Title = "OpenBlocks";
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 480;
            _graphics.ApplyChanges();
            Entrypoint.Logger.Information("[MainGame] Firing Initialize event...");
            EventsManager.OnInitialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Entrypoint.Logger.Information("[MainGame] Loading content...");
            CrosshairTexture = Content.Load<Texture2D>("Crosshair");
            TextureAtlas = Content.Load<Texture2D>("TextureAtlas");
            SteveTexture = Content.Load<Texture2D>("Steve");
            LogoTexture = Content.Load<Texture2D>("Logo");
            MainFont = Content.Load<SpriteFont>("Minecraftia");
            RawMainFont = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Minecraftia.ttf"));
            Entrypoint.Logger.Information("[MainGame] Loading Myra GUI...");
            MyraEnvironment.Game = this;
            Entrypoint.Logger.Information("[MainGame] Firing LoadContent event...");
            EventsManager.OnLoadContent();
            Entrypoint.Logger.Information("[MainGame] Switching to TitleScreen...");
            _screenManager.LoadScreen(new TitleScreen(this));
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            EventsManager.OnUpdate(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            EventsManager.OnDraw(gameTime);
            base.Draw(gameTime);
        }
    }
}