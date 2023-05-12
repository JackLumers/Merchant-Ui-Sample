using System;
using System.Collections.Generic;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.ScriptableObjects.InventoryData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.ItemsGrid
{
    public class ItemsGridController : MonoBehaviour
    {
        [SerializeField] private Fraction _fraction;
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private ItemsGridCell[] _itemsGridSlots;
        
        /// <summary>
        /// Invoked when user is trying to place a <see cref="CellItem"/> in <see cref="ItemsGridCell"/>
        /// by dragging. If false returned, <see cref="CellItem"/> will move back to it's own cell.
        /// </summary>
        public Func<TryPlaceItemParam, bool> TryPlaceItem;

        public GraphicRaycaster GraphicRaycaster { get; private set; }
        public Fraction Fraction => _fraction;

        public void InitGraphicRaycaster(GraphicRaycaster graphicRaycaster)
        {
            GraphicRaycaster = graphicRaycaster;
        }

        private void Awake()
        {
            _headerText.text = _fraction.FractionData.Name + " Items";
            
            foreach (var itemsGridSlot in _itemsGridSlots)
            {
                itemsGridSlot.SetOwnerGrid(this);
            }
        }

        public void SetItems(IReadOnlyList<GameItemData> items)
        {
            if (items.Count > _itemsGridSlots.Length)
            {
                Debug.LogError("There is no pagination or scrolling implemented, " +
                               "so items count can't be more than cells count.", this);
                
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                _itemsGridSlots[i].PlaceItem(items[i]);
            }
        }

        private void OnDestroy()
        {
            TryPlaceItem = null;
        }

        public struct TryPlaceItemParam
        {
            public CellItem CellItem;
            public ItemsGridCell CellToMoveItem;

            public TryPlaceItemParam(CellItem cellItem, ItemsGridCell cellToMoveItem)
            {
                CellItem = cellItem;
                CellToMoveItem = cellToMoveItem;
            }
        }
    }
}