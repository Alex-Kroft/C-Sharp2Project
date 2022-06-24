using AgeOfEmpires.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AgeOfEmpires.Components
{
    class Grinding
    {

        private bool inGrinding = false;
        private int speed = 1100;
        private Skin Skin { get; set; }
        public Grinding() {
            
        }
        public void setInGrinding(Skin skin)
        {
            Skin = skin;
            this.inGrinding = false;
        }

        public void grindWood() {
            inGrinding = true;
            while (inGrinding) {
                GamePlay.Resource.addWood(10);
                Thread.Sleep(speed);
            }
            Skin.animationName = "idle";
        }

        public void grindWoodSmall()
        {
            inGrinding = true;
            while (inGrinding)
            {
                GamePlay.Resource.addWood(5);
                Thread.Sleep(speed);
            }
            Skin.animationName = "idle";
        }

        public void grindStone() {
            inGrinding = true;
            while (inGrinding) {
                GamePlay.Resource.addStone(10);
                Thread.Sleep(speed);
            }
            Skin.animationName = "idle";
        }
        public void grindGold() {
            inGrinding = true;
            while(inGrinding) {
                GamePlay.Resource.addGold(10);
                Thread.Sleep(speed);
            }
            Skin.animationName = "idle";
        }
        public void collectFood() {
            inGrinding = true;
            while (inGrinding) {
                GamePlay.Resource.addFood(10);
                Thread.Sleep(speed);
            }
            Skin.animationName = "idle";
        }
    }
}
