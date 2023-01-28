using System;
using System.Collections;
using UnityEngine;

public class CamBehavior : MonoBehaviour
{
    public GameObject player;
    private Vector3 initialCamPosition;
    
    private WaitForFixedUpdate wffuObj = new WaitForFixedUpdate();
    
    private void Awake()
    {
        initialCamPosition = transform.position;
        StartCoroutine(FollowPlayer(initialCamPosition));
    }

    private IEnumerator FollowPlayer(Vector3 camDistance)
    {
        while(true)
        {
            transform.position = camDistance + player.transform.position;
            yield return wffuObj;
        }
    }
}
