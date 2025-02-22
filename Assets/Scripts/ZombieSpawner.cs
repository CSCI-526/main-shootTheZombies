using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; 
    public float spawnInterval = 2f; 
    public Vector2 spawnRangeX = new Vector2(-4f, 4f);  
    public float fixedY = 10f;  

    private float timeSinceLastSpawn = 0f;  
    private Vector3 spawnStartPos;  

    private void Start()
    {
        spawnStartPos = transform.position;
    }

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

        float randomY = fixedY;

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    }
}
