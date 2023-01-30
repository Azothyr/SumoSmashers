using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    public UnityEvent gameOverEvent, deathTriggerEvent;
    
    public EnemyData enemyData;
    public Rigidbody enemyRB;
    public Vector3Data playerV3;

    private bool canRun, gameOver;
    private float knockbackPower, speed;
    private Vector3 moveDirection, playerLocation;

    private WaitForFixedUpdate wffuObj= new WaitForFixedUpdate();
    
    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody>();
        
        speed = enemyData.speed;
        knockbackPower = enemyData.knockbackPower;
        gameOver = enemyData.gameOver.value;
        
        StartPursuit();
    }
    
    public void StartPursuit()
    {
        StartCoroutine(Pursuit());
    }
    
    public void TriggerDeath()
    {
        deathTriggerEvent.Invoke();
    }
    
    private IEnumerator Pursuit()
    {
        while (enemyData.canRun.value)
        { 
            playerLocation = playerV3.value;
            moveDirection = (playerLocation - transform.position).normalized;
            enemyRB.AddForce(moveDirection * speed, ForceMode.Acceleration);

            if (enemyRB.position.y > 0)
            {
                enemyRB.position = new Vector3(enemyRB.position.x, 0, enemyRB.position.z);
            }
            
            yield return wffuObj;
        }
        GameOverCheck();
    }

    private void GameOverCheck()
    {
        if (gameOver)
        {
            StopCoroutine(Pursuit());
            gameOverEvent.Invoke();
        }
    }
}

