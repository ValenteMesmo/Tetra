using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tetra
{
    public class CameraKeyboardControls : IHandleUpdates
    {
        private Camera camera;
        private float Zoom => camera.Zoom;

        public CameraKeyboardControls(Camera camera)
        {
            this.camera = camera;
        }

        public void Update()
        {
            Point cameraMovement = Point.Zero;
            int moveSpeed;

            if (Zoom > .8f)
                moveSpeed = 15;
            else if (Zoom < .8f && Zoom >= .6f)
                moveSpeed = 20;
            else if (Zoom < .6f && Zoom > .35f)
                moveSpeed = 25;
            else if (Zoom <= .35f)
                moveSpeed = 30;
            else
                moveSpeed = 10;

            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
                cameraMovement.Y = -moveSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
                cameraMovement.Y = moveSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
                cameraMovement.X = -moveSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
                cameraMovement.X = moveSpeed;

            camera.Position = camera.Position + cameraMovement;
        }
    }
}
