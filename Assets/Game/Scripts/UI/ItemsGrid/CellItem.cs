using System.Collections.Generic;
using Game.Scripts.ScriptableObjects.InventoryData;
using Game.Scripts.UI.DragAndDrop;
using ToolBox.Pools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.UI.ItemsGrid
{
    /// <summary>
    /// Represents an item in a <see cref="ItemsGridCell"/>.
    /// </summary>
    [RequireComponent(typeof(UiDragAndDrop))]
    public class CellItem : MonoBehaviour, IPoolable
    {
        [SerializeField] private Image _itemImage;

        private UiDragAndDrop _uiDragAndDrop;
        private Transform _transform;
        
        public GameItemData GameItemData { get; private set; }
        public ItemsGridCell OwnerGridCell { get; private set; }

        private void Awake()
        {
            _transform = transform;
            
            _uiDragAndDrop = GetComponent<UiDragAndDrop>();
            _uiDragAndDrop.DragEnded += OnDragEnded;
            _uiDragAndDrop.DragStarted += OnDragStarted;
        }
        
        public void OnReuse() { }

        public void SetItem(GameItemData itemData)
        {
            GameItemData = itemData;
            
            _itemImage.sprite = itemData.ItemData.Sprite;
        }
        
        public void SetOwnerCell(ItemsGridCell ownerCell)
        {
            OwnerGridCell = ownerCell;

            MoveToOwnerCell();
        }

        public void MoveToOwnerCell()
        {
            var cellTransform = OwnerGridCell.transform;
            _transform.SetParent(cellTransform, true);
            _transform.position = cellTransform.position;
        }

        private void OnDragStarted(PointerEventData pointerEventData)
        {
            var position = _transform.position;
            
            _transform.SetParent(OwnerGridCell.GridController.transform.parent, true);
            _transform.position = position;
        }

        private void OnDragEnded(PointerEventData pointerEventData)
        {
            var raycastResults = new List<RaycastResult>();
            OwnerGridCell.GraphicRaycaster.Raycast(pointerEventData, raycastResults);

            foreach (var raycastResult in raycastResults)
            {
                if (!raycastResult.gameObject.TryGetComponent<ItemsGridCell>(out var cellToMoveItem)) 
                    continue;

                var tryPlaceParam = new ItemsGridController.TryPlaceItemParam(this, cellToMoveItem);
                
                if (cellToMoveItem.GridController.TryPlaceItem != null &&
                    cellToMoveItem.GridController.TryPlaceItem.Invoke(tryPlaceParam))
                {
                    OwnerGridCell.ClearData();
                    cellToMoveItem.PlaceCellItem(this);
                    
                    return;
                }
            }
            
            MoveToOwnerCell();
        }
        
        public void OnRelease()
        {
            OwnerGridCell = null;
            GameItemData = null;
            _itemImage.sprite = null;
        }
    }
}