using Game.Scripts.ScriptableObjects;
using Game.Scripts.ScriptableObjects.CommonVariables;
using Game.Scripts.UI.ItemsGrid;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public class MerchantWindowController : MonoBehaviour
    {
        [SerializeField] private DummyMerchantWindowData _dummyMerchantWindowData;
        [SerializeField] private FloatVariable _goldValueReference;

        [SerializeField] private ItemsGridController _playerItemsGrid;
        [SerializeField] private ItemsGridController _merchantItemsGrid;

        private GraphicRaycaster _graphicRaycaster;

        private void Awake()
        {
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
            
            _playerItemsGrid.InitGraphicRaycaster(_graphicRaycaster);
            _merchantItemsGrid.InitGraphicRaycaster(_graphicRaycaster);
            
            _playerItemsGrid.TryPlaceItem += TryPlaceItem;
            _merchantItemsGrid.TryPlaceItem += TryPlaceItem;
        }

        private void Start()
        {
            _goldValueReference.Value = _dummyMerchantWindowData.GoldAmount;

            _playerItemsGrid.SetItems(_dummyMerchantWindowData.PlayerItems);
            _merchantItemsGrid.SetItems(_dummyMerchantWindowData.MerchantItems);
        }
        
        /// <inheritdoc cref="ItemsGridController.TryPlaceItem"/>
        private bool TryPlaceItem(ItemsGridController.TryPlaceItemParam arg)
        {
            var item = arg.CellItem;
            var cellToMoveItem = arg.CellToMoveItem;

            if (!cellToMoveItem.IsFree)
            {
                Debug.LogWarning("User tried to place an item in cell that already has item. Ignored.",
                    this);

                return false;
            }

            // Same fraction. Just move to cell
            if (item.OwnerGridCell.Fraction == cellToMoveItem.Fraction)
            {
                item.OwnerGridCell.ClearData();
                cellToMoveItem.PlaceCellItem(item);

                return true;
            }

            // Not the same fraction and new cell is not player fraction
            // -- SELL
            if (item.OwnerGridCell.Fraction != cellToMoveItem.Fraction
                && cellToMoveItem.Fraction != _dummyMerchantWindowData.PlayerFraction)
            {
                _goldValueReference.Value += item.GameItemData.ItemData.GoldValue
                                             * _dummyMerchantWindowData.SellCoefficient;

                item.OwnerGridCell.ClearData();
                cellToMoveItem.PlaceCellItem(item);

                return true;
            }

            // Not the same fraction and new cell is player fraction
            // -- BUY
            if (item.OwnerGridCell.Fraction != cellToMoveItem.Fraction
                && cellToMoveItem.Fraction == _dummyMerchantWindowData.PlayerFraction)
            {
                var expenses = item.GameItemData.ItemData.GoldValue
                               * _dummyMerchantWindowData.BuyCoefficient;
                
                // Player have enough gold
                if (_goldValueReference.Value - expenses >= 0)
                {
                    _goldValueReference.Value -= expenses;

                    item.OwnerGridCell.ClearData();
                    cellToMoveItem.PlaceCellItem(item);

                    return true;
                }

                Debug.LogWarning("User tried to buy an item without enough gold. Ignored.",
                    this);

                return false;
            }

            Debug.LogWarning("Not processed case of moving item. Ignored.",
                this);
            
            return false;
        }
    }
}
