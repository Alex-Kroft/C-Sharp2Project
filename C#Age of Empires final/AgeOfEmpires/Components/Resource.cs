using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Components
{
    class Resource
    {
        private int wood = 0;
        private int stone = 0;
        private int gold = 0;
        private int food = 0;

        public Resource() { }

        public void setWood(int wood)
        {
            this.wood = wood;
        }

        public void setStone(int stone)
        {
            this.stone = stone;
        }

        public void setGold(int gold)
        {
            this.gold = gold;
        }

        public void setFood(int food)
        {
            this.food = food;
        }

        public int getWood()
        {
            return this.wood;
        }

        public void addWood(int wood)
        {
            this.wood += wood;
        }

        public int getStone()
        {
            return this.stone;
        }

        public void addStone(int stone)
        {
            this.stone += stone;
        }

        public int getGold()
        {
            return this.gold;
        }

        public void addGold(int gold)
        {
            this.gold += gold;
        }

        public int getFood()
        {
            return this.food;
        }

        public void addFood(int food)
        {
            this.food += food;
        }

    }
}
