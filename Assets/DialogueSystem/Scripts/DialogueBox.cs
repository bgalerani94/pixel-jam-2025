using System;
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
        #region Singleton

        private static DialogueBox _instance;

        public static DialogueBox Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<DialogueBox>();
                    if (_instance == null)
                    {
                        _instance = Instantiate(Resources.Load<DialogueBox>("DialogueSystem"));
                    }

                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }

        #endregion

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

        public bool CanShowDialogueBox => !objectHolder.activeSelf;

        public event Action OnDialogueStarted;
        public event Action OnDialogueEnded;

        private float _initialFadeValue;

        private void Awake()
        {
            _initialFadeValue = fadeBackground.color.a;
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }

            fadeBackground.DOFade(0, 0);
        }

        public void ShowText(string conversationText)
        {
            if (objectHolder.activeSelf)
            {
                Debug.LogWarning("Trying to open DialogueBox that is already opened.");
            }
            else
            {
                OnDialogueStarted?.Invoke();
                StartCoroutine(ShowDialogue(conversationText));
            }
        }

        private IEnumerator ShowDialogue(string conversationText)
        {
            yield return ShowUI();

            var textSize = conversationText.Length;
            var repetitions = Mathf.CeilToInt(textSize / (float)maxCharactersAtOnce);
            var lastIndex = -1;

            for (var i = 0; i < repetitions; i++)
            {
                dialogueText.text = string.Empty;
                var startingIndex = lastIndex + 1;
                lastIndex = Mathf.Min(maxCharactersAtOnce, conversationText.Length - startingIndex);
                var textToShow = conversationText.Substring(startingIndex, lastIndex);
                var lastSpaceIndex = textToShow.LastIndexOf(" ", StringComparison.Ordinal);

                if (lastIndex + startingIndex < textSize - 1)
                {
                    lastIndex = lastSpaceIndex > -1 ? lastSpaceIndex : lastIndex;
                }

                for (var j = 0; j < lastIndex; j++)
                {
                    var character = textToShow[j].ToString();
                    if (character == "<")
                    {
                        if (textToShow[j + 1] == '/')
                        {
                            character = "</b>";
                            j += 3;
                        }
                        else
                        {
                            character = "<b>";
                            j += 2;
                        }
                    }

                    dialogueText.text += character;
                    yield return new WaitForSeconds(textSecondsInterval);
                }

                lastIndex += startingIndex;

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
            fadeBackground.DOFade(0, fadeAnimationTime * 1.5f);
            OnDialogueEnded?.Invoke();
            objectHolder.SetActive(false);
        }
    }
}