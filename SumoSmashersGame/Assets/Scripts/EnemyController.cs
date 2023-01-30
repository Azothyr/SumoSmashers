using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public UnityEvent gameOverEvent;
    
    private bool canRun;
    private float damage, health, speed;
    private Vector2 moveDirection;
    private Vector3 playerLocation;
    

    public EnemyData enemyData;
    public NavAgentBehaviour navAgentBehaviour;
    
    
    private WaitForFixedUpdate wffuObj= new WaitForFixedUpdate();
    private void Awake()
    {
        navAgentBehaviour = GetComponent<NavAgentBehaviour>();
        
        speed = enemyData.speed;
        
        navAgentBehaviour.SetMovementVariables(speed);
        StartPursuit();
    }
    
    public void StartPursuit()
    {
        StartCoroutine(Pursuit());
    }
    
    private IEnumerator Pursuit()
    {
        while (enemyData.canRun.value)
        { 
            navAgentBehaviour.SetV3ToPlayer();
            
            yield return wffuObj;
        }
        navAgentBehaviour.SetToCurrentLocation();
        GameOverCheck();
    }

    private void GameOverCheck()
    {
        if (!enemyData.canRun.value)
        {
            StopCoroutine(Pursuit());
            gameOverEvent.Invoke();
        }
    }
}

