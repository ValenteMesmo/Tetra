namespace Tetra
{
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
                Player.State = PlayerState.Idle;
        }
    }
}
