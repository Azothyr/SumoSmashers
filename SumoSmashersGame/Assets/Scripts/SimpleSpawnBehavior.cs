using UnityEngine;
using UnityEngine.Events;

public class SimpleSpawnBehavior : MonoBehaviour
{
    public Instancer instancer;
    public Vector3 spawnLocation;

    public void Spawn()
    {
        spawnLocation = new Vector3(-5, 0, 5);
        instancer.CreateInstance(spawnLocation);
    }
}
