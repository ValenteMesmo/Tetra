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

        public GameLoop(Camera Camera)
        {
            this.Camera = Camera;
            quadtree = new QuadTree(new Rectangle(-11000, -7000, 23000, 15000), 50, 5);

            var Player = new Player();

            GameObjects.Add(Player);
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
                GameObject.Update();

                GameObject.Position.Y += GameObject.Velocity.Y * elapsed;
                var colliders = GameObject.GetColliders();
                foreach (var collider in colliders)
                {
                    if (collider.IsDumb)
                        continue;

                    collider.BeforeCollisions();
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
                    source.IsCollidingV(targets[i]);
                else
                    source.IsCollidingH(targets[i]);
            }
        }

        public enum CollisionDirection
        {
            Horizontal,
            Vertical
        }
    }
}
