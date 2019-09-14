using System;
using System.Collections.Generic;

namespace Tetra
{
    public interface IHandleAnimations
    {
        IEnumerable<AnimationFrame> GetFrame();
        void Update();
        [Obsolete("Remove?")]
        bool RenderOnUiLayer { get; }
    }
}
