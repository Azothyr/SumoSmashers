using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0;
    }
    
    public void Unpause()
    {
        Time.timeScale = 1;
    }
}
