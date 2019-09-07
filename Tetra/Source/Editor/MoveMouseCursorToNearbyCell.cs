namespace Tetra
{
    public class MoveMouseCursorToNearbyCell : IHandleUpdates
    {
        private readonly MouseInfo mouse;
        private readonly MouseCursor cursor;
        private const int size = GameConstants.BlockSize;

        public MoveMouseCursorToNearbyCell(MouseInfo mouse, MouseCursor cursor)
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
