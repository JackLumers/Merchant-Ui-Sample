using Game.Scripts.ScriptableObjects;
using Game.Scripts.ScriptableObjects.CommonVariables;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class MerchantWindowController : MonoBehaviour
    {
        [SerializeField] private DummyMerchantWindowData _dummyMerchantWindowData;
        [SerializeField] private FloatVariable _goldValueReference;
        
        private void Awake()
        {
            _goldValueReference.Value = _dummyMerchantWindowData.GoldAmount;
        }
    }
}
