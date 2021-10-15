using System.Collections.Generic;

namespace Tetra
{
    public class Block : GameObject
    {
        private readonly Collider Collider;

        public Block()
        {
            Collider = new Collider(this)
            {
                OffsetX = 1,
                OffsetY = 1,
                Width = GameConstants.BlockSize -2,
                Height = GameConstants.BlockSize -2
            };

            Animation = new SimpleAnimation(
                new AnimationFrame(
                    this, 
                    "block", 
                    0, 
                    0, 
                    GameConstants.BlockSize, 
                    GameConstants.BlockSize
                )
            );
        }

        public override IEnumerable<Collider> GetColliders()
        {
            yield return Collider;
        }
    }
}
