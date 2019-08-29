using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetra.Desktop
{
    public interface World
    {
        IReadOnlyList<GameObject> GetObjects();
        void AddObject(GameObject Object);
    }

    public class EditorWorld : World
    {
        public List<GameObject> GameObjects = new List<GameObject>();
        public GameWorld GameWorld = new GameWorld();
        private bool playing;

        public EditorWorld(MouseInfo mouseInfo)
        {
            GameObjects.Add(new Player());
            GameObjects.Add(new MouseCursor(this, mouseInfo));
            GameObjects.Add(new GameObject { Update = new ChangeToGameMode(this) });
        }

        public void Play()
        {
            GameWorld.Clear();
            GameWorld.AddRange(GameObjects.OfType<Block>());
            GameWorld.AddObject(new GameObject { Update = new ChangeToEditorMode(this) });

            playing = true;
        }

        public void Pause()
        {
            playing = false;
        }

        public void AddObject(GameObject Object)
        {
            if (playing)
                GameWorld.AddObject(Object);
            else
                GameObjects.Add(Object);
        }

        public IReadOnlyList<GameObject> GetObjects()
        {
            if (playing)
                return GameWorld.GameObjects;

            return GameObjects;
        }
    }

    public class GameWorld : World
    {
        public List<GameObject> GameObjects = new List<GameObject>();

        public void AddObject(GameObject Object)
        {
            GameObjects.Add(Object);
        }

        public void AddRange(IEnumerable<GameObject> Objects)
        {
            GameObjects.AddRange(Objects);
        }

        public IReadOnlyList<GameObject> GetObjects()
        {
            return GameObjects;
        }

        public void Clear()
        {
            GameObjects.Clear();
        }
    }

    public class GameLoop
    {
        public readonly QuadTree quadtree;
        private readonly World World;

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

                    CheckCollisions(CollisionDirection.Vertical, collider);
                }

                GameObject.Position.X += GameObject.Velocity.X * elapsed;
                foreach (var collider in colliders)
                {
                    if (collider.IsDumb)
                        continue;

                    CheckCollisions(CollisionDirection.Horizontal, collider);
                }

                GameObject.Animation.Update();
            }

            //quadtree.DrawDebug();
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

        public IReadOnlyList<GameObject> GetGameObjects()
        {
            return World.GetObjects();
        }

        public enum CollisionDirection
        {
            Horizontal,
            Vertical
        }
    }
}
