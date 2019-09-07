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

        private UpdateByState CreateUpdatesByState(GameInput Inputs)
        {

            var changesSpeed = new IncreaseHorizontalVelocity(this, GameConstants.WalkAccel);
            var decreaseVelocity = new DecreaseHorizontalVelocity(this, GameConstants.Friction);
            var limitHorizontalVelocity = new LimitHorizontalVelocity(this, GameConstants.WalkMaxSpeed);
            var gravityChangesVerticalSpeed = new GravityChangesVerticalSpeed(this, GameConstants.GravityAccel, GameConstants.GravityMaxSpeed);

            var ChangePlayerStateToFalling = new ChangePlayerStateToFalling(this);
            var changePlayerToIdle = new ChangePlayerStateToIdle(this, Inputs);
            var changePlayerToWalking = new ChangePlayerStateToWalking(this, Inputs);
            var ChangePlayerToJumpingState = new ChangePlayerStateToJumping(this, Inputs, GameConstants.JumpForce);

            var updateByState = new UpdateByState(this);

            updateByState.Add(PlayerState.IDLE, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
                , ChangePlayerStateToFalling
                , changePlayerToWalking
                , ChangePlayerToJumpingState
            ));

            updateByState.Add(PlayerState.FALLING, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , changePlayerToWalking
                , changePlayerToIdle
            ));

            updateByState.Add(PlayerState.WALKING, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , limitHorizontalVelocity
                , changesSpeed
                , changePlayerToWalking
                , ChangePlayerToJumpingState
                , ChangePlayerStateToFalling
                , changePlayerToIdle
            ));

            updateByState.Add(PlayerState.JUMP, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , ChangePlayerStateToFalling
                , changePlayerToIdle
            ));

            return updateByState;
        }
    }
}
