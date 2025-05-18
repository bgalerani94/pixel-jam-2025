using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonClickHandler : MonoBehaviour
{
    [SerializeField] private int buttonIndex;
    private Colours coloursController;
    private Button button;
    private Image buttonImage;
    public float flashDuration = 0.3f;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        coloursController = Object.FindFirstObjectByType<Colours>();

        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {

        // Get the button's unique highlight color from Colours
        Color highlightColor = coloursController.GetHighlightColor(buttonIndex);

        // Flash on click
        Sequence clickSequence = DOTween.Sequence();
        clickSequence.Append(buttonImage.DOColor(highlightColor, 0.1f));
        clickSequence.Join(buttonImage.transform.DOScale(1.1f, flashDuration * 0.8f));
        clickSequence.Append(buttonImage.DOColor(coloursController.GetOriginalColor(buttonIndex), 0.2f));
        clickSequence.Join(buttonImage.transform.DOScale(1f, flashDuration * 0.8f));

        // Call your existing button press logic
        //Object.FindFirstObjectByType<GButtons>().Press(buttonIndex);
        clickSequence.Play();
        coloursController.PlayAudio(buttonIndex);
    }
}