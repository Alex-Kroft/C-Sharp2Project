using System;
using System.Collections.Generic;
using System.Text;
using AgeOfEmpires.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace AgeOfEmpires.Systems
{
    //This system responsible for drawing the entities
    class RenderSystem : EntityDrawSystem
    {
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;
        private ComponentMapper<HealthPoints> _healthPointsMapper;

        public RenderSystem(GraphicsDevice graphicsDevice) 
            : base(Aspect.All(typeof(HealthPoints))) 
        {
            _graphicsDevice = graphicsDevice;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _healthPointsMapper = mapperService.GetMapper<HealthPoints>();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            foreach (var entity in ActiveEntities)
            {
                // draw your entities
            }

            _spriteBatch.End();
        }

        
    }
}
