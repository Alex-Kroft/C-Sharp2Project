using AgeOfEmpires.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Systems
{
    //This system responsible for the Unit game logic
    class UnitSystem : EntityUpdateSystem
    {
        private ComponentMapper<HealthPoints> _healthPointsMapper;
        private ComponentMapper<Position> _positionMapper;
        private ComponentMapper<Size> _sizeMapper;
        private ComponentMapper<Level> _levelMapper;
        private ComponentMapper<LongeRangeAttack> _longRangeAttackMapper;
        private ComponentMapper<MeleeAttack> _meleeAttackMapper;
        private ComponentMapper<Grinding> _grindingMapper;
        private ComponentMapper<Movement> _movementMapper;
        private ComponentMapper<Resource> _resourceMapper;

        public UnitSystem() 
            :base(Aspect.All(typeof(UnitDistance) ))
        { }
        public override void Initialize(IComponentMapperService mapperService)
        {
            _healthPointsMapper = mapperService.GetMapper<HealthPoints>();
            _positionMapper = mapperService.GetMapper<Position>();
            _sizeMapper = mapperService.GetMapper<Size>();
            _levelMapper = mapperService.GetMapper<Level>();
            _longRangeAttackMapper = mapperService.GetMapper<LongeRangeAttack>();
            _meleeAttackMapper = mapperService.GetMapper<MeleeAttack>();
            _grindingMapper = mapperService.GetMapper<Grinding>();
            _movementMapper = mapperService.GetMapper<Movement>();
            _resourceMapper = mapperService.GetMapper<Resource>();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
