using System.Collections.Generic;

namespace Tetra
{
    public interface World
    {
        bool RenderColliders { get; set; }

        IReadOnlyList<GameObject> GetObjects();
        void AddObject(GameObject Object);
    }
}
