namespace Tetra.Desktop
{
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
                Player.Velocity.Y = -50;
                Player.State = PlayerState.JUMP;
            }
        }
    }
}
