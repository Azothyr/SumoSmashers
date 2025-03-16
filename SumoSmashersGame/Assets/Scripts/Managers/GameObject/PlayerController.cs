using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : ControllerBase
{
    [SerializeField] private Vector3 velocity;

    public Vector3Data playerV3;
    private InputActions inputActions;
    private Quaternion skew = Quaternion.Euler(new Vector3(0, -45, 0));
    private Vector2 inputVector;
    private Vector3 inputDirection;
    private float negativeTopSpeed;

    protected override void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        SetCurrentV3();
        
        inputActions = new InputActions();
        
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += GetMoveInput;
        inputActions.Player.Move.canceled += StopMovement;
        
        speed = controllerData.speed.value;
        topSpeed = controllerData.topSpeed.value;
        negativeTopSpeed = topSpeed * -1;
        knockBackResistance = controllerData.knockBackResistance;
    }
    
    private void GetMoveInput(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    private void StopMovement(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        StopCoroutine(Move());
    }

    protected override IEnumerator Move()
    {
        while (controllerData.canRun.value)
        {
            inputDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;
            moveDirection = (skew * inputDirection);
            rigidBody.AddForce(moveDirection * speed, ForceMode.Acceleration);

            if (rigidBody.linearVelocity.x > topSpeed)
            {
                rigidBody.linearVelocity = new Vector3(topSpeed, rigidBody.linearVelocity.y, rigidBody.linearVelocity.z);
            }

            if (rigidBody.linearVelocity.x < negativeTopSpeed)
            {
                rigidBody.linearVelocity = new Vector3(negativeTopSpeed, rigidBody.linearVelocity.y, rigidBody.linearVelocity.z);
            }

            if (rigidBody.position.y > 0)
            {
                rigidBody.position = new Vector3(rigidBody.position.x, 0, rigidBody.position.z);
            }

            if (rigidBody.linearVelocity.z > topSpeed)
            {
                rigidBody.linearVelocity = new Vector3(rigidBody.linearVelocity.x, rigidBody.linearVelocity.y, topSpeed);
            }

            if (rigidBody.linearVelocity.z < negativeTopSpeed)
            {
                rigidBody.linearVelocity = new Vector3(rigidBody.linearVelocity.x, rigidBody.linearVelocity.y, negativeTopSpeed);
            }

            velocity = rigidBody.linearVelocity;

            SetCurrentV3();
            yield return wffuObj;
        }
    }

    protected override void SetCurrentV3()
    {
        currentLocation = rigidBody.position;
        playerV3.SetValue(currentLocation.x, currentLocation.y, currentLocation.z);
    }
}

