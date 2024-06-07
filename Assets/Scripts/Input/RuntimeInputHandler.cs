using JG.FG.Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace JG.FG.Input
{
    public class RuntimeInputHandler : MonoBehaviour
    {
        [BoxGroup("References")]
        [SerializeField] private RuntimeInputManager runtimeInputManager;

        private void OnEnable()
        {
            runtimeInputManager.OnGameObjectClicked += OnGameObjectClicked;
        }

        private void OnDisable()
        {
            runtimeInputManager.OnGameObjectClicked -= OnGameObjectClicked;
        }

        private void OnGameObjectClicked(GameObject gameObject)
        {
            gameObject.TryGetComponent(out IInteractable interactable);

            interactable?.Interact();
        }
    }
}
