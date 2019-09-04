using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tetra
{
    public class GameObject
    {
        public Vector2 Position;
        public Vector2 Velocity;

        public IHandleUpdates Update { get; set; } = No.Update;
        public IHandleAnimations Animation { get; set; } = No.Animation;
        [Obsolete]
        public bool FacingRight { get; internal set; }

        public virtual IEnumerable<Collider> GetColliders() => No.Colliders;
    }
}
