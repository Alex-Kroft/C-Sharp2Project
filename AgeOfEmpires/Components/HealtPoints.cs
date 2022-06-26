using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Components
{
    class HealthPoints
    {

        public int Hp;
        public int TotalHP;

        public HealthPoints(int healthpoints) {
            Hp = healthpoints;
            TotalHP = healthpoints;
        }
    }
}
