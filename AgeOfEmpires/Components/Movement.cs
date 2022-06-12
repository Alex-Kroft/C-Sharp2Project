using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AgeOfEmpires.Components
{
    class Movement
    {
        public int Time { get; set; }

        public Movement(int time) {
            Time = time;
        }

        public void GoSomeWhere(Vector2 destination, Components.Position entityPosition) {
            Vector2 dir = destination - entityPosition.VectorPosition;

            dir.Normalize();
            Thread newThread = new Thread(new ThreadStart(() => {
                do
                {
                    entityPosition.VectorPosition += dir * 1f;
                    Thread.Sleep(Time);

                } while (destination.X - entityPosition.VectorPosition.X > 0.5f && destination.Y - entityPosition.VectorPosition.Y > 0.5f);
            }));
            newThread.Start();
        }
    }
}
