using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.PathFinding
{
    public class LesserTurnsPathFinder : IPathFinder
    {
        public IEnumerable<Vector2> FindPath(Vector2 a, Vector2 c, IEnumerable<Edge> edges)
        { 
            return AStarSearch.SearchPath(new EdgesAndRectanglesCentersGraph(edges, a, c), a, c).Keys;
        }
        
        /// <summary>
        /// Graph where every point is <see cref="Edge"/> or <see cref="Rectangle"/> center.
        ///
        /// Not most precise, but quite performant.
        /// </summary>
        private class EdgesAndRectanglesCentersGraph : IWeightedGraph<Vector2>
        {
            private Dictionary<Vector2, List<Vector2>> _nodesDictionary = new Dictionary<Vector2, List<Vector2>>();

            public EdgesAndRectanglesCentersGraph(IEnumerable<Edge> edges, Vector2 start, Vector2 end)
            {
                throw new NotImplementedException();
                
                using var enumerator = edges.GetEnumerator();
                
                var firstEdge = enumerator.Current;
                var firstRect = firstEdge.First;

                ValidateEdge(firstEdge);
                ValidateRectangle(firstRect);

                if (!InBounds(start, firstRect))
                    throw new ArgumentException("Start point is out of bounds of the first Rectangle.");

                var edgeCenter = (firstEdge.Start + firstEdge.End) / 2;
                var firstRectCenter = (firstRect.Min + firstRect.Max) / 2;
                var secondRectCenter = (firstEdge.Second.Min + firstEdge.Second.Max) / 2;

                if (_nodesDictionary.TryGetValue(start, out var neighbours))
                {
                    neighbours.Add(edgeCenter);
                    neighbours.Add(firstRectCenter);
                    
                    //if(Vector2.)
                    neighbours.Add(secondRectCenter);
                }

                while (enumerator.MoveNext())
                {
                        
                }
            }
            
            public double Cost(Vector2 a, Vector2 b)
            {
                // Cost between two graph nodes here is 1 all times, because we don't have any "expensive" nodes.
                return 1;
            }

            public IEnumerable<Vector2> Neighbors(Vector2 node)
            {
                throw new NotImplementedException();
            }
            
            private bool InBounds(Vector2 node, Rectangle rectangle)
            {
                return node.x >= rectangle.Min.x && node.x <= rectangle.Max.x && node.y >= rectangle.Min.y &&
                       node.y <= rectangle.Max.y;
            }

            private static void ValidateEdge(Edge edge)
            {
                if (Math.Abs(edge.Start.y - edge.End.y) > 0)
                {
                    throw new ArgumentException(
                        $"Edge Start and End positions must have equal Y axis! Edge: {edge}");
                }

                if (edge.Start.x >= edge.End.x)
                {
                    throw new ArgumentException(
                        $"Edge Start position must be lees than Edge End position! Edge: {edge}");
                }
            }
            
            private static void ValidateRectangle(Rectangle rectangle)
            {
                if (rectangle.Min.x >= rectangle.Max.x || rectangle.Min.y >= rectangle.Max.y)
                {
                    throw new ArgumentException($"Rectangle Min position must be lees than Rectangle Max position! {rectangle}");
                }
            }
        }
    }
}