namespace Tetra
{
    public class MoveMouseCursorToNearbyCell : IHandleUpdates
    {
        private readonly MouseInput mouse;
        private readonly MouseCursor cursor;
        private const int size = GameConstants.BlockSize;

        public MoveMouseCursorToNearbyCell(MouseInput mouse, MouseCursor cursor)
        {
            this.mouse = mouse;
            this.cursor = cursor;
        }

        public void Update()
        {
            cursor.Position = mouse.GetCellPosition();
        }
    }
}
