using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Components
{
    class Resource
    {
        int wood = 0;
        int stone = 0;
        int gold = 0;
        int food = 0;

        public Resource() { 

        }

        public int getWood()
        {
            return this.wood;
        }

        public void setWood(int wood)
        {
            this.wood = wood;
        }

        public int getStone()
        {
            return this.stone;
        }

        public void setStone(int stone)
        {
            this.stone = stone;
        }

        public int getGold()
        {
            return this.gold;
        }

        public void setGold(int gold)
        {
            this.gold = gold;
        }

        public int getFood()
        {
            return this.food;
        }

        public void setFood(int food)
        {
            this.food = food;
        }

    }
}
