using Game.Scripts.Globals;
using Game.Scripts.ScriptableObjects.InventoryData;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "New " + nameof(DummyMerchantWindowData),
        menuName = ProjectConstants.ScriptableObjectsAssetMenuName +
                   "/" + ProjectConstants.DummyDataAssetMenuName +
                   "/Create new " + nameof(DummyMerchantWindowData))]
    public class DummyMerchantWindowData : ScriptableObject
    {
        [SerializeField] private float _goldAmount;
        [SerializeField] private float _sellCoefficient = 1f;
        [SerializeField] private float _buyCoefficient = 1f;
        [SerializeField] private Fraction _playerFraction;
        [SerializeField] private GameItemData[] _playerItems;
        [SerializeField] private GameItemData[] _merchantItems;

        public float GoldAmount => _goldAmount;
        public float SellCoefficient => _sellCoefficient;
        public float BuyCoefficient => _buyCoefficient;
        public GameItemData[] PlayerItems => _playerItems;
        public GameItemData[] MerchantItems => _merchantItems;
        public Fraction PlayerFraction => _playerFraction;
    }
}