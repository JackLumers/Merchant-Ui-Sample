using System;
using System.Collections.Generic;
using Priority_Queue;
using UnityEngine;

namespace Game.Scripts.PathFinding
{
    public static class AStarSearch
    {
        public static Dictionary<Vector2, Vector2> SearchPath(IWeightedGraph<Vector2> graph, Vector2 start, Vector2 goal)
        {
            var path = new Dictionary<Vector2, Vector2>();
            var costSoFar = new Dictionary<Vector2, double>();
            
            // Using third party open source code with MIT licence here!
            // New C# version has it's own PriorityQueue but on current project version we don't have it so...
            var frontier = new SimplePriorityQueue<Vector2, double>();

            frontier.Enqueue(start, 0);

            path[start] = start;
            costSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current.Equals(goal))
                {
                    break;
                }

                foreach (var next in graph.Neighbors(current))
                {
                    var newCost = costSoFar[current] + graph.Cost(current, next);
                    
                    if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        var priority = newCost + Heuristic(next, goal);
                        frontier.Enqueue(next, priority);
                        path[next] = current;
                    }
                }
            }

            return path;
        }

        private static double Heuristic(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }
    }
    
    public interface IWeightedGraph<TCoordinate>
    {
        double Cost(TCoordinate a, TCoordinate b);
        IEnumerable<TCoordinate> Neighbors(TCoordinate node);
    }
}