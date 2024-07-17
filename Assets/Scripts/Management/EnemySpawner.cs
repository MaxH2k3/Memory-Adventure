using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // The object you want to spawn
    public int numberOfObjects = 10; // The number of objects to spawn
    public float spawnRadius = 10f; // The radius within which objects will be spawned

    private void Start()
    {
        if(objectsToSpawn.Length != 0)
        {
            SpawnObjects();
        }
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            GameObject objectToSpawn = GetRandomObject();
            Vector3 randomPosition = GetRandomPosition();
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection += transform.position;
        randomDirection.y = 0f; // Set the y-coordinate to 0 to spawn objects on the same plane

        // Check for overlap with existing objects
        Collider[] colliders = Physics.OverlapSphere(randomDirection, 3f); // Adjust the sphere radius as needed

        if (colliders.Length > 0)
        {
            // There is an overlap, generate a new random position
            return GetRandomPosition();
        }

        return randomDirection;
    }

    private GameObject GetRandomObject()
    {
        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        return objectsToSpawn[randomIndex];
    }
}
