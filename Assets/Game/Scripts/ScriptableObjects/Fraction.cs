using System;
using Game.Scripts.Globals;
using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    /// <remarks>
    /// I don't like enums and I like Scriptable Objects so...
    /// 
    /// Found this approach from Unity Austin 2017
    /// https://youtu.be/raQ3iHhE_Kk?t=2770
    /// </remarks>
    [CreateAssetMenu(
        fileName = "New " + nameof(Fraction),
        menuName = ProjectConstants.ScriptableObjectsAssetMenuName +
                   "/Create new " + nameof(Fraction))]
    public class Fraction : ScriptableObject
    {
        public FractionData FractionData;
    }

    [Serializable]
    public struct FractionData
    {
        // Could be id so can be taken from some Localization module.
        public string Name;
    }
}