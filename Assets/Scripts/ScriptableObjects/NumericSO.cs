using System;
using UnityEngine;

namespace JG.FG.ScriptableObjects
{
    public class NumericSO<T> : ScriptableObject
    {
        public event Action OnValueChanged;

        [field: SerializeField] public T Value { get; set; }

#if UNITY_EDITOR
        private T initialValue;

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
        public void Set(T value)
        {
            Value = value;
        }

        public virtual void Add(T value)
        {
            OnValueChanged?.Invoke();
        }

        public virtual void Subtract(T value)
        {
            OnValueChanged?.Invoke();
        }
    }
}
