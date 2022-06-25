using AgeOfEmpires.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace AgeOfEmpires.IngameUI_s
{
    class BarracksClickState : Component
    {
        private GraphicsDevice graphicsDevice;
        private Texture2D _buttonContainer;

        private Texture2D _barbarian;
        private Texture2D _archer;
        private Texture2D _swordman;

        private bool isHoveringBarbarian;
        private bool isHoveringArcher;
        private bool isHoveringSwordMan;

        private MouseState _previousMouse;

        private MouseState _currentMouse;

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Rectangle RectangleBarbarian
        {
            get
            {
                return new Rectangle(_buttonContainer.Width / 6, _buttonContainer.Height + 450, 70, 70);
            }
        }
        public Rectangle RectangleArcher
        {
            get
            {
                return new Rectangle(_buttonContainer.Width / 2, _buttonContainer.Height + 450, 70, 70);
            }
        }
        public Rectangle RectangleSwordMan
        {
            get
            {
                return new Rectangle(_buttonContainer.Width / 3, _buttonContainer.Height + 450, 70, 70);
            }
        }
        public BarracksClickState(GraphicsDevice graphicsDevice, Texture2D buttonContainer, Texture2D barbarian, Texture2D archer, Texture2D swordman)
        {
            this.graphicsDevice = graphicsDevice;
            this._buttonContainer = buttonContainer;
            this._barbarian = barbarian;
            this._archer = archer;
            this._swordman = swordman;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var buttonColourBarbarian = Color.White;
            var buttonColourArcher = Color.White;
            var buttonColourSwordMan = Color.White;
            if (isHoveringBarbarian)
            {
                buttonColourBarbarian = Color.Gray;
            }
            if (isHoveringArcher)
            {
                buttonColourArcher = Color.Gray;
            }
            if (isHoveringSwordMan)
            {
                buttonColourSwordMan = Color.Gray;
            }
            spriteBatch.Draw(_buttonContainer, new Rectangle(0, graphicsDevice.Adapter.CurrentDisplayMode.Height - _buttonContainer.Height + 60, _buttonContainer.Width - 50, _buttonContainer.Height - 80), Color.White);
            spriteBatch.Draw(_barbarian, RectangleBarbarian, buttonColourBarbarian);
            spriteBatch.Draw(_archer, RectangleArcher, buttonColourArcher);
            spriteBatch.Draw(_swordman, RectangleSwordMan, buttonColourSwordMan);
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            isHoveringBarbarian = false;
            isHoveringArcher = false;
            isHoveringSwordMan = false;

            if (GamePlay._itemSelected == 3) {
                if (mouseRectangle.Intersects(RectangleBarbarian))
                {
                    isHoveringBarbarian = true;
                    // If a click is needed for an update here
                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        GamePlay.characterTobeDeployed = "barbarian";

                        Thread newThread = new Thread(new ThreadStart(() =>
                        {
                            while (GamePlay.buildingToBeConstructed != null)
                            {

                                Debug.WriteLine(GamePlay.characterTobeDeployed + "");
                                Thread.Sleep(1000);
                            }

                        }));
                        newThread.Start();
                    }
                }
                if (mouseRectangle.Intersects(RectangleArcher))
                {
                    isHoveringArcher = true;
                    // If a click is needed for an update here
                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        GamePlay.characterTobeDeployed = "archer";

                        Thread newThread = new Thread(new ThreadStart(() =>
                        {
                            while (GamePlay.buildingToBeConstructed != null)
                            {

                                Debug.WriteLine(GamePlay.characterTobeDeployed + "");
                                Thread.Sleep(1000);
                            }

                        }));
                        newThread.Start();
                    }
                }
                if (mouseRectangle.Intersects(RectangleSwordMan))
                {
                    isHoveringSwordMan = true;
                    // If a click is needed for an update here
                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        GamePlay.characterTobeDeployed = "swordman";

                        Thread newThread = new Thread(new ThreadStart(() =>
                        {
                            while (GamePlay.buildingToBeConstructed != null)
                            {

                                Debug.WriteLine(GamePlay.characterTobeDeployed + "");
                                Thread.Sleep(1000);
                            }

                        }));
                        newThread.Start();
                    }
                }
                //if backspace then drop action
                if (Keyboard.GetState().IsKeyDown(Keys.Back) && GamePlay.buildingToBeConstructed != null)
                {
                    GamePlay.characterTobeDeployed = null;
                }
            }
            
        }
    }
}
