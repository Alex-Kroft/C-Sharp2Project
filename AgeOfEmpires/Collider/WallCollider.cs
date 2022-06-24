using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Collider
{
    public class WallCollider : ICollisionActor
    {
        public IShapeF Bounds { get; set; }
        public void OnCollision(CollisionEventArgs collisionInfo)
        {
        }

        public WallCollider(RectangleF rectangleF)
        {
            Bounds = rectangleF;
        }
    }
}
