using UnityEngine;
using UnityEngine.Events;

public class SimpleSpawnBehavior : MonoBehaviour
{
    public Instancer instancer;
    public Vector3 spawnLocation;
    
    void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        spawnLocation = new Vector3(5, 0, -5);
        Debug.Log("instancing");
        instancer.CreateInstance(spawnLocation);
    }
}
