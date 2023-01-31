using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour, ICollidable
{
    public UnityEvent gameOverEvent, deathTriggerEvent;
    
    public EnemyData enemyData;
    public Rigidbody enemyRB;
    public Vector3Data playerV3;
    
    private ICollidable collidable;
    private bool canRun, gameOver;
    private float knockBackPower, speed, knockBackResistance;
    private Vector3 moveDirection, playerLocation, currentLocation;

    private WaitForFixedUpdate wffuObj= new WaitForFixedUpdate();
    
    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody>();

        currentLocation = enemyRB.position;
        speed = enemyData.speed;
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
                enemyRB.position = new Vector3(currentLocation.x, 0, currentLocation.z);
            }
            
            SetCurrentV3();
            
            yield return wffuObj;
        }
        GameOverCheck();
    }

    private void SetCurrentV3()
    {
        currentLocation = enemyRB.position;
    }

    private void GameOverCheck()
    {
        if (gameOver)
        {
            StopCoroutine(Pursuit());
            gameOverEvent.Invoke();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        knockBackPower = enemyData.knockBackPower;
        collidable = other.collider.GetComponent<ICollidable>();
        collidable?.KnockBack(knockBackPower, currentLocation);
    }

    public void KnockBack(float amount, Vector3 otherObjVector3)
    {
        knockBackResistance = enemyData.knockBackResistance;
        enemyRB.AddForce((currentLocation - otherObjVector3) * (amount - knockBackResistance), ForceMode.Impulse);
    }
}

