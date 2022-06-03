using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace AgeOfEmpires.States
{
    class GamePlay : GameScreen
    {
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

        private new Game1 Game => (Game1)base.Game;



        public GamePlay(Game1 game) : base(game)
        {
            
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
