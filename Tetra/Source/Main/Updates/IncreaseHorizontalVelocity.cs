namespace Tetra
{
    public class IncreaseHorizontalVelocity : IHandleUpdates
    {
        public readonly int Speed;
        public readonly GameObject Target;
        private readonly GameInput Input;

        public IncreaseHorizontalVelocity(GameObject Target, int Speed, GameInput Input)
        {
            this.Speed = Speed;
            this.Target = Target;
            this.Input = Input;
        }

        public void Update()
        {
            if (Input.Right)
                Target.Velocity.X += Speed;
            else if (Input.Left)
                Target.Velocity.X -= Speed;
        }
    }
}
