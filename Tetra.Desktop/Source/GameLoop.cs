using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tetra.Desktop
{
    public class GameLoop
    {
        public List<GameObject> GameObjects = new List<GameObject>();
        private readonly Camera Camera;
        public readonly QuadTree quadtree;

        public GameLoop(Camera Camera, MouseInfo mouseInfo)
        {
            this.Camera = Camera;
            quadtree = new QuadTree(new Rectangle(-11000, -7000, 23000, 15000), 50, 5);

            GameObjects.Add(new Player());
            GameObjects.Add(new MouseCursor(mouseInfo));
        }

        public void Update(float elapsed)
        {
            quadtree.Clear();

            for (int i = 0; i < GameObjects.Count; i++)
            {
                quadtree.AddRange(GameObjects[i].GetColliders());
            }

            foreach (var GameObject in GameObjects.ToList())
            {
                GameObject.Update.Update();

                GameObject.Position.Y += GameObject.Velocity.Y * elapsed;
                var colliders = GameObject.GetColliders();
                foreach (var collider in colliders)
                {
                    collider.BeforeCollisions.Update();

                    if (collider.IsDumb)
                        continue;

                    CheckCollisions(CollisionDirection.Vertical, collider);
                }

                GameObject.Position.X += GameObject.Velocity.X * elapsed;
                foreach (var collider in colliders)
                {
                    if (collider.IsDumb)
                        continue;

                    CheckCollisions(CollisionDirection.Horizontal, collider);
                }

                //GameObject.Animation.Update();
            }

            quadtree.DrawDebug();
        }

        private void CheckCollisions(CollisionDirection direction, Collider source)
        {
            var targets = quadtree.Get(source);

            for (int i = 0; i < targets.Length; i++)
            {
                if (source.Parent == targets[i].Parent)
                    continue;

                if (direction == CollisionDirection.Vertical)
                    source.IsCollidingVertically(targets[i]);
                else
                    source.IsCollidingHorizontally(targets[i]);
            }
        }

        public enum CollisionDirection
        {
            Horizontal,
            Vertical
        }
    }
}
