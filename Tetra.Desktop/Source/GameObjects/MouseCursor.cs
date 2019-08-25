using System;
using System.Collections.Generic;

namespace Tetra.Desktop
{
    public class MouseCursor : GameObject
    {
        private readonly Collider Collider;

        public MouseCursor(IAddToWorld world, MouseInfo mouse)
        {
            var collisionKeeper = new CollisionsKeeper();

            Collider = new Collider(this)
            {
                OffsetX = 1,
                OffsetY = 1,
                Width = 1,
                Height = 1,
                AnyCollision = collisionKeeper,
                BeforeCollisions = collisionKeeper
            };

            Update = new UpdateAggregation(
                new AddBlockOnMouseClick(mouse, collisionKeeper, world),
                new MoveMouseCursorToNearbyCell(mouse, this)
            );

            Animation = new SimpleAnimation(
                new AnimationFrame(
                    this,
                    "cursor",
                    0,
                    0,
                    GameConstants.BLOCK_SIZE,
                    GameConstants.BLOCK_SIZE));
        }

        public override IEnumerable<Collider> GetColliders()
        {
            yield return Collider;
        }
    }
}
