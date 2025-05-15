using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float movementSpeed = 5f;

        [Header("Components")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private InputActionAsset playerInputMap;

        private Rigidbody2D _rb;
        private InputAction _movementHorizontalAction;
        private InputAction _movementVerticalAction;
        private Vector2 _velocity;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _movementHorizontalAction = playerInputMap.FindAction("MoveHorizontal");
            _movementHorizontalAction.started += OnMoveHorizontalTriggered;
            _movementHorizontalAction.canceled += OnMoveHorizontalEnded;

            _movementVerticalAction = playerInputMap.FindAction("MoveVertical");
            _movementVerticalAction.started += OnMoveVerticalTriggered;
            _movementVerticalAction.canceled += OnMoveVerticalEnded;
        }

        private void OnMoveHorizontalTriggered(InputAction.CallbackContext context)
        {
            _velocity.x = context.ReadValue<Vector2>().x;
            if (_velocity.x != 0) spriteRenderer.flipX = _velocity.x < 0;
        }

        private void OnMoveVerticalTriggered(InputAction.CallbackContext context)
        {
            _velocity.y = context.ReadValue<Vector2>().y;
        }

        private void OnMoveHorizontalEnded(InputAction.CallbackContext context)
        {
            _velocity.x = 0;
        }

        private void OnMoveVerticalEnded(InputAction.CallbackContext context)
        {
            _velocity.y = 0;
        }

        private void LateUpdate()
        {
            _rb.linearVelocity = _velocity * movementSpeed;
        }
    }
}