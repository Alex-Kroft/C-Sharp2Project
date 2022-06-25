using System;
using System.Collections.Generic;
using System.Text;
using AgeOfEmpires.Components;
using AgeOfEmpires.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace AgeOfEmpires.Systems
{
    //This system responsible for drawing the entities
    class RenderSystem : EntityDrawSystem
    {
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;
        private ComponentMapper<HealthPoints> _healthPointsMapper;
        private ComponentMapper<Skin> _skinMapper;
        private ComponentMapper<Position> _positionMapper;
        private ComponentMapper<BuildingSkin> _buildingSkinMapper;

        private GamePlay _gamePlay;
        

        //private AnimatedSprite villager;
        //private Vector2 spritePosition;

        public RenderSystem(GraphicsDevice graphicsDevice, GamePlay gamePlay) 
            : base(Aspect.One(typeof(Skin),typeof(BuildingSkin))) 
        {
            _graphicsDevice = graphicsDevice;
            _gamePlay = gamePlay;
            _spriteBatch = _gamePlay._spriteBatch;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {

            _healthPointsMapper = mapperService.GetMapper<HealthPoints>();
            _skinMapper = mapperService.GetMapper<Skin>();
            _positionMapper = mapperService.GetMapper<Position>();
            _buildingSkinMapper = mapperService.GetMapper<BuildingSkin>();
        }

        public override void Draw(GameTime gameTime)
        {
            var transformMatrix = GamePlay._camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);

            foreach (var entity in ActiveEntities)
            {
                
                var skin = _skinMapper.Get(entity);
                var position = _positionMapper.Get(entity);

                var buildingSkin = _buildingSkinMapper.Get(entity);
                if (skin == null)
                {
                    _spriteBatch.Draw(buildingSkin.skin, position.VectorPosition, Color.White);
                }
                else
                {
                    _spriteBatch.Draw(skin.villager, position.VectorPosition);
                }
                
                

            }

            _spriteBatch.End();

            
        }


    }
}
