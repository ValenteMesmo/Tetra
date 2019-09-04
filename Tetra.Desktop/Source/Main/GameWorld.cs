﻿using System.Collections.Generic;

namespace Tetra.Desktop
{
    public class GameWorld : World
    {
        public bool RenderColliders { get; set; } = true;
        public List<GameObject> GameObjects = new List<GameObject>();

        public void AddObject(GameObject Object)
        {
            GameObjects.Add(Object);
        }

        public void AddRange(IEnumerable<GameObject> Objects)
        {
            GameObjects.AddRange(Objects);
        }

        public IReadOnlyList<GameObject> GetObjects()
        {
            return GameObjects;
        }

        public void Clear()
        {
            GameObjects.Clear();
        }
    }
}
