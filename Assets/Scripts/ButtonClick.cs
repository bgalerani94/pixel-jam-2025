using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonClickHandler : MonoBehaviour
{
    [SerializeField] private int buttonIndex;
    private Colours coloursController;
    private Button button;
    private Image buttonImage;

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
        clickSequence.Append(buttonImage.DOColor(coloursController.GetOriginalColor(buttonIndex), 0.2f));

        // Call your existing button press logic
        //Object.FindFirstObjectByType<GButtons>().Press(buttonIndex);
        clickSequence.Play();
    }
}