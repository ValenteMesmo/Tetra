using System.Collections.Generic;

namespace Tetra
{
    public interface World
    {
        bool RenderColliders { get; set; }

        IReadOnlyList<GameObject> GetObjects();
        void RemoveObject(GameObject Object);
        void AddObject(GameObject Object);
    }
}
