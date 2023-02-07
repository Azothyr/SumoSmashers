using UnityEngine;

public class DestroyBehavior : MonoBehaviour
{
    private float seconds;

    public void TriggerDestroy()
    {
        DestroyImmediate(gameObject, true);
    }
}
