using System;
using System.Collections.Generic;

namespace Tetra
{
    public class Player : GameObject, IHaveState
    {
        private readonly Collider Collider;
        [Obsolete]
        internal bool Grounded;

        public Player()
        {
            Animation = new SimpleAnimation(new AnimationFrame(this, "player", 0, 0, GameConstants.BlockSize, GameConstants.BlockSize * 2));

            var flagGrounded = new FlagAsGrounded(this);
            Collider = new Collider(this)
            {
                Width = GameConstants.BlockSize - 2,
                Height = (GameConstants.BlockSize * 2) - 2,
                OffsetX = 1,
                OffsetY = 1,
                Collision = new CollisionHandlerAggregation(flagGrounded, new BlockCollisionHandler()),
                BeforeCollisions = flagGrounded
            };

            var input = new GameInput();

            Update = new UpdateAggregation(
                new UpdateInput(input)
                , CreateUpdatesByState(input)
            );
        }

        public int State { get; set; }

        public override IEnumerable<Collider> GetColliders()
        {
            yield return Collider;
        }

        private UpdateByState CreateUpdatesByState(GameInput Input)
        {

            var groundSpeed = new IncreaseHorizontalVelocity(this, GameConstants.WalkAccel, Input);
            var airSpeed = new IncreaseHorizontalVelocity(this, GameConstants.WalkAccel / 2, Input);
            var groundFriction = new DecreaseHorizontalVelocity(this, GameConstants.Friction);
            var limitHorizontalVelocity = new LimitHorizontalVelocity(this, GameConstants.WalkMaxSpeed);
            var gravityChangesVerticalSpeed = new GravityChangesVerticalSpeed(this, GameConstants.GravityAccel, GameConstants.GravityMaxSpeed);

            var ChangePlayerStateToFalling = new ChangePlayerStateToFalling(this);
            var changePlayerToIdle = new ChangePlayerStateToIdle(this, Input);
            var changePlayerToWalking = new ChangePlayerStateToWalking(this, Input);
            var ChangePlayerToJumpingState = new ChangeToJumpState(this, Input, GameConstants.JumpForce);

            var updateByState = new UpdateByState(this);

            updateByState.Add(PlayerState.Idle, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , groundFriction
                , ChangePlayerStateToFalling
                , changePlayerToWalking
                , ChangePlayerToJumpingState
            ));

            updateByState.Add(PlayerState.Jump, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , airSpeed
                , limitHorizontalVelocity
                , ChangePlayerStateToFalling
                , changePlayerToIdle
            ));

            updateByState.Add(PlayerState.Fall, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , airSpeed
                , limitHorizontalVelocity
                , changePlayerToWalking
                , changePlayerToIdle
            ));

            updateByState.Add(PlayerState.Walk, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , groundSpeed
                , limitHorizontalVelocity
                , changePlayerToWalking
                , ChangePlayerToJumpingState
                , ChangePlayerStateToFalling
                , changePlayerToIdle
            ));

            return updateByState;
        }
    }
}
