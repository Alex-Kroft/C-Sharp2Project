using AgeOfEmpires.Components;
using AgeOfEmpires.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Tiled;
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
                //select unit -- left click
                if (args.Button == MonoGame.Extended.Input.MouseButton.Left && GamePlay.mouseTaken == null)
                {
                    Vector2 clickWorldPos = GamePlay._camera.ScreenToWorld(args.Position.ToVector2());
                    foreach (var entity in ActiveEntities)
                    {
                        var position = _positionMapper.Get(entity);
                        var size = _sizeMapper.Get(entity);
                        var grinding = _grindingMapper.Get(entity);
                       
                        if (Vector2.Distance(position.VectorPosition, clickWorldPos)<= size.EntityRadius) {
                            selectedEntity = entity;
                            if (grinding != null) {
                                GamePlay._itemSelected = 2;
                                return;
                            }
                            GamePlay._itemSelected = 3;
                            return;
                        }
                    }
                    
                    
                }

                if (args.Button == MonoGame.Extended.Input.MouseButton.Right && selectedEntity != -1 && GamePlay.mouseTaken != null)
                {
                    Vector2 clickWorldPos = GamePlay._camera.ScreenToWorld(args.Position.ToVector2());

                    //Create building entity here
                    //check building type with mouseTaken variable
                    if (GamePlay.mouseTaken == "building")
                    {
                        if (GamePlay.Resource.getWood() >=20)
                        {
                            var house = GamePlay._world.CreateEntity();
                            house.Attach(new HealthPoints(200));
                            house.Attach(new Position(clickWorldPos));
                            house.Attach(new Components.Size(64));
                            house.Attach(new Level());
                            house.Attach(new BuildingSkin(Game.Content.Load<Texture2D>(GamePlay.mouseTaken)));
                            GamePlay.Resource.setWood(GamePlay.Resource.getWood() - 20);
                            GamePlay.noOfHouses = GamePlay.noOfHouses + 1;
                        }
                       
                    }
                    if (GamePlay.mouseTaken == "farm")
                    {
                        if(GamePlay.Resource.getWood() >= 30 && GamePlay.Resource.getGold() >= 10)
                        {
                            var farm = GamePlay._world.CreateEntity();
                            farm.Attach(new HealthPoints(50));
                            farm.Attach(new Position(clickWorldPos));
                            farm.Attach(new Components.Size(64));
                            farm.Attach(new Level());
                            farm.Attach(new BuildingSkin(Game.Content.Load<Texture2D>(GamePlay.mouseTaken)));
                            GamePlay.Resource.setWood(GamePlay.Resource.getWood() - 30);
                            GamePlay.Resource.setGold(GamePlay.Resource.getGold() - 10);
                        }
                      
                    }
                    if (GamePlay.mouseTaken == "barrack")
                    {
                        if (GamePlay.Resource.getStone() >= 20 && GamePlay.Resource.getGold() >= 10 && GamePlay.Resource.getFood() >= 50)
                        {
                            var farm = GamePlay._world.CreateEntity();
                            farm.Attach(new HealthPoints(300));
                            farm.Attach(new Position(clickWorldPos));
                            farm.Attach(new Components.Size(64));
                            farm.Attach(new Level());
                            farm.Attach(new BuildingSkin(Game.Content.Load<Texture2D>(GamePlay.mouseTaken)));
                            GamePlay.Resource.setStone(GamePlay.Resource.getStone() - 20);
                            GamePlay.Resource.setGold(GamePlay.Resource.getGold() - 10);
                            GamePlay.Resource.setFood(GamePlay.Resource.getFood() - 50);
                        }
                       
                    }
                    GamePlay.mouseTaken = null;

                }


                //move selected unit -- right click
                //attack unit -- if right click is on a player attack
                if (args.Button == MonoGame.Extended.Input.MouseButton.Right && selectedEntity != -1 && GamePlay.mouseTaken == null)
                {
                    Vector2 clickWorldPos = GamePlay._camera.ScreenToWorld(args.Position.ToVector2());

                    //selected entity
                    var selectedPosition = _positionMapper.Get(selectedEntity);
                    var selectedMovement = _movementMapper.Get(selectedEntity);
                    var selectedSkin = _skinMapper.Get(selectedEntity);
                    var selectedUnitDistance = _unitDistance.Get(selectedEntity);
                    var melleeAttack = _meleeAttackMapper.Get(selectedEntity);

                    var peasantGrinding = _grindingMapper.Get(selectedEntity);

                    //if moving while combat current attack stops/current grinding
                    melleeAttack.setInCombat();
                    if (peasantGrinding != null) {
                        peasantGrinding.setInGrinding(selectedSkin);
                    }
                    

                    foreach (var entity in ActiveEntities)
                    {
                            var position = _positionMapper.Get(entity);
                            var size = _sizeMapper.Get(entity);

                            if (Vector2.Distance(position.VectorPosition, clickWorldPos) <= size.EntityRadius && entity != selectedEntity)
                            {
                                focusEntity = entity;
                                var focusSkin = _skinMapper.Get(focusEntity);
                                var focusHealthPoints = _healthPointsMapper.Get(focusEntity);
                                
                                selectedMovement.GoSomeWhereAttack(clickWorldPos, selectedPosition, selectedSkin, selectedUnitDistance, melleeAttack, focusEntity, focusSkin, focusHealthPoints, position);
                                return;
                            }
                    }
                    //No attack then just move to location
                    selectedMovement.GoSomeWhere(clickWorldPos, selectedPosition, selectedSkin, peasantGrinding);
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

        public bool block(Vector2 characterPos)
        {
            var mines = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("mines");
            var grass = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("base");
            var trees = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("trees");
            var bushes = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("bushes");
            var berryBushes = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("berryBushes");
            TiledMapTile? tile;
            int tx = (int)(characterPos.X / GamePlay._tiledMap.TileWidth);
            int ty = (int)(characterPos.Y / GamePlay._tiledMap.TileHeight);

            if (grass.TryGetTile((ushort)tx, (ushort)ty, out tile))
            {
                if (tile.Value.GlobalIdentifier == 1)
                {
                    var id = tile.Value.GlobalIdentifier;
                    return true;
                }
            }
            if (mines.TryGetTile((ushort)tx, (ushort)ty, out tile))
            {
                if (tile.Value.GlobalIdentifier == 3)
                {
                    var id = tile.Value.GlobalIdentifier;
                    return true;
                }
            }
            if (trees.TryGetTile((ushort)tx, (ushort)ty, out tile))
            {
                if (tile.Value.GlobalIdentifier == 2)
                {
                    var id = tile.Value.GlobalIdentifier;
                    return true;
                }
            }
            if (bushes.TryGetTile((ushort)tx, (ushort)ty, out tile))
            {
                if (tile.Value.GlobalIdentifier == 4)
                {
                    var id = tile.Value.GlobalIdentifier;
                    return true;
                }
            }
            if (berryBushes.TryGetTile((ushort)tx, (ushort)ty, out tile))
            {
                if (tile.Value.GlobalIdentifier == 5)
                {
                    var id = tile.Value.GlobalIdentifier;
                    return true;
                }
            }

            return false;
        }

    }
}
