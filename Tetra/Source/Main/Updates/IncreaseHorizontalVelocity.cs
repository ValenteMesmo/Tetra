namespace Tetra
{
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
}
