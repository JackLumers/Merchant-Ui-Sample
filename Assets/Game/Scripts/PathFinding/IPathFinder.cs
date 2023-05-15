using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.PathFinding
{
    public interface IPathFinder
    {
        public IEnumerable<Vector2> FindPath(Vector2 a, Vector2 c, IEnumerable<Edge> edges);
    }
    
    [Serializable]
    public struct Edge
    {
        public Rectangle First;
        public Rectangle Second;
        
        // It's Vector3 in test task so I left it Vector3.
        // But since z coordinate not used it's just ignored without exception.
        public Vector3 Start;
        public Vector3 End;
    }
        
    [Serializable]
    public struct Rectangle
    {
        public Vector2 Min;
        public Vector2 Max;
    }
}