using System.Collections.Generic;
using DialogueSystem.Scripts;
using RotateMinigame.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tomb
{
    public class TombController : MonoBehaviour
    {
        [SerializeField, TextArea] private string introDialogue;
        [SerializeField, TextArea] private string endingDialogue;
        [SerializeField] private List<Reward> rewards;

        private int _rewardsCount;

        private void Start()
        {
            DialogueBox.Instance.ShowText(introDialogue);
            rewards.ForEach(reward => reward.OnCollected += OnRewardCollected);
        }

        private void OnDestroy()
        {
            rewards.ForEach(reward => reward.OnCollected -= OnRewardCollected);
            DialogueBox.Instance.OnDialogueEnded -= OnFinalDialogEnded;
        }

        private void OnRewardCollected()
        {
            if (++_rewardsCount >= rewards.Count)
            {
                DialogueBox.Instance.OnDialogueEnded += OnFinalDialogEnded;
                DialogueBox.Instance.ShowText(endingDialogue);
            }
        }

        private void OnFinalDialogEnded()
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}