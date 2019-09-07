using Microsoft.Xna.Framework;
using System;

namespace Tetra
{
    public class Collider
    {
        public int OffsetX;
        public int OffsetY;
        public int Width;
        public int Height;
        public bool Disabled;
        public readonly GameObject Parent;

        public bool IsDumb { get; private set; } = true;

        private IHandleCollisions _Collision = No.Collision;
        public IHandleCollisions Collision { get => _Collision; set { _Collision = value; IsDumb = false; } }

        public IHandleUpdates BeforeCollisions { get; set; } = No.Update;

        public Collider(GameObject Parent)
        {
            this.Parent = Parent;
        }

        public Rectangle AsRectangle() =>
            new Rectangle(this.RelativeX(), this.RelativeY(), Width, Height);

        public override string ToString() => $"({Parent.Position.X + OffsetX}, {Parent.Position.Y + OffsetY}) {Parent.GetType().Name}";
    }
}
