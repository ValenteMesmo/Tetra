using Microsoft.Xna.Framework;
using System;

namespace Tetra.Desktop
{
    public class Collider
    {
        public float OffsetX;
        public float OffsetY;
        public float Width;
        public float Height;
        public bool Disabled;
        public readonly GameObject Parent;

        public bool IsDumb { get; private set; } = true;

        private IHandleCollisions _TopCollision = No.Collision;
        public IHandleCollisions TopCollision { get => _TopCollision; set { _TopCollision = value; IsDumb = false; } }

        private IHandleCollisions _LeftCollision = No.Collision;
        public IHandleCollisions LeftCollision { get => _LeftCollision; set { _LeftCollision = value; IsDumb = false; } }

        private IHandleCollisions _BotCollision = No.Collision;
        public IHandleCollisions BotCollision { get => _BotCollision; set { _BotCollision = value; IsDumb = false; } }

        private IHandleCollisions _RightCollision = No.Collision;
        public IHandleCollisions RightCollision { get => _RightCollision; set { _RightCollision = value; IsDumb = false; } }

        private IHandleCollisions _AnyCollision = No.Collision;
        public IHandleCollisions AnyCollision { get => _AnyCollision; set { _AnyCollision = value; IsDumb = false; } }

        public IHandleUpdates BeforeCollisions { get; set; } = No.Update;

        public Collider(GameObject Parent)
        {
            this.Parent = Parent;
        }

        public Rectangle AsRectangle() =>
            new Rectangle((int)this.RelativeX(), (int)this.RelativeY(), (int)Width, (int)Height);

        public override string ToString() => $"({Parent.Position.X + OffsetX}, {Parent.Position.Y + OffsetY}) {Parent.GetType().Name}";
    }
}
