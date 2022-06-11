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
using MonoGame.Extended.Input.InputListeners;

namespace AgeOfEmpires
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;
        private readonly MouseListener _mouseListener;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _mouseListener = new MouseListener();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            if (GraphicsDevice == null) { _graphics.ApplyChanges(); }
            _graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();

            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
            Components.Add(new InputListenerComponent(this, _mouseListener));
        }

        public MouseListener mouseListener{ get { return _mouseListener; } }


        protected override void Initialize()
        {
            //on default load the menu
            LoadMainMenu();
            base.Initialize();
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            //On key pressed load the game
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
        //load menu function
        private void LoadMainMenu()
        {
            _screenManager.LoadScreen(new MainMenu(this));
        }
        //load gameplay function
        public void LoadGamePlay()
        {
            _screenManager.LoadScreen(new GamePlay(this));
        }

    }
}
