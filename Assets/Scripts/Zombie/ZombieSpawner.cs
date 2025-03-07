using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject meleeZombiePrefab;
    public GameObject rangedZombiePrefab;
    public GameObject explodingZombiePrefab;
    public float spawnInterval = 2f; // Time between spawns
    public Vector2 spawnRangeX = new Vector2(-4f, 4f); // X-axis spawn range
    public float fixedY = 10f; // Y position for spawning

    private float timeSinceLastSpawn = 0f;

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnZombie();
            timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnZombie()
    {
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        Vector3 spawnPosition = new Vector3(randomX, fixedY, 0f);

        // Randomly select a zombie type
        int zombieType = Random.Range(0, 3);
        GameObject zombiePrefab = (zombieType == 0) ? meleeZombiePrefab :
                                  (zombieType == 1) ? rangedZombiePrefab :
                                  explodingZombiePrefab;

        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    }
}
