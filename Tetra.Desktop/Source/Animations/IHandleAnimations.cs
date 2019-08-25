using System.Collections.Generic;

namespace Tetra.Desktop
{
    public interface IHandleAnimations
    {
        IEnumerable<AnimationFrame> GetFrame();
        void Update();
        bool RenderOnUiLayer { get; }
    }
}
