using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tetra
{
    public class MouseInfo
    {
        private readonly Camera Camera;

        public Vector2 ScreenPosition { get; private set; }
        public Vector2 WorldPosition { get; private set; }
        public bool LeftButtonPressed { get; private set; }
        public bool LeftButtonReleased => !LeftButtonPressed;
        public bool RightButtonPressed { get; private set; }
        public bool RightButtonReleased => !RightButtonPressed;
        

        public MouseInfo(Camera Camera)
        {
            this.Camera = Camera;
        }

        public void Update()
        {
            var mouse = Mouse.GetState();

            WorldPosition = Camera.ToWorld(mouse.Position.ToVector2());
            ScreenPosition = mouse.Position.ToVector2();

            LeftButtonPressed = mouse.LeftButton == ButtonState.Pressed;
            RightButtonPressed = mouse.RightButton == ButtonState.Pressed;
        }
    }
}
