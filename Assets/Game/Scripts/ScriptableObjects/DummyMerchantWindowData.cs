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
        [SerializeField] private float _sellCoefficient = 0.8f;
        [SerializeField] private float _buyCoefficient = 1.5f;
        [SerializeField] private ItemData[] _playerItems;
        [SerializeField] private ItemData[] _merchantItems;
        
        public float GoldAmount => _goldAmount;
    }
}