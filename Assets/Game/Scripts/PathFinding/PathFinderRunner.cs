using UnityEngine;

namespace Game.Scripts.PathFinding
{
    public class PathFinderRunner : MonoBehaviour
    {
        [SerializeField] private Vector2 _startPoint, _endPoint;
        [SerializeField] private Edge[] _edges;

        private IPathFinder _pathFinder;

        private void Awake()
        {
            _pathFinder = new AStarPathFinder();
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