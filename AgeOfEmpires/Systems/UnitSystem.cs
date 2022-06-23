using AgeOfEmpires.Components;
using AgeOfEmpires.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input.InputListeners;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace AgeOfEmpires.Systems
{
    //This system responsible for the Unit game logic
    class UnitSystem : EntityUpdateSystem
    {

        private Game1 Game;
        
        private ComponentMapper<HealthPoints> _healthPointsMapper;
        private ComponentMapper<Position> _positionMapper;
        private ComponentMapper<Components.Size> _sizeMapper;
        private ComponentMapper<Level> _levelMapper;
        private ComponentMapper<Combat> _meleeAttackMapper;
        private ComponentMapper<Grinding> _grindingMapper;
        private ComponentMapper<Movement> _movementMapper;
        private ComponentMapper<Resource> _resourceMapper;
        private ComponentMapper<Skin> _skinMapper;
        private ComponentMapper<UnitDistance> _unitDistance;
        private int selectedEntity = -1;
        private int focusEntity = -1;
        public static float deltaSeconds;

        public UnitSystem(Game1 game)
            : base(Aspect.All(typeof(UnitDistance)))
        {
            Game = game;
            
        }
        public override void Initialize(IComponentMapperService mapperService)
        {
            _healthPointsMapper = mapperService.GetMapper<HealthPoints>();
            _positionMapper = mapperService.GetMapper<Position>();
            _sizeMapper = mapperService.GetMapper<Components.Size>();
            _levelMapper = mapperService.GetMapper<Level>();
            _meleeAttackMapper = mapperService.GetMapper<Combat>();
            _grindingMapper = mapperService.GetMapper<Grinding>();
            _movementMapper = mapperService.GetMapper<Movement>();
            _resourceMapper = mapperService.GetMapper<Resource>();
            _skinMapper = mapperService.GetMapper<Skin>();
            _unitDistance = mapperService.GetMapper<UnitDistance>();

            
            Game.mouseListener.MouseClicked += (sender, args) => {
                //select unit
                if (args.Button == MonoGame.Extended.Input.MouseButton.Left)
                {
                    Vector2 clickWorldPos = GamePlay._camera.ScreenToWorld(args.Position.ToVector2());
                    foreach (var entity in ActiveEntities)
                    {
                        var position = _positionMapper.Get(entity);
                        var size = _sizeMapper.Get(entity);
                       
                        if (Vector2.Distance(position.VectorPosition, clickWorldPos)<= size.EntityRadius) {
                            selectedEntity = entity;
                            GamePlay._itemSelected = 2;
                        } 
                    } 
                }

                
                //move selected unit
                //attack unit
                if (args.Button == MonoGame.Extended.Input.MouseButton.Right && selectedEntity != -1)
                {
                    Vector2 clickWorldPos = GamePlay._camera.ScreenToWorld(args.Position.ToVector2());

                    //selected entity
                    var selectedPosition = _positionMapper.Get(selectedEntity);
                    var selectedMovement = _movementMapper.Get(selectedEntity);
                    var selectedSkin = _skinMapper.Get(selectedEntity);
                    var selectedUnitDistance = _unitDistance.Get(selectedEntity);
                    var melleeAttack = _meleeAttackMapper.Get(selectedEntity);

                    //if moving while combat current attack stops
                    melleeAttack.InCombat = false;

                    foreach (var entity in ActiveEntities)
                    {
                        //if (entity != selectedEntity) {
                            var position = _positionMapper.Get(entity);
                            var size = _sizeMapper.Get(entity);

                            if (Vector2.Distance(position.VectorPosition, clickWorldPos) <= size.EntityRadius && entity != selectedEntity)
                            {
                                focusEntity = entity;
                                var focusSkin = _skinMapper.Get(focusEntity);
                                var focusHealthPoints = _healthPointsMapper.Get(focusEntity);
                                //Vector2 calculatedFocusPosition = new Vector2(focusPosition.VectorPosition.X - selectedUnitDistance.AttackDistance, focusPosition.VectorPosition.Y - selectedUnitDistance.AttackDistance);
                                selectedMovement.GoSomeWhereAttack(clickWorldPos, selectedPosition, selectedSkin, selectedUnitDistance, melleeAttack, focusEntity, focusSkin, focusHealthPoints, position);
                                return;
                            }
                        //}
                        
                    }

                    selectedMovement.GoSomeWhere(clickWorldPos, selectedPosition, selectedSkin);
                }
            };
        }
        public override void Update(GameTime gameTime)
        {
            deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var entity in ActiveEntities)
            {
                var skin = _skinMapper.Get(entity);
                skin.villager.Update(deltaSeconds);
                skin.villager.Play(skin.animationName);
            }
        }

    }
}
