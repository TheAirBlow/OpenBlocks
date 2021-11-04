using FontStashSharp;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;

namespace OpenBlocks.Main
{
    public class TitleScreen : GameScreen
    {
        private readonly MainGame _game;
        private readonly Desktop _desktop;
        
        public TitleScreen(MainGame game) : base(game)
        {
            _game = game;
            _game.Window.Title = "OpenBlocks | Title Screen";
            var root = new VerticalStackPanel {
                Background = new TextureRegion(_game.TextureAtlas, new Rectangle(16, 0, 16, 16))
            };
            
            // Font
            FontSystem fontSystem = new FontSystem();
            fontSystem.AddFont(_game.RawMainFont);
            var font = fontSystem.GetFont(24);

            // Logo
            root.AddChild(new Image {
                Renderable = new TextureRegion(_game.LogoTexture, _game.LogoTexture.Bounds),
                Margin = new Thickness(_game.GetGraphics().GraphicsDevice.Viewport.Bounds.Center.X
                                       - _game.LogoTexture.Width / 2, 10, 0, 0)
            });

            // Singleplayer Button
            var singleplayerButton = new TextButton {
                Text = "Singleplayer",
                Font = font,
                Margin = new Thickness(10, 10, 0, 0),
                Padding = new Thickness(6, 6, 6, 6)
            };
            
            singleplayerButton.TouchDown += (s, a) => { _game.GetScreenManager().LoadScreen(new WorldSelectScreen(_game)); };
            root.AddChild(singleplayerButton);
            
            // Multiplayer Button
            var multiplayerButton = new TextButton {
                Text = "Multiplayer",
                Font = font,
                Margin = new Thickness(10, 10, 0, 0),
                Padding = new Thickness(6, 6, 6, 6)
            };
            
            root.AddChild(multiplayerButton);
            
            // Settings Button
            var settingsButton = new TextButton {
                Text = "Settings",
                Font = font,
                Margin = new Thickness(10, 10, 0, 0),
                Padding = new Thickness(6, 6, 6, 6)
            };
            
            root.AddChild(settingsButton);

            // Quit Button
            var quitButton = new TextButton {
                Text = "Quit",
                Font = font,
                Margin = new Thickness(10, 10, 0, 0),
                Padding = new Thickness(6, 6, 6, 6)
            };
            
            quitButton.TouchDown += (s, a) => { _game.Exit(); };
            root.AddChild(quitButton);

            _desktop = new Desktop { Root = root };
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _desktop.Render();
        }
    }
}