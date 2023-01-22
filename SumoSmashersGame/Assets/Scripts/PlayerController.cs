using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public UnityEvent deathEvent;
    
    public PlayerData playerData;
    public CharacterController playerCharacterController;

    private InputActions inputActions;
    private Vector3 moveDirection;
    private Vector2 inputVector;
    private Vector2 currentLocation;

    private float speed;
    private float knockbackPower;
    
    private WaitForFixedUpdate wffuObj = new WaitForFixedUpdate();

    private void Awake()
    {
        playerCharacterController = GetComponent<CharacterController>();
        
        inputActions = new InputActions();
        
        inputActions.Player.Enable();
        
        inputActions.Player.Move.performed += StartMove;
        inputActions.Player.Move.canceled += StopMove;
        
        speed = playerData.speed;
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
            moveDirection = new Vector3(inputVector.x, 0, inputVector.y)*Time.deltaTime;
            playerCharacterController.Move(moveDirection);
            //currentLocation = playerRB.position;
            //playerData.v3Position.SetValue(currentLocation.x, currentLocation.y, 0);
            //playerRB.MovePosition(currentLocation + inputMoveVector * (playerData.speed * Time.deltaTime));
            yield return wffuObj;
        }
    }

    public void SetCurrentV3()
    {
        //currentLocation = playerRB.position;
        currentLocation = playerCharacterController.center;
        playerData.v3Position.SetValue(currentLocation.x, currentLocation.y, 0);
    }

    private void GameOver()
    {
        playerData.gameOver.SetTrue();
        deathEvent.Invoke();
    }
}

