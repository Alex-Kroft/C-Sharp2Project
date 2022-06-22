using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        //Go somewhere
        public void GoSomeWhere(Vector2 destination, Components.Position entityPosition, Skin skin) {
            Vector2 dir = destination - entityPosition.VectorPosition;
            Debug.WriteLine(dir);
            Debug.WriteLine(entityPosition.VectorPosition);
            if (dir.X < 0 && dir.Y < 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "run";
                    do
                    {
                        entityPosition.VectorPosition += dir * 1f;
                        Debug.WriteLine("Current: " + entityPosition.VectorPosition);
                        Debug.WriteLine("Dir: " + dir);
                        Debug.WriteLine("Mouse : " + destination);
                        Thread.Sleep(Time);

                    } while (entityPosition.VectorPosition.X - destination.X > 0.5f && entityPosition.VectorPosition.Y - destination.Y > 0.5f);
                    skin.animationName = "attack";
                }));
                newThread.Start();
            }
            else if (dir.X > 0 && dir.Y > 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "run";
                    do
                    {
                        entityPosition.VectorPosition += dir * 1f;
                        Debug.WriteLine("Current: " + entityPosition.VectorPosition);
                        Debug.WriteLine("Dir: " + dir);
                        Debug.WriteLine("Mouse : " + destination);
                        Thread.Sleep(Time);

                    } while (destination.X - entityPosition.VectorPosition.X > 0.5f && destination.Y - entityPosition.VectorPosition.Y > 0.5f);
                    skin.animationName = "attack";
                }));
                newThread.Start();
            } else if (dir.X > 0 && dir.Y < 0) {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "run";
                    do
                    {
                        entityPosition.VectorPosition += dir * 1f;
                        Debug.WriteLine("Current: " + entityPosition.VectorPosition);
                        Debug.WriteLine("Dir: " + dir);
                        Debug.WriteLine("Mouse : " + destination);
                        Thread.Sleep(Time);

                    } while (destination.X - entityPosition.VectorPosition.X > 0.5f &&  entityPosition.VectorPosition.Y -destination.Y> 0.5f);
                    skin.animationName = "attack";
                }));
                newThread.Start();
            }
            else if (dir.X < 0 && dir.Y > 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "run";
                    do
                    {
                        entityPosition.VectorPosition += dir * 1f;
                        Debug.WriteLine("Current: " + entityPosition.VectorPosition);
                        Debug.WriteLine("Dir: " + dir);
                        Debug.WriteLine("Mouse : " + destination);
                        Thread.Sleep(Time);

                    } while (entityPosition.VectorPosition.X - destination.X > 0.5f && destination.Y - entityPosition.VectorPosition.Y > 0.5f);
                    skin.animationName = "attack";
                }));
                newThread.Start();
            }else if (dir.X == 0 && dir.Y < 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "run";
                    do
                    {
                        entityPosition.VectorPosition.Y += dir.Y * 1f;
                        Debug.WriteLine("Current: " + entityPosition.VectorPosition);
                        Debug.WriteLine("Dir: " + dir);
                        Debug.WriteLine("Mouse : " + destination);
                        Thread.Sleep(Time);

                    } while (entityPosition.VectorPosition.Y - destination.Y > 0.5f);
                    skin.animationName = "attack";
                }));
                newThread.Start();
            }else if (dir.X < 0 && dir.Y == 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "run";
                    do
                    {
                        entityPosition.VectorPosition.X += dir.X * 1f;
                        Debug.WriteLine("Current: " + entityPosition.VectorPosition);
                        Debug.WriteLine("Dir: " + dir);
                        Debug.WriteLine("Mouse : " + destination);
                        Thread.Sleep(Time);

                    } while (entityPosition.VectorPosition.X - destination.X > 0.5f);
                    skin.animationName = "attack";
                }));
                newThread.Start();
            }


        }
        //Go somewhere and attack
        public void GoSomeWhere(Vector2 destination, Components.Position entityPosition, Skin skin, UnitDistance unitDistance)
        {
            Vector2 dir = destination - entityPosition.VectorPosition;
            Debug.WriteLine(dir);
            Debug.WriteLine(entityPosition.VectorPosition);
            if (dir.X < 0 && dir.Y < 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "run";
                    do
                    {
                        entityPosition.VectorPosition += dir * 1f;
                        Debug.WriteLine("Current: " + entityPosition.VectorPosition);
                        Debug.WriteLine("Dir: " + dir);
                        Debug.WriteLine("Mouse : " + destination);
                        Thread.Sleep(Time);

                    } while (entityPosition.VectorPosition.X - (destination.X+unitDistance.AttackDistance) > 0.5f && entityPosition.VectorPosition.Y - (destination.Y+unitDistance.AttackDistance) > 0.5f);
                    skin.animationName = "attack";
                }));
                newThread.Start();
            }
            else if (dir.X > 0 && dir.Y > 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "run";
                    do
                    {
                        entityPosition.VectorPosition += dir * 1f;
                        Debug.WriteLine("Current: " + entityPosition.VectorPosition);
                        Debug.WriteLine("Dir: " + dir);
                        Debug.WriteLine("Mouse : " + destination);
                        Thread.Sleep(Time);

                    } while ((destination.X-unitDistance.AttackDistance) - entityPosition.VectorPosition.X > 0.5f && (destination.Y-unitDistance.AttackDistance) - entityPosition.VectorPosition.Y > 0.5f);
                    skin.animationName = "attack";
                }));
                newThread.Start();
            }
            else if (dir.X > 0 && dir.Y < 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "run";
                    do
                    {
                        entityPosition.VectorPosition += dir * 1f;
                        Debug.WriteLine("Current: " + entityPosition.VectorPosition);
                        Debug.WriteLine("Dir: " + dir);
                        Debug.WriteLine("Mouse : " + destination);
                        Thread.Sleep(Time);

                    } while ((destination.X - unitDistance.AttackDistance) - entityPosition.VectorPosition.X > 0.5f && entityPosition.VectorPosition.Y - (destination.Y + unitDistance.AttackDistance) > 0.5f);
                    skin.animationName = "attack";
                }));
                newThread.Start();
            }
            else if (dir.X < 0 && dir.Y > 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "run";
                    do
                    {
                        entityPosition.VectorPosition += dir * 1f;
                        Debug.WriteLine("Current: " + entityPosition.VectorPosition);
                        Debug.WriteLine("Dir: " + dir);
                        Debug.WriteLine("Mouse : " + destination);
                        Thread.Sleep(Time);

                    } while (entityPosition.VectorPosition.X - (destination.X + unitDistance.AttackDistance) > 0.5f && (destination.Y - unitDistance.AttackDistance) - entityPosition.VectorPosition.Y > 0.5f);
                    skin.animationName = "attack";
                }));
                newThread.Start();
            }
            else if (dir.X == 0 && dir.Y < 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "run";
                    do
                    {
                        entityPosition.VectorPosition.Y += dir.Y * 1f;
                        Debug.WriteLine("Current: " + entityPosition.VectorPosition);
                        Debug.WriteLine("Dir: " + dir);
                        Debug.WriteLine("Mouse : " + destination);
                        Thread.Sleep(Time);

                    } while (entityPosition.VectorPosition.Y - (destination.Y + unitDistance.AttackDistance) > 0.5f);
                    skin.animationName = "attack";
                }));
                newThread.Start();
            }
            else if (dir.X < 0 && dir.Y == 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "run";
                    do
                    {
                        entityPosition.VectorPosition.X += dir.X * 1f;
                        Debug.WriteLine("Current: " + entityPosition.VectorPosition);
                        Debug.WriteLine("Dir: " + dir);
                        Debug.WriteLine("Mouse : " + destination);
                        Thread.Sleep(Time);

                    } while (entityPosition.VectorPosition.X - (destination.X + unitDistance.AttackDistance) > 0.5f);
                    skin.animationName = "attack";
                }));
                newThread.Start();
            }


        }
    }
}
