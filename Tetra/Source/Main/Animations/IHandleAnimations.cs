using System.Collections.Generic;

namespace Tetra
{
    public interface IHandleAnimations
    {
        IEnumerable<AnimationFrame> GetFrame();
        void Update();
        bool RenderOnUiLayer { get; }
    }
}
