using AgeOfEmpires;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;
using System.Diagnostics;
using AgeOfEmpires.States;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using AgeOfEmpires.Controls;

namespace AgeOfEmpires.States
{
    public class MainMenu : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        private List<Component> _components;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Vector2 fontCoord;
        private Texture2D button;
        private SpriteFont font2;
        private Texture2D Background;

        public MainMenu(Game1 game) : base(game) 
        {
            _components = new List<Component>();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            fontCoord = new Vector2(0,0);

            
        }

        public override void LoadContent()
        {
            button = Content.Load<Texture2D>("Controls/Button");
            font2 = Content.Load<SpriteFont>("Fonts/Font2");
            Background = Content.Load<Texture2D>("mainmenu_bg");

            var newGameButton = new Button(button, font2)
            {
                Position = new Vector2(GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2, GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2),
            };
            newGameButton.Click += NewGameButton_Click;

            _components = new List<Component>(){newGameButton};
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(Background, new Rectangle((int)fontCoord.X,(int)fontCoord.Y, GraphicsDevice.Adapter.CurrentDisplayMode.Width, GraphicsDevice.Adapter.CurrentDisplayMode.Height), Color.White);
            
            foreach (var component in _components)
            {
                component.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            Game.LoadGamePlay();
        }


    }
}
