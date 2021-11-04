using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace OpenBlocks.Main
{
    /// <summary>
    /// Global Events Manager
    /// </summary>
    public static class EventsManager
    {
        public abstract class BaseGameEvent
        {
            public GraphicsDeviceManager GraphicsDeviceManager;
            public ContentManager ContentManager;
            public ScreenManager ScreenManager;
            public SpriteBatch SpriteBatch;
            public GameWindow Window;
        }
        
        public abstract class BaseGameTimeEvent : BaseGameEvent
        {
            public GameTime GameTime;
        }

        public class InitializeEventArguments : BaseGameEvent { }
        public class LoadContentEventArguments : BaseGameEvent { }
        public class UpdateEventArguments : BaseGameTimeEvent { }
        public class DrawEventArguments : BaseGameTimeEvent { }

        public static event EventHandler<InitializeEventArguments> InitializeEvent;
        public static void OnInitialize()
            => InitializeEvent?.Invoke(null, new InitializeEventArguments {
                GraphicsDeviceManager = Entrypoint.Game.GetGraphics(),
                ScreenManager = Entrypoint.Game.GetScreenManager(),
                SpriteBatch = Entrypoint.Game.GetSpriteBatch(),
                ContentManager = Entrypoint.Game.Content,
                Window = Entrypoint.Game.Window
            });
        
        public static event EventHandler<LoadContentEventArguments> LoadContentEvent;
        public static void OnLoadContent()
            => LoadContentEvent?.Invoke(null, new LoadContentEventArguments {
                GraphicsDeviceManager = Entrypoint.Game.GetGraphics(),
                ScreenManager = Entrypoint.Game.GetScreenManager(),
                SpriteBatch = Entrypoint.Game.GetSpriteBatch(),
                ContentManager = Entrypoint.Game.Content,
                Window = Entrypoint.Game.Window
            });
        
        public static event EventHandler<UpdateEventArguments> UpdateEvent;
        public static void OnUpdate(GameTime gameTime)
            => UpdateEvent?.Invoke(null, new UpdateEventArguments {
                GraphicsDeviceManager = Entrypoint.Game.GetGraphics(),
                ScreenManager = Entrypoint.Game.GetScreenManager(),
                SpriteBatch = Entrypoint.Game.GetSpriteBatch(),
                ContentManager = Entrypoint.Game.Content,
                Window = Entrypoint.Game.Window,
                GameTime = gameTime
            });
        
        public static event EventHandler<DrawEventArguments> DrawEvent;
        public static void OnDraw(GameTime gameTime)
            => DrawEvent?.Invoke(null, new DrawEventArguments {
                GraphicsDeviceManager = Entrypoint.Game.GetGraphics(),
                ScreenManager = Entrypoint.Game.GetScreenManager(),
                SpriteBatch = Entrypoint.Game.GetSpriteBatch(),
                ContentManager = Entrypoint.Game.Content,
                Window = Entrypoint.Game.Window,
                GameTime = gameTime
            });
    }
}