using System.Collections;
using DG.Tweening;
using DialogueSystem.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Cemetery.Scripts
{
    public class CemeteryScene : MonoBehaviour
    {
        [SerializeField] private Player.Scripts.Player player;
        [SerializeField] private Image darkImage;
        [SerializeField] private float timeToFade;
        [SerializeField] private float timeToShowDialog;
        [SerializeField, TextArea] private string introText;

        private bool _canProceed;

        private IEnumerator Start()
        {
            DialogueBox.Instance.OnDialogueStarted += OnDialogueStarted;
            DialogueBox.Instance.OnDialogueEnded += OnDialogueEnded;
            darkImage.color = Color.black;
            darkImage.gameObject.SetActive(true);
            player.CanMove = false;
            yield return PlayIntroAnim();
        }

        private void OnDestroy()
        {
            DialogueBox.Instance.OnDialogueStarted -= OnDialogueStarted;
            DialogueBox.Instance.OnDialogueEnded -= OnDialogueEnded;
        }

        private void OnDialogueStarted()
        {
            _canProceed = false;
        }

        private void OnDialogueEnded()
        {
            _canProceed = true;
        }

        private IEnumerator PlayIntroAnim()
        {
            yield return FadeAndShowDialogue("Hey! You! Wake up!", false, .6f);
            yield return darkImage.DOFade(1f, timeToFade).WaitForCompletion();
            yield return FadeAndShowDialogue("Yes, you! Wake up! We have a lot to do!", true, .6f);
            yield return darkImage.DOFade(1f, timeToFade).WaitForCompletion();
            yield return new WaitForSeconds(timeToShowDialog);
            yield return FadeAndShowDialogue(introText, true, 0);
            player.CanMove = true;
        }

        private IEnumerator FadeAndShowDialogue(string dialogue, bool showDarkImage, float fade)
        {
            if (showDarkImage)
            {
                yield return darkImage.DOFade(1f, timeToFade).WaitForCompletion();
                yield return new WaitForSeconds(timeToFade / 2f);
            }

            yield return darkImage.DOFade(fade, timeToFade).WaitForCompletion();
            DialogueBox.Instance.ShowText(dialogue, false);
            yield return new WaitUntil(() => _canProceed);
            player.CanMove = false;
        }
    }
}