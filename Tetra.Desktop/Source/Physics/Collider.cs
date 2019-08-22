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

        private Action<Collider, Collider> _TopCollision = EMPTY_COLLISION_HANDLER;
        public Action<Collider, Collider> TopCollision { get => _TopCollision; set { _TopCollision = value; IsDumb = false; } }

        private Action<Collider, Collider> _LeftCollision = EMPTY_COLLISION_HANDLER;
        public Action<Collider, Collider> LeftCollision { get => _LeftCollision; set { _LeftCollision = value; IsDumb = false; } }

        private Action<Collider, Collider> _BotCollision = EMPTY_COLLISION_HANDLER;
        public Action<Collider, Collider> BotCollision { get => _BotCollision; set { _BotCollision = value; IsDumb = false; } }

        private Action<Collider, Collider> _RightCollision = EMPTY_COLLISION_HANDLER;
        public Action<Collider, Collider> RightCollision { get => _RightCollision; set { _RightCollision = value; IsDumb = false; } }

        private Action _BeforeCollisions = EMPTY_BEFORE_COLLISION;
        public Action BeforeCollisions { get => _BeforeCollisions; set { _BeforeCollisions = value; IsDumb = false; } }

        private static Action<Collider, Collider> EMPTY_COLLISION_HANDLER = (source, target) => { };
        private static Action EMPTY_BEFORE_COLLISION = () => { };

        public Collider(GameObject Parent)
        {
            this.Parent = Parent;
        }

        public Rectangle AsRectangle() => 
            new Rectangle((int)this.RelativeX(), (int)this.RelativeY(), (int)Width, (int)Height);
    }
}
