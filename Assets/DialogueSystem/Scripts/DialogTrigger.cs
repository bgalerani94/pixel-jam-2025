using UnityEngine;
using UnityEngine.InputSystem;

namespace DialogueSystem.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class DialogTrigger : MonoBehaviour
    {
        [SerializeField] [TextArea] private string dialogueText;

        private bool _isInRange;

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame && _isInRange && DialogueBox.Instance.CanShowDialogueBox)
            {
                DialogueBox.Instance.ShowText(dialogueText);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isInRange = false;
            }
        }
    }
}