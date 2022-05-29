using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.Extended.Entities;

namespace AgeOfEmpires.Components
{
    class LongeRangeAttack
    {

        int Damage;
        float Time;

        public LongeRangeAttack(int damage, float time)
        {
            Damage = damage;
            Time = time;
        }

        protected void attack(int entityID) { }
    }
}
