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

namespace AgeOfEmpires
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private OrthographicCamera _camera;
        private Vector2 _cameraPosition;


        private readonly ScreenManager _screenManager;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            if (GraphicsDevice == null) { _graphics.ApplyChanges(); }
            _graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();

            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

       
        protected override void Initialize()
        {

            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 600);
            _camera = new OrthographicCamera(viewportadapter);
            _cameraPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 + 100);
            base.Initialize();
            LoadMainMenu();
        }

        protected override void LoadContent()
        {
     
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime)
        {
            _camera.LookAt(_cameraPosition);
            MoveCamera(gameTime);

            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
            {
                LoadGamePlay();
            }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        private void LoadMainMenu()
        {
            _screenManager.LoadScreen(new MainMenu(this));
        }

        private void LoadGamePlay()
        {
            _screenManager.LoadScreen(new GamePlay(this));
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
            if (state.X < 50)
            {
                movementDirection -= Vector2.UnitX;
            }
            if (state.X > _graphics.PreferredBackBufferWidth - 50)
            {
                movementDirection += Vector2.UnitX;
            }
            if (state.Y < 50)
            {
                movementDirection -= Vector2.UnitY;
            }
            if (state.Y > _graphics.PreferredBackBufferHeight - 50)
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


    }
}
