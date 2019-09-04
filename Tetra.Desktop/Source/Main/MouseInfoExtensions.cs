using Microsoft.Xna.Framework;
using System;

namespace Tetra.Desktop
{
    public static class MouseInfoExtensions
    {
        public static Vector2 GetCellPosition(this MouseInfo mouse) {
            return new Vector2(
                (float)(Math.Floor(mouse.WorldPosition.X / GameConstants.BLOCK_SIZE) * GameConstants.BLOCK_SIZE),
                (float)(Math.Floor(mouse.WorldPosition.Y / GameConstants.BLOCK_SIZE) * GameConstants.BLOCK_SIZE)
            );
        }
    }
}
