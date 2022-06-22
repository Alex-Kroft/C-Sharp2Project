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
    class VillagerClickState : Component
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

        public event EventHandler Click;

        public bool Clicked { get; private set; }

       
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

        public VillagerClickState(GraphicsDevice graphicsDevice, Texture2D buttonConatiner, Texture2D buildHouse, Texture2D buildBarracks, Texture2D buildFarm)
        {
            this._buttonContainer = buttonConatiner;
            this.graphicsDevice = graphicsDevice;
            this._buildHouse = buildHouse;
            this._buildBarracks = buildBarracks;
            this._buildFarm = buildFarm;
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

            spriteBatch.Draw(_buttonContainer, new Rectangle(0, graphicsDevice.Adapter.CurrentDisplayMode.Height - _buttonContainer.Height + 60, _buttonContainer.Width - 50, _buttonContainer.Height - 80) , Color.White);
            spriteBatch.Draw(_buildHouse, RectangleBuildHouse, buttonColourHouse);
            spriteBatch.Draw(_buildFarm, RectangleBuildFarm, buttonColourFarm);
            spriteBatch.Draw(_buildBarracks, RectangleBuildbarracks, buttonColourBarracks);

        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            isHoveringHouse = false;
            isHoveringFarm = false;
            isHoveringBarracks = false;

           
            if (mouseRectangle.Intersects(RectangleBuildHouse))
            {
                isHoveringHouse = true;
                // If a click is needed for an update here
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
            if (mouseRectangle.Intersects(RectangleBuildFarm))
            {
                isHoveringFarm = true;
                // If a click is needed for an update here
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
            if (mouseRectangle.Intersects(RectangleBuildbarracks))
            {
                isHoveringBarracks = true;
                // If a click is needed for an update here
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
}
}
