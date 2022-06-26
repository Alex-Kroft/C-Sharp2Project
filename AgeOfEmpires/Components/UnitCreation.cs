using AgeOfEmpires.States;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Components
{
    class UnitCreation
    {
        public UnitCreation() { 
        
        }

        public void CreateUnit(String type, Vector2 position) {
            if (GamePlay.Resource.getFood() >= 5) {
                if (type.Equals("swordman"))
                {
                    var entity = GamePlay._world.CreateEntity();
                    entity.Attach(new Skin(GamePlay.baseGame.Content, "idle", "BlueSwordsman.sf"));
                    entity.Attach(new HealthPoints(150));
                    entity.Attach(new Level());
                    entity.Attach(new Combat(25, 1100));
                    entity.Attach(new Position(position));
                    entity.Attach(new UnitDistance(10, 25));
                    entity.Attach(new Movement(15));
                    entity.Attach(new Components.Size(64));
                    entity.Attach(new Faction("blue"));
                    GamePlay.characterTobeDeployed = null;
                    GamePlay.Resource.setFood(GamePlay.Resource.getFood() - 5);
                    return;
                }
                if (type.Equals("archer"))
                {
                    var entity = GamePlay._world.CreateEntity();
                    entity.Attach(new Skin(GamePlay.baseGame.Content, "idle", "BlueArcher.sf"));
                    entity.Attach(new HealthPoints(90));
                    entity.Attach(new Level());
                    entity.Attach(new Combat(15, 800));
                    entity.Attach(new Position(position));
                    entity.Attach(new UnitDistance(10, 70));
                    entity.Attach(new Movement(10));
                    entity.Attach(new Components.Size(64));
                    entity.Attach(new Faction("blue"));
                    GamePlay.characterTobeDeployed = null;
                    GamePlay.Resource.setFood(GamePlay.Resource.getFood() - 5);
                    return;
                }
                if (type.Equals("barbarian"))
                {
                    var entity = GamePlay._world.CreateEntity();
                    entity.Attach(new Skin(GamePlay.baseGame.Content, "idle", "BLueBarbarian.sf"));
                    entity.Attach(new HealthPoints(120));
                    entity.Attach(new Level());
                    entity.Attach(new Combat(25, 800));
                    entity.Attach(new Position(position));
                    entity.Attach(new UnitDistance(10, 25));
                    entity.Attach(new Movement(10));
                    entity.Attach(new Components.Size(64));
                    entity.Attach(new Faction("blue"));
                    GamePlay.characterTobeDeployed = null;
                    return;
                }
                if (type.Equals("peasant"))
                {
                    var entity = GamePlay._world.CreateEntity();
                    entity.Attach(new Skin(GamePlay.baseGame.Content, "idle", "BluePeasant.sf"));
                    entity.Attach(new HealthPoints(70));
                    entity.Attach(new Level());
                    entity.Attach(new Position(position));
                    entity.Attach(new UnitDistance(10, 25));
                    entity.Attach(new Movement(10));
                    entity.Attach(new Grinding());
                    entity.Attach(new Components.Size(64));
                    entity.Attach(new Faction("blue"));
                    GamePlay.characterTobeDeployed = null;
                    GamePlay.Resource.setFood(GamePlay.Resource.getFood() - 5);
                    return;
                }
            }
        }
    }
}
