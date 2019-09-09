using Microsoft.Xna.Framework;
using System;

namespace Tetra
{
    public static class MouseInfoExtensions
    {
        public static Point GetCellPosition(this MouseInput mouse) => new Point(
            (int)(Math.Floor((float)mouse.WorldPosition.X / GameConstants.BlockSize) * GameConstants.BlockSize),
            (int)(Math.Floor((float)mouse.WorldPosition.Y / GameConstants.BlockSize) * GameConstants.BlockSize)
        );
    }
}
