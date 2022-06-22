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

        public void CreateUnit(String type) {
            if (type.Equals("normal")) {
                var entity = GamePlay._world.CreateEntity();
                entity.Attach(new Skin(GamePlay.baseGame.Content, "idle"));
                entity.Attach(new HealthPoints(100));
                entity.Attach(new Level());
                entity.Attach(new MeleeAttack(5, 1100));
                entity.Attach(new Position(new Vector2(0, 0)));
                entity.Attach(new UnitDistance(10, 5));
                entity.Attach(new Movement(50));
                entity.Attach(new Components.Size(64));
            }
            if (type.Equals("archer"))
            {
            }
            if (type.Equals("barbarian"))
            {
            }
            if (type.Equals("peasant"))
            {
            }
            if (type.Equals("normal"))
            {
            }
            
        }
    }
}
