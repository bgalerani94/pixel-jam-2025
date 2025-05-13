using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DialogueSystem.Scripts
{
    public class DialogueBox : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private int maxCharactersAtOnce;
        [SerializeField] private float textSecondsInterval;
        [SerializeField] private float fadeAnimationTime;

        [Header("Components")]
        [SerializeField] private GameObject objectHolder;
        [SerializeField] private Image fadeBackground;
        [SerializeField] private CanvasGroup frameCanvasGroup;
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private BouncingUI bouncingArrow;

        private float _initialFadeValue;
        private Coroutine _coroutine;


        //TEMP Begin
        [SerializeField] private Button testButton;
        [SerializeField, TextArea] private string testText;

        private void Awake()
        {
            testButton.onClick.AddListener(() => ShowText(testText));
            _initialFadeValue = fadeBackground.color.a;
        }
        //TEMP End

        public void ShowText(string conversationText)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ShowDialogue(conversationText));
        }

        private IEnumerator ShowDialogue(string conversationText)
        {
            yield return ShowUI();

            var textSize = conversationText.Length;
            var repetitions = Mathf.CeilToInt(textSize / (float)maxCharactersAtOnce);

            for (var i = 0; i < repetitions; i++)
            {
                dialogueText.text = string.Empty;
                var startingIndex = i * maxCharactersAtOnce;
                var textToShow = conversationText.Substring(startingIndex,
                    Mathf.Min(maxCharactersAtOnce, conversationText.Length - startingIndex));

                foreach (var character in textToShow)
                {
                    dialogueText.text += character;
                    yield return new WaitForSeconds(textSecondsInterval);
                }

                bouncingArrow.Show();
                yield return new WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame);
                bouncingArrow.Hide();
            }

            yield return HideUI();
        }

        private IEnumerator ShowUI()
        {
            dialogueText.text = string.Empty;
            fadeBackground.DOFade(0, 0);
            frameCanvasGroup.alpha = 0;
            objectHolder.SetActive(true);

            fadeBackground.DOFade(_initialFadeValue, fadeAnimationTime);
            yield return new WaitForSeconds(0.15f);
            yield return frameCanvasGroup.DOFade(1, fadeAnimationTime).WaitForCompletion();
        }

        private IEnumerator HideUI()
        {
            frameCanvasGroup.DOFade(0, fadeAnimationTime * 1.5f);
            yield return new WaitForSeconds(0.1f);
            yield return fadeBackground.DOFade(0, fadeAnimationTime * 1.5f).WaitForCompletion();
            objectHolder.SetActive(false);
        }
    }
}