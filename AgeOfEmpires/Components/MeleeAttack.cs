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
        public float Time { get; set; }

        public MeleeAttack(int damage, float time) {
            Damage = damage;
            Time = time;
        }

        public void attack(Skin selectedSkin, int entityID, Skin focusEntity) {
            selectedSkin.animationName = "attack";
            focusEntity.animationName = "dead";
            Thread.Sleep(700); //To play the animation

            GamePlay._world.DestroyEntity(entityID);
            selectedSkin.animationName = "idle";
        }
    }
}
