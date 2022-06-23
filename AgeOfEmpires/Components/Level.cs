using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Components
{
    class Level
    {
        int level =1;

        public Level() { }

        public void upgrade(HealthPoints hp, Combat combat) {
            level++;
            hp.Hp += 25;
            combat.Damage += 5;
        }
    }
}
