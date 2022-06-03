using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Components
{
    class Level
    {
        int level =0;

        public Level() { }

        public void upgrade() {
            level++;
        }
    }
}
