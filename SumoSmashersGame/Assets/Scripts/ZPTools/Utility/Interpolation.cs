using UnityEngine;

namespace ZPTools.Utility
{
    public class Interpolation : MonoBehaviour
    {
        private static float LogarithmicLerp(float start, float end, float normalizedTime, float exponentialFactor,
            float timeScale = 1f, bool performDebug = false)
        {
            // Clamp normalized time to [0, 1]
            normalizedTime = Mathf.Clamp01(normalizedTime);

            // Scale normalized time to adjust transition behavior
            float scaledTime = Mathf.Pow(normalizedTime, timeScale);

            // Calculate the exponential term
            float exponentialTerm;
            if (exponentialFactor > 0)
            {
                // Growth
                exponentialTerm = 1 - Mathf.Exp(-exponentialFactor * scaledTime);
            }
            else
            {
                // Decay
                exponentialTerm = (Mathf.Exp(exponentialFactor * scaledTime) - 1) * -1;
            }

            // Compute and return the interpolated value
            var result = start + (end - start) * exponentialTerm;
            if (performDebug) Debug.Log($"Result: {result}");
            return result;
        }
    }
}
