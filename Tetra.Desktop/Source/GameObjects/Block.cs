using System.Collections.Generic;

namespace Tetra.Desktop
{
    public class Block : GameObject
    {
        private readonly Collider Collider;

        public Block()
        {
            Collider = new Collider(this) {
                Width = GameConstants.BLOCK_SIZE, 
                Height = GameConstants.BLOCK_SIZE
            };
        }

        public override IEnumerable<Collider> GetColliders()
        {
            yield return Collider;
        }
    }
}
