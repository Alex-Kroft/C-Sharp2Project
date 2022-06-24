using AgeOfEmpires.States;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
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
        public void GoSomeWhere(Vector2 destination, Components.Position entityPosition, Skin skin, Grinding grinding) {
            Vector2 dir = destination - entityPosition.VectorPosition;
            Debug.WriteLine(dir);
            Debug.WriteLine(entityPosition.VectorPosition);
            if (dir.X < 0 && dir.Y < 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "move";
                    do
                    {
                        
                        
                            entityPosition.VectorPosition += dir * 1f;
                            Thread.Sleep(Time);
                        
                    } while (entityPosition.VectorPosition.X - destination.X > 0.5f && entityPosition.VectorPosition.Y - destination.Y > 0.5f && !block(entityPosition.VectorPosition + dir * 1f)); //move while destination or something is blocking

                    //if right click is on an object
                    peaseantGrind(destination, grinding, skin);

                    //if it is not a peasant
                    if (grinding == null)
                    {
                        skin.animationName = "idle";
                    }
                }));
                newThread.Start();
            }
            else if (dir.X > 0 && dir.Y > 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "move";
                    do
                    {
                        
                            entityPosition.VectorPosition += dir * 1f;
                            Thread.Sleep(Time);
                        

                    } while (destination.X - entityPosition.VectorPosition.X > 0.5f && destination.Y - entityPosition.VectorPosition.Y > 0.5f && !block(entityPosition.VectorPosition + dir * 1f));

                    //if right click is on an object
                    peaseantGrind(destination, grinding, skin);

                    //if it is not a peasant
                    if (grinding == null)
                    {
                        skin.animationName = "idle";
                    }


                }));
                newThread.Start();
            } else if (dir.X > 0 && dir.Y < 0) {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "move";
                    do
                    {
                        
                            entityPosition.VectorPosition += dir * 1f;
                            
                            Thread.Sleep(Time);
                        
                    } while (destination.X - entityPosition.VectorPosition.X > 0.5f &&  entityPosition.VectorPosition.Y -destination.Y> 0.5f && !block(entityPosition.VectorPosition + dir * 1f));

                    //if right click is on an object
                    peaseantGrind(destination, grinding, skin);

                    //if it is not a peasant
                    if (grinding == null)
                    {
                        skin.animationName = "idle";
                    }
                }));
                newThread.Start();
            }
            else if (dir.X < 0 && dir.Y > 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "move";
                    do
                    {
                        
                            entityPosition.VectorPosition += dir * 1f;
                            
                            Thread.Sleep(Time);
                        
                    } while (entityPosition.VectorPosition.X - destination.X > 0.5f && destination.Y - entityPosition.VectorPosition.Y > 0.5f && !block(entityPosition.VectorPosition + dir * 1f));

                    //if right click is on an object
                    peaseantGrind(destination, grinding, skin);

                    //if it is not a peasant
                    if (grinding == null)
                    {
                        skin.animationName = "idle";
                    }
                }));
                newThread.Start();
            }else if (dir.X == 0 && dir.Y < 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "move";
                    do
                    {
                        
                            entityPosition.VectorPosition.Y += dir.Y * 1f;
                            
                            Thread.Sleep(Time);
                        
                        

                    } while (entityPosition.VectorPosition.Y - destination.Y > 0.5f && !block(0f, entityPosition.VectorPosition.Y + dir.Y * 1f));

                    //if right click is on an object
                    peaseantGrind(0f, destination.Y, grinding, skin);

                    //if it is not a peasant
                    if (grinding == null)
                    {
                        skin.animationName = "idle";
                    }
                }));
                newThread.Start();
            }else if (dir.X < 0 && dir.Y == 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "move";
                    do
                    {
                        
                            entityPosition.VectorPosition.X += dir.X * 1f;
                            
                            Thread.Sleep(Time);
                        
                        

                    } while (entityPosition.VectorPosition.X - destination.X > 0.5f && !block(entityPosition.VectorPosition.X + dir.X * 1f, 0f));

                    //if right click is on an object
                    peaseantGrind(destination.X, 0f, grinding, skin);

                    //if it is not a peasant
                    if (grinding == null) {
                        skin.animationName = "idle";
                    }
                }));
                newThread.Start();
            }


        }


        //Go somewhere and attack
        public void GoSomeWhereAttack(Vector2 destination, Components.Position entityPosition, Skin skin, UnitDistance unitDistance, Combat combat, int focusEntity, Skin focusSkin, HealthPoints focusHealthPoints, Position focusPosition)
        {
            Vector2 dir = destination - entityPosition.VectorPosition;
            Debug.WriteLine(dir);
            Debug.WriteLine(entityPosition.VectorPosition);
            if (dir.X < 0 && dir.Y < 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "move";
                    do
                    {
                        if (block(entityPosition.VectorPosition + dir * 1f)) {
                            return;
                        }
                        entityPosition.VectorPosition += dir * 1f;
                        
                        Thread.Sleep(Time);

                    } while (entityPosition.VectorPosition.X - (destination.X+unitDistance.AttackDistance) > 0.5f && entityPosition.VectorPosition.Y - (destination.Y+unitDistance.AttackDistance) > 0.5f);
                    
                    //Attack enemy
                    combat.Attack(skin, focusEntity, focusSkin, focusHealthPoints, unitDistance, entityPosition, focusPosition);
                    
                }));
                newThread.Start();
            }
            else if (dir.X > 0 && dir.Y > 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "move";
                    do
                    {
                        if (block(entityPosition.VectorPosition + dir * 1f))
                        {
                            return;
                        }
                        entityPosition.VectorPosition += dir * 1f;
                        
                        Thread.Sleep(Time);

                    } while ((destination.X-unitDistance.AttackDistance) - entityPosition.VectorPosition.X > 0.5f && (destination.Y-unitDistance.AttackDistance) - entityPosition.VectorPosition.Y > 0.5f);

                    //Attack enemy
                    combat.Attack(skin, focusEntity, focusSkin, focusHealthPoints, unitDistance, entityPosition, focusPosition);
                }));
                newThread.Start();
            }
            else if (dir.X > 0 && dir.Y < 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "move";
                    do
                    {
                        if (block(entityPosition.VectorPosition + dir * 1f))
                        {
                            return;
                        }
                        entityPosition.VectorPosition += dir * 1f;
                        
                        Thread.Sleep(Time);

                    } while ((destination.X - unitDistance.AttackDistance) - entityPosition.VectorPosition.X > 0.5f && entityPosition.VectorPosition.Y - (destination.Y + unitDistance.AttackDistance) > 0.5f);

                    //Attack enemy
                    combat.Attack(skin, focusEntity, focusSkin, focusHealthPoints, unitDistance, entityPosition, focusPosition);
                }));
                newThread.Start();
            }
            else if (dir.X < 0 && dir.Y > 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "move";
                    do
                    {
                        if (block(entityPosition.VectorPosition + dir * 1f))
                        {
                            return;
                        }
                        entityPosition.VectorPosition += dir * 1f;
                        
                        Thread.Sleep(Time);

                    } while (entityPosition.VectorPosition.X - (destination.X + unitDistance.AttackDistance) > 0.5f && (destination.Y - unitDistance.AttackDistance) - entityPosition.VectorPosition.Y > 0.5f);

                    //Attack enemy
                    combat.Attack(skin, focusEntity, focusSkin, focusHealthPoints, unitDistance, entityPosition, focusPosition);
                }));
                newThread.Start();
            }
            else if (dir.X == 0 && dir.Y < 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "move";
                    do
                    {
                        if (block(0f, entityPosition.VectorPosition.Y += dir.Y * 1f))
                        {
                            return;
                        }
                        entityPosition.VectorPosition.Y += dir.Y * 1f;
                        
                        Thread.Sleep(Time);

                    } while (entityPosition.VectorPosition.Y - (destination.Y + unitDistance.AttackDistance) > 0.5f);

                    //Attack enemy
                    combat.Attack(skin, focusEntity, focusSkin, focusHealthPoints, unitDistance, entityPosition, focusPosition);
                }));
                newThread.Start();
            }
            else if (dir.X < 0 && dir.Y == 0)
            {
                dir.Normalize();

                Thread newThread = new Thread(new ThreadStart(() =>
                {
                    skin.animationName = "move";
                    do
                    {
                        if (block(entityPosition.VectorPosition.X += dir.X * 1f, 0f))
                        {
                            return;
                        }
                        entityPosition.VectorPosition.X += dir.X * 1f;
                        
                        Thread.Sleep(Time);

                    } while (entityPosition.VectorPosition.X - (destination.X + unitDistance.AttackDistance) > 0.5f);

                    //Attack enemy
                    combat.Attack(skin, focusEntity, focusSkin, focusHealthPoints, unitDistance, entityPosition, focusPosition);
                }));
                newThread.Start();
            }


        }

        public bool block(Vector2 characterPos) {
            var mines = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("mines");
            var trees = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("trees");
            var bushes = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("bushes");
            var berryBushes = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("berryBushes");
            TiledMapTile? tile;
            int tx = (int)(characterPos.X / GamePlay._tiledMap.TileWidth);
            int ty = (int)(characterPos.Y / GamePlay._tiledMap.TileHeight);

            if (mines.TryGetTile((ushort)tx, (ushort)ty, out tile))
            {
                if (tile.Value.GlobalIdentifier == 3)
                {
                    var id = tile.Value.GlobalIdentifier;
                    return true;
                }
            }
            if (trees.TryGetTile((ushort)tx, (ushort)ty, out tile))
            {
                if (tile.Value.GlobalIdentifier == 2)
                {
                    var id = tile.Value.GlobalIdentifier;
                    return true;
                }
            }
            if (bushes.TryGetTile((ushort)tx, (ushort)ty, out tile))
            {
                if (tile.Value.GlobalIdentifier == 4)
                {
                    var id = tile.Value.GlobalIdentifier;
                    return true;
                }
            }
            if (berryBushes.TryGetTile((ushort)tx, (ushort)ty, out tile))
            {
                if (tile.Value.GlobalIdentifier == 5)
                {
                    var id = tile.Value.GlobalIdentifier;
                    return true;
                }
            }

            return false;
        }

        public bool block(float x, float y)
        {
            var mines = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("mines");
            var trees = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("trees");
            var bushes = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("bushes");
            var berryBushes = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("berryBushes");
            TiledMapTile? tile;
            int tx = (int)(x / GamePlay._tiledMap.TileWidth);
            int ty = (int)(y / GamePlay._tiledMap.TileHeight);

            if (mines.TryGetTile((ushort)tx, (ushort)ty, out tile))
            {
                if (tile.Value.GlobalIdentifier == 3)
                {
                    var id = tile.Value.GlobalIdentifier;
                    return true;
                }
            }
            if (trees.TryGetTile((ushort)tx, (ushort)ty, out tile))
            {
                if (tile.Value.GlobalIdentifier == 2)
                {
                    var id = tile.Value.GlobalIdentifier;
                    return true;
                }
            }
            if (bushes.TryGetTile((ushort)tx, (ushort)ty, out tile))
            {
                if (tile.Value.GlobalIdentifier == 4)
                {
                    var id = tile.Value.GlobalIdentifier;
                    return true;
                }
            }
            if (berryBushes.TryGetTile((ushort)tx, (ushort)ty, out tile))
            {
                if (tile.Value.GlobalIdentifier == 5)
                {
                    var id = tile.Value.GlobalIdentifier;
                    return true;
                }
            }

            return false;
        }

        //normal movement
        public void peaseantGrind(Vector2 destination, Grinding grinding, Skin skin)
        {
            //if right click is on an object
            if (block(destination) && grinding != null)
            {
                var mines = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("mines");
                var trees = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("trees");
                var bushes = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("bushes");
                var berryBushes = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("berryBushes");

                //base
                //bush
                //berry
                //mine 3
                //tree
                TiledMapTile? tile;
                int tx = (int)(destination.X / GamePlay._tiledMap.TileWidth);
                int ty = (int)(destination.Y / GamePlay._tiledMap.TileHeight);
                if (mines.TryGetTile((ushort)tx, (ushort)ty, out tile))
                {
                    if (tile.Value.GlobalIdentifier == 3)
                    {
                        var id = tile.Value.GlobalIdentifier;
                        //mine
                        skin.animationName = "mine";
                        grinding.grindGold();
                        grinding.grindStone();
                    }
                }
                if (trees.TryGetTile((ushort)tx, (ushort)ty, out tile))
                {
                    if (tile.Value.GlobalIdentifier == 2)
                    {
                        var id = tile.Value.GlobalIdentifier;
                        //chop wood
                        skin.animationName = "wood";
                        grinding.grindWood();
                    }
                }
                if (bushes.TryGetTile((ushort)tx, (ushort)ty, out tile))
                {
                    if (tile.Value.GlobalIdentifier == 4)
                    {
                        var id = tile.Value.GlobalIdentifier;
                        //chop bush
                        skin.animationName = "wood";
                        grinding.grindWoodSmall();
                    }
                }
                if (berryBushes.TryGetTile((ushort)tx, (ushort)ty, out tile))
                {
                    if (tile.Value.GlobalIdentifier == 5)
                    {
                        var id = tile.Value.GlobalIdentifier;
                        //collect food
                        skin.animationName = "collect";
                        grinding.collectFood();
                    }
                }
            }
        }

        //x or y is 0
        public void peaseantGrind(float x, float y, Grinding grinding, Skin skin)
        {
            //if right click is on an object
            if (block(x,y) && grinding != null)
            {
                var mines = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("mines");
                var trees = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("trees");
                var bushes = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("bushes");
                var berryBushes = GamePlay._tiledMap.GetLayer<TiledMapTileLayer>("berryBushes");

                //base
                //bush
                //berry
                //mine 3
                //tree
                TiledMapTile? tile;
                int tx = (int)(x / GamePlay._tiledMap.TileWidth);
                int ty = (int)(y / GamePlay._tiledMap.TileHeight);
                if (mines.TryGetTile((ushort)tx, (ushort)ty, out tile))
                {
                    if (tile.Value.GlobalIdentifier == 3)
                    {
                        var id = tile.Value.GlobalIdentifier;
                        //mine
                        skin.animationName = "mine";
                        grinding.grindGold();
                        grinding.grindStone();
                    }
                }
                if (trees.TryGetTile((ushort)tx, (ushort)ty, out tile))
                {
                    if (tile.Value.GlobalIdentifier == 2)
                    {
                        var id = tile.Value.GlobalIdentifier;
                        //chop wood
                        skin.animationName = "wood";
                        grinding.grindWood();
                    }
                }
                if (bushes.TryGetTile((ushort)tx, (ushort)ty, out tile))
                {
                    if (tile.Value.GlobalIdentifier == 4)
                    {
                        var id = tile.Value.GlobalIdentifier;
                        //chop bush
                        skin.animationName = "wood";
                        grinding.grindWoodSmall();
                    }
                }
                if (berryBushes.TryGetTile((ushort)tx, (ushort)ty, out tile))
                {
                    if (tile.Value.GlobalIdentifier == 5)
                    {
                        var id = tile.Value.GlobalIdentifier;
                        //collect food
                        skin.animationName = "collect";
                        grinding.collectFood();
                    }
                }
            }
        }
    }
}
