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
    class UnitBuildingInfo : Component
    {
        private GraphicsDevice graphicsDevice;
        private Texture2D _buttonContainer;

        private SpriteFont SpriteFont;

        private Texture2D _health;
        private Texture2D _level;

        private int Health;
        private int Level;
        private int OverallHealth;

        public void setHealth(int health)
        {
            this.Health = health;
        }

        public void setLevel(int level)
        {
            this.Level = level;
        }
        
        public void setOverallHealth(int overallHealth)
        {
            this.OverallHealth = overallHealth;
        }
        public UnitBuildingInfo(GraphicsDevice graphicsDevice, Texture2D buttonConatiner, Texture2D health, Texture2D level, SpriteFont spriteFont)
        {
            this._buttonContainer = buttonConatiner;
            this.graphicsDevice = graphicsDevice;
            this._health = health;
            this._level = level;
            this.SpriteFont = spriteFont;
            Health = 0;
            Level = 1;
            OverallHealth = 0;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_buttonContainer, new Rectangle(0, graphicsDevice.Adapter.CurrentDisplayMode.Height - _buttonContainer.Height + 60, _buttonContainer.Width - 50, _buttonContainer.Height - 80), Color.White);
            spriteBatch.Draw(_health, new Rectangle(_buttonContainer.Width / 6, _buttonContainer.Height + 450, 40, 40), Color.White);
            spriteBatch.DrawString(this.SpriteFont, $"{Health}/{OverallHealth}", new Vector2((_buttonContainer.Width / 4) + 30, _buttonContainer.Height + 450), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.1f);
            spriteBatch.Draw(_level, new Rectangle(_buttonContainer.Width / 6, _buttonContainer.Height + 520, 40, 40), Color.White);
            spriteBatch.DrawString(this.SpriteFont, $"{Level}/4", new Vector2((_buttonContainer.Width / 4) + 30, _buttonContainer.Height + 520), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.1f);

        }

        public override void Update(GameTime gameTime)
        {
            
        }

        
    }
}
