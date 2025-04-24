using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private int waveCount = 40;
    public PlayerManagerScript playerManager;
    public GameObject meleeZombiePrefab;
    public GameObject rangedZombiePrefab;
    public GameObject explodingZombiePrefab;
    public float spawnInterval = 10f; // Time between spawns
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
        GameObject zombiePrefab;

        //for wave 1
        if (waveCount > 25 && waveCount <= 40){
            zombiePrefab = meleeZombiePrefab;
            waveCount -= 5;
        }
        else if (waveCount > 10){
            spawnInterval = 3f;
            zombiePrefab = explodingZombiePrefab;
            waveCount -= 5;
        }
        else if (waveCount > 0){
            spawnInterval = 5f;
            zombiePrefab = rangedZombiePrefab;
            waveCount -= 5;
        }
        else{
            spawnInterval = 3f;
            int zombieType = Random.Range(0, 3);
            zombiePrefab = (zombieType == 0) ? meleeZombiePrefab :
                          (zombieType == 1) ? rangedZombiePrefab :
                          explodingZombiePrefab;
        }
        Debug.Log(" wavecount == " + waveCount);
        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    }
}
