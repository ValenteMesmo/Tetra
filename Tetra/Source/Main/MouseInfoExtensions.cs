using Microsoft.Xna.Framework;
using System;

namespace Tetra
{
    public static class MouseInfoExtensions
    {
        public static Point GetCellPosition(this MouseInfo mouse) {
            return new Point(
                (int)(Math.Floor(mouse.WorldPosition.X / GameConstants.BLOCK_SIZE) * GameConstants.BLOCK_SIZE),
                (int)(Math.Floor(mouse.WorldPosition.Y / GameConstants.BLOCK_SIZE) * GameConstants.BLOCK_SIZE)
            );
        }
    }
}
