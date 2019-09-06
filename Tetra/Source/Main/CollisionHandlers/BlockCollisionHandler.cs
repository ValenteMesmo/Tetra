namespace Tetra
{
    public class BlockCollisionHandler : IHandleCollisions
    {
        public void Collide(Collider Source, CollisionDirection direction, Collider Target)
        {
            if (Target.Parent is Block == false)
                return;

            if (direction == CollisionDirection.Bot)
            {
                var newY = Target.Top() - Source.Height - Source.OffsetY - 1;
                //var diff = newY - Source.Parent.Position.Y;
                //if (diff > Target.Height || diff < -Target.Height)
                //    return;

                Source.Parent.Position.Y = newY;
                Source.Parent.Velocity.Y = 0;
                return;
            }

            if (direction == CollisionDirection.Top)
            {
                var newY = Target.Bottom() - Source.OffsetY + 1;
                //var diff = newY - Source.Parent.Position.Y;
                //if (diff > Target.Height || diff < -Target.Height)
                //    return;

                Source.Parent.Position.Y = newY;
                Source.Parent.Velocity.Y = 0;
                return;
            }

            if (direction == CollisionDirection.Left)
            {
                var newX = Target.Right() - Source.OffsetX + 1;
                //var diff = newX - Source.Parent.Position.X;
                //if (diff > Target.Width || diff < -Target.Width)
                //    return;

                Source.Parent.Position.X = newX;
                Source.Parent.Velocity.X = 0;
                return;
            }

            if (direction == CollisionDirection.Right)
            {
                var newX = Target.Left() - Source.OffsetX - Source.Width - 1;
                //var diff = newX - Source.Parent.Position.X;
                //if (diff > Target.Width || diff < -Target.Width)
                //    return;

                Source.Parent.Position.X = newX;
                Source.Parent.Velocity.X = 0;
                return;
            }
        }
    }
}
