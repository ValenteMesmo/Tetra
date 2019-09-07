namespace Tetra
{
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
            {
                Target.Velocity.X -= Speed;
                if(Target.Velocity.X < 0)
                    Target.Velocity.X = 0;
            }
            else if (Target.Velocity.X < 0)
            {
                Target.Velocity.X += Speed;
                if (Target.Velocity.X > 0)
                    Target.Velocity.X = 0;
            }
        }
    }
}
