using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetra.Desktop
{
    public class AnimationFrame
    {
        private readonly GameObject Parent;
        private readonly string Texture;
        public readonly float OffsetX;
        public readonly float OffsetY;
        public readonly int Width;
        public readonly int Height;
        public readonly Color Color = Color.White;

        public AnimationFrame(
            GameObject Parent,
            string Texture,
            float OffsetX,
            float OffsetY,
            int Width,
            int Height)
        {
            this.Parent = Parent;
            this.Texture = Texture;
            this.OffsetX = OffsetX;
            this.OffsetY = OffsetY;
            this.Width = Width;
            this.Height = Height;
        }
    }

    public interface IHandleAnimations
    {
        IEnumerable<AnimationFrame> GetFrame();
        void Update();
        bool RenderOnUiLayer { get; }
    }

    public class SimpleAnimation : IHandleAnimations
    {
        public bool RenderOnUiLayer => false;
        private AnimationFrame frame;

        public SimpleAnimation(AnimationFrame frame)
        {
            this.frame = frame;
        }

        public void Update() { }

        public IEnumerable<AnimationFrame> GetFrame()
        {
            yield return frame;
        }
    }

    public class MouseCursor : GameObject
    {
        private readonly Collider Collider;

        public MouseCursor(MouseInfo mouse)
        {
            Collider = new Collider(this)
            {
                OffsetX = 1,
                OffsetY = 1,
                Width = 1,
                Height = 1
            };

            Update = new MoveMouseCursorToNearbyCell(mouse, this);

            Animation = new SimpleAnimation(
                new AnimationFrame(
                    this,
                    "cursor",
                    0,
                    0,
                    GameConstants.BLOCK_SIZE,
                    GameConstants.BLOCK_SIZE));
        }

        public override IEnumerable<Collider> GetColliders()
        {
            yield return Collider;
        }
    }

    [Obsolete("Will be removed")]
    public class WorldPieceAdder
    {
        private readonly List<GameObject> WorldPieces;
        private readonly MouseInfo mouse;
        private const int size = GameConstants.BLOCK_SIZE;

        public WorldPieceAdder(List<GameObject> WorldPieces, MouseInfo mouse)
        {
            this.WorldPieces = WorldPieces;
            this.mouse = mouse;
        }

        public void Update()
        {
            if (mouse.LeftButtonPressed)
            {
                if (!WorldPieces.Any(f => new Rectangle(f.Position.ToPoint(), new Vector2(size, size).ToPoint()).Contains(mouse.WorldPosition)))
                {
                    WorldPieces.Add(new GameObject()
                    {
                        Position = new Vector2(
                            (float)(Math.Floor(mouse.WorldPosition.X / size) * size),
                            (float)(Math.Floor(mouse.WorldPosition.Y / size) * size)
                        )
                    });
                }
            }
            else if (mouse.RightButtonPressed)
            {
                var obj = WorldPieces.FirstOrDefault(f => new Rectangle(f.Position.ToPoint(), new Vector2(size, size).ToPoint()).Contains(mouse.WorldPosition));
                if (obj != null)
                {
                    WorldPieces.Remove(obj);
                }
            }
        }
    }
}
