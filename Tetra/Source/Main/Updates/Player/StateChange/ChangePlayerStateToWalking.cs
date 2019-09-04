namespace Tetra
{
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
}
