using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text thankYouText;
    [SerializeField] private float animTime;

    private void Start()
    {
        titleText.DOFade(0, 0);
        thankYouText.DOFade(0, 0);

        titleText.DOFade(1, animTime);
        thankYouText.DOFade(1, animTime).SetDelay(animTime / 2f);
    }
}