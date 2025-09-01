using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CoroutineBehavior : MonoBehaviour
{
    public UnityEvent startEvent, startCountEvent,repeatCountEvent, endCountEvent;
    
    [SerializeField] private IntData counterNum;
    private const float Second = 1.0f;
    private WaitForSeconds wfsObj;

    public void Start()
    {
        wfsObj = new WaitForSeconds(Second);
        startEvent.Invoke();
    }

    public void StartCounting()
    {
        StartCoroutine(Counting());
    }
    
    private IEnumerator Counting()
    {
        startCountEvent.Invoke();
        while (counterNum.value > 0)
        {
            repeatCountEvent.Invoke();
            counterNum.value--;
            yield return wfsObj;
        }
        endCountEvent.Invoke();
    }
}
