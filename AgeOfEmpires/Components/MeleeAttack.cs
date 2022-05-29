using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Components
{
    class MeleeAttack
    {
        int Damage;
        float Time;

        public MeleeAttack(int damage, float time) {
            Damage = damage;
            Time = time;
        }

        protected void attack(int entityID) { }
    }
}
