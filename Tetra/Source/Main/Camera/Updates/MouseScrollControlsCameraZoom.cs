using Microsoft.Xna.Framework.Input;

namespace Tetra
{
    public class MouseScrollControlsCameraZoom : IHandleUpdates
    {
        private readonly Camera Camera;

        private float currentMouseWheelValue;
        private float previousMouseWheelValue;

        public MouseScrollControlsCameraZoom(Camera Camera)
        {
            this.Camera = Camera;
        }

        public void Update()
        {
            var mouse = Mouse.GetState();
            previousMouseWheelValue = currentMouseWheelValue;
            currentMouseWheelValue = mouse.ScrollWheelValue;

            if (currentMouseWheelValue > previousMouseWheelValue)
                Camera.AdjustZoom(.05f);

            if (currentMouseWheelValue < previousMouseWheelValue)
                Camera. AdjustZoom(-.05f);
        }
    }
}
