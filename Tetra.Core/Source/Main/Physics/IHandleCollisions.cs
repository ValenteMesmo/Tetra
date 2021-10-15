namespace Tetra
{
    public interface IHandleCollisions
    {
        void Collide(Collider Source, CollisionDirection direction, Collider Target);
    }

    public enum CollisionDirection
    {
        Top,
        Bot,
        Left,
        Right
    }
}
