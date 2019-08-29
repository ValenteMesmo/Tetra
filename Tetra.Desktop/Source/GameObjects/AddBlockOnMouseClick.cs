using System.Linq;

namespace Tetra.Desktop
{
    public class AddBlockOnMouseClick : IHandleUpdates
    {
        private readonly MouseInfo mouse;
        private readonly CollisionsKeeper parentCollisions;
        private readonly World World;

        public AddBlockOnMouseClick(MouseInfo mouse, CollisionsKeeper parentCollisions, World World)
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
