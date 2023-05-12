using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.UI.DragAndDrop
{
    /// <remarks>
    /// OnDrag events work only if old input supported.
    ///
    /// With new Input System support only, this events will not raise.
    /// So make sure "Both" support in player settings set.
    /// </remarks>
    [RequireComponent(typeof(RectTransform))]
    public class UiDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform _rectTransform;
        private Vector3 _currentVelocity;
        
        public event Action<PointerEventData> DragStarted;
        public event Action<PointerEventData> Drag;
        public event Action<PointerEventData> DragEnded;
        
        private void Awake()
        {
            _rectTransform = transform as RectTransform;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            DragStarted?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, eventData.position,
                    eventData.pressEventCamera, out var pointerWorldPosition))
            {
                _rectTransform.position = pointerWorldPosition;
                
                Drag?.Invoke(eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DragEnded?.Invoke(eventData);
        }

        private void OnDestroy()
        {
            DragStarted = null;
            Drag = null;
            DragEnded = null;
        }
    }
}