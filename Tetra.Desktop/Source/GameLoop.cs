using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetra.Desktop
{
    public class CollisionDebugToggler : IHandleUpdates
    {
        private readonly World world;
        private readonly CooldownTracker cooldown;

        public CollisionDebugToggler(World world, CooldownTracker cooldown)
        {
            this.world = world;
            this.cooldown = cooldown;
        }

        public void Update()
        {
            cooldown.Update();
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.F2) && cooldown.IsOver())
            {
                cooldown.Start();
                world.RenderColliders = !world.RenderColliders;
            }
        }
    }

    public class CooldownTracker : IHandleUpdates
    {
        private int Count = 0;
        private readonly int Duration;

        public CooldownTracker(int Duration)
        {
            this.Duration = Duration;
        }

        public void Start()
        {
            Count = Duration;
        }

        public bool IsOver()
        {
            return Count == 0;
        }

        public void Update()
        {
            if (Count > 0)
                Count--;
        }
    }

    public class EditorObject : GameObject
    {
        private readonly Func<GameObject> Factory;
        private readonly IEnumerable<Collider> Colliders;

        public EditorObject(Func<GameObject> Factory)
        {
            this.Factory = Factory;
            var temp = Factory();
            Animation = temp.Animation;
            Colliders = temp.GetColliders();
        }

        public override IEnumerable<Collider> GetColliders() => Colliders;

        public GameObject Create()
        {
            var obj = Factory();
            obj.Position = Position;
            return obj;
        }
    }

    public interface World
    {
        bool RenderColliders { get; set; }

        IReadOnlyList<GameObject> GetObjects();
        void AddObject(GameObject Object);
    }

    public class EditorWorld : World
    {
        public List<GameObject> EditorObjects = new List<GameObject>();
        public GameWorld GameWorld = new GameWorld();
        private bool playing;

        public bool RenderColliders { get => GameWorld.RenderColliders; set => GameWorld.RenderColliders = value; }

        public EditorWorld(MouseInfo mouseInfo)
        {
            EditorObjects.Add(new EditorObject(() => new Player()));
            EditorObjects.Add(new GameObject { Update = new ChangeToGameMode(this, new CooldownTracker(30)) });
            EditorObjects.Add(new GameObject { Update = new CollisionDebugToggler(this, new CooldownTracker(30)) });            
            EditorObjects.Add(new MouseCursor(this, mouseInfo));
        }

        public void Play()
        {
            GameWorld.Clear();
            GameWorld.AddRange(EditorObjects.OfType<EditorObject>().Select(f => f.Create()));
            GameWorld.AddObject(new GameObject { Update = new ChangeToEditorMode(this, new CooldownTracker(30)) });
            GameWorld.AddObject(new GameObject { Update = new CollisionDebugToggler(this, new CooldownTracker(30)) });

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
                EditorObjects.Add(Object);
        }

        public IReadOnlyList<GameObject> GetObjects()
        {
            if (playing)
                return GameWorld.GameObjects;

            return EditorObjects;
        }
    }

    public class GameWorld : World
    {
        public bool RenderColliders { get; set; } = true;
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
