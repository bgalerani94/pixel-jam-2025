using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace RotateMinigame.Scripts
{
    public class TriggerMinigame : MonoBehaviour
    {
        [SerializeField] private UnityEvent miniGameAction;

        private bool _canInteract;

        private void Update()
        {
            if (_canInteract && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                _canInteract = false;
                miniGameAction?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _canInteract = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _canInteract = false;
            }
        }
    }
}