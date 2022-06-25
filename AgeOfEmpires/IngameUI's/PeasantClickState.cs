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
using System.Threading;

namespace AgeOfEmpires.IngameUI_s
{
    class PeasantClickState : Component
    {
        private GraphicsDevice graphicsDevice;
        private Texture2D _buttonContainer;

        private Texture2D _buildHouse;
        private Texture2D _buildBarracks;
        private Texture2D _buildFarm;

        private MouseState _previousMouse;

        private MouseState _currentMouse;

        private bool isHoveringHouse;
        private bool isHoveringBarracks;
        private bool isHoveringFarm;

        private SpriteFont SpriteFont;

        private Texture2D _health;
        private Texture2D _level;

        private int Health;
        private int Level;
        private int OverallHealth;

        public event EventHandler Click;
        
        public void setHealth(int health)
        {
            this.Health = health;
        }

        public int getHealth()
        {
            return this.Health;
        }

        public void setLevel(int level)
        {
            this.Level = level;
        }

        public int getLevel()
        {
            return this.Level;
        }

        public void setOverallHealth(int overallhealth)
        {
            this.OverallHealth = overallhealth;
        }

        public int getOverallHealth()
        {
            return this.OverallHealth;
        }

        public Rectangle RectangleBuildHouse
        {
            get
            {
                return new Rectangle(_buttonContainer.Width / 6, _buttonContainer.Height + 450, 70, 70);
            }
        }
        public Rectangle RectangleBuildbarracks
        {
            get
            {
                return new Rectangle(_buttonContainer.Width / 2, _buttonContainer.Height + 450, 70, 70);
            }
        }
        public Rectangle RectangleBuildFarm
        {
            get
            {
                return new Rectangle(_buttonContainer.Width / 3, _buttonContainer.Height + 450, 70, 70);
            }
        }

        public PeasantClickState(GraphicsDevice graphicsDevice, Texture2D buttonConatiner, Texture2D buildHouse, Texture2D buildBarracks, Texture2D buildFarm, Texture2D health, Texture2D level, SpriteFont spriteFont)
        {
            this._buttonContainer = buttonConatiner;
            this.graphicsDevice = graphicsDevice;
            this._buildHouse = buildHouse;
            this._buildBarracks = buildBarracks;
            this._buildFarm = buildFarm;
            this._health = health;
            this._level = level;
            this.SpriteFont = spriteFont;
            this.Health = 0;
            this.Level = 1;
            this.OverallHealth = 0;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var buttonColourHouse = Color.White;
            var buttonColourFarm = Color.White;
            var buttonColourBarracks = Color.White;
            if (isHoveringHouse)
            {
                buttonColourHouse = Color.Gray;
            }
            if (isHoveringFarm)
            {
                buttonColourFarm = Color.Gray;
            }
            if (isHoveringBarracks)
            {
                buttonColourBarracks = Color.Gray;
            }

            spriteBatch.Draw(_buttonContainer, new Rectangle(0, graphicsDevice.Adapter.CurrentDisplayMode.Height - _buttonContainer.Height + 60, _buttonContainer.Width - 50, _buttonContainer.Height - 80), Color.White);
            spriteBatch.Draw(_buildHouse, RectangleBuildHouse, buttonColourHouse);
            spriteBatch.Draw(_buildFarm, RectangleBuildFarm, buttonColourFarm);
            spriteBatch.Draw(_buildBarracks, RectangleBuildbarracks, buttonColourBarracks);
            spriteBatch.Draw(_health, new Rectangle(_buttonContainer.Width / 6, _buttonContainer.Height + 550, 40, 40), Color.White);
            spriteBatch.DrawString(this.SpriteFont, $"{Health}/{OverallHealth}", new Vector2((_buttonContainer.Width / 4) + 10, _buttonContainer.Height + 550), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(_level, new Rectangle((_buttonContainer.Width / 3) + 60, _buttonContainer.Height + 550, 40, 40), Color.White);
            spriteBatch.DrawString(this.SpriteFont, $"{Level}/5", new Vector2((_buttonContainer.Width / 3) + 120, _buttonContainer.Height + 550), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.1f);

        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            isHoveringHouse = false;
            isHoveringFarm = false;
            isHoveringBarracks = false;

            if(GamePlay._itemSelected == 2)
            {
                if (mouseRectangle.Intersects(RectangleBuildHouse))
                {
                    isHoveringHouse = true;
                    // If a click is needed for an update here
                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        GamePlay.buildingToBeConstructed = "building";

                        Thread newThread = new Thread(new ThreadStart(() =>
                        {
                            while (GamePlay.buildingToBeConstructed != null)
                            {

                                Debug.WriteLine(GamePlay.buildingToBeConstructed + "");
                                Thread.Sleep(1000);
                            }

                        }));
                        newThread.Start();
                    }
                }
                if (mouseRectangle.Intersects(RectangleBuildFarm))
                {
                    isHoveringFarm = true;

                    // If a click is needed for an update here
                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        GamePlay.buildingToBeConstructed = "farm";

                        Thread newThread = new Thread(new ThreadStart(() =>
                        {
                            while (GamePlay.buildingToBeConstructed != null)
                            {

                                Debug.WriteLine(GamePlay.buildingToBeConstructed + "");
                                Thread.Sleep(1000);
                            }

                        }));
                        newThread.Start();
                    }
                }
                if (mouseRectangle.Intersects(RectangleBuildbarracks))
                {
                    isHoveringBarracks = true;

                    // If a click is needed for an update here
                    if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    {
                        if (GamePlay.barackCounter == 0) {
                            GamePlay.buildingToBeConstructed = "barrack";
                            GamePlay.barackCounter++;
                            Thread newThread = new Thread(new ThreadStart(() =>
                            {
                                while (GamePlay.buildingToBeConstructed != null)
                                {

                                    Debug.WriteLine(GamePlay.buildingToBeConstructed + "");
                                    Thread.Sleep(1000);
                                }

                            }));
                            newThread.Start();
                        }
                        
                    }
                }

                //if backspace then drop action
                if (Keyboard.GetState().IsKeyDown(Keys.Back) && GamePlay.buildingToBeConstructed != null)
                {
                    GamePlay.buildingToBeConstructed = null;
                }
            }
        }
    }
}
