using System.Linq;

namespace Tetra.Desktop
{
    public class AddBlockOnMouseClick : IHandleUpdates
    {
        private readonly MouseInfo mouse;
        private readonly CollisionsKeeper parentCollisions;
        private readonly IAddToWorld World;

        public AddBlockOnMouseClick(MouseInfo mouse, CollisionsKeeper parentCollisions, IAddToWorld World)
        {
            this.mouse = mouse;
            this.parentCollisions = parentCollisions;
            this.World = World;
        }

        public void Update()
        {
            if (mouse.LeftButtonPressed && parentCollisions.Collisions.Any() == false)
            {
                World.Add(new Block { Position = mouse.GetCellPosition() });
            }
        }
    }
}
