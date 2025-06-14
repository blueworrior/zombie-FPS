using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;        // Drag your zombie prefab here
    public Transform[] spawnPoints;        // Points where zombies can appear
    public float spawnInterval = 5f;       // Time between spawns
    public int maxZombies = 10;            // Limit zombies in the scene

    private int currentZombies = 0;

    void Start()
    {
        InvokeRepeating("SpawnZombie", 2f, spawnInterval);
    }

    void SpawnZombie()
    {
        if (currentZombies >= maxZombies)
            return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
        currentZombies++;

        // Destroy after some time (optional), or manage count manually
        // Destroy(zombie, 30f);
    }
}
