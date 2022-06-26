using AgeOfEmpires.Components;
using AgeOfEmpires.IngameUI_s;
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
        private ComponentMapper<UnitDistance> _unitDistanceMapper;
        private ComponentMapper<Faction> _factionMapper;
        private ComponentMapper<Combat> _combatMapper;
        //no unit selected at the moment
        public static int selectedUnit = -1;
        //no focus unit selected at
        private int focusUnit = -1;
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
            _unitDistanceMapper = mapperService.GetMapper<UnitDistance>();
            _factionMapper = mapperService.GetMapper<Faction>();
            _combatMapper = mapperService.GetMapper<Combat>();

            
            Game.mouseListener.MouseClicked += (sender, args) => {
                //select unit -- left click
                if (args.Button == MonoGame.Extended.Input.MouseButton.Left && GamePlay.buildingToBeConstructed == null)
                {
                    Vector2 clickWorldPos = GamePlay._camera.ScreenToWorld(args.Position.ToVector2());
                    foreach (var entity in ActiveEntities)
                    {
                        var position = _positionMapper.Get(entity);
                        var size = _sizeMapper.Get(entity);
                        var grinding = _grindingMapper.Get(entity);
                       
                        if (Vector2.Distance(position.VectorPosition, clickWorldPos)<= size.EntityRadius) {
                            selectedUnit = entity;
                            BuildingSystem.selectedBuilding = -1;
                            if (grinding != null) {
                                //setting the health here for the peasant
                                GamePlay.VillagerClickState.setOverallHealth(20);
                                var health = _healthPointsMapper.Get(entity);
                                GamePlay.VillagerClickState.setHealth(health.Hp);
                                GamePlay._itemSelected = 2;
                                return;
                            }
                            var healthArmy = _healthPointsMapper.Get(entity);
                            GamePlay.UnitBuildingInfo.setHealth(healthArmy.Hp);
                            GamePlay.UnitBuildingInfo.setOverallHealth(healthArmy.TotalHP);
                            GamePlay._itemSelected = 4;
                            return;
                        }
                        GamePlay._itemSelected = 1;
                    }
                }

                //Build a building
                //Only for peasant
                if (args.Button == MonoGame.Extended.Input.MouseButton.Right && selectedUnit != -1 && GamePlay.buildingToBeConstructed != null)
                {
                    //Position of the click
                    Vector2 clickWorldPos = GamePlay._camera.ScreenToWorld(args.Position.ToVector2());

                    //Create building entity here
                    //check building type with mouseTaken variable
                    if (GamePlay.buildingToBeConstructed == "building")
                    {
                        if (GamePlay.Resource.getWood() >=20)
                        {
                            var house = GamePlay._world.CreateEntity();
                            house.Attach(new HealthPoints(200));
                            house.Attach(new Position(clickWorldPos));
                            house.Attach(new Components.Size(189));
                            house.Attach(new Level());
                            house.Attach(new BuildingSkin(Game.Content.Load<Texture2D>(GamePlay.buildingToBeConstructed)));
                            house.Attach(new Faction("blue"));
                            GamePlay.Resource.setWood(GamePlay.Resource.getWood() - 20);
                            GamePlay.noOfHouses = GamePlay.noOfHouses + 10;
                            GamePlay.buildingToBeConstructed = null;
                        }
                       
                    }
                    if (GamePlay.buildingToBeConstructed == "farm")
                    {
                        //if(GamePlay.Resource.getWood() >= 30 && GamePlay.Resource.getGold() >= 10)
                        //{
                            var farm = GamePlay._world.CreateEntity();
                            farm.Attach(new HealthPoints(50));
                            farm.Attach(new Position(clickWorldPos));
                            farm.Attach(new BuildingArea(189));
                            farm.Attach(new Level());
                            farm.Attach(new BuildingSkin(Game.Content.Load<Texture2D>(GamePlay.buildingToBeConstructed)));
                            farm.Attach(new Faction("blue"));
                            GamePlay.Resource.addFood(20);
                            GamePlay.Resource.setWood(GamePlay.Resource.getWood() - 30);
                            GamePlay.Resource.setGold(GamePlay.Resource.getGold() - 10);
                            GamePlay.buildingToBeConstructed = null;
                        //}
                      
                    }
                    if (GamePlay.buildingToBeConstructed == "barrack")
                    {
                        //if (GamePlay.Resource.getStone() >= 20 && GamePlay.Resource.getGold() >= 10 && GamePlay.Resource.getFood() >= 50)
                        //{
                            var barack = GamePlay._world.CreateEntity();
                            barack.Attach(new HealthPoints(300));
                            barack.Attach(new Position(clickWorldPos));
                            barack.Attach(new BuildingArea(189));
                            barack.Attach(new Level());
                            barack.Attach(new UnitCreation());
                            barack.Attach(new BuildingSkin(Game.Content.Load<Texture2D>(GamePlay.buildingToBeConstructed)));
                            barack.Attach(new Faction("blue"));
                            GamePlay.Resource.setStone(GamePlay.Resource.getStone() - 20);
                            GamePlay.Resource.setGold(GamePlay.Resource.getGold() - 10);
                            GamePlay.Resource.setFood(GamePlay.Resource.getFood() - 50);
                            GamePlay.buildingToBeConstructed = null;
                        //}

                    }
                    
                }

                //Perform right click event related to one of the unit
                //peasent = move/build/grind
                //archer/barbarian/swordsman = move/attack
                if (args.Button == MonoGame.Extended.Input.MouseButton.Right && selectedUnit != -1 && GamePlay.buildingToBeConstructed == null)
                {
                    Vector2 clickWorldPos = GamePlay._camera.ScreenToWorld(args.Position.ToVector2());

                    //selected entity
                    var selectedPosition = _positionMapper.Get(selectedUnit);
                    var selectedMovement = _movementMapper.Get(selectedUnit);
                    var selectedSkin = _skinMapper.Get(selectedUnit);
                    var selectedUnitDistance = _unitDistanceMapper.Get(selectedUnit);
                    var melleeAttack = _meleeAttackMapper.Get(selectedUnit);
                    var selectedFaction = _factionMapper.Get(selectedUnit);
                    var peasantGrinding = _grindingMapper.Get(selectedUnit);

                    //if moving while combat current attack/current grinding stops
                    melleeAttack.setInCombat();
                    if (peasantGrinding != null) {
                        peasantGrinding.setInGrinding(selectedSkin);
                    }

                    //Unit to be attacked
                    foreach (var entity in ActiveEntities)
                    {
                            var position = _positionMapper.Get(entity);
                            var size = _sizeMapper.Get(entity);
                            var focusFaction = _factionMapper.Get(entity);

                        if (Vector2.Distance(position.VectorPosition, clickWorldPos) <= size.EntityRadius && entity != selectedUnit)
                        {
                            focusUnit = entity;
                            var focusSkin = _skinMapper.Get(focusUnit);
                            var focusHealthPoints = _healthPointsMapper.Get(focusUnit);

                            //Attack
                            if (!selectedFaction.Name.Equals(focusFaction.Name))
                            {
                                selectedMovement.GoSomeWhereAttack(clickWorldPos, selectedPosition, selectedSkin, selectedUnitDistance, melleeAttack, focusUnit, focusSkin, focusHealthPoints, position);
                                return;
                            }
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
            int counter = 0;
            int upgradeCounter = 0;
            foreach (var entity in ActiveEntities)
            {
                var skin = _skinMapper.Get(entity);
                var faction = _factionMapper.Get(entity);
                var level = _levelMapper.Get(entity);
                var hp = _healthPointsMapper.Get(entity);
                var combat = _combatMapper.Get(entity);
                skin.unit.Update(deltaSeconds);
                skin.unit.Play(skin.animationName);
                if (faction.Name.Equals("blue")) {
                    counter++;
                    if (TownHallClickState.upgradeOn )
                    {
                        if (TownHallClickState.Level < 4)
                        {
                            
                            ++upgradeCounter;
                            
                            level.upgradeHP(hp);
                            level.upgradeCombat(combat);
                            if (upgradeCounter <= 1)
                            {
                                ++TownHallClickState.Level;
                            }
                        }
                        
                    }

                }
                GamePlay.blueEntityCounter = counter;
                
            }
            
            TownHallClickState.upgradeOn = false;

        }

        //this method checks if there is something on the tiledmap which blocks moving
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
