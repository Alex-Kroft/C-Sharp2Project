using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgeOfEmpires.Collider
{
    public static class GetWallCollider
    {
        public static List<ICollisionActor> GetCollisionActors(this MonoGame.Extended.Tiled.TiledMap map)
        {
            List<ICollisionActor> Result = new List<ICollisionActor>();

            int tiles_x = map.Width;
            int tiles_y = map.Height;
            int pixel_x = map.WidthInPixels;
            int pixel_y = map.HeightInPixels;

            List<(TiledMapTilesetTile, int start)> TileDefition = new List<(TiledMapTilesetTile, int start)>();

            int globalCount = 1;

            foreach (var tileset in map.Tilesets)
            {
                foreach (var tile in tileset.Tiles)
                {
                    TileDefition.Add((tile, globalCount));
                }
                globalCount += tileset.TileCount;
            }

            foreach (var layer in map.TileLayers)
            {
                foreach (var tile in layer.Tiles)
                {
                    var defintion = TileDefition
                        .Where(x => x.Item1.LocalTileIdentifier + x.start == tile.GlobalIdentifier)
                        .ToList();

                    foreach (var d in defintion)
                    {
                        foreach (var o in d.Item1.Objects)
                        {
                            if (o.Type == "WallCollider" && o is TiledMapRectangleObject)
                            {
                                var def = o as TiledMapRectangleObject;
                                RectangleF rect = new RectangleF(
                                    tile.X * layer.TileWidth + def.Position.X,
                                    tile.Y * layer.TileHeight + def.Position.Y,
                                    def.Size.Width,
                                    def.Size.Height
                                    );

                                Result.Add(new WallCollider(rect));
                            }
                        }
                    }
                }
            }
            return Result;
        }
    }
}
