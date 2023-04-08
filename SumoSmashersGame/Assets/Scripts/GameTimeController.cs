using UnityEngine;
using UnityEngine.Events;

public class GameTimeController : MonoBehaviour
{
    public UnityEvent onPauseEvent, onUnpauseEvent;
    public void Pause()
    {
        Time.timeScale = 0;
        onPauseEvent.Invoke();
    }
    
    public void Unpause()
    {
        Time.timeScale = 1;
        onUnpauseEvent.Invoke();
    }
}
