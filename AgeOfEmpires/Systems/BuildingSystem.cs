using AgeOfEmpires.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Systems
{
    //This system responsible for the Building game logic
    class BuildingSystem : EntityUpdateSystem
    {
        private ComponentMapper<HealthPoints> _healthPointsMapper;
        private ComponentMapper<Position> _positionMapper;
        private ComponentMapper<Size> _sizeMapper;
        private ComponentMapper<Level> _levelMapper;

        public BuildingSystem()
            : base(Aspect.All(typeof(BuildingArea)))
        { }
        public override void Initialize(IComponentMapperService mapperService)
        {
            _healthPointsMapper = mapperService.GetMapper<HealthPoints>();
            _positionMapper = mapperService.GetMapper<Position>();
            _sizeMapper = mapperService.GetMapper<Size>();
            _levelMapper = mapperService.GetMapper<Level>();
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
