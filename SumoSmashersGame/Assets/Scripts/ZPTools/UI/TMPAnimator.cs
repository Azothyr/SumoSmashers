using System.Collections;
using UnityEngine;

namespace ZPTools.UI
{
    public class TMPAnimator : TMPBehaviorBase
    {
        public bool pulseOnStart;
    
        private bool _isPulsing;
        
        /*
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

        private IEnumerator PulseAnimation(float duration)
        {
            var alphaOnValue = !Mathf.Approximately(_textObj.color.a, 1f) ? _textObj.color.a : 1f;
            var alphaOffValue = 0f;
            var durationHalf = duration / 2f;
        
            while (_isPulsing)
            {
                // Fade in
                _textObj.color.a = HandleInterpolation(alphaOnValue, alphaOffValue);
                yield return new WaitForSeconds(durationHalf);
            
                // Fade out
                _textObj.CrossFadeAlpha(alphaStart, durationHalf, false);
                yield return new WaitForSeconds(durationHalf);
            }
        
            _textObj.CrossFadeAlpha(1f, 0f, false);
        }
        
        private IEnumerator HandleInterpolation(out float updateValue, float initialValue, float finalValue, float transitionDuration, int transitionType)
        {
            Debug.Log($"[DEBUG] Starting color transition at {Time.time}. Initial Value: {initialValue}, Final Value: {finalValue}", this);
            
            var startTime = Time.time;
            var elapsedTime = 0f;


            // Determine exponential factor based on transition type (In: positive -> growth, Out: negative -> decay)
            const float timeScalar = 2f;
            const float exponentialBaseFactor = 3f;
            var exponentialFactor = transitionType == 0 ? exponentialBaseFactor : -exponentialBaseFactor;

            Debug.LogWarning($"[WARNING] Exponential factor: {exponentialFactor} Performing Exponential " +
                             $"{(exponentialFactor <= 0 ? "Decay" : "Growth")}", this);

            var debugSpacer = 0;
            const int debugMod = 30;

            while (elapsedTime <= transitionDuration)
            {
                // Normalize elapsed time to a range [0, 1] for interpolation.
                // normalizedTime = elapsedTime (current progress) / transitionDuration (total transition duration)
                var normalizedTime = elapsedTime / transitionDuration;
                
                float interpolatedValue = Utility.Interpolation.LogarithmicLerp(initialValue, finalValue, normalizedTime, exponentialFactor, timeScalar, false);

                // Compute interpolated color using logarithmic lerp for each channel
                if (debugSpacer++ % debugMod == 0)
                {
                    Debug.Log($"[DEBUG] Time: {Time.time}, Elapsed: {elapsedTime} / {transitionDuration}, " +
                              $"Normalized: {normalizedTime}, Interpolated Value: {interpolatedValue}", this);
                }
                
                // Apply the interpolated color to the material
                yield return interpolatedValue;

                // Wait for the next frame
                yield return null;

                // Update elapsed time
                elapsedTime = Time.time - startTime;
            }

            // Ensure the material ends with the exact target color
            yield return finalValue;

            Debug.Log($"[DEBUG] Color transition completed at {Time.time}. Total Time: {elapsedTime}, Final Color: {endColor}", this);
        }
        */
    }
}