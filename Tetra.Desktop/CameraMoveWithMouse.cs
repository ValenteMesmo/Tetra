using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tetra.Desktop
{
    public class CameraMoveWithMouse
    {
        private readonly Camera camera;
        private bool wasPressed;
        private Point mouseOrigin;

        public CameraMoveWithMouse(Camera camera)
        {
            this.camera = camera;
        }

        public void Update()
        {
            var mouse = Mouse.GetState();

            if (mouse.RightButton == ButtonState.Pressed)
            {

                if (!wasPressed)
                {
                    wasPressed = true;
                    mouseOrigin = mouse.Position;
                }

                var speedX = NewMethod(mouse.Position.X, mouseOrigin.X);
                var speedY = NewMethod(mouse.Position.Y, mouseOrigin.Y);

                camera.Position = new Vector2(camera.Position.X + speedX, camera.Position.Y + speedY);
            }
            else
                wasPressed = false;

        }

        private int NewMethod(int from, int to)
        {
            var result = from - to;

            if (result > 0)
            {
                if (result < 100)
                    return 0;

                if (result < 250)
                    return 5;

                if (result < 500)
                    return 10;

                return 20;
            }

            if (result < 0)
            {
                if (result > -100)
                    return 0;

                if (result > -250)
                    return -5;

                if (result > -500)
                    return -10;

                return -20;
            }

            return 0;
        }
    }
}
