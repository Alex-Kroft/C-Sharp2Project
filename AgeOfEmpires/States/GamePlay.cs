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
using AgeOfEmpires.Characters;

using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;

namespace AgeOfEmpires.States
{
    class GamePlay : GameScreen
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

        private OrthographicCamera _camera;

        private AnimatedSprite villager;
        private Vector2 spritePosition;


        private Vector2 _cameraPosition;




        private new Game1 Game => (Game1)base.Game;



        public GamePlay(Game1 game) : base(game)
        {
            _graphics = game._graphics;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            if (GraphicsDevice == null) { _graphics.ApplyChanges(); }
            _graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();
        }

        public override void Initialize()
        {
           
            base.Initialize();
            var viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, 800, 480);
            _camera = new OrthographicCamera(viewportAdapter);
            spritePosition = new Vector2(0, 0);
            _cameraPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 + 100);
        }

        public override void LoadContent()
        {

            _tiledMap = Content.Load<TiledMap>("editedTilesSet");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            var spriteSheet = Content.Load<SpriteSheet>("LightBandit.sf", new JsonContentLoader());
            
            var sprite = new AnimatedSprite(spriteSheet);


            sprite.Play("idle");
            villager = sprite;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw();
            var position1 = new Vector2(0, 0);
            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            _spriteBatch.DrawRectangle(new RectangleF(250, 250, 50, 50), Color.Black, 1f);
            _spriteBatch.Draw(villager, spritePosition);
            _spriteBatch.End();

            
        }

        public override void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);
            const float movementSpeed = 200;
            _camera.Move(GetMovementDirection() * movementSpeed * gameTime.GetElapsedSeconds());

            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            villager.Update(deltaSeconds);
            villager.Play("idle");


        }

        private void MoveCamera(GameTime gameTime)
        {
            var speed = 400;
            var seconds = gameTime.GetElapsedSeconds();
            var movementDirection = GetMovementDirection();
            
            _cameraPosition += speed * movementDirection * seconds;
            Debug.WriteLine(movementDirection);
        }

        private Vector2 GetMovementDirection()
        {
            var movementDirection = Vector2.Zero;

            var state = Mouse.GetState();
            if (state.X < 50)
            {
                movementDirection += Vector2.UnitX;
            }
            if (state.X > GraphicsDevice.Adapter.CurrentDisplayMode.Width - 50)
            {
                movementDirection -= Vector2.UnitX;
            }
            if (state.Y < 50)
            {
                movementDirection += Vector2.UnitY;
            }
            if (state.Y > GraphicsDevice.Adapter.CurrentDisplayMode.Height - 50)
            {
                movementDirection -= Vector2.UnitY;
            }

            //Can't normalize the zero vector so test for it before normalizing
            if (movementDirection != Vector2.Zero)
            {
                movementDirection.Normalize();
            }

            return movementDirection;
        }


    }
}
