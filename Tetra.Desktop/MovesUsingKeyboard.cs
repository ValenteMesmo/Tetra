using Microsoft.Xna.Framework.Input;

namespace Tetra.Desktop
{
    public class MovesUsingKeyboard : IHandleUpdates
    {
        private readonly GameObject Parent;

        public MovesUsingKeyboard(GameObject Parent)
        {
            this.Parent = Parent;
        }

        public void Update()
        {
            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.A))
                Parent.Velocity.X = -5;
            else if (keyboard.IsKeyDown(Keys.D))
                Parent.Velocity.X = 5;
            else
                Parent.Velocity.X = 0;
        }
    }
}
