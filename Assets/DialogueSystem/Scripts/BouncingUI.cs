using DG.Tweening;
using UnityEngine;

namespace DialogueSystem.Scripts
{
    [RequireComponent(typeof(RectTransform))]
    public class BouncingUI : MonoBehaviour
    {
        [SerializeField] private float yMovementAmount;
        [SerializeField] private float animTime;

        private float _initialY;
        
        private RectTransform _rectTransform;
        private RectTransform RectTransform => _rectTransform ??= GetComponent<RectTransform>();

        public void Show()
        {
            _initialY = RectTransform.anchoredPosition.y;
            DOTween.Kill(RectTransform);
            gameObject.SetActive(true);
            RectTransform.DOScale(Vector2.one, animTime);
            RectTransform.DOAnchorPosY(yMovementAmount, animTime)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutCubic);
        }

        public void Hide()
        {
            DOTween.Kill(RectTransform);
            RectTransform.DOScale(Vector2.zero, animTime).OnComplete(() =>
            {
                gameObject.SetActive(false);
                RectTransform.DOAnchorPosY(_initialY, 0);
            });
        }
    }
}