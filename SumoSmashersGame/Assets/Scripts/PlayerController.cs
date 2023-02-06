using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, ICollidable
{
    public UnityEvent deathEvent;
    
    public PlayerData playerData;
    public Vector3 velocity;
    private ICollidable collidable;

    private Rigidbody playerRB;
    private InputActions inputActions;
    private Vector3 inputDirection, moveDirection, currentLocation;
    private Quaternion skew = Quaternion.Euler(new Vector3(0, -45, 0));
    private Vector2 inputVector;

    private float speed, knockBackPower, knockBackResistance, negativeTopSpeed;

    [SerializeField] float topSpeed;
    
    private WaitForFixedUpdate wffuObj = new WaitForFixedUpdate();

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        SetCurrentV3();
        AllowMovement();
        
        inputActions = new InputActions();
        
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += StartMoveInput;
        inputActions.Player.Move.canceled += StopMove;
        
        speed = playerData.speed;
        negativeTopSpeed = topSpeed * -1;
        knockBackResistance = playerData.knockBackResistance;
    }
    
    private void StartMoveInput(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    public void AllowMovement()
    {
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
            inputDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;
            moveDirection = (skew * inputDirection);
            playerRB.AddForce(moveDirection * speed, ForceMode.Acceleration);

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

            SetCurrentV3();
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
    
    private void OnCollisionEnter(Collision other)
    {
        knockBackPower = playerData.knockBackPower;
        collidable = other.collider.GetComponent<ICollidable>();
        collidable?.KnockBack(knockBackPower, currentLocation);
    }

    public void KnockBack(float amount, Vector3 otherObjVector3)
    {
        playerRB.AddForce((currentLocation - otherObjVector3) * (amount - knockBackResistance), ForceMode.Impulse);
    }
}

