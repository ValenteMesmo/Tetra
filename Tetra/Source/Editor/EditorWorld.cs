using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tetra
{

    public class EditorGridRenderrer : IHandleAnimations
    {
        public bool RenderOnUiLayer => false;
        private readonly List<AnimationFrame> Frames = new List<AnimationFrame>();
        private readonly Camera camera;
        private readonly GameObject parent;

        public EditorGridRenderrer(GameObject parent, Camera camera)
        {
            this.camera = camera;
            this.parent = parent;
        }

        public IEnumerable<AnimationFrame> GetFrame()
        {
            return Frames;
        }

        public void Update()
        {
            Frames.Clear();

            var offset = GameConstants.BlockSize / 10;
            var size = GameConstants.BlockSize - offset - offset;

            for (int i = (camera.VisibleArea.Left / GameConstants.BlockSize) - 1; i < (camera.VisibleArea.Right / GameConstants.BlockSize) + 1; i++)
            {
                for (int j = (camera.VisibleArea.Top / GameConstants.BlockSize) - 1; j < (camera.VisibleArea.Bottom / GameConstants.BlockSize) + 1; j++)
                {
                    Frames.Add(
                        new AnimationFrame(
                            parent
                            , "pixel"
                            , (i * GameConstants.BlockSize) + offset
                            , (j * GameConstants.BlockSize) + offset
                            , size
                            , size
                        )
                        { Color = new Color(10, 10, 10, 10) }
                    );
                }
            }
        }
    }

    public class EditorWorld : World
    {
        public List<GameObject> EditorObjects = new List<GameObject>();
        public GameWorld GameWorld = new GameWorld();
        private bool playing;

        public bool RenderColliders { get => GameWorld.RenderColliders; set => GameWorld.RenderColliders = value; }

        public EditorWorld(MouseInput mouseInput, Camera Camera)
        {
            var obj = new GameObject();
            obj.Animation = new EditorGridRenderrer(obj, Camera);
            EditorObjects.Add(obj);

            EditorObjects.Add(new EditorObject(() => new Player(Camera), 1, 2));
            EditorObjects.Add(new GameObject { Update = new ChangeToGameMode(this, new CooldownTracker(30)) });
            EditorObjects.Add(new GameObject
            {
                Update = new UpdateAggregation(
                    new MouseScrollControlsCameraZoom(Camera, mouseInput)
                    , new CameraMouseControls(Camera, mouseInput)
                    , new CameraKeyboardControls(Camera)
                )
            });
            EditorObjects.Add(new GameObject { Update = new CollisionDebugToggler(this, new CooldownTracker(30)) });
            EditorObjects.Add(new MouseCursor(this, mouseInput));
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

        public void RemoveObject(GameObject Object)
        {
            if (playing)
                GameWorld.RemoveObject(Object);
            else
                EditorObjects.Remove(Object);
        }

        public IReadOnlyList<GameObject> GetObjects()
        {
            if (playing)
                return GameWorld.GameObjects;

            return EditorObjects;
        }
    }
}
