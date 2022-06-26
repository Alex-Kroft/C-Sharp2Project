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
        private GamePlay _gamePlay;
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;

        private ComponentMapper<HealthPoints> _healthPointsMapper;
        private ComponentMapper<Skin> _skinMapper;
        private ComponentMapper<Position> _positionMapper;
        private ComponentMapper<BuildingSkin> _buildingSkinMapper;
        private ComponentMapper<BuildingArea> _buildingAreamapper;
        private ComponentMapper<Size> _sizeMapper;

        public RenderSystem(GraphicsDevice graphicsDevice, GamePlay gamePlay)
            : base(Aspect.One(typeof(Skin), typeof(BuildingSkin)))
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
            _buildingAreamapper = mapperService.GetMapper<BuildingArea>();
            _sizeMapper = mapperService.GetMapper<Size>();
        }

        public override void Draw(GameTime gameTime)
        {
            var transformMatrix = GamePlay._camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);

            foreach (var entity in ActiveEntities)
            {
                
                var skin = _skinMapper.Get(entity);
                var position = _positionMapper.Get(entity);
                var buildingArea = _buildingAreamapper.Get(entity);
                var size = _sizeMapper.Get(entity);
                var buildingSkin = _buildingSkinMapper.Get(entity);
                if (skin == null)
                {
                    //drawing building
                    _spriteBatch.Draw(buildingSkin.skin,new Vector2(position.VectorPosition.X - buildingArea.Radius, position.VectorPosition.Y - buildingArea.Radius), Color.White);
                }
                else
                {
                    //drawing unit
                    _spriteBatch.Draw(skin.unit,position.VectorPosition);
                }
            }
            _spriteBatch.End();
        }
    }
}
