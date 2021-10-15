using Microsoft.Xna.Framework.Input;

namespace Tetra
{
    public class CollisionDebugToggler : IHandleUpdates
    {
        private readonly World world;
        private readonly CooldownTracker cooldown;

        public CollisionDebugToggler(World world, CooldownTracker cooldown)
        {
            this.world = world;
            this.cooldown = cooldown;
        }

        public void Update()
        {
            cooldown.Update();
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.F2) && cooldown.IsOver())
            {
                cooldown.Start();
                world.RenderColliders = !world.RenderColliders;
            }
        }
    }
}
