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
            Animation = new SimpleAnimation(new AnimationFrame(this, "player", 0, 0, GameConstants.BLOCK_SIZE, GameConstants.BLOCK_SIZE * 2));

            var flagGrounded = new FlagAsGrounded(this);
            Collider = new Collider(this)
            {
                Width = GameConstants.BLOCK_SIZE,
                Height = GameConstants.BLOCK_SIZE * 2,
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
            var attackCooldwon = new CooldownTracker(20);
            var HurtCooldwon = new CooldownTracker(10);

            var changesSpeed = new IncreaseHorizontalVelocity(this, 5);
            var decreaseVelocity = new DecreaseHorizontalVelocity(this, 5);
            var limitHorizontalVelocity = new LimitHorizontalVelocity(this, 20);
            var gravityChangesVerticalSpeed = new GravityChangesVerticalSpeed(this);

            var ChangePlayerStateToFalling = new ChangePlayerStateToFalling(this);
            var changePlayerToIdle = new ChangePlayerStateToIdle(this, Inputs);
            var changePlayerToWalking = new ChangePlayerStateToWalking(this, Inputs);
            var ChangePlayerToJumpingState = new ChangePlayerStateToJumping(this, Inputs);
            //var changePlayerStateToCrouch = new ChangePlayerStateToCrouch(this);
            //var changePlayerStateToLookingUp = new ChangePlayerStateToLookingUp(this);
            //var ChangePlayerStateToAttack = new ChangePlayerStateToAttack(this, attackCooldwon);
            //var ChangePlayerStateToAfterAttack = new ChangePlayerStateToAfterAttack(this, attackCooldwon);
            //var ChangePlayerStateToHurt = new ChangePlayerStateToHurt(this, HurtCooldwon);
            //var ChangePlayerStateToAfterHurt = new ChangePlayerStateToAfterHurt(this, HurtCooldwon);

            var updateByState = new UpdateByState(this);

            updateByState.Add(PlayerState.IDLE, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
                , ChangePlayerStateToFalling
                , changePlayerToWalking
                , ChangePlayerToJumpingState
            //, changePlayerStateToCrouch
            //, changePlayerStateToLookingUp
            //, ChangePlayerStateToAttack
            //, ChangePlayerStateToHurt
            ));

            updateByState.Add(PlayerState.FALLING, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , changePlayerToWalking
                , changePlayerToIdle
            //, changePlayerStateToCrouch
            //, changePlayerStateToLookingUp
            //, ChangePlayerStateToHurt
            ));

            updateByState.Add(PlayerState.WALKING, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , limitHorizontalVelocity
                , changesSpeed
                , changePlayerToWalking
                , ChangePlayerToJumpingState
                , ChangePlayerStateToFalling
                , changePlayerToIdle
            //, changePlayerStateToCrouch
            //, changePlayerStateToLookingUp
            //, ChangePlayerStateToAttack
            //, ChangePlayerStateToHurt
            ));

            updateByState.Add(PlayerState.JUMP, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , ChangePlayerStateToFalling
                , changePlayerToIdle
            //, ChangePlayerStateToHurt
            ));

            updateByState.Add(PlayerState.CROUCH, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
                , changePlayerToWalking
                , changePlayerToIdle
                , ChangePlayerStateToFalling
            //, changePlayerStateToLookingUp
            //, ChangePlayerStateToHurt
            ));

            updateByState.Add(PlayerState.LOOKING_UP, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
                , changePlayerToWalking
                , changePlayerToIdle
                , ChangePlayerStateToFalling
            //, changePlayerStateToCrouch
            //, ChangePlayerStateToHurt
            ));

            updateByState.Add(PlayerState.ATTACK, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
            //, ChangePlayerStateToAfterAttack
            //, ChangePlayerStateToHurt
            ));

            updateByState.Add(PlayerState.AFTER_ATTACK, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
                , changePlayerToIdle
                , changePlayerToWalking
                , ChangePlayerStateToFalling
            //, changePlayerStateToCrouch
            //, ChangePlayerStateToHurt
            ));

            updateByState.Add(PlayerState.HURT, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
            //, ChangePlayerStateToAfterHurt
            ));

            updateByState.Add(PlayerState.AFTER_HURT, new UpdateAggregation(
                gravityChangesVerticalSpeed
                , decreaseVelocity
                , changePlayerToIdle
                , changePlayerToWalking
                , ChangePlayerStateToFalling
            //, changePlayerStateToCrouch
            //, ChangePlayerStateToHurt
            ));

            return updateByState;
        }
    }
}
