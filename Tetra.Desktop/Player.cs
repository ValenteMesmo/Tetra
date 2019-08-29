using System.Collections.Generic;

namespace Tetra.Desktop
{
    public class Player : GameObject
    {
        private readonly Collider Collider;

        public Player()
        {
            Animation = new SimpleAnimation(new AnimationFrame(this, "player", 0, 0, GameConstants.BLOCK_SIZE, GameConstants.BLOCK_SIZE * 2));
            Collider = new Collider(this) { Width = GameConstants.BLOCK_SIZE, Height = GameConstants.BLOCK_SIZE * 2 };
        }

        public override IEnumerable<Collider> GetColliders()
        {
            yield return Collider;
        }
    }
}
