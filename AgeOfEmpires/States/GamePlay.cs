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
        public  static OrthographicCamera _camera;
        private World _world;
        private Vector2 _cameraPosition;

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

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
        }

        public override void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);
            const float movementSpeed = 800;
            _camera.Move(GetMovementDirection() * movementSpeed * gameTime.GetElapsedSeconds());
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
