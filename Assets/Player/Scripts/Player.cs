using System;
using DialogueSystem.Scripts;
using RotateMinigame.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Player : MonoBehaviour
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

        [NonSerialized] public bool CanMove = true;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _movementHorizontalAction = playerInputMap.FindAction("MoveHorizontal");
            _movementHorizontalAction.started += OnMoveHorizontalTriggered;
            _movementHorizontalAction.canceled += OnMoveHorizontalEnded;

            _movementVerticalAction = playerInputMap.FindAction("MoveVertical");
            _movementVerticalAction.started += OnMoveVerticalTriggered;
            _movementVerticalAction.canceled += OnMoveVerticalEnded;

            DialogueBox.Instance.OnDialogueStarted += OnDialogueStarted;
            DialogueBox.Instance.OnDialogueEnded += OnDialogueEnded;
        }

        private void OnDestroy()
        {
            if (DialogueBox.Instance != null)
            {
                DialogueBox.Instance.OnDialogueStarted -= OnDialogueStarted;
                DialogueBox.Instance.OnDialogueEnded -= OnDialogueEnded;
            }
        }

        private void OnMoveHorizontalTriggered(InputAction.CallbackContext context)
        {
            if (CanMove)
            {
                _velocity.x = context.ReadValue<Vector2>().x;
                if (_velocity.x != 0) spriteRenderer.flipX = _velocity.x < 0;
            }
        }

        private void OnMoveVerticalTriggered(InputAction.CallbackContext context)
        {
            if (CanMove)
            {
                _velocity.y = context.ReadValue<Vector2>().y;
            }
        }

        private void OnMoveHorizontalEnded(InputAction.CallbackContext context)
        {
            _velocity.x = 0;
        }

        private void OnMoveVerticalEnded(InputAction.CallbackContext context)
        {
            _velocity.y = 0;
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = _velocity * movementSpeed * Time.fixedDeltaTime;
        }

        private void OnDialogueStarted()
        {
            CanMove = false;
            _velocity = Vector2.zero;
            _rb.linearVelocity = _velocity;
        }

        private void OnDialogueEnded()
        {
            CanMove = true;
        }
    }
}