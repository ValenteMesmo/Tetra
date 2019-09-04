using System.Collections.Generic;

namespace Tetra.Desktop
{
    public interface World
    {
        bool RenderColliders { get; set; }

        IReadOnlyList<GameObject> GetObjects();
        void AddObject(GameObject Object);
    }
}
