using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Utils
{
    [RequireComponent(typeof(Light2D))]
    public class LightAnimation : MonoBehaviour
    {
        [SerializeField] private float maxIntensity;
        [SerializeField] private float animTime;

        private Light2D _light;

        private void Start()
        {
            _light = GetComponent<Light2D>();
            DOTween.To(intensity => _light.intensity = intensity, _light.intensity, maxIntensity, animTime)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo)
                .Play();
        }
    }
}