namespace Tetra
{
    public class GravityChangesVerticalSpeed : IHandleUpdates
    {
        private readonly GameObject GameObject;
        private readonly int speed;
        private readonly int maxSpeed;

        public GravityChangesVerticalSpeed(GameObject GameObject, int speed, int maxSpeed)
        {
            this.GameObject = GameObject;
            this.speed = speed;
            this.maxSpeed = maxSpeed;
        }

        public void Update()
        {
            GameObject.Velocity.Y += speed;
            if (GameObject.Velocity.Y > maxSpeed)
                GameObject.Velocity.Y = maxSpeed;
        }
    }
}
