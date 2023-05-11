using System.Globalization;
using Game.Scripts.ScriptableObjects.CommonVariables;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class GoldAmountElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _goldValueText;
        [SerializeField] private FloatVariable _goldVariable;

        private void OnEnable()
        {
            SetGoldVisualValue(_goldVariable.Value);
            
            _goldVariable.Changed.AddListener(SetGoldVisualValue);
        }

        private void SetGoldVisualValue(float value)
        {
            _goldValueText.text = value.ToString(CultureInfo.InvariantCulture);
        }

        private void OnDisable()
        {
            _goldVariable.Changed.RemoveListener(SetGoldVisualValue);
        }
    }
}