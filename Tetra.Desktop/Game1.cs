﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Tetra.Desktop
{
    public class Game1 : Game
    {
        public static Queue<Rectangle> RectanglesToRender = new Queue<Rectangle>();
        public static Queue<Rectangle> RectanglesToRenderUI = new Queue<Rectangle>();

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteBatch spriteBatchUi;
        private Texture2D blockTexture;
        private Texture2D playerTexture;
        private Texture2D pixel;
        private SpriteFont SpriteFont;
        private Camera Camera;
        private MouseInfo Mouse;
        private WorldPieceAdder Adder;
        private List<GameObject> WorldPieces = new List<GameObject>();

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
            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            SpriteFont = Content.Load<SpriteFont>("SpriteFont");

            Mouse = new MouseInfo(Camera);

            Adder = new WorldPieceAdder(WorldPieces, Mouse);
            WorldPieces.Add(new Player { });
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //update inputs
            Mouse.Update();

            //custom updates
            Adder.Update();

            //update camera
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

            foreach (var obj in WorldPieces)
                if (obj is Player)
                    spriteBatch.Draw(playerTexture, new Rectangle((int)obj.Position.X, (int)obj.Position.Y, (int)size, (int)size), Color.White);
                else
                    spriteBatch.Draw(
                        texture: blockTexture
                        , destinationRectangle: new Rectangle((int)obj.Position.X, (int)obj.Position.Y, (int)size, (int)size)
                        , sourceRectangle: null
                        , color: Color.White
                        , rotation: 0
                        , origin: Vector2.Zero
                        , effects: SpriteEffects.None
                        , layerDepth: 0f
                    );


            while (RectanglesToRenderUI.Count > 0)
            {
                var rectangle = RectanglesToRenderUI.Dequeue();
                DrawBorder(rectangle, 2, Color.Red, spriteBatchUi);
            }

            while (RectanglesToRender.Count > 0)
            {
                var rectangle = RectanglesToRender.Dequeue();
                DrawBorder(rectangle, 50, Color.Green, spriteBatch);
            }

            spriteBatchUi.DrawString(SpriteFont, $"mouse {Mouse.ScreenPosition.X}, {Mouse.ScreenPosition.Y}", new Vector2(50, 50), Color.White);
            spriteBatchUi.DrawString(SpriteFont, $"world {Mouse.WorldPosition.X}, {Mouse.WorldPosition.Y}", new Vector2(50, 100), Color.White);
            spriteBatchUi.DrawString(SpriteFont, $"camera {Camera.Position.X}, {Camera.Position.Y}", new Vector2(50, 150), Color.White);

            spriteBatch.End();
            spriteBatchUi.End();
            base.Draw(gameTime);
        }


        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder), rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder, rectangleToDraw.Width, thicknessOfBorder), null, borderColor, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
