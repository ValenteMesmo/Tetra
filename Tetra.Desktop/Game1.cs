using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetra.Desktop
{
    class GameObject
    {
        public Vector2 Postition;
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteBatch spriteBatchUi;
        private Texture2D blockTexture;
        private Texture2D playerTexture;
        private SpriteFont SpriteFont;
        private Camera Camera;

        private List<GameObject> Objects = new List<GameObject>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            blockTexture = Content.Load<Texture2D>("tiles/floor");
            playerTexture = Content.Load<Texture2D>("tiles/player");
            SpriteFont = Content.Load<SpriteFont>("SpriteFont");
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var mouse = Mouse.GetState();


            if (mouse.LeftButton == ButtonState.Pressed)
            {
                var mousePosition = Camera.ToWorld(mouse.Position.ToVector2());
                if (!Objects.Any(f => new Rectangle(f.Postition.ToPoint(), new Vector2(size, size).ToPoint()).Contains(mousePosition)))
                {
                    Objects.Add(new GameObject()
                    {
                        Postition = new Vector2(
                            (float)(Math.Floor(mousePosition.X / size) * size),
                            (float)(Math.Floor(mousePosition.Y / size) * size)
                        )
                    });
                }
            }
            else if (mouse.RightButton == ButtonState.Pressed)
            {
                var mousePosition = Camera.ToWorld(mouse.Position.ToVector2());
                var obj = Objects.FirstOrDefault(f => new Rectangle(f.Postition.ToPoint(), new Vector2(size, size).ToPoint()).Contains(mousePosition));
                if (obj!=null)
                {
                    Objects.Remove(obj);
                }
            }

            Camera.UpdateCamera(GraphicsDevice.Viewport);

            base.Update(gameTime);
        }
        const float size = 100;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatchUi.Begin();
            spriteBatch.Begin(
                   SpriteSortMode.BackToFront,
                   BlendState.AlphaBlend,
                   null,
                   null,
                   null,
                   null,
                   Camera.Transform
               );

            foreach (var obj in Objects)
                spriteBatch.Draw(
                    texture: blockTexture
                    , destinationRectangle: new Rectangle((int)obj.Postition.X, (int)obj.Postition.Y, (int)size, (int)size)
                    , sourceRectangle: null
                    , color: Color.White
                    , rotation: 0
                    , origin: Vector2.Zero
                    , effects: SpriteEffects.None
                    , layerDepth: 0f
                );

            var mouse = Mouse.GetState();
            var mouse2 = Camera.ToWorld(mouse.Position.ToVector2());
            spriteBatchUi.DrawString(SpriteFont, $"mouse {mouse.Position.X}, {mouse.Position.Y}", new Vector2(50, 50), Color.White);
            spriteBatchUi.DrawString(SpriteFont, $"world {mouse2.X}, {mouse2.Y}", new Vector2(50, 100), Color.White);
            spriteBatchUi.DrawString(SpriteFont, $"camera {Camera.Position.X}, {Camera.Position.Y}", new Vector2(50, 150), Color.White);

            spriteBatch.End();
            spriteBatchUi.End();
            base.Draw(gameTime);
        }
    }
}
