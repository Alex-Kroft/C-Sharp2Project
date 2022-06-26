using AgeOfEmpires.Components;
using AgeOfEmpires.States;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private ComponentMapper<UnitCreation> _unitCreationMapper;
        private ComponentMapper<Identifier> _identifierMapper;

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
            _unitCreationMapper = mapperService.GetMapper<UnitCreation>();
            _identifierMapper = mapperService.GetMapper<Identifier>();
        }

        public override void Update(GameTime gameTime)
        {
            Game.mouseListener.MouseClicked += (sender, args) => {
                if (args.Button == MonoGame.Extended.Input.MouseButton.Left && GamePlay.characterTobeDeployed == null)
                {
                    //Position of the click
                    Vector2 clickWorldPos = GamePlay._camera.ScreenToWorld(args.Position.ToVector2());

                    foreach (var entity in ActiveEntities) {
                        var unitCreation = _unitCreationMapper.Get(entity);
                        var position = _positionMapper.Get(entity);
                        var buildingArea = _buildingAreaMapper.Get(entity);
                        var Identifier = _identifierMapper.Get(entity);
                        var health = _healthPointsMapper.Get(entity);
                        if (unitCreation != null && buildingArea != null && Identifier.getIdentity() == "townhall")
                        {
                            if (Vector2.Distance(position.VectorPosition, clickWorldPos) <= buildingArea.Radius)
                            {
                                selectedBuilding = entity;
                                UnitSystem.selectedUnit = -1;
                                GamePlay._itemSelected = 5;
                                GamePlay.TownHallClickState.setOverallHealth(500);
                                GamePlay.TownHallClickState.setHealth(health.Hp);
                                return;
                            }
                        }
                        if (unitCreation != null && buildingArea != null) {
                            if (Vector2.Distance(position.VectorPosition, clickWorldPos) <= buildingArea.Radius) {
                                selectedBuilding = entity;
                                UnitSystem.selectedUnit = -1;
                                GamePlay._itemSelected = 3;
                                return;
                            }
                            
                        }
                    }
                }

                if (args.Button == MonoGame.Extended.Input.MouseButton.Right && GamePlay.characterTobeDeployed !=null) {
                    Debug.WriteLine(GamePlay.characterTobeDeployed);
                    //Position of the click
                    Vector2 clickWorldPos = GamePlay._camera.ScreenToWorld(args.Position.ToVector2());

                    var unitCreation = _unitCreationMapper.Get(selectedBuilding);

                    unitCreation.CreateUnit(GamePlay.characterTobeDeployed, clickWorldPos);
                }
            };
        }
    }
}
