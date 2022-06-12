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
        private int selectedBuilding = -1;

        private ComponentMapper<HealthPoints> _healthPointsMapper;
        private ComponentMapper<Position> _positionMapper;
        private ComponentMapper<Size> _sizeMapper;
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
            _sizeMapper = mapperService.GetMapper<Size>();
            _levelMapper = mapperService.GetMapper<Level>();
        }

        public override void Update(GameTime gameTime)
        {
            Game.mouseListener.MouseClicked += (sender, args) => {
                if (args.Button == MonoGame.Extended.Input.MouseButton.Left)
                {
                    Vector2 clickWorldPos = GamePlay._camera.ScreenToWorld(args.Position.ToVector2());
                    foreach (var entity in ActiveEntities)
                    {
                        var position = _positionMapper.Get(entity);
                        var size = _sizeMapper.Get(entity);

                        if (Vector2.Distance(position.VectorPosition, clickWorldPos) <= size.EntityRadius)
                        {
                            selectedBuilding = entity;
                        }
                    }
                }
                if (args.Button == MonoGame.Extended.Input.MouseButton.Right && selectedEntity != -1)
                {
                    Vector2 clickWorldPos = GamePlay._camera.ScreenToWorld(args.Position.ToVector2());

                    var position = _positionMapper.Get(selectedEntity);
                    var movement = _movementMapper.Get(selectedEntity);
                    movement.GoSomeWhere(clickWorldPos, position);
                }
            };
        }
    }
}
