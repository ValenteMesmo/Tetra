using System.Collections.Generic;

namespace Tetra
{
    public class SimpleAnimation : IHandleAnimations
    {
        public bool RenderOnUiLayer => false;
        private AnimationFrame frame;

        public SimpleAnimation(AnimationFrame frame)
        {
            this.frame = frame;
        }

        public void Update() { }

        public IEnumerable<AnimationFrame> GetFrame()
        {
            yield return frame;
        }
    }
}
