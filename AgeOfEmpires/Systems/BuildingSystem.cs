using AgeOfEmpires.Components;
using AgeOfEmpires.States;
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
        private Game1 Game;
        public static int selectedBuilding = -1;

        private ComponentMapper<HealthPoints> _healthPointsMapper;
        private ComponentMapper<Position> _positionMapper;
        private ComponentMapper<BuildingArea> _buildingAreaMapper;
        private ComponentMapper<Level> _levelMapper;

        public BuildingSystem(Game1 game)
            : base(Aspect.All(typeof(BuildingArea)))
        {
            Game = game;
        }
        public override void Initialize(IComponentMapperService mapperService)
        {
            _healthPointsMapper = mapperService.GetMapper<HealthPoints>();
            _positionMapper = mapperService.GetMapper<Position>();
            _buildingAreaMapper = mapperService.GetMapper<BuildingArea>();
            _levelMapper = mapperService.GetMapper<Level>();
        }

        public override void Update(GameTime gameTime)
        {
            Game.mouseListener.MouseClicked += (sender, args) => {
                if (args.Button == MonoGame.Extended.Input.MouseButton.Left)
                {
                    foreach (var entity in ActiveEntities) { 
                        
                    }
                }
            };
        }
    }
}
