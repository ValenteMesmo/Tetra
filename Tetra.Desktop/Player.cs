using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Tetra.Desktop
{
    public static class PlayerState
    {
        public const int IDLE = 0;
        public const int WALKING = 1;
        public const int FALLING = 2;
        public const int CROUCH = 3;
        public const int JUMP = 4;
        public const int LOOKING_UP = 5;

        public const int ATTACK = 6;
        public const int AFTER_ATTACK = 7;
        public const int HURT = 8;
        public const int AFTER_HURT = 9;
    }

    public class IncreaseHorizontalVelocity : IHandleUpdates
    {
        public readonly int Speed;
        public readonly GameObject Target;

        public IncreaseHorizontalVelocity(GameObject Target, int Speed)
        {
            this.Speed = Speed;
            this.Target = Target;
        }

        public void Update()
        {
            if (Target.FacingRight)
                Target.Velocity.X += Speed;
            else
                Target.Velocity.X -= Speed;
        }
    }

    public class LimitHorizontalVelocity : IHandleUpdates
    {
        public readonly int Limit;
        public readonly GameObject Target;

        public LimitHorizontalVelocity(GameObject Target, int Limit)
        {
            if (Limit <= 0)
                throw new System.Exception("Limit must be positive!");

            this.Limit = Limit;
            this.Target = Target;
        }

        public void Update()
        {
            if (Target.Velocity.X > Limit)
                Target.Velocity.X = Limit;
            else if (Target.Velocity.X < -Limit)
                Target.Velocity.X = -Limit;
        }
    }

    public class DecreaseHorizontalVelocity : IHandleUpdates
    {
        public readonly int Speed;
        public readonly GameObject Target;

        public DecreaseHorizontalVelocity(GameObject Target, int Speed)
        {
            if (Speed <= 0)
                throw new System.Exception("Speed must be positive!");

            this.Speed = Speed;
            this.Target = Target;
        }

        public void Update()
        {
            if (Target.Velocity.X > 0)
                Target.Velocity.X -= Speed;
            else if (Target.Velocity.X < 0)
                Target.Velocity.X += Speed;
        }
    }

    public enum InputDirection
    {
        None,
        Right,
        Left,
        Up,
        Down
    }

    public enum InputAction
    {
        None,
        Attack,
        Defense,
        Jump
    }

    public interface IHaveState
    {
        int State { get; set; }
    }
    public class UpdateByState : IHandleUpdates
    {
        private readonly Dictionary<int, UpdateAggregation> Options = new Dictionary<int, UpdateAggregation>();
        private readonly IHaveState gameOjbect;

        public UpdateByState(IHaveState gameOjbect) =>
            this.gameOjbect = gameOjbect;

        public void Update()
        {
            var updates = Options[gameOjbect.State].updates;
            var initialState = gameOjbect.State;

            foreach (var update in updates)
            {
                update.Update();

                if (initialState != gameOjbect.State)
                    break;
            }
        }

        public void Add(int state, UpdateAggregation updateHandler)
        {
            if (Options.ContainsKey(state))
                throw new Exception($"{nameof(UpdateByState)} already have an update handler for state {state}");

            Options[state] = updateHandler;
        }
    }
    public class ChangePlayerStateToFalling : IHandleUpdates
    {
        public readonly Player Player;

        public ChangePlayerStateToFalling(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            if (!Player.Grounded && Player.Velocity.Y > 0)
                Player.State = PlayerState.FALLING;
        }
    }

    public class UpdateInput : IHandleUpdates
    {
        private readonly GameInput Input;

        public UpdateInput(GameInput Input)
        {
            this.Input = Input;
        }

        public void Update()
        {
            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.A))
                Input.Direction = InputDirection.Left;
            else if (keyboard.IsKeyDown(Keys.D))
                Input.Direction = InputDirection.Right;
            else if (keyboard.IsKeyDown(Keys.W))
                Input.Direction = InputDirection.Up;
            else if (keyboard.IsKeyDown(Keys.S))
                Input.Direction = InputDirection.Down;
            else
                Input.Direction = InputDirection.None;

            if (keyboard.IsKeyDown(Keys.Space))
                Input.Action = InputAction.Jump;
            else
                Input.Action = InputAction.None;
        }
    }

    public class GameInput
    {
        public InputDirection Direction { get; set; }
        public InputAction Action { get; set; }

        public bool Up => Direction == InputDirection.Up;
        public bool Down => Direction == InputDirection.Down;
        public bool Left => Direction == InputDirection.Left;
        public bool Right => Direction == InputDirection.Right;
        public bool Jump => Action == InputAction.Jump;
    }

    public class ChangePlayerStateToIdle : IHandleUpdates
    {
        public readonly Player Player;
        private readonly GameInput input;

        public ChangePlayerStateToIdle(Player Player, GameInput input)
        {
            this.Player = Player;
            this.input = input;
        }

        public void Update()
        {
            if (Player.Grounded && input.Direction == InputDirection.None)
                Player.State = PlayerState.IDLE;
        }
    }

    public class ChangePlayerStateToWalking : IHandleUpdates
    {
        public readonly Player Player;
        private readonly GameInput input;

        public ChangePlayerStateToWalking(Player Player, GameInput input)
        {
            this.Player = Player;
            this.input = input;
        }

        public void Update()
        {
            if (Player.Grounded)
            {
                if (input.Direction == InputDirection.Left)
                {
                    Player.State = PlayerState.WALKING;
                    Player.FacingRight = false;
                }
                else if (input.Direction == InputDirection.Right)
                {
                    Player.State = PlayerState.WALKING;
                    Player.FacingRight = true;
                }
            }
        }
    }
    public class ChangePlayerStateToJumping : IHandleUpdates
    {
        public readonly Player Player;
        private readonly GameInput input;

        public ChangePlayerStateToJumping(Player Player, GameInput input)
        {
            this.Player = Player;
            this.input = input;
        }

        public void Update()
        {
            if (Player.Grounded && input.Action == InputAction.Jump)
            {
                Player.Velocity.Y = -200;
                Player.State = PlayerState.JUMP;
            }
        }
    }
    public class Player : GameObject, IHaveState
    {
        private readonly Collider Collider;
        [Obsolete]
        internal bool Grounded;

        public Player()
        {
            Animation = new SimpleAnimation(new AnimationFrame(this, "player", 0, 0, GameConstants.BLOCK_SIZE, GameConstants.BLOCK_SIZE * 2));
            Collider = new Collider(this)
            {
                Width = GameConstants.BLOCK_SIZE,
                Height = GameConstants.BLOCK_SIZE * 2,
                Collision = new BlockCollisionHandler()
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

            var changesSpeed = new IncreaseHorizontalVelocity(this, 10);
            var decreaseVelocity = new DecreaseHorizontalVelocity(this, 5);
            var limitHorizontalVelocity = new LimitHorizontalVelocity(this, 100);
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

    public class BlockCollisionHandler : IHandleCollisions
    {
        public void Collide(Collider Source, CollisionDirection direction, Collider Target)
        {
            if (Target.Parent is Block == false)
                return;

            if (direction == CollisionDirection.Bot)
            {
                //TODO: - offsetY
                Source.Parent.Position.Y = Target.Top() - Source.Height - 1;
                Source.Parent.Velocity.Y = 0;
                return;
            }

            if (direction == CollisionDirection.Top)
            {
                //TODO: - offsetY
                Source.Parent.Position.Y = Target.Bottom() + Target.Height + 1;
                Source.Parent.Velocity.Y = 0;
                return;
            }

            if (direction == CollisionDirection.Left)
            {
                Source.Parent.Position.X = Target.Right() + 1 - Source.OffsetX;
                Source.Parent.Velocity.X = 0;
                return;
            }

            if (direction == CollisionDirection.Right)
            {
                Source.Parent.Position.X = Target.Left() - Source.OffsetX - Source.Width - 1;
                Source.Parent.Velocity.X = 0;
                return;
            }
        }
    }
}
