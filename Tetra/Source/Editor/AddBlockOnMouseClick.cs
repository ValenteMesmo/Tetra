using System.Linq;

namespace Tetra
{
    public class AddBlockOnMouseClick : IHandleUpdates
    {
        private readonly MouseInput mouse;
        private readonly MouseCursor cursor;
        private readonly CollisionsKeeper parentCollisions;
        private readonly World World;

        public AddBlockOnMouseClick(MouseCursor cursor, MouseInput mouse, CollisionsKeeper parentCollisions, World World)
        {
            this.mouse = mouse;
            this.cursor = cursor;
            this.parentCollisions = parentCollisions;
            this.World = World;
        }

        public void Update()
        {
            if (mouse.LeftButtonPressed && parentCollisions.Collisions.Any() == false)
            {
                var position = cursor.Position.GetCellPosition();
                World.AddObject(
                    new EditorObject(() => new Block() { Position = position }) { Position = position });
            }
        }
    }
}
