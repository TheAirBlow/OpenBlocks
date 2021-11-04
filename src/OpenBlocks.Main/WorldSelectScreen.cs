using FontStashSharp;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;

namespace OpenBlocks.Main
{
    public class WorldSelectScreen : GameScreen
    {
        private readonly MainGame _game;
        private readonly Desktop _desktop;
        
        public WorldSelectScreen(MainGame game) : base(game)
        {
            _game = game;
            _game.Window.Title = "OpenBlocks | World select screen";
            var root = new VerticalStackPanel();
            
            // Font
            FontSystem fontSystem = new FontSystem();
            fontSystem.AddFont(_game.RawMainFont);
            var font = fontSystem.GetFont(24);
            
            // Button bar
            var topPanel = new HorizontalStackPanel {
                Margin = new Thickness(-2, -5, 0, 6)
            };

            // Create Button
            var createButton = new TextButton {
                Text = "Create",
                Font = font,
                Margin = new Thickness(10, 10, 0, 0),
                Padding = new Thickness(6, 6, 6, 6)
            };
            
            createButton.TouchDown += (s, a) => {
                
            };
            topPanel.AddChild(createButton);
            
            // Delete Button
            var deleteButton = new TextButton {
                Text = "Delete",
                Font = font,
                Margin = new Thickness(10, 10, 0, 0),
                Padding = new Thickness(6, 6, 6, 6)
            };
            
            deleteButton.TouchDown += (s, a) => {
                
            };
            topPanel.AddChild(deleteButton);

            // Back Button
            var backButton = new TextButton {
                Text = "Back",
                Font = font,
                Margin = new Thickness(10, 10, 0, 0),
                Padding = new Thickness(6, 6, 6, 6)
            };
            
            topPanel.AddChild(backButton);
            root.AddChild(topPanel);
            
            // Worlds list
            var list = new ScrollViewer {
                Content = new VerticalStackPanel(),
                Background = new SolidBrush(Color.Gray),
                Height = 450
            };
            
            var content = (VerticalStackPanel) list.Content;
            var test = new TextButton {
                Text = "Test world",
                Font = font,
                Margin = new Thickness(10, 10, 0, 0),
                Padding = new Thickness(6, 6, 6, 6),
                Background = new SolidBrush(Color.White),
                OverBackground = new SolidBrush(Color.White),
                TextColor = Color.Black
            };
            test.TouchDown += (s, a) => {
                
            };
            content.AddChild(test);

            root.AddChild(list);
            backButton.TouchDown += (s, a) => { _game.GetScreenManager().LoadScreen(new TitleScreen(_game)); };

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