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
    
    protected ICollidable collidable;
    protected float knockBackPower, speed, knockBackResistance;
    protected Vector3 moveDirection, currentLocation;
    protected WaitForFixedUpdate wffuObj = new WaitForFixedUpdate();

    public abstract void Awake();

    protected void StartMovement()
    {
        StartCoroutine(Move());
    }

    protected void StopMovement()
    {
        StopCoroutine(Move());
    }

    protected abstract IEnumerator Move();
    
    public abstract void Pause();

    protected void SetCurrentV3()
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
}
