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
        
        public Vector2 Start;
        public Vector2 End;
    }

    [Serializable]
    public struct Rectangle
    {
        public Vector2 Min;
        public Vector2 Max;
    }
}