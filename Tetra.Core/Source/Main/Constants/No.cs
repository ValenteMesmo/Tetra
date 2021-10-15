using System.Collections.Generic;

namespace Tetra
{
    public static class No
    {
        public static readonly Collider[] Colliders = new Collider[0];
        public static readonly IHandleUpdates Update = new NoUpdate();
        public static readonly IHandleCollisions Collision = new NoCollision();
        public static readonly IHandleAnimations Animation = new NoAnimation();
        public static readonly AnimationFrame[] Frames = new AnimationFrame[0];

        private class NoUpdate : IHandleUpdates
        {
            public void Update() { }
        }

        private class NoCollision : IHandleCollisions
        {
            public void Collide(Collider Source, CollisionDirection direction, Collider Target) { }
        }

        private class NoAnimation : IHandleAnimations
        {
            public bool RenderOnUiLayer => false;

            public IEnumerable<AnimationFrame> GetFrame() => Frames;

            public void Update() { }
        }
    }
}
