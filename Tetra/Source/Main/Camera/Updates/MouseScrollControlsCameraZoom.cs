using Microsoft.Xna.Framework.Input;

namespace Tetra
{
    public class MouseScrollControlsCameraZoom : IHandleUpdates
    {
        private readonly Camera Camera;
        private readonly MouseInput mouse;
        private float currentMouseWheelValue;
        private float previousMouseWheelValue;

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
                Camera.AdjustZoom(.05f);

            if (currentMouseWheelValue < previousMouseWheelValue)
                Camera. AdjustZoom(-.05f);
        }
    }
}
