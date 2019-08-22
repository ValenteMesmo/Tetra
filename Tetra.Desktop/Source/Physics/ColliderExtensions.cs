namespace Tetra.Desktop
{
    public static class ColliderExtensions
    {
        public static void IsCollidingH(
            this Collider a,
            Collider b)
        {
            if (a.Left() <= b.Right()
                && b.Left() <= a.Right()
                && a.Top() <= b.Bottom()
                && b.Top() <= a.Bottom())
            {
                if (a.Right() - b.Right() > 0)
                    a.LeftCollision(a, b);
                else if (a.Right() - b.Right() < 0)
                    a.RightCollision(a, b);
            }
        }

        public static void IsCollidingV(
           this Collider a,
            Collider b)
        {

            if (a.Left() <= b.Right()
                && b.Left() <= a.Right()
                && a.Top() <= b.Bottom()
                && b.Top() <= a.Bottom())
            {
                if (a.Bottom() - b.Bottom() > 0)
                    a.TopCollision(a, b);
                else if (a.Bottom() - b.Bottom() < 0)
                    a.BotCollision(a, b);
            }
        }

        public static float Left(this Collider a) => a.RelativeX();

        public static float Right(this Collider a) => a.RelativeX() + a.Width;

        public static float Top(this Collider a) => a.RelativeY();

        public static float Bottom(this Collider a) => a.RelativeY() + a.Height;

        public static float CenterX(this Collider collider) => (collider.Left() + collider.Right()) * 0.5f;

        public static float CenterY(this Collider collider) => (collider.Top() + collider.Bottom()) * 0.5f;

        public static float RelativeX(this Collider collider) => collider.Parent.Position.X + collider.OffsetX;

        public static float RelativeY(this Collider collider) => collider.Parent.Position.Y + collider.OffsetY;
    }
}
