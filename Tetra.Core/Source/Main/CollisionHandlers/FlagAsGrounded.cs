namespace Tetra
{
    public class FlagAsGrounded : IHandleCollisions, IHandleUpdates
    {
        private readonly Player Player;

        public FlagAsGrounded(Player Player)
        {
            this.Player = Player;
        }

        public void Update()
        {
            Player.Grounded = false;
        }

        public void Collide(Collider Source, CollisionDirection direction, Collider Target)
        {
            if (direction == CollisionDirection.Bot && Target.Parent is Block)
                Player.Grounded = true;
        }
    }
}
