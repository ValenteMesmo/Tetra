using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tetra
{
    public class CameraKeyboardControls
    {
        private Camera camera;
        private float Zoom => camera.Zoom;

        public CameraKeyboardControls(Camera camera)
        {
            this.camera = camera;
        }

        public void Update()
        {
            Vector2 cameraMovement = Vector2.Zero;
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

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                cameraMovement.Y = -moveSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                cameraMovement.Y = moveSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                cameraMovement.X = -moveSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                cameraMovement.X = moveSpeed;

            camera.MoveCamera(cameraMovement);
        }
    }
}
