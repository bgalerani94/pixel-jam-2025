using UnityEngine;

namespace DialogueSystem.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractionPoint : MonoBehaviour
    {
        [SerializeField] private GameObject indicator;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                indicator.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                indicator.SetActive(false);
            }
        }
    }
}