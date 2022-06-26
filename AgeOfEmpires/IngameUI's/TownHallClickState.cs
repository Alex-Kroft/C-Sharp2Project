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

namespace AgeOfEmpires.IngameUI_s
{
    public class TownHallClickState : Component
    {
        private GraphicsDevice graphicsDevice;
        private Texture2D _buttonContainer;
        private Texture2D _newVillager;
        private Texture2D _upgrade;

        private MouseState _previousMouse;

        private MouseState _currentMouse;

        private bool isHoveringNewVillager;
        private bool isHoveringUpgrade;

        private SpriteFont SpriteFont;

        private Texture2D _health;
        private Texture2D _level;

        private int Health;
        public static int Level;
        private int OverallHealth;

        public static bool upgradeOn;

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
            Level = level;
        }

        public int getLevel()
        {
            return Level;
        }

        public void setOverallHealth(int overallhealth)
        {
            this.OverallHealth = overallhealth;
        }

        public int getOverallHealth()
        {
            return this.OverallHealth;
        }

        public Rectangle RectangleNewVillager
        {
            get
            {
                return new Rectangle(_buttonContainer.Width / 6, _buttonContainer.Height + 450, 70, 70);
            }
        }
        public Rectangle RectangleUpgrade
        {
            get
            {
                return new Rectangle(_buttonContainer.Width / 2, _buttonContainer.Height + 450, 70, 70);
            }
        }

        public TownHallClickState(GraphicsDevice graphicsDevice, Texture2D buttonConatiner, Texture2D newVillager, Texture2D upgrade, Texture2D health, Texture2D level, SpriteFont spriteFont)
        {
            this._buttonContainer = buttonConatiner;

            this.graphicsDevice = graphicsDevice;
            this._newVillager = newVillager;
            this._upgrade = upgrade;
            this._health = health;
            this._level = level;
            this.SpriteFont = spriteFont;
            this.Health = 0;
            Level = 1;
            this.OverallHealth = 0;
            upgradeOn = false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var buttonColourNewVillager = Color.White;
            var buttonColourUpgrade = Color.White;
            
            if (isHoveringNewVillager)
            {
                buttonColourNewVillager = Color.Gray;
            }
            if (isHoveringUpgrade)
            {
                buttonColourUpgrade = Color.Gray;
            }
         

            spriteBatch.Draw(_buttonContainer, new Rectangle(0, graphicsDevice.Adapter.CurrentDisplayMode.Height - _buttonContainer.Height + 60, _buttonContainer.Width - 50, _buttonContainer.Height - 80), Color.White);
            spriteBatch.Draw(_newVillager, RectangleNewVillager, buttonColourNewVillager);
            spriteBatch.Draw(_upgrade, RectangleUpgrade, buttonColourUpgrade);
            spriteBatch.Draw(_health, new Rectangle(_buttonContainer.Width / 6, _buttonContainer.Height + 550, 40, 40), Color.White);
            spriteBatch.DrawString(this.SpriteFont, $"{Health}/{OverallHealth}", new Vector2((_buttonContainer.Width / 4) - 5, _buttonContainer.Height + 550), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(_level, new Rectangle((_buttonContainer.Width / 3) + 60, _buttonContainer.Height + 550, 40, 40), Color.White);
            spriteBatch.DrawString(this.SpriteFont, $"{Level}/4", new Vector2((_buttonContainer.Width / 3) + 120, _buttonContainer.Height + 550), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.1f);
        }

        public override void Update(GameTime gameTime)
        {
            
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            isHoveringNewVillager = false;
            isHoveringUpgrade = false;

            if (mouseRectangle.Intersects(RectangleNewVillager))
            {
                isHoveringNewVillager = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    GamePlay.characterTobeDeployed = "peasant";
                }
            }

            if (mouseRectangle.Intersects(RectangleUpgrade))
            {
                isHoveringUpgrade = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    upgradeOn = true;
                }
            }

            
        }
    }
}
