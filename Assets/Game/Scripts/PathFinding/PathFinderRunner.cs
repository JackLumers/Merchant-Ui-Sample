using UnityEngine;

namespace Game.Scripts.PathFinding
{
    public class PathFinderRunner : MonoBehaviour
    {
        [SerializeField] private Drawer _drawer;
        [SerializeField] private Vector2 _startPoint, _endPoint;
        [SerializeField] private Edge[] _edges;

        private IPathFinder _pathFinder;

        private void Awake()
        {
            _pathFinder = new LesserTurnsPathFinder();
            
            // Drawing all edges and rectangles
            foreach (var edge in _edges)
            {
                _drawer.DrawRect(edge.First);
                _drawer.DrawEdge(edge);
            }
            
            if (_edges.Length > 0)
            {
                _drawer.DrawRect(_edges[_edges.Length-1].Second);
            }

            //var path = _pathFinder.FindPath(_startPoint, _endPoint, _edges);
        }

        [ContextMenu("Find path and log results")]
        private void FindPathAndLog()
        {
            var path = _pathFinder.FindPath(_startPoint, _endPoint, _edges);

            Debug.Log("--- Path log:");
            foreach (var point in path)
            {
                Debug.Log(point);
            }
            Debug.Log("--- Path log end.");
        }
    }
}