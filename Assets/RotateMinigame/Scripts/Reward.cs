using DG.Tweening;
using UnityEngine;

namespace RotateMinigame.Scripts
{
    public class Reward : MonoBehaviour
    {
        [SerializeField] private float yLoopMovementAmount;
        [SerializeField] private float scaleAnimTime;
        [SerializeField] private float loopAnimTime;

        private void OnEnable()
        {
            transform.DOScale(Vector3.one, scaleAnimTime);
            transform.DOLocalMoveY(transform.localPosition.y + yLoopMovementAmount, loopAnimTime)
                .SetEase(Ease.InOutCubic)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}