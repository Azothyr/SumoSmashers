using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic; 

[System.Serializable] // This makes GameActionEvent visible in the inspector.
public class GameActionEvent
{
    public GameAction actionObj;
    public UnityEvent onRaiseEvent;
}


public class GameActionHandler: MonoBehaviour
{
    // This list will allow you to add multiple GameActionEvent objects from the inspector.
    public List<GameActionEvent> gameActions;

    private void OnEnable()
    {
        // Subscribe to all the events in the list.
        foreach (var gameAction in gameActions)
        {
            gameAction.actionObj.raise += Raise;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from all the events in the list.
        foreach (var gameAction in gameActions)
        {
            gameAction.actionObj.raise -= Raise;
        }
    }

    private void Raise(GameAction callingObj)
    {
        // Invoke the corresponding UnityEvent for the raised GameAction.
        foreach (var gameAction in gameActions)
        {
            if (gameAction.actionObj == callingObj)
            {
                gameAction.onRaiseEvent.Invoke();
                break; // Once the correct action is found and invoked, no need to continue.
            }
        }
    }
}