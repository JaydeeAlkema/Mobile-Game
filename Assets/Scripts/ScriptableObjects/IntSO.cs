using System;
using UnityEngine;

namespace JG.FG.ScriptableObjects
{
    [CreateAssetMenu(fileName = "IntSO", menuName = "ScriptableObjects/IntSO")]
    public class IntSO : ScriptableObject
    {
        public event Action OnValueChanged;

        [field: SerializeField] public int Value { get; private set; }

#if UNITY_EDITOR
        private int initialValue;

        // Called when the ScriptableObject is created or loaded
        private void OnEnable()
        {
            // Cache the initial value
            initialValue = Value;

            // Subscribe to play mode state changes
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        // Called when the ScriptableObject is destroyed or unloaded
        private void OnDisable()
        {
            // Unsubscribe from play mode state changes
            UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange state)
        {
            // Reset the value to the cached initial value when exiting play mode
            if (state == UnityEditor.PlayModeStateChange.EnteredEditMode)
                Value = initialValue;
        }
#endif

        public void Add(int value)
        {
            Value += value;

            OnValueChanged?.Invoke();
        }

        public void Subtract(int value)
        {
            Value -= value;

            OnValueChanged?.Invoke();
        }
    }
}
