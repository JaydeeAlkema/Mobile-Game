using System;
using System.Collections.Generic;
using UnityEngine;

namespace JG.FG.ScriptableObjects
{
    public class RuntimeReferenceListSO<T> : MonoBehaviour
    {
        public event Action OnListChanged;

        [field: SerializeField] public List<T> Values { get; set; } = new();

#if UNITY_EDITOR
        private List<T> initialValues;

        // Called when the ScriptableObject is created or loaded
        private void OnEnable()
        {
            // Cache the initial value
            initialValues = Values;

            // Subscribe to play mode state changes
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        // Called when the ScriptableObject is destroyed or unloaded
        private void OnDisable()
        {
            // Unsubscribe from play mode state changes
            UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnValidate()
        {
            OnListChanged?.Invoke();
        }

        private void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange state)
        {
            // Reset the value to the cached initial value when exiting play mode
            if (state == UnityEditor.PlayModeStateChange.EnteredEditMode)
                Values = initialValues;
        }
#endif
        public void Set(List<T> value)
        {
            Values = value;
        }

        public virtual void Add(T value)
        {
            OnListChanged?.Invoke();
        }

        public virtual void Subtract(T value)
        {
            OnListChanged?.Invoke();
        }
    }
}
