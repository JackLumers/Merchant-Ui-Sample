using Game.Scripts.Globals;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.ScriptableObjects.CommonVariables
{
    /// <remarks>
    /// Found this approach from Unity Austin 2017
    /// https://youtu.be/raQ3iHhE_Kk?t=1109
    ///
    /// Event/Action can be in Event Architecture from the same approach, but for the sake of simplicity will do.
    /// </remarks>
    [CreateAssetMenu(
        fileName = "New " + nameof(FloatVariable),
        menuName = ProjectConstants.ScriptableObjectsAssetMenuName +
                   "/" + ProjectConstants.VariablesAssetMenuName +
                   "/Create new " + nameof(FloatVariable))]
    public class FloatVariable : ScriptableObject
    {
        [Header("Changed in runtime. Shown here only for debugging.")]
        [SerializeField] private float _value;

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                Changed?.Invoke(value);
            }
        }

        /// <summary>
        /// Called when <see cref="Value"/> got changed.
        /// </summary>
        /// <remarks>
        /// This is UnityEvent but not an Action, because we can be sure that UnityEvent will be called only in runtime
        /// if we want, and we cant make Action null here in OnDisable, since there is no OnDisable.
        /// UnityEvent is more safe and convenient here.
        /// </remarks>
        public UnityEvent<float> Changed;
    }
}