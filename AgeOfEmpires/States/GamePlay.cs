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


using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using AgeOfEmpires.Systems;
using MonoGame.Extended.Entities.Systems;
using AgeOfEmpires.Components;
using System.Collections.Generic;
using AgeOfEmpires.IngameUI_s;

namespace AgeOfEmpires.States
{
    class GamePlay : GameScreen
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;

        public  static OrthographicCamera _camera;
        public static World _world;
        private Vector2 _cameraPosition;

        public static Game1 baseGame;

        

        private Texture2D _resourcesCover;
        private Texture2D _buttonContainer;
        private Texture2D _bottomBar;
        private Texture2D _miniMap;
        private Texture2D _miniMapCam;
        private Vector2 _miniMapCamPos;
        private Texture2D _age;
        private Texture2D _villagersCount;
        private SpriteFont _fontResources;
        private Resource Resource;

        private List<Component> _uiComponents;

        private bool _currentEntityPressed;
        private Texture2D _buildBuilding;

        public Vector2 getMiniMapPos()
        {
            return this._miniMapCamPos;
        }

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

            Mouse.SetCursor(MouseCursor.FromTexture2D(Content.Load<Texture2D>("Cursor"), 0,0));

            _uiComponents = new List<Component>();

            //creating world
            _world = new WorldBuilder()
                .AddSystem(new RenderSystem(GraphicsDevice, this))
                .AddSystem(new UnitSystem(baseGame))
                 .Build();

            var entity = _world.CreateEntity();
            entity.Attach(new Skin(baseGame.Content, "idle"));
            entity.Attach(new HealthPoints(100));
            entity.Attach(new Level());
            entity.Attach(new MeleeAttack(5, 1.1F));
            entity.Attach(new Position(new Vector2(200, 200)));
            entity.Attach(new UnitDistance(10, 50));
            entity.Attach(new Movement(50));
            entity.Attach(new Components.Size(64));

            var enemy = _world.CreateEntity();
            enemy.Attach(new Skin(baseGame.Content, "idle"));
            enemy.Attach(new HealthPoints(100));
            enemy.Attach(new Level());
            enemy.Attach(new MeleeAttack(5, 1.1F));
            enemy.Attach(new Position(new Vector2(400, 400)));
            enemy.Attach(new UnitDistance(10, 5));
            enemy.Attach(new Movement(50));
            enemy.Attach(new Components.Size(64));

            baseGame.Components.Add(_world);

            Resource = new Resource();

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
            _fontResources = Content.Load<SpriteFont>("Gold");
            _age = Content.Load<Texture2D>("shield_dark_age_slav_normal");
            _villagersCount = Content.Load<Texture2D>("villagers");

            _buildBuilding = Content.Load<Texture2D>("031_");
            
            _uiComponents.Add(new NoClickState(GraphicsDevice,_buttonContainer,_buildBuilding));
          


            base.LoadContent();
        }

       

        public override void Draw(GameTime gameTime)
        {

            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
            _spriteBatch.Begin();
            _spriteBatch.Draw(_bottomBar, new Rectangle(_buttonContainer.Width -100, GraphicsDevice.Adapter.CurrentDisplayMode.Height - _bottomBar.Height, _bottomBar.Width, _bottomBar.Height), Color.White);
            _spriteBatch.Draw(_miniMap, new Rectangle((GraphicsDevice.Adapter.CurrentDisplayMode.Width - _miniMap.Width), (GraphicsDevice.Adapter.CurrentDisplayMode.Height - _miniMap.Height), _miniMap.Width, _miniMap.Height), Color.White);
            _spriteBatch.Draw(_resourcesCover, new Rectangle(0, 0, _resourcesCover.Width, _resourcesCover.Height), Color.White);
            _spriteBatch.Draw(_miniMapCam, new Rectangle((int)_miniMapCamPos.X, (int) _miniMapCamPos.Y, (GraphicsDevice.Adapter.CurrentDisplayMode.Width) / 12, (GraphicsDevice.Adapter.CurrentDisplayMode.Height) / 12), Color.White);
            _spriteBatch.DrawString(_fontResources, Resource.getFood().ToString(), new Vector2((_resourcesCover.Width)/14, _resourcesCover.Height / 5), Color.White, 0, new Vector2(0,0),2.0f,SpriteEffects.None, 0.1f);
            _spriteBatch.DrawString(_fontResources, Resource.getWood().ToString(), new Vector2((_resourcesCover.Width) / 6, _resourcesCover.Height / 5), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.1f);
            _spriteBatch.DrawString(_fontResources, Resource.getGold().ToString(), new Vector2(((_resourcesCover.Width) / 4) + 50, _resourcesCover.Height / 5), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.1f);
            _spriteBatch.DrawString(_fontResources, Resource.getStone().ToString(), new Vector2(((_resourcesCover.Width) / 3) + 80, _resourcesCover.Height / 5), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.1f);
            _spriteBatch.Draw(_age, new Rectangle(_resourcesCover.Width / 2, 0, _age.Width, _age.Height), Color.White);
            _spriteBatch.Draw(_villagersCount, new Rectangle((_resourcesCover.Width / 2) - 160, _resourcesCover.Height / 7, _villagersCount.Width + 35, _villagersCount.Height + 20), Color.White);
            _spriteBatch.DrawString(_fontResources, "0/200", new Vector2((_resourcesCover.Width / 2) - 70, _resourcesCover.Height / 5), Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0.1f);
            
            foreach (var component in _uiComponents)
            {
                component.Draw(gameTime, _spriteBatch);
            }

            _spriteBatch.End();

            
        }

        public override void Update(GameTime gameTime)
        {
           
            
            var state = Mouse.GetState();
            var position = _camera.ScreenToWorld(new Vector2(state.X, state.Y));
            _tiledMapRenderer.Update(gameTime);
            const float movementSpeed = 800;
            if (position.X > 50)
            {
                if(position.X < 3800)
                {
                    if (position.Y > 50)
                    {
                        if(position.Y < 2500)
                        {
                            _camera.Move(GetMovementDirection() * movementSpeed * gameTime.GetElapsedSeconds());
                            _miniMapCamPos.X = _miniMapCamPos.X + ((GetMovementDirection().X * movementSpeed * gameTime.GetElapsedSeconds()) / 9);
                            _miniMapCamPos.Y = _miniMapCamPos.Y + ((GetMovementDirection().Y * movementSpeed * gameTime.GetElapsedSeconds()) / 9);
                        }
                        
                    }
                }
            }

            foreach (var component in _uiComponents)
            {
                component.Update(gameTime);
            }



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
