using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Components
{
    class UnitDistance
    {
        public float LineOfSight { get; set; }
        public float AttackDistance { get; set; }

        
        public UnitDistance(float lineOfSight, float attackDistance) {
            LineOfSight = lineOfSight;
            AttackDistance = attackDistance;
        }
    }
}
