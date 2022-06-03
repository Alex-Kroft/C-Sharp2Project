using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Components
{
    class UnitDistance
    {
        float LineOfSight;
        float AttackDistance;
        public UnitDistance(float lineOfSight, float attackDistance) {
            LineOfSight = lineOfSight;
            AttackDistance = attackDistance;
        }
    }
}
