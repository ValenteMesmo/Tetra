using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tetra
{
    public class CameraMouseControls : IHandleUpdates
    {
        private readonly Camera camera;
        private readonly MouseInput mouse;
        private bool wasPressed;
        private Point mouseOrigin;

        public CameraMouseControls(Camera camera, MouseInput mouse)
        {
            this.camera = camera;
            this.mouse = mouse;
        }

        public void Update()
        {
            if (mouse.MiddleButtonPressed)
            {
                if (!wasPressed)
                {
                    wasPressed = true;
                    mouseOrigin = mouse.ScreenPosition;
                }

                var speedX = NewMethod(mouse.ScreenPosition.X, mouseOrigin.X);
                var speedY = NewMethod(mouse.ScreenPosition.Y, mouseOrigin.Y);

                camera.Position = new Point(camera.Position.X + speedX, camera.Position.Y + speedY);
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
