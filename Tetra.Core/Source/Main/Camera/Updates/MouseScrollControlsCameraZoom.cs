using Microsoft.Xna.Framework.Input;

namespace Tetra
{
    public class MouseScrollControlsCameraZoom : IHandleUpdates
    {
        private readonly Camera Camera;
        private readonly MouseInput mouse;
        private float currentMouseWheelValue;
        private float previousMouseWheelValue;
        private float speed = 0;
        private float Zoom = .05f;

        public MouseScrollControlsCameraZoom(Camera Camera, MouseInput mouse)
        {
            this.Camera = Camera;
            this.mouse = mouse;
        }

        public void Update()
        {
            previousMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = mouse.ScrollWheelValue;

            if (currentMouseWheelValue > previousMouseWheelValue)
                speed += .001f;
            else if (currentMouseWheelValue < previousMouseWheelValue)
                speed -= .001f;
            else if (speed > 0)
            {
                speed -= .00025f;
                if (speed < 0)
                    speed = 0;
            }
            else if (speed < 0)
            {
                speed += .00025f;
                if (speed > 0)
                    speed = 0;
            }

            Zoom += speed;

            if (Zoom < .01f)
            {
                Zoom = .01f;
                speed = 0;
            }

            if (Zoom > .13f)
            {
                Zoom = .13f;
                speed = 0;
            }

            Camera.SetZoom(Zoom);
        }
    }
}
