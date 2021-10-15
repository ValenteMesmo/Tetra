namespace Tetra
{
    public class ChangeToJumpState : IHandleUpdates
    {
        public readonly Player Player;
        private readonly GameInput input;
        private readonly int jumpForce;

        public ChangeToJumpState(Player Player, GameInput input, int jumpForce)
        {
            this.Player = Player;
            this.input = input;
            this.jumpForce = jumpForce;
        }

        public void Update()
        {
            if (Player.Grounded && input.Action == InputAction.Jump)
            {
                Player.Velocity.Y = -jumpForce;
                Player.State = PlayerState.Jump;
            }
        }
    }
}
