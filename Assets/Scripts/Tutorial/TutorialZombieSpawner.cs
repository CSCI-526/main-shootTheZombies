using UnityEngine;

public class TutorialZombieSpawner : MonoBehaviour
{
    public GameObject tutorialMeleeZombiePrefab;
    public GameObject tutorialRangedZombiePrefab;
    public GameObject tutorialExplodingZombiePrefab;
    //public float spawnDelay = 5f;
    public Vector2 spawnRangeX = new Vector2(-4f, 4f);
    public float fixedY = 10f;
    private float timeSinceLastSpawn = 0f;
    public float spawnInterval = 10f; // Time between spawns
    private int spawnStep = 0;
    //private bool hasSpawned = false;

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnNextZombie();
            timeSinceLastSpawn = 0f;
        }
        // if (!hasSpawned)
        // {
        //     hasSpawned = true;
        //     Invoke("SpawnNextZombie", spawnDelay);
        // }
    }

    private void SpawnNextZombie()
    {
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        Vector3 spawnPosition = new Vector3(randomX, fixedY, 0f);
        GameObject zombiePrefab;

        switch (spawnStep)
        {
            case 0:
                zombiePrefab = tutorialMeleeZombiePrefab;  // Red
                break;
            case 1:
                zombiePrefab = tutorialExplodingZombiePrefab;  // Blue
                break;
            case 2:
                zombiePrefab = tutorialRangedZombiePrefab;  // Green
                break;
            default:
                return;  // End of tutorial spawns
        }

        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
        spawnStep++;
        //hasSpawned = false;
    }
}
