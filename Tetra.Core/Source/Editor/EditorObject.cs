using System;
using System.Collections.Generic;

namespace Tetra
{
    public class EditorObject : GameObject
    {
        private readonly Func<GameObject> Factory;
        private readonly Collider Collider;

        public EditorObject(Func<GameObject> Factory, int widthInBlocks = 1, int heightInBlocks = 1)
        {
            this.Factory = Factory;
            var temp = Factory();
            Animation = temp.Animation;
            Collider = new Collider(this)
            {
                OffsetX = 1,
                OffsetY = 1,
                Width = (GameConstants.BlockSize*widthInBlocks) - 2,
                Height = (GameConstants.BlockSize* heightInBlocks) - 2
            };
        }

        public override IEnumerable<Collider> GetColliders()
        {
            yield return Collider;
        }

        public GameObject Create()
        {
            var obj = Factory();
            obj.Position = Position;
            return obj;
        }
    }
}
