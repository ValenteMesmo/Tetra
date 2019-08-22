using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tetra.Desktop
{
    public class GameObject
    {
        public Vector2 Position;
        public Vector2 Velocity;

        public virtual IEnumerable<Collider> GetColliders() => EMPTY;
        public virtual void Update() { }

        private static readonly Collider[] EMPTY = new Collider[0];
    }
}
