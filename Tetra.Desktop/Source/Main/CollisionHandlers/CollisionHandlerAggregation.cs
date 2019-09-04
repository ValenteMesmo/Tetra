namespace Tetra.Desktop
{
    public class CollisionHandlerAggregation : IHandleCollisions
    {
        private readonly IHandleCollisions[] collisions;

        public CollisionHandlerAggregation(params IHandleCollisions[] collisions)
        {
            this.collisions = collisions;
        }

        public void Collide(Collider Source, CollisionDirection direction, Collider Target)
        {
            foreach (var item in collisions)
            {
                item.Collide(Source, direction, Target);
            }
        }
    }
}
