using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tetra.Desktop
{
    public class GameLoop
    {
        public readonly QuadTree quadtree;
        public readonly World World;

        public GameLoop(World World)
        {
            quadtree = new QuadTree(new Rectangle(-11000, -7000, 23000, 15000), 50, 5);
            this.World = World;
        }

        public void Update(float elapsed)
        {
            quadtree.Clear();

            var GameObjects = World.GetObjects().ToList();

            for (int i = 0; i < GameObjects.Count; i++)
                quadtree.AddRange(GameObjects[i].GetColliders());

            foreach (var GameObject in GameObjects)
            {
                GameObject.Update.Update();

                GameObject.Position.Y += GameObject.Velocity.Y * elapsed;
                var colliders = GameObject.GetColliders();
                foreach (var collider in colliders)
                {
                    collider.BeforeCollisions.Update();

                    if (collider.IsDumb)
                        continue;

                    CheckCollisions(InternalCollisionDirection.Vertical, collider);
                }

                GameObject.Position.X += GameObject.Velocity.X * elapsed;
                foreach (var collider in colliders)
                {
                    if (collider.IsDumb)
                        continue;

                    CheckCollisions(InternalCollisionDirection.Horizontal, collider);
                }

                GameObject.Animation.Update();
            }

            quadtree.DrawDebug();
        }

        private void CheckCollisions(InternalCollisionDirection direction, Collider source)
        {
            var targets = quadtree.Get(source);

            for (int i = 0; i < targets.Length; i++)
            {
                if (source.Parent == targets[i].Parent)
                    continue;

                if (direction == InternalCollisionDirection.Vertical)
                    source.IsCollidingVertically(targets[i]);
                else
                    source.IsCollidingHorizontally(targets[i]);
            }
        }

        public IReadOnlyList<GameObject> GetGameObjects()
        {
            return World.GetObjects();
        }

        public enum InternalCollisionDirection
        {
            Horizontal,
            Vertical
        }
    }
}
