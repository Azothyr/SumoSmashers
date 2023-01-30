using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public UnityEvent deathEvent;
    
    public PlayerData playerData;
    public Rigidbody playerRB;
    public Vector3 velocity;

    private InputActions inputActions;
    private Vector3 moveDirection, currentLocation;
    private Quaternion skew;
    private Vector2 inputVector;

    private float speed, knockbackPower, negativeTopSpeed;

    [SerializeField] float topSpeed;
    
    private WaitForFixedUpdate wffuObj = new WaitForFixedUpdate();

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        
        inputActions = new InputActions();
        
        inputActions.Player.Enable();
        
        inputActions.Player.Touch.performed += StartTouch;
        //inputActions.Player.Touch.canceled += StopTouch;
        
        inputActions.Player.Move.performed += StartMove;
        inputActions.Player.Move.canceled += StopMove;
        
        
        speed = (float) (playerData.speed * 0.1);
        negativeTopSpeed = topSpeed * -1;
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        print(inputVector);
    }
    
    private void StartMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        StartCoroutine(Move());
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        StopCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (playerData.canRun.value)
        {
            moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
            skew = Quaternion.Euler(new Vector3(0, -45, 0));
            playerRB.AddForce(skew * (moveDirection * speed));

            if (playerRB.velocity.x > topSpeed)
            {
                playerRB.velocity = new Vector3(topSpeed, playerRB.velocity.y, playerRB.velocity.z);
            }
            
            if (playerRB.velocity.x < negativeTopSpeed)
            {
                playerRB.velocity = new Vector3(negativeTopSpeed, playerRB.velocity.y, playerRB.velocity.z);
            }
            
            if (playerRB.position.y > 0)
            {
                playerRB.position = new Vector3(playerRB.position.x, 0, playerRB.position.z);
            }
            
            if (playerRB.velocity.z > topSpeed)
            {
                playerRB.velocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, topSpeed);
            }
            
            if (playerRB.velocity.z < negativeTopSpeed)
            {
                playerRB.velocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, negativeTopSpeed);
            }
            
            velocity = playerRB.velocity;
            
            yield return wffuObj;
        }
    }

    private void SetCurrentV3()
    {
        currentLocation = playerRB.position;
        playerData.v3Position.SetValue(currentLocation.x, currentLocation.y, currentLocation.z);
    }

    public void GameOverEvent()
    {
        deathEvent.Invoke();
    }
}

