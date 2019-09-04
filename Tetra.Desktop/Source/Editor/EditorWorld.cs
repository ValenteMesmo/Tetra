using System.Collections.Generic;
using System.Linq;

namespace Tetra.Desktop
{
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
}
