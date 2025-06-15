using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Zombie Prefabs")]
    public GameObject[] zombiePrefabs; // Assign multiple zombies in Inspector

    [Header("Spawning Settings")]
    public Transform[] spawnPoints; // Points where zombies can spawn
    public float spawnInterval = 5f; // Time between spawns

    void Start()
    {
        InvokeRepeating("SpawnZombie", 2f, spawnInterval);
    }

    void SpawnZombie()
    {
        if (zombiePrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Assign zombiePrefabs and spawnPoints in the Inspector!");
            return;
        }

        // Pick a random zombie
        int zombieIndex = Random.Range(0, zombiePrefabs.Length);
        GameObject selectedZombie = zombiePrefabs[zombieIndex];

        // Pick a random spawn point
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Instantiate the selected zombie at the chosen spawn point
        Instantiate(selectedZombie, spawnPoint.position, spawnPoint.rotation);
    }
}
    