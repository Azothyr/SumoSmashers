using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(DestroyBehavior))]
public abstract class ControllerBase : MonoBehaviour, ICollidable
{
    public UnityEvent deathTriggerEvent;

    public CharacterData controllerData;
    public Rigidbody rigidBody;

    private ICollidable collidable;
    protected float speed, topSpeed, knockBackPower, knockBackResistance;
    protected Vector3 moveDirection, currentLocation;
    protected WaitForFixedUpdate wffuObj = new WaitForFixedUpdate();

    protected abstract void Awake();
    
    public void StartMovement()
    {
        StartCoroutine(Move());
    }

    public void StopMovement()
    {
        StopCoroutine(Move());
    }

    protected abstract IEnumerator Move();

    protected virtual void SetCurrentV3()
    {
        currentLocation = rigidBody.position;
    }
    
    public virtual void OnCollisionEnter(Collision other)
    {
        knockBackPower = controllerData.knockBackPower;
        collidable = other.collider.GetComponent<ICollidable>();
        collidable?.KnockBack(knockBackPower, currentLocation);
    }

    public virtual void KnockBack(float amount, Vector3 otherObjVector3)
    {
        knockBackResistance = controllerData.knockBackResistance;
        rigidBody.AddForce((currentLocation - otherObjVector3) * (amount - knockBackResistance), ForceMode.Impulse);
    }
    
    public void TriggerDeathEvent()
    {
        deathTriggerEvent.Invoke();
    }
    
    public void Pause()
    {
        
    }
    
    public void UnPause()
    {
        
    }
}
