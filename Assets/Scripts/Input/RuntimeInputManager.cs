using NaughtyAttributes;
using System;
using UnityEngine;

namespace JG.FG.Input
{
    public class RuntimeInputManager : MonoBehaviour
    {
        #region Events
        public event Action<GameObject> OnGameObjectClicked;
        #endregion

        [BoxGroup("Settings")]
        [SerializeField] private LayerMask hitLayers = default;

        [Foldout("Runtime Data")]
        [SerializeField, ReadOnly] private Vector2 PointerPosition = new();
        [Foldout("Runtime Data")]
        [SerializeField, ReadOnly] private Collider2D LastHitCollider = default;

        private PlayerControls playerControls = default;
        private Camera mainCamera = default;

        private void Awake()
        {
            mainCamera = Camera.main;
            playerControls = new PlayerControls();

            playerControls.Enable();
            playerControls.Game.PointerClick.performed += ctx => OnPointerClick();
        }

        private void Update()
        {
            UpdatePointerPosition();
        }

        private void OnDisable()
        {
            playerControls.Disable();
            playerControls.Game.PointerClick.performed -= ctx => OnPointerClick();
        }

        private void OnDestroy()
        {
            playerControls.Disable();
            playerControls.Game.PointerClick.performed -= ctx => OnPointerClick();
        }

        private void OnPointerClick()
        {
            // This is because if we only set it through the Update method, touchscreens will not work.
            UpdatePointerPosition();

            Collider2D collider = RaycastForCollider();
            if (collider == null)
                return;

            OnGameObjectClicked?.Invoke(collider.gameObject);
            LastHitCollider = collider;
        }

        private void UpdatePointerPosition()
        {
            PointerPosition = playerControls.Game.PointerPosition.ReadValue<Vector2>();
        }

        private Collider2D RaycastForCollider()
        {
            // Convert the pointer position to a world position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(PointerPosition.x, PointerPosition.y, Camera.main.nearClipPlane));

            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, hitLayers);

            return hit.collider;
        }
    }
}
