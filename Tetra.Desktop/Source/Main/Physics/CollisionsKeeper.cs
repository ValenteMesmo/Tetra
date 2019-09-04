using System.Collections.Generic;

namespace Tetra.Desktop
{
    public class CollisionsKeeper : IHandleCollisions, IHandleUpdates
    {
        public IEnumerable<Collider> Collisions => _collisions;
        private List<Collider> _collisions = new List<Collider>();

        public void Update()
        {
            _collisions.Clear();
        }

        public void Collide(Collider Source, CollisionDirection direction, Collider Target)
        {
            _collisions.Add(Target);
        }
    }
}
