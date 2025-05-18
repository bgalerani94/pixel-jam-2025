using System.Collections.Generic;
using DG.Tweening;
using DialogueSystem.Scripts;
using RotateMinigame.Scripts;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tomb
{
    public class TombController : MonoBehaviour
    {
        [SerializeField, TextArea] private string introDialogue;
        [SerializeField, TextArea] private string endingDialogue;
        [SerializeField] private List<Reward> rewards;
        [SerializeField] private Player.Scripts.Player player;
        [SerializeField] private CinemachineVirtualCameraBase virtualCameraBase;

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
                player.CanMove = false;
                virtualCameraBase.Follow = null;
                virtualCameraBase.transform.DOShakePosition(1.3f, Vector3.one * 0.7f)
                    .OnComplete(() =>
                    {
                        DialogueBox.Instance.OnDialogueEnded += OnFinalDialogEnded;
                        DialogueBox.Instance.ShowText(endingDialogue);
                    });
            }
        }

        private void OnFinalDialogEnded()
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}