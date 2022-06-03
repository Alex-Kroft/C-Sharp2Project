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
        private Texture2D font;

        


        public MainMenu(Game1 game) : base(game) 
        {
            _components = new List<Component>();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            fontCoord = new Vector2(0, 0);

            
        }

        public override void LoadContent()
        {
            
            button = Content.Load<Texture2D>("Controls/Button");
            font2 = Content.Load<SpriteFont>("Fonts/Font2");
            font = Content.Load<Texture2D>("Fonts/Font");
            
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
            _spriteBatch.Draw(font, fontCoord, Color.White);
            foreach (var component in _components)
            {
                component.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();
           


        }

       
    }
}
