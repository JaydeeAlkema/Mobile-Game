using MobileGame.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MobileGame.Scripts.Managers
{
	public class TouchInputHandler : MonoBehaviour
	{
		private PlayerControls inputActions;
		private Camera mainCamera;

		private void Awake()
		{
			mainCamera = Camera.main;

			inputActions = new PlayerControls();
			inputActions.Enable();

			inputActions.Game.TouchPosition.performed += OnTouchPressed;
		}

		private void OnDestroy()
		{
			inputActions.Disable();
			inputActions.Game.TouchPosition.performed -= OnTouchPressed;
		}

		private void OnDisable()
		{
			inputActions.Disable();
			inputActions.Game.TouchPosition.performed -= OnTouchPressed;
		}

		private void OnTouchPressed(InputAction.CallbackContext context)
		{
			Vector2 touchPosition = context.ReadValue<Vector2>();
			Ray ray = mainCamera.ScreenPointToRay(touchPosition);

			RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

			Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 1f);
			Debug.Log($"Touch Position: {touchPosition}");

			if (hit.collider == null)
				return;

			Debug.Log($"Hit: {hit.collider.name}");

			if (!hit.collider.TryGetComponent(out IInteractable interactable))
				return;

			Debug.Log($"Interactable: {interactable}");
			interactable.Interact();

			// Return after handling the first hit
			return;
		}
	}
}