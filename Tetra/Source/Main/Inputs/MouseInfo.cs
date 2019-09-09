using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tetra
{
    public class MouseInput
    {
        private readonly Camera Camera;

        public Point ScreenPosition { get; private set; }
        public Point WorldPosition { get; private set; }

        public bool LeftButtonPressed { get; private set; }
        public bool LeftButtonReleased => !LeftButtonPressed;

        public bool RightButtonPressed { get; private set; }
        public bool RightButtonReleased => !RightButtonPressed;

        public bool MiddleButtonPressed { get; private set; }
        public bool MiddleButtonReleased => !MiddleButtonPressed;

        public int ScrollWheelValue { get; private set; }

        public MouseInput(Camera Camera)
        {
            this.Camera = Camera;
        }

        public void Update()
        {
            var mouse = Mouse.GetState();

            WorldPosition = Camera.ToWorld(mouse.Position);
            ScreenPosition = mouse.Position;

            LeftButtonPressed = mouse.LeftButton == ButtonState.Pressed;
            RightButtonPressed = mouse.RightButton == ButtonState.Pressed;
            MiddleButtonPressed = mouse.MiddleButton == ButtonState.Pressed;

            ScrollWheelValue = mouse.ScrollWheelValue;
        }
    }
}
