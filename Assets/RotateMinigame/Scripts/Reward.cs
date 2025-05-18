using DG.Tweening;
using UnityEngine;

namespace RotateMinigame.Scripts
{
    public class Reward : MonoBehaviour
    {
        [SerializeField] private float yFirstMovementAmount;
        [SerializeField] private float yLoopMovementAmount;
        [SerializeField] private float firstMovementAnimTime;
        [SerializeField] private float loopAnimTime;

        private void OnEnable()
        {
            transform.DOLocalMoveY(yFirstMovementAmount, firstMovementAnimTime)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() =>
                    transform.DOLocalMoveY(transform.localPosition.y + yLoopMovementAmount, loopAnimTime)
                        .SetEase(Ease.InOutCubic)
                        .SetLoops(-1, LoopType.Yoyo));
        }
    }
}