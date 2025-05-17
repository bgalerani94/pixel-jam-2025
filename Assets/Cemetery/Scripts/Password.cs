using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Cemetery.Scripts
{
    public class Password : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private string password;

        [Header("Components")]
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private GameObject gameHolder;
        [SerializeField] private Player.Scripts.Player player;

        private bool _isShowing;
        private bool _canInteract;

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame && _canInteract && !_isShowing)
            {
                player.CanMove = false;
                _isShowing = true;
                ShowPasswordPuzzle();
            }

            if (_isShowing && canvasGroup.interactable && Keyboard.current.enterKey.wasPressedThisFrame)
            {
                if (IsPasswordCorrect())
                {
                    SceneManager.LoadScene("Tomb");
                }
                else
                {
                    HidePasswordPuzzle();
                }
            }
            
            //TODO TEMP
            if (_isShowing && canvasGroup.interactable && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                HidePasswordPuzzle();
            }
        }

        private void ShowPasswordPuzzle()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            passwordInputField.text = string.Empty;
            gameHolder.SetActive(true);

            canvasGroup.DOFade(1, .35f).OnComplete(() => canvasGroup.interactable = true);
        }

        private void HidePasswordPuzzle()
        {
            canvasGroup.interactable = false;
            canvasGroup.DOFade(0, .35f).OnComplete(() =>
            {
                _isShowing = false;
                gameHolder.SetActive(false);
            });
        }

        private bool IsPasswordCorrect()
        {
            return passwordInputField.text.ToLower().Equals(password.ToLower());
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log($"CanInterace");
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