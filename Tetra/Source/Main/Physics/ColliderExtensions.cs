namespace Tetra
{
    public static class ColliderExtensions
    {
        public static void IsCollidingHorizontally(
            this Collider a,
            Collider b)
        {
            if (a.Left() <= b.Right()
                && b.Left() <= a.Right()
                && a.Top() <= b.Bottom()
                && b.Top() <= a.Bottom())
            {
                if (a.Right() - b.Right() > 0)
                    a.Collision.Collide(a, CollisionDirection.Left, b);
                else if (a.Right() - b.Right() < 0)
                    a.Collision.Collide(a, CollisionDirection.Right, b);
            }
        }

        public static void IsCollidingVertically(
           this Collider a,
            Collider b)
        {

            if (a.Left() <= b.Right()
                && b.Left() <= a.Right()
                && a.Top() <= b.Bottom()
                && b.Top() <= a.Bottom())
            {
                if (a.Bottom() - b.Bottom() > 0)
                    a.Collision.Collide(a, CollisionDirection.Top, b);
                else if (a.Bottom() - b.Bottom() < 0)
                    a.Collision.Collide(a, CollisionDirection.Bot, b);
            }
        }

        public static int Left(this Collider a) => a.RelativeX();
        public static int Right(this Collider a) => a.RelativeX() + a.Width;
        public static int Top(this Collider a) => a.RelativeY();
        public static int Bottom(this Collider a) => a.RelativeY() + a.Height;
        public static int CenterX(this Collider collider) => (collider.Left() + collider.Right()) / 2;
        public static int CenterY(this Collider collider) => (collider.Top() + collider.Bottom()) / 2;
        public static int RelativeX(this Collider collider) => collider.Parent.Position.X + collider.OffsetX;
        public static int RelativeY(this Collider collider) => collider.Parent.Position.Y + collider.OffsetY;
    }
}
