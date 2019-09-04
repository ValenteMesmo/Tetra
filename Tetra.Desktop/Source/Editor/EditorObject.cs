using System;
using System.Collections.Generic;

namespace Tetra.Desktop
{
    public class EditorObject : GameObject
    {
        private readonly Func<GameObject> Factory;
        private readonly IEnumerable<Collider> Colliders;

        public EditorObject(Func<GameObject> Factory)
        {
            this.Factory = Factory;
            var temp = Factory();
            Animation = temp.Animation;
            Colliders = temp.GetColliders();
        }

        public override IEnumerable<Collider> GetColliders() => Colliders;

        public GameObject Create()
        {
            var obj = Factory();
            obj.Position = Position;
            return obj;
        }
    }
}
