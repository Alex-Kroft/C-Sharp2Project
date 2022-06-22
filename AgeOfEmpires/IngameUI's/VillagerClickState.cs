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
        private Texture2D _buildBuilding;

        private MouseState _previousMouse;

        private MouseState _currentMouse;

        private bool _isHovering;

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(_buttonContainer.Width / 6, _buttonContainer.Height + 380, _buildBuilding.Width - 20, _buildBuilding.Height - 20);
            }
        }

        public VillagerClickState(GraphicsDevice graphicsDevice, Texture2D buttonConatiner, Texture2D buildBuilding)
        {
            this._buttonContainer = buttonConatiner;
            this._buildBuilding = buildBuilding;
            this.graphicsDevice = graphicsDevice;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var buttonColour = Color.White;
            if (_isHovering)
            {
                buttonColour = Color.Gray;
            }

            spriteBatch.Draw(_buttonContainer, new Rectangle(0, graphicsDevice.Adapter.CurrentDisplayMode.Height - _buttonContainer.Height, _buttonContainer.Width, _buttonContainer.Height), Color.White);
            spriteBatch.Draw(_buildBuilding, Rectangle, buttonColour);

        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;
                // If a click is needed for an update here
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
}
}
