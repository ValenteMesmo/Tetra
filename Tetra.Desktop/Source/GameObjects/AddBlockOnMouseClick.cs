using System.Linq;

namespace Tetra.Desktop
{
    public class AddBlockOnMouseClick : IHandleUpdates
    {
        private readonly MouseInfo mouse;
        private readonly CollisionsKeeper parentCollisions;
        private readonly GameWorld World;

        public AddBlockOnMouseClick(MouseInfo mouse, CollisionsKeeper parentCollisions, GameWorld World)
        {
            this.mouse = mouse;
            this.parentCollisions = parentCollisions;
            this.World = World;
        }

        public void Update()
        {
            if (mouse.LeftButtonPressed && parentCollisions.Collisions.Any() == false)
            {
                World.AddObject(new Block { Position = mouse.GetCellPosition() });
            }
        }
    }
}
