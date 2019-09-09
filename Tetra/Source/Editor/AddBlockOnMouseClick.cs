using System.Linq;

namespace Tetra
{
    public class AddBlockOnMouseClick : IHandleUpdates
    {
        private readonly MouseInput mouse;
        private readonly CollisionsKeeper parentCollisions;
        private readonly World World;

        public AddBlockOnMouseClick(MouseInput mouse, CollisionsKeeper parentCollisions, World World)
        {
            this.mouse = mouse;
            this.parentCollisions = parentCollisions;
            this.World = World;
        }

        public void Update()
        {
            if (mouse.LeftButtonPressed && parentCollisions.Collisions.Any() == false)
            {
                var position = mouse.GetCellPosition();
                World.AddObject(
                    new EditorObject(() => new Block() { Position = position }) { Position = position });
            }
        }
    }
}
