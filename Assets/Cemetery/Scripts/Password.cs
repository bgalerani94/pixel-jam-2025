using DG.Tweening;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Cemetery.Scripts
{
    public class Password : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private string password;
        [SerializeField] private float errorAnimTime = .75f;

        [Header("Components")]
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private GameObject gameHolder;
        [SerializeField] private GameObject errorBorder;
        [SerializeField] private Button closeButton;
        [SerializeField] private Player.Scripts.Player player;
        [SerializeField] private CinemachineVirtualCameraBase virtualCameraBase;

        private bool _isShowing;
        private bool _canInteract;

        private void Start()
        {
            passwordInputField.onValueChanged.AddListener(ResetState);
            closeButton.onClick.AddListener(HidePasswordPuzzle);
        }

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
                    player.CanMove = false;
                    virtualCameraBase.Follow = null;

                    var sequence = DOTween.Sequence();
                    sequence.Append(canvasGroup.DOFade(0, .3f));
                    sequence.Append(virtualCameraBase.transform.DOShakePosition(1.3f, Vector3.one * 0.7f));
                    sequence.AppendInterval(0.2f);
                    sequence.OnComplete(() => SceneManager.LoadScene("Tomb"));
                    sequence.Play();
                }
                else
                {
                    ShowError();
                }
            }

            if (_isShowing && canvasGroup.interactable && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                HidePasswordPuzzle();
            }
        }

        private void ShowError()
        {
            passwordInputField.transform.parent.DOShakePosition(errorAnimTime, Vector3.one * .7f);
            errorBorder.SetActive(true);
            passwordInputField.textComponent.color = Color.red;
        }

        private void ResetState(string _)
        {
            errorBorder.SetActive(false);
            passwordInputField.textComponent.color = Color.black;
        }

        private void ShowPasswordPuzzle()
        {
            ResetState("");
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            passwordInputField.text = string.Empty;
            gameHolder.SetActive(true);

            canvasGroup.DOFade(1, .35f).OnComplete(() =>
            {
                canvasGroup.interactable = true;
                passwordInputField.ActivateInputField();
            });
        }

        private void HidePasswordPuzzle()
        {
            canvasGroup.interactable = false;
            canvasGroup.DOFade(0, .35f).OnComplete(() =>
            {
                _isShowing = false;
                player.CanMove = true;
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