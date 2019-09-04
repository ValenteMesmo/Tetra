namespace Tetra.Desktop
{
    public class GameInput
    {
        public InputDirection Direction { get; set; }
        public InputAction Action { get; set; }

        public bool Up => Direction == InputDirection.Up;
        public bool Down => Direction == InputDirection.Down;
        public bool Left => Direction == InputDirection.Left;
        public bool Right => Direction == InputDirection.Right;
        public bool Jump => Action == InputAction.Jump;
    }
}
