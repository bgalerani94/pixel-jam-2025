using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Colours : MonoBehaviour
{
    // Reference to the button's Image component
    private Image buttonImage;
    
    // The original color of the button
    private Color originalColor;
    
    // The color to change to during animation
    public Color highlightColor = Color.white;
    
    // Duration of the color change animation
    public float colorChangeDuration = 0.3f;

    private void Awake()
    {
        // Get the Image component attached to this button
        buttonImage = GetComponent<Image>();
        
        // Store the original color
        originalColor = buttonImage.color;
    }

    // Method to animate the color change
    public void AnimateColor()
    {
        // Sequence allows us to chain animations
        Sequence colorSequence = DOTween.Sequence();
        
        // Change to highlight color
        colorSequence.Append(buttonImage.DOColor(highlightColor, colorChangeDuration));
        
        // Change back to original color
        colorSequence.Append(buttonImage.DOColor(originalColor, colorChangeDuration));
    }

    // Optional: Add a method to animate with a specific color
    public void AnimateWithColor(Color targetColor, float duration)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(buttonImage.DOColor(targetColor, duration));
        sequence.Append(buttonImage.DOColor(originalColor, duration));
    }
}