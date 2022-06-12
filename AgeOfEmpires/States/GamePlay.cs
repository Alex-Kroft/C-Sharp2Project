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
using AgeOfEmpires.Systems;
using MonoGame.Extended.Entities.Systems;
using AgeOfEmpires.Components;

namespace AgeOfEmpires.States
{
    class GamePlay : GameScreen
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        public static OrthographicCamera _camera;
        private World _world;
        private Vector2 _cameraPosition;

       
        private Texture2D _resourcesCover;
        private Texture2D _buttonContainer;
        private Texture2D _bottomBar;
        private Texture2D _miniMap;
        private Texture2D _miniMapCam;
        private Vector2 _miniMapCamPos;

        private Game1 baseGame;

        public GamePlay(Game1 game) : base(game)
        {
            baseGame = game;
            _graphics = game._graphics;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            if (GraphicsDevice == null) { _graphics.ApplyChanges(); }
            _graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();
        }

        public override void Initialize()
        {


            //camera
            var viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, 1920, 1280);
            _camera = new OrthographicCamera(viewportAdapter);
            _cameraPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 + 100);


            //creating world
            _world = new WorldBuilder()
                .AddSystem(new RenderSystem(GraphicsDevice, this))
                .AddSystem(new UnitSystem(baseGame))
                 .Build();

            var entity = _world.CreateEntity();
            entity.Attach(new Skin(baseGame.Content));
            entity.Attach(new HealthPoints(100));
            entity.Attach(new Level());
            entity.Attach(new MeleeAttack(5, 1.1F));
            entity.Attach(new Position(new Vector2(0, 0)));
            entity.Attach(new UnitDistance(10, 5));
            entity.Attach(new Components.Size(64));
            baseGame.Components.Add(_world);
            base.Initialize();
        }

        public override void LoadContent()
        {
            //tiledmap
            _tiledMap = Content.Load<TiledMap>("editedTilesSet");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

           
            _resourcesCover = Content.Load<Texture2D>("resource-panel_full");
            _buttonContainer = Content.Load<Texture2D>("command-panel_collapsed");
            _bottomBar = Content.Load<Texture2D>("bottombar");
            _miniMap = Content.Load<Texture2D>("miniMap");
            _miniMapCam = Content.Load<Texture2D>("miniMapRect");
            _miniMapCamPos = new Vector2(GraphicsDevice.Adapter.CurrentDisplayMode.Width - _miniMap.Width + 40, GraphicsDevice.Adapter.CurrentDisplayMode.Height - _miniMap.Height + 60);




            base.LoadContent();
        }

       

        public override void Draw(GameTime gameTime)
        {

            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
            _spriteBatch.Begin();
            _spriteBatch.Draw(_bottomBar, new Rectangle(_buttonContainer.Width -100, GraphicsDevice.Adapter.CurrentDisplayMode.Height - _bottomBar.Height, _bottomBar.Width, _bottomBar.Height), Color.White);
            _spriteBatch.Draw(_miniMap, new Rectangle((GraphicsDevice.Adapter.CurrentDisplayMode.Width - _miniMap.Width), (GraphicsDevice.Adapter.CurrentDisplayMode.Height - _miniMap.Height), _miniMap.Width, _miniMap.Height), Color.White);
            _spriteBatch.Draw(_resourcesCover, new Rectangle(0, 0, _resourcesCover.Width, _resourcesCover.Height), Color.White);
            _spriteBatch.Draw(_buttonContainer, new Rectangle(0, GraphicsDevice.Adapter.CurrentDisplayMode.Height - _buttonContainer.Height, _buttonContainer.Width, _buttonContainer.Height), Color.White);
            _spriteBatch.Draw(_miniMapCam, new Rectangle((int)_miniMapCamPos.X, (int) _miniMapCamPos.Y, (GraphicsDevice.Adapter.CurrentDisplayMode.Width) / 12, (GraphicsDevice.Adapter.CurrentDisplayMode.Height) / 12), Color.White);
            _spriteBatch.End();

            
        }

        public override void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);
            const float movementSpeed = 800;
            _camera.Move(GetMovementDirection() * movementSpeed * gameTime.GetElapsedSeconds());

            _miniMapCamPos.X = _miniMapCamPos.X + ((GetMovementDirection().X * movementSpeed * gameTime.GetElapsedSeconds()) / 9);
            _miniMapCamPos.Y = _miniMapCamPos.Y + ((GetMovementDirection().Y * movementSpeed * gameTime.GetElapsedSeconds()) / 9);
        }

        private void MoveCamera(GameTime gameTime)
        {
            var speed = 800;
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
                movementDirection -= Vector2.UnitX;
            }
            if (state.X > GraphicsDevice.Adapter.CurrentDisplayMode.Width - 50)
            {
                movementDirection += Vector2.UnitX;
            }
            if (state.Y < 50)
            {
                movementDirection -= Vector2.UnitY;
            }
            if (state.Y > GraphicsDevice.Adapter.CurrentDisplayMode.Height - 50)
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
