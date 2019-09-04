using Microsoft.Xna.Framework.Input;

namespace Tetra.Desktop
{
    public class UpdateInput : IHandleUpdates
    {
        private readonly GameInput Input;

        public UpdateInput(GameInput Input)
        {
            this.Input = Input;
        }

        public void Update()
        {
            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.A))
                Input.Direction = InputDirection.Left;
            else if (keyboard.IsKeyDown(Keys.D))
                Input.Direction = InputDirection.Right;
            else if (keyboard.IsKeyDown(Keys.W))
                Input.Direction = InputDirection.Up;
            else if (keyboard.IsKeyDown(Keys.S))
                Input.Direction = InputDirection.Down;
            else
                Input.Direction = InputDirection.None;

            if (keyboard.IsKeyDown(Keys.Space))
                Input.Action = InputAction.Jump;
            else
                Input.Action = InputAction.None;
        }
    }
}
