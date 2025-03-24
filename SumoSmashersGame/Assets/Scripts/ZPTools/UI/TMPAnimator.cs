using System.Collections;
using UnityEngine;

public class TMPAnimator : TMPBehaviorBase
{
    public bool pulseOnStart;
    
    private bool _isPulsing = false;
    
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
        var alphaStart = _textObj.color.a;
        var durationHalf = duration / 2f;
        
        while (_isPulsing)
        {
            // Fade in
            Debug.Log("Fading in");
            _textObj.CrossFadeAlpha(1f, durationHalf, false);
            yield return new WaitForSeconds(durationHalf);
            
            // Fade out
            _textObj.CrossFadeAlpha(alphaStart, durationHalf, false);
            yield return new WaitForSeconds(durationHalf);
        }
        
        _textObj.CrossFadeAlpha(1f, 0f, false);
    }
}