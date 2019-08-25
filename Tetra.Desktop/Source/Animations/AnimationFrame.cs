using Microsoft.Xna.Framework;

namespace Tetra.Desktop
{
    public class AnimationFrame
    {
        public readonly GameObject Parent;
        public readonly string Texture;
        public readonly float OffsetX;
        public readonly float OffsetY;
        public readonly int Width;
        public readonly int Height;
        public readonly Color Color = Color.White;

        public AnimationFrame(
            GameObject Parent,
            string Texture,
            float OffsetX,
            float OffsetY,
            int Width,
            int Height)
        {
            this.Parent = Parent;
            this.Texture = Texture;
            this.OffsetX = OffsetX;
            this.OffsetY = OffsetY;
            this.Width = Width;
            this.Height = Height;
        }
    }
}
