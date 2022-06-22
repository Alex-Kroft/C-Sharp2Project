using AgeOfEmpires.States;
using AgeOfEmpires.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AgeOfEmpires.Components
{
    class MeleeAttack
    {
        public int Damage { get; set; }
        public int Time { get; set; }
        public bool InCombat = false; 
        public MeleeAttack(int damage, int time) {
            Damage = damage;
            Time = time;
        }

        public void attack(Skin selectedSkin, int entityID, Skin focusEntity, HealthPoints focusHealthPoints) {
            InCombat = true;
            do
            {
                selectedSkin.animationName = "attack";
                focusHealthPoints.Hp -= Damage;
                Thread.Sleep(Time);
            } while (focusHealthPoints.Hp > 0 && InCombat );
            selectedSkin.animationName = "idle";
            if (focusHealthPoints.Hp <= 0) {
                focusEntity.animationName = "dead";
                Thread.Sleep(700); //To play the animation
                GamePlay._world.DestroyEntity(entityID);
            }
            
        }
    }
}
