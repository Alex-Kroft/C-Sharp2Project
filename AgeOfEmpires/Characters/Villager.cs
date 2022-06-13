using AgeOfEmpires.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Characters
{
    class Villager
    {
        private Texture2D _texture;
        public Vector2 Position;
        private HealthPoints HealthPoints;
        private String _playerColour;

        public HealthPoints propHealthPoints { get; set; }
        public String propPlayerColour { get; set; }

        public Villager(Texture2D texture)
        {
            _texture = texture;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }

}
