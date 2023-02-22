using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : ControllerBase
{
    public Vector3Data playerV3;
    public UnityEvent onGameOverEvent;

    private Vector3 playerLocation;
    
    protected override void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        
        speed = controllerData.speed;
        
        StartMovement();
    }
    
    protected override IEnumerator Move()
    {
        while (controllerData.canRun.value)
        {
            SetCurrentV3();
            
            playerLocation = playerV3.value;
            moveDirection = (playerLocation - transform.position).normalized;
            rigidBody.AddForce(moveDirection * speed, ForceMode.Acceleration);

            if (rigidBody.position.y > 0)
            {
                rigidBody.position = new Vector3(currentLocation.x, 0, currentLocation.z);
            }

            yield return wffuObj;
        }
        GameOverCheck();
    }

    private void GameOverCheck()
    {
        if (controllerData.gameOver.value)
        {
            onGameOverEvent.Invoke();
        }
    }
}

