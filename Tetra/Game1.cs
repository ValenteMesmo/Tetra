using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Tetra
{
    public class Game1 : Game
    {
        public static Queue<Rectangle> RectanglesToRender = new Queue<Rectangle>();
        public static Queue<Rectangle> RectanglesToRenderUI = new Queue<Rectangle>();
        public static string Log;

        private FramerateCounter FramerateCounter = new FramerateCounter();
        private Dictionary<string, Texture2D> Texture = new Dictionary<string, Texture2D>();
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteBatch spriteBatchUi;
        private SpriteFont SpriteFont;
        private Camera Camera;
        private MouseInput Mouse;
        private GameLoop GameLoop;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //IsFixedTimeStep = false;
            //graphics.SynchronizeWithVerticalRetrace = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Camera = new Camera(GraphicsDevice.Viewport);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatchUi = new SpriteBatch(GraphicsDevice);

            Texture.Add("block", Content.Load<Texture2D>("floor"));
            Texture.Add("player", Content.Load<Texture2D>("player"));
            var pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
            Texture.Add("pixel", pixel);

            SpriteFont = Content.Load<SpriteFont>("SpriteFont");

            Mouse = new MouseInput(Camera);

            GameLoop = new GameLoop(new EditorWorld(Mouse, Camera));
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            FramerateCounter.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            //update inputs
            Mouse.Update();

            Log = "";
            Log += $"{FramerateCounter.AverageFramesPerSecond}";

            //custom updates
            GameLoop.Update();

            //update camera
            Camera.UpdateCamera(GraphicsDevice.Viewport);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatchUi.Begin();
            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                Camera.Transform
            );
            var gameObjects = GameLoop.GetGameObjects();
            foreach (var obj in gameObjects)
                foreach (var frame in obj.Animation.GetFrame())
                    spriteBatch.Draw(
                        Texture[frame.Texture],
                        new Rectangle((int)obj.Position.X + (int)frame.OffsetX, (int)obj.Position.Y + (int)frame.OffsetY, (int)frame.Width, (int)frame.Height),
                        frame.Color
                    );

            if (GameLoop.World.RenderColliders)
                while (RectanglesToRenderUI.Count > 0)
                {
                    var rectangle = RectanglesToRenderUI.Dequeue();
                    DrawBorder(rectangle, 2, Color.Red, spriteBatchUi);
                }

            if (GameLoop.World.RenderColliders)
                while (RectanglesToRender.Count > 0)
                {
                    var rectangle = RectanglesToRender.Dequeue();
                    DrawBorder(rectangle, 2, Color.Green, spriteBatch);
                }

            spriteBatchUi.DrawString(SpriteFont, Log, new Vector2(50, 50), Color.White);
            //spriteBatchUi.DrawString(SpriteFont, $"world {Mouse.WorldPosition.X}, {Mouse.WorldPosition.Y}", new Vector2(50, 100), Color.White);
            //spriteBatchUi.DrawString(SpriteFont, $"camera {Camera.Position.X}, {Camera.Position.Y}", new Vector2(50, 150), Color.White);
            //spriteBatchUi.DrawString(SpriteFont, $"objects {gameObjects.Count}", new Vector2(50, 200), Color.White);

            spriteBatch.End();
            spriteBatchUi.End();
            base.Draw(gameTime);
        }


        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture["pixel"], new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(Texture["pixel"], new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(Texture["pixel"], new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder), rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(Texture["pixel"], new Rectangle(rectangleToDraw.X, rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder, rectangleToDraw.Width, thicknessOfBorder), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }

    public class FramerateCounter
    {
        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }

        public const int MAXIMUM_SAMPLES = 10;

        private Queue<float> _sampleBuffer = new Queue<float>();

        internal void Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += deltaTime;
        }
    }
}
