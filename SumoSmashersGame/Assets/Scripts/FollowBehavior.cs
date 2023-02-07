using System.Collections;
using UnityEngine;

public class FollowBehavior : MonoBehaviour
{
    public GameObject player;
    private Vector3 initialPosition;
    
    private WaitForFixedUpdate wffuObj = new WaitForFixedUpdate();

    private void Awake()
    {
        initialPosition = transform.position;
        StartCoroutine(FollowPlayer(initialPosition));
    }

    private IEnumerator FollowPlayer(Vector3 offsetDistance)
    {
        while(player)
        {
            transform.position = offsetDistance + player.transform.position;
            yield return wffuObj;
        }
    }
}
