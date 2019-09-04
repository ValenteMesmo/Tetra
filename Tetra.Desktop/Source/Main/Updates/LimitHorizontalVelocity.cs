namespace Tetra.Desktop
{
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
}
