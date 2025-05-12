using System.Collections;
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

        [Header("Components")]
        [SerializeField] private GameObject objectHolder;

        [SerializeField] private Image fadeBackground;
        [SerializeField] private TMP_Text dialogueText;

        private Coroutine _coroutine;


        //TEMP Begin
        [SerializeField] private Button testButton;
        [SerializeField, TextArea] private string testText;

        private void Awake()
        {
            testButton.onClick.AddListener(() => ShowText(testText));
        }

        //TEMP End

        public void ShowText(string conversationText)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ShowDialogue(conversationText));
        }

        private IEnumerator ShowDialogue(string conversationText)
        {
            dialogueText.text = string.Empty;
            objectHolder.SetActive(true);

            var textSize = conversationText.Length;
            var repetitions = Mathf.CeilToInt(textSize / (float)maxCharactersAtOnce);

            for (var i = 0; i < repetitions; i++)
            {
                dialogueText.text = string.Empty;
                var startingIndex = i * maxCharactersAtOnce;
                var textToShow = conversationText.Substring(startingIndex,
                    Mathf.Min(maxCharactersAtOnce, conversationText.Length - startingIndex));
                
                Debug.Log(textToShow.Length);
                Debug.Log(textToShow);

                foreach (var character in textToShow)
                {
                    dialogueText.text += character;
                    yield return new WaitForSeconds(textSecondsInterval);
                }

                yield return new WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame);
            }
        }
    }
}