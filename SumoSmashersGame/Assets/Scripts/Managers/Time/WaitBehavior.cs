using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WaitBehavior : MonoBehaviour
{
    [SerializeField] private string purpose;
    public UnityEvent endWaitForSeconds, endWaitForZero;
    
    private WaitForSeconds wfsObj = new(1);
    private WaitForFixedUpdate wffuObj = new();

    public void startWaitForSecondsEvent(int seconds)
    {
        StartCoroutine(WaitForSecondsEvent(seconds));
    }

    public void startWaitForZeroIntDataEvent(IntData data)
    {
        StartCoroutine(WaitForZeroIntDataEvent(data));
    }
    
    private IEnumerator WaitForZeroIntDataEvent(IntData obj)
    {
        var waitAmount = obj.value;
        
        while (waitAmount > 0)
        {
            waitAmount = obj.value;
            yield return wffuObj;
        }
        endWaitForZero.Invoke();
    }
    
    private IEnumerator WaitForSecondsEvent(int num)
    {
        var waitAmount = num;
        
        while (waitAmount > 0)
        {
            waitAmount--;
            yield return wfsObj;
        }
        endWaitForSeconds.Invoke();
    }
}
