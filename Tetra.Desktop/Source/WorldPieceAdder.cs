using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetra.Desktop
{
    public class MouseCursor : GameObject
    {
        public MouseCursor()
        {
        }

        public override void Update()
        {
            Position = new Vector2(
                           (float)(Math.Floor(mouse.WorldPosition.X / size) * size),
                           (float)(Math.Floor(mouse.WorldPosition.Y / size) * size)
                       );
        }

        public override IEnumerable<Collider> GetColliders()
        {
            return base.GetColliders();
        }        
    }

    public class WorldPieceAdder
    {
        private readonly List<GameObject> WorldPieces;
        private readonly MouseInfo mouse;
        private const int size = GameConstants.BLOCK_SIZE;

        public WorldPieceAdder(List<GameObject> WorldPieces, MouseInfo mouse)
        {
            this.WorldPieces = WorldPieces;
            this.mouse = mouse;
        }

        public void Update()
        {
            if (mouse.LeftButtonPressed)
            {
                if (!WorldPieces.Any(f => new Rectangle(f.Position.ToPoint(), new Vector2(size, size).ToPoint()).Contains(mouse.WorldPosition)))
                {
                    WorldPieces.Add(new GameObject()
                    {
                        Position = new Vector2(
                            (float)(Math.Floor(mouse.WorldPosition.X / size) * size),
                            (float)(Math.Floor(mouse.WorldPosition.Y / size) * size)
                        )
                    });
                }
            }
            else if (mouse.RightButtonPressed)
            {
                var obj = WorldPieces.FirstOrDefault(f => new Rectangle(f.Position.ToPoint(), new Vector2(size, size).ToPoint()).Contains(mouse.WorldPosition));
                if (obj != null)
                {
                    WorldPieces.Remove(obj);
                }
            }
        }
    }    
}
