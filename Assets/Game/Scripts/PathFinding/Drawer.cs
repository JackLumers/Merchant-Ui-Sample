using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.PathFinding
{
    /// <summary>
    /// Just a simple utility component to visualize path finding algorithm and structs.
    /// Not optimized with pooling, e.t.c., but it's not intended to be.
    /// </summary>
    public class Drawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _rectLineRendererPrefab;
        [SerializeField] private LineRenderer _edgeLineRendererPrefab;
        
        [SerializeField] private LineRenderer _pathLineRenderer;

        private Dictionary<Rectangle, LineRenderer> _rectangleRenderers = new Dictionary<Rectangle, LineRenderer>();
        private Dictionary<Edge, LineRenderer> _edgesRenderers = new Dictionary<Edge, LineRenderer>();
        
        public void DrawRect(Rectangle rectangle)
        {
            var cornersPositions = new Vector3[]
            {
                rectangle.Min,
                new Vector2(rectangle.Max.x, rectangle.Min.y),
                rectangle.Max,
                new Vector2(rectangle.Min.x, rectangle.Max.y)
            };
            
            var lineRenderer = Instantiate(_rectLineRendererPrefab, transform);
            lineRenderer.positionCount = 4;
            lineRenderer.SetPositions(cornersPositions);
        }

        public void DrawEdge(Edge edge)
        {
            var cornersPositions = new Vector3[]
            {
                edge.Start,
                edge.End
            };

            var lineRenderer = Instantiate(_edgeLineRendererPrefab, transform);
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(cornersPositions);
        }
        
        public void DrawPath(Vector3[] points)
        {
            _pathLineRenderer.positionCount = points.Length;
            _pathLineRenderer.SetPositions(points);
        }
        
        public void Clear()
        {
            foreach (var instance in _rectangleRenderers)
            {
                Destroy(instance.Value.gameObject);
            }

            foreach (var instance in _edgesRenderers)
            {
                Destroy(instance.Value.gameObject);
            }
            
            _rectangleRenderers.Clear();
            _edgesRenderers.Clear();
            
            _pathLineRenderer.SetPositions(Array.Empty<Vector3>());
        }
    }
}