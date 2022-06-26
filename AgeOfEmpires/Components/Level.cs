using AgeOfEmpires.IngameUI_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Components
{
    class Level
    {

        public int level;
        public Level() { }

        public void upgradeHP(HealthPoints hp) {
            
            
            hp.Hp += 25;
            hp.TotalHP += 25;
            
        }
        public void upgradeCombat( Combat combat)
        {
            
            
            combat.Damage += 5;
        }
    }
}
