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

namespace AgeOfEmpires.States
{
    class GamePlay : GameScreen
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

       

        private new Game1 Game => (Game1)base.Game;



        public GamePlay(Game1 game) : base(game)
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Initialize()
        {
           
            base.Initialize();
        }

        public override void LoadContent()
        {

            _tiledMap = Content.Load<TiledMap>("editedTilesSet");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw();
        }

        public override void Update(GameTime gameTime)
        {
            
            
            _tiledMapRenderer.Update(gameTime);
        }

       

       
    }
}
