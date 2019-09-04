namespace Tetra
{
    public class GravityChangesVerticalSpeed : IHandleUpdates
    {
        private readonly GameObject GameObject;

        public GravityChangesVerticalSpeed(GameObject GameObject)
        {
            this.GameObject = GameObject;
        }

        public void Update()
        {
            GameObject.Velocity.Y += 10;
            if (GameObject.Velocity.Y > 80)
                GameObject.Velocity.Y = 80;
        }
    }
}
