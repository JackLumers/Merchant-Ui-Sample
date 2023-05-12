using System;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.ScriptableObjects.InventoryData;
using ToolBox.Pools;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.ItemsGrid
{
    /// <summary>
    /// Represents cell in <see cref="ItemsGridController"/>.
    /// </summary>
    public class ItemsGridCell : MonoBehaviour
    {
        [SerializeField] private GameObject _cellItemPrefab;

        private CellItem _cellItemInstance;
        
        public ItemsGridController GridController { get; private set; }
        public GraphicRaycaster GraphicRaycaster => GridController.GraphicRaycaster;
        public Fraction Fraction => GridController.Fraction;

        public GameItemData ItemData { get; private set; }

        public bool IsFree => ReferenceEquals(ItemData, null);

        public void SetOwnerGrid(ItemsGridController itemsGridController)
        {
            GridController = itemsGridController;
        }
        
        /// <summary>
        /// Used to place an item from external source.
        /// </summary>
        public void PlaceItem(GameItemData gameItemData)
        {
            if (!ReferenceEquals(ItemData, null))
                throw new ApplicationException("Cannot place multiple items in the same cell!");

            _cellItemInstance = _cellItemPrefab.Reuse<CellItem>();
            _cellItemInstance.SetItem(gameItemData);

            PlaceCellItem(_cellItemInstance);
        }

        /// <summary>
        /// Used to place an item that already associated with a cell item.
        /// </summary>
        public void PlaceCellItem(CellItem cellItem)
        {
            if (!ReferenceEquals(ItemData, null))
                throw new ApplicationException("Cannot place multiple items in the same cell!");

            ItemData = cellItem.GameItemData;
            
            cellItem.SetOwnerCell(this);
        }
        
        public void ClearData()
        {
            ItemData = null;
        }
        
        public void ClearAndReleaseItem()
        {
            ClearData();
            
            _cellItemInstance.gameObject.Release();
            _cellItemInstance = null;
        }
    }
}