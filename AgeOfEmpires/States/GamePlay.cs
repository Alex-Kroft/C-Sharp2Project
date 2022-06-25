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
        public static TiledMap _tiledMap;
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

        public static Resource Resource;

        public static int noOfHouses;

        public static String mouseTaken = null;

        // 1 = none, 2 = villager, 3 = army
        public static int _itemSelected;
        private List<Component> _uiComponents;
        private NoClickState noClickState;
        private VillagerClickState VillagerClickState;
        private BarracksClickState BarracksClickState;
        

        
        private Texture2D _buildFarm;
        private Texture2D _buildHouse;
        private Texture2D _buildBarracks;

        private Texture2D _barbarian;
        private Texture2D _archer;
        private Texture2D _swordMan;
       

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
            _itemSelected = 1;
            noOfHouses = 0;
        }

        

       

        public override void Initialize()
        {


            //camera
            var viewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, 4096, 2160);
            _camera = new OrthographicCamera(viewportAdapter);
            _cameraPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 + 100);

            Mouse.SetCursor(MouseCursor.FromTexture2D(Content.Load<Texture2D>("Cursor"), 0,0));

            _uiComponents = new List<Component>();

            //creating world
            _world = new WorldBuilder()
                .AddSystem(new RenderSystem(GraphicsDevice, this))
                .AddSystem(new UnitSystem(baseGame))
                 .Build();

            //test entity
            var entity = _world.CreateEntity();
            entity.Attach(new Skin(baseGame.Content, "idle", "BluePeasant.sf"));
            entity.Attach(new HealthPoints(100));
            entity.Attach(new Level());
            entity.Attach(new Combat(20, 1100));
            entity.Attach(new Position(new Vector2(2300, 1400)));
            entity.Attach(new UnitDistance(10, 50));
            entity.Attach(new Movement(100));
            entity.Attach(new Grinding());
            entity.Attach(new Components.Size(64));

            //test enemy
            var enemy = _world.CreateEntity();
            enemy.Attach(new Skin(baseGame.Content, "idle", "BlueArcher.sf"));
            enemy.Attach(new HealthPoints(100));
            enemy.Attach(new Level());
            enemy.Attach(new Combat(5, 1100));
            enemy.Attach(new Position(new Vector2(2400, 1500)));
            enemy.Attach(new UnitDistance(10, 50));
            enemy.Attach(new Movement(10));
            enemy.Attach(new Components.Size(64));

            

            baseGame.Components.Add(_world);

            Resource = new Resource();

            base.Initialize();
        }

        public override void LoadContent()
        {
            //tiledmap
            _tiledMap = Content.Load<TiledMap>("map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

           // Loading all textures from pipeline
            _resourcesCover = Content.Load<Texture2D>("resource-panel_full");
            _buttonContainer = Content.Load<Texture2D>("command-panel_collapsed");
            _bottomBar = Content.Load<Texture2D>("bottombar");
            _miniMap = Content.Load<Texture2D>("miniMap");
            _miniMapCam = Content.Load<Texture2D>("miniMapRect");
            _miniMapCamPos = new Vector2(GraphicsDevice.Adapter.CurrentDisplayMode.Width - _miniMap.Width + 40, GraphicsDevice.Adapter.CurrentDisplayMode.Height - _miniMap.Height + 60);
            _fontResources = Content.Load<SpriteFont>("Gold");
            _age = Content.Load<Texture2D>("shield_dark_age_slav_normal");
            _villagersCount = Content.Load<Texture2D>("villagers");

            _buildHouse = Content.Load<Texture2D>("CR-004");
            _buildFarm = Content.Load<Texture2D>("CR-005");
            _buildBarracks = Content.Load<Texture2D>("CR-010");

            _barbarian = Content.Load<Texture2D>("008_50730");
            _archer = Content.Load<Texture2D>("017_50730");
            _swordMan = Content.Load<Texture2D>("012_50730");

            // init the click states for when a villager or building is clicked
            noClickState = new NoClickState(GraphicsDevice, _buttonContainer);
            VillagerClickState = new VillagerClickState(GraphicsDevice,_buttonContainer,_buildHouse,_buildBarracks,_buildFarm);
            this.BarracksClickState = new BarracksClickState(GraphicsDevice, _buttonContainer, _barbarian, _archer, _swordMan);
            // adding the states to the LIST
            _uiComponents.Add(noClickState);
            _uiComponents.Add(VillagerClickState);
            _uiComponents.Add(this.BarracksClickState);
          


            base.LoadContent();
        }

       

        public override void Draw(GameTime gameTime)
        {
            // Drawing all the loaded textures and putting a rectangle around them for collision and size handling. The rectangle is then given coordinates of where it should be
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
            _spriteBatch.Begin();
            _spriteBatch.Draw(_bottomBar, new Rectangle(_buttonContainer.Width -100, GraphicsDevice.Adapter.CurrentDisplayMode.Height - _bottomBar.Height, _bottomBar.Width, _bottomBar.Height), Color.White);
            _spriteBatch.Draw(_miniMap, new Rectangle((GraphicsDevice.Adapter.CurrentDisplayMode.Width - _miniMap.Width), (GraphicsDevice.Adapter.CurrentDisplayMode.Height - _miniMap.Height), _miniMap.Width, _miniMap.Height), Color.White);
            _spriteBatch.Draw(_resourcesCover, new Rectangle(0, 0, _resourcesCover.Width, _resourcesCover.Height), Color.White);
            _spriteBatch.Draw(_miniMapCam, new Rectangle((int)_miniMapCamPos.X, (int)_miniMapCamPos.Y, (GraphicsDevice.Adapter.CurrentDisplayMode.Width) / 30, (GraphicsDevice.Adapter.CurrentDisplayMode.Height) / 30), Color.White);
            _spriteBatch.DrawString(_fontResources, Resource.getFood().ToString(), new Vector2((_resourcesCover.Width)/14, _resourcesCover.Height / 5), Color.White, 0, new Vector2(0,0),2.0f,SpriteEffects.None, 0.1f);
            _spriteBatch.DrawString(_fontResources, Resource.getWood().ToString(), new Vector2((_resourcesCover.Width) / 6, _resourcesCover.Height / 5), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.1f);
            _spriteBatch.DrawString(_fontResources, Resource.getGold().ToString(), new Vector2(((_resourcesCover.Width) / 4) + 50, _resourcesCover.Height / 5), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.1f);
            _spriteBatch.DrawString(_fontResources, Resource.getStone().ToString(), new Vector2(((_resourcesCover.Width) / 3) + 80, _resourcesCover.Height / 5), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.1f);
            _spriteBatch.Draw(_age, new Rectangle(_resourcesCover.Width / 2, 0, _age.Width, _age.Height), Color.White);
            _spriteBatch.Draw(_villagersCount, new Rectangle((_resourcesCover.Width / 2) - 160, _resourcesCover.Height / 7, _villagersCount.Width + 35, _villagersCount.Height + 20), Color.White);
            _spriteBatch.DrawString(_fontResources, $"0/{noOfHouses*10}", new Vector2((_resourcesCover.Width / 2) - 70, _resourcesCover.Height / 5), Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0.1f);
           
            // Looping through all the click states to change the state depending on the item selected number
                foreach (var component in _uiComponents)
                {
                    switch (_itemSelected)
                    {
                        case 1:
                            if (component.Equals(noClickState))
                            {
                                component.Draw(gameTime, _spriteBatch);
                            }
                            break;
                        case 2:
                            if (component.Equals(VillagerClickState))
                            {
                                component.Draw(gameTime, _spriteBatch);
                            }
                            break;
                        case 3:
                            if (component.Equals(this.BarracksClickState))
                            {
                                component.Draw(gameTime, _spriteBatch);
                            }
                            break;
                    }

                }
           
           
            
            _spriteBatch.End();

            
        }

        public override void Update(GameTime gameTime)
        {
            // getting the state of the mouse
            var state = Mouse.GetState();
            // converting the camera coordinates to the world coordinates
            var position = _camera.ScreenToWorld(new Vector2(state.X, state.Y));
            // updating the tilemap renderer with the gametime
            _tiledMapRenderer.Update(gameTime);
            // setting the movement speed
            const float movementSpeed = 1600;
            // checking the mouse position to move the camera in the direction
            if (position.X > 50)
            {
                if(position.X < 20000)
                {
                    if (position.Y > 50)
                    {
                        if(position.Y < 20000)
                        {
                            // moving the camera and moving the minimap
                            _camera.Move(GetMovementDirection() * movementSpeed * gameTime.GetElapsedSeconds());
                            _miniMapCamPos.X = _miniMapCamPos.X + ((GetMovementDirection().X * movementSpeed * gameTime.GetElapsedSeconds()) / 53);
                            _miniMapCamPos.Y = _miniMapCamPos.Y + ((GetMovementDirection().Y * movementSpeed * gameTime.GetElapsedSeconds()) / 95);
                        }
                        
                    }
                }
            }
            // calling the update in all the ui states
            foreach (var component in _uiComponents)
            {
                component.Update(gameTime);
            }



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
