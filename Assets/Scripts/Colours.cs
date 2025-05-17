using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class Colours : MonoBehaviour
{
    // Button references and their individual colors
    [System.Serializable]
    public class ButtonData
    {
        public Button button;
        public Color highlightColor = Color.white;
    }

    public ButtonData[] buttonData = new ButtonData[4]; // Array for 4 buttons
    
    private Color[] originalColors;
    
    // Timing settings
    public float flashDuration = 0.3f;
    public float delayBetweenButtons = 0.5f;

    // Add this to your Colours class
                    public Color GetOriginalColor(int buttonIndex)
                    {
                        if (buttonIndex < 0 || buttonIndex >= originalColors.Length)
                            return Color.white;
                        return originalColors[buttonIndex];
                    }
                    public Color GetHighlightColor(int buttonIndex)
                    {
                        if (buttonIndex < 0 || buttonIndex >= buttonData.Length)
                            return Color.white;
                        return buttonData[buttonIndex].highlightColor;
                    }


    private void Awake()
    {
        // Store original colors
        originalColors = new Color[buttonData.Length];
        for (int i = 0; i < buttonData.Length; i++)
        {
            if (buttonData[i].button != null)
            {
                originalColors[i] = buttonData[i].button.image.color;
            }
        }
    }



    // New method to play the sequence animation
    public IEnumerator PlaySequenceAnimation(List<int> sequence)
    {
        SetPlayerInput(false);

        foreach (int buttonIndex in sequence)
        {

            Button btn = buttonData[buttonIndex-1].button;
            Image btnImage = btn.image;
            Color highlight = buttonData[buttonIndex-1].highlightColor;

            Sequence flash = DOTween.Sequence();
            flash.Append(btnImage.DOColor(highlight, flashDuration / 2));
            flash.Join(btn.transform.DOScale(1.1f, flashDuration / 2));
            flash.Append(btnImage.DOColor(originalColors[buttonIndex-1], flashDuration / 2));
            flash.Join(btn.transform.DOScale(1f, flashDuration / 2));

            yield return flash.WaitForCompletion();
            yield return new WaitForSeconds(delayBetweenButtons);
        }

        SetPlayerInput(true);
    }

    private void SetPlayerInput(bool active)
    {
        foreach (ButtonData data in buttonData)
        {
            if (data.button != null)
            {
                data.button.interactable = active;
            }
        }
    }






    // Flash a single button by index (0-3)
    public void FlashButton(int buttonIndex)
    {
        Image buttonImage = buttonData[buttonIndex].button.image;
        Color highlightColor = buttonData[buttonIndex].highlightColor;

        Sequence flashSequence = DOTween.Sequence();
        flashSequence.Append(buttonImage.DOColor(highlightColor, flashDuration / 2));
        flashSequence.Append(buttonImage.DOColor(originalColors[buttonIndex], flashDuration / 2));
        flashSequence.Play();
    }

    // Play a sequence of button flashes
    public void PlaySequence(List<int> sequence)
    {
        StartCoroutine(PlaySequenceCoroutine(sequence));
    }

    private IEnumerator PlaySequenceCoroutine(List<int> sequence)
    {
        foreach (int buttonIndex in sequence)
        {
            FlashButton(buttonIndex);
            yield return new WaitForSeconds(flashDuration + delayBetweenButtons);
        }
    }

    // Play all buttons in order (for testing)
    public void PlayAllButtons()
    {
        for (int i = 0; i < buttonData.Length; i++)
        {
            int index = i;
            DOVirtual.DelayedCall(i * delayBetweenButtons, () => FlashButton(index));
        }
    }
}