using AgeOfEmpires;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

using MonoGame.Extended.Content;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Diagnostics;

namespace AgeOfEmpires
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private OrthographicCamera _camera;
        private Vector2 _cameraPosition;

        private AnimatedSprite _LightBanditRun;
        private Vector2 _LightBanditRunPosition;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 600);
            _camera = new OrthographicCamera(viewportadapter);
            _cameraPosition = new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight/2+100);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //_tiledMap = Content.Load<TiledMap>("editedTilesSet");
            //_tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var spriteSheet = Content.Load<SpriteSheet>("LightBandit.sf", new JsonContentLoader());
            var sprite = new AnimatedSprite(spriteSheet);

            sprite.Play("run");
            _LightBanditRunPosition = new Vector2(100, 100);
            _LightBanditRun = sprite;


        }

        protected override void Update(GameTime gameTime)
        {
            //_tiledMapRenderer.Update(gameTime);

            MoveCamera(gameTime);
           _camera.LookAt(_cameraPosition);
            base.Update(gameTime);

            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var walkSpeed = deltaSeconds * 128;
            var keyboardState = Keyboard.GetState();
            var animation = "idle";

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                animation = "run";
                _LightBanditRunPosition.Y -= walkSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                animation = "run";
                _LightBanditRunPosition.Y += walkSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                animation = "injured";
                _LightBanditRunPosition.X -= walkSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                animation = "attack";
                _LightBanditRunPosition.X += walkSpeed;
            }

            _LightBanditRun.Play(animation);

           

            _LightBanditRun.Update(deltaSeconds);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //_tiledMapRenderer.Draw(_camera.GetViewMatrix());

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_LightBanditRun, _LightBanditRunPosition);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Vector2 GetMovementDirection()
        {
            var movementDirection = Vector2.Zero;
            /*var state = Keyboard.GetState();
            
            if (state.IsKeyDown(Keys.Down))
            {
                movementDirection += Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                movementDirection -= Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                movementDirection -= Vector2.UnitX;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                movementDirection += Vector2.UnitX;
            }*/

            var state = Mouse.GetState();
            Debug.WriteLine("Mouse: " + state.X);
            if (state.X < 50) {
                movementDirection -= Vector2.UnitX;
            }
            if (state.X > _graphics.PreferredBackBufferWidth - 50) {
                movementDirection += Vector2.UnitX;
            }
            if (state.Y < 50 ) {
                movementDirection -= Vector2.UnitY;
            }
            if (state.Y > _graphics.PreferredBackBufferHeight-50)
            {
                movementDirection += Vector2.UnitY;
            }

            //Can't normalize the zero vector so test for it before normalizing
            if (movementDirection != Vector2.Zero)
            {
                movementDirection.Normalize();
            }

            return movementDirection;
        }

        private void MoveCamera(GameTime gameTime)
        {
            var speed = 400;
            var seconds = gameTime.GetElapsedSeconds();
            var movementDirection = GetMovementDirection();
            _cameraPosition += speed * movementDirection * seconds;
            Debug.WriteLine(movementDirection);
        }
    }
}
