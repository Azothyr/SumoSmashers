using System.Collections;
using UnityEngine;

namespace ZPTools.UI
{
    public class TMPAnimator : TMPBehaviorBase
    {
        [SerializeField] private bool allowDebug;
        public bool pulseOnStart;
    
        private bool _isPulsing;
        
        private void OnEnable()
        {
            if (pulseOnStart)
            {
                PlayPulseAnimation(1f);
            }
        }
        
        public void PlayPulseAnimation(float duration)
        {
            if (_isPulsing) return;
            _isPulsing = true;

            StartCoroutine(PulseAnimation(duration));
        }

        public void StopPulseAnimation()
        {
            _isPulsing = false;
        }
        
        private Coroutine fadeInCoroutine;
        private Coroutine fadeOutCoroutine;
        private IEnumerator PulseAnimation(float duration)
        {
            var alphaStartValue = !Mathf.Approximately(_textObj.color.a, 1f) ? _textObj.color.a : 1f;
            var alphaEndValue = 0f;
            var durationHalf = duration / 2f;

            while (_isPulsing)
            {
                // Fade in
                fadeInCoroutine = StartCoroutine(
                    HandleInterpolation(alphaStartValue, alphaEndValue, durationHalf, 0));
                yield return new WaitUntil(() => fadeInCoroutine == null);

                // Fade out
                fadeOutCoroutine = StartCoroutine(
                    HandleInterpolation(alphaEndValue, alphaStartValue, durationHalf, 1));
                yield return new WaitUntil(() => fadeOutCoroutine == null);
            }

            _textObj.CrossFadeAlpha(1f, 0f, false);
        }

        private IEnumerator HandleInterpolation(float initialValue, float finalValue, float transitionDuration,
            int transitionType)
        {
            var startTime = Time.time;
            var elapsedTime = 0f;
            
            // Determine exponential factor based on transition type (In: positive -> growth, Out: negative -> decay)
            const float TimeScalar = 2f;
            const float ExponentialBaseFactor = 3f;
            var exponentialFactor = transitionType == 0 ? ExponentialBaseFactor : -ExponentialBaseFactor;

            while (elapsedTime <= transitionDuration)
            {
                // Normalize elapsed time to a range [0, 1] for interpolation.
                // normalizedTime = elapsedTime (current progress) / transitionDuration (total transition duration)
                var normalizedTime = elapsedTime / transitionDuration;

                float interpolatedValue = Utility.Interpolation.LogarithmicLerp(initialValue, finalValue, normalizedTime,
                    exponentialFactor, TimeScalar);

                // Apply the interpolated value to the material
                Color temp_color = _textObj.color;
                temp_color.a = interpolatedValue;
                _textObj.color = temp_color;

                // Wait for the next frame
                yield return null;

                // Update elapsed time
                elapsedTime = Time.time - startTime;
            }

            // Ensure the material ends with the exact target color
            yield return finalValue;
            
            fadeInCoroutine = null;
            fadeOutCoroutine = null;
        }
    }
}