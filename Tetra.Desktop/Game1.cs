using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetra.Desktop
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch spriteBatchUi;
        private Texture2D blockTexture;
        private Texture2D playerTexture;
        private SpriteFont SpriteFont;
        private Camera Camera;

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
            Camera.UpdateCamera(GraphicsDevice.Viewport);

            base.Update(gameTime);
        }

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

            const int size = 50;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    spriteBatch.Draw(
                        texture: blockTexture
                        , destinationRectangle: new Rectangle(i * size, j * size, size, size)
                        , sourceRectangle: null
                        , color: Color.White
                        , rotation: 0
                        , origin: Vector2.Zero
                        , effects: SpriteEffects.None
                        , layerDepth: 0f
                    );

                }
            }

            var mouse = Mouse.GetState();
            var mouse2 = Camera.GetWorldPostion(mouse.Position.ToVector2());
            spriteBatchUi.DrawString(SpriteFont, $"mouse {mouse.Position.X}, {mouse.Position.Y}", new Vector2(50, 50), Color.White);
            spriteBatchUi.DrawString(SpriteFont, $"world {mouse2.X}, {mouse2.Y}", new Vector2(50, 100), Color.White);
            spriteBatchUi.DrawString(SpriteFont, $"camera {Camera.Position.X}, {Camera.Position.Y}", new Vector2(50, 150), Color.White);

            spriteBatch.End();
            spriteBatchUi.End();
            base.Draw(gameTime);
        }
    }
}
