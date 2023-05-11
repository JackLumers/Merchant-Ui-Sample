using System;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects.InventoryData
{
    [Serializable]
    public struct ItemData
    {
        // Can be a key for addressable asset so sprite can be loaded asynchronously by Addressable Asset System,
        // but for the sake of simplicity will do.
        public Sprite Sprite;
        
        public float GoldValue;
    }
}