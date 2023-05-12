using System;
using Game.Scripts.Globals;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects.InventoryData
{
    /// <summary>
    /// Represents an in-game item
    /// </summary>
    [CreateAssetMenu(
        fileName = "New " + nameof(GameItemData),
        menuName = ProjectConstants.ScriptableObjectsAssetMenuName +
                   "/Create new " + nameof(GameItemData))]
    public class GameItemData : ScriptableObject
    {
        public ItemData ItemData;
    }
    
    [Serializable]
    public struct ItemData
    {
        // Can be a key for addressable asset so sprite can be loaded asynchronously by Addressable Asset System,
        // but for the sake of simplicity will do.
        public Sprite Sprite;
        
        public float GoldValue;
    }
}