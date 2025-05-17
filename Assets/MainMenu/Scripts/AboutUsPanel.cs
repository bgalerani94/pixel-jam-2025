using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Scripts
{
    public class AboutUsPanel : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float animTime;

        [Header("Components")]
        [SerializeField] private Button closeButton;
        [SerializeField] private GameObject panelGo;
        [SerializeField] private Image fadeBg;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Start()
        {
            closeButton.onClick.AddListener(Close);
            fadeBg.DOFade(0, 0);
        }

        public void Show()
        {
            fadeBg.DOFade(0, 0);
            canvasGroup.alpha = 0;

            panelGo.SetActive(true);
            fadeBg.DOFade(.45f, animTime);
            canvasGroup.DOFade(1, animTime).SetDelay(animTime / 2f);
        }

        private void Close()
        {
            fadeBg.DOFade(0, animTime);
            canvasGroup.DOFade(0, animTime).OnComplete(() => panelGo.SetActive(false));
        }
    }
}