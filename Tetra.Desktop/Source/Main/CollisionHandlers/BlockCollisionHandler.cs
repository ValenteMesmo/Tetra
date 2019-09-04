namespace Tetra.Desktop
{
    public class BlockCollisionHandler : IHandleCollisions
    {
        public void Collide(Collider Source, CollisionDirection direction, Collider Target)
        {
            if (Target.Parent is Block == false)
                return;

            if (direction == CollisionDirection.Bot)
            {
                //TODO: - offsetY
                Source.Parent.Position.Y = Target.Top() - Source.Height - 1;
                Source.Parent.Velocity.Y = 0;
                return;
            }

            if (direction == CollisionDirection.Top)
            {
                //TODO: - offsetY
                Source.Parent.Position.Y = Target.Bottom() + Target.Height + 1;
                Source.Parent.Velocity.Y = 0;
                return;
            }

            if (direction == CollisionDirection.Left)
            {
                Source.Parent.Position.X = Target.Right() + 1 - Source.OffsetX;
                Source.Parent.Velocity.X = 0;
                return;
            }

            if (direction == CollisionDirection.Right)
            {
                Source.Parent.Position.X = Target.Left() - Source.OffsetX - Source.Width - 1;
                Source.Parent.Velocity.X = 0;
                return;
            }
        }
    }
}
