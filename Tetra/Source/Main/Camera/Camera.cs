using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetra
{
    public class Camera
    {
        public float Zoom { get; private set; }
        public Point Position { get; set; }
        public Rectangle Bounds { get; private set; }
        public Rectangle VisibleArea { get; private set; }
        public Matrix Transform { get; private set; }
        const float defaultZoom = .050f;
        public Camera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
            Zoom = defaultZoom;
            Position = Point.Zero;            
        }

        private void UpdateVisibleArea()
        {
            var inverseViewMatrix = Matrix.Invert(Transform);

            var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            var tr = Vector2.Transform(new Vector2(Bounds.X, 0), inverseViewMatrix);
            var bl = Vector2.Transform(new Vector2(0, Bounds.Y), inverseViewMatrix);
            var br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

            var min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));

            var max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));

            VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        internal void ResetZoom()
        {
            Zoom = defaultZoom;
        }

        private void UpdateMatrix()
        {
            Transform =
                Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0))
                * Matrix.CreateScale(Zoom)
                * Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));

            UpdateVisibleArea();
        }

        public void AdjustZoom(float zoomAmount)
        {
            Zoom += zoomAmount;

            LimitZoom();
        }

        public void SetZoom(float value)
        {
            Zoom = value;

            LimitZoom();
        }

        private void LimitZoom()
        {
            if (Zoom < .01f)
                Zoom = .01f;

            if (Zoom > .5f)
                Zoom = .5f;
        }

        public Point ToWorld(Point position) =>
            Vector2.Transform(position.ToVector2(), Matrix.Invert(Transform)).ToPoint();

        public Point ToScreen(Point position) =>
            Vector2.Transform(position.ToVector2(), Transform).ToPoint();

        public void UpdateCamera(Viewport bounds)
        {
            Bounds = bounds.Bounds;
            UpdateMatrix();
        }
    }
}
