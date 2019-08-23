using Microsoft.Xna.Framework;
using System;

namespace Tetra.Desktop
{
    public class MoveMouseCursorToNearbyCell : IHandleUpdates
    {
        private readonly MouseInfo mouse;
        private readonly MouseCursor cursor;
        private const int size = GameConstants.BLOCK_SIZE;

        public MoveMouseCursorToNearbyCell(MouseInfo mouse, MouseCursor cursor)
        {
            this.mouse = mouse;
            this.cursor = cursor;
        }

        public void Update()
        {
            cursor.Position = new Vector2(
                (float)(Math.Floor(mouse.WorldPosition.X / size) * size),
                (float)(Math.Floor(mouse.WorldPosition.Y / size) * size)
            );
        }
    }
}
