using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private int waveCount = 30;
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
        if (waveCount > 20 && waveCount <= 30){
            zombiePrefab = meleeZombiePrefab;
            waveCount -= 1;
        }
        else if (waveCount > 10){
            spawnInterval = 3f;
            zombiePrefab = explodingZombiePrefab;
            waveCount -= 1;
        }
        else if (waveCount > 0){
            spawnInterval = 2f;
            zombiePrefab = rangedZombiePrefab;
            waveCount -= 1;
        }
        else{
            spawnInterval = 1f;
            int zombieType = Random.Range(0, 3);
            zombiePrefab = (zombieType == 0) ? meleeZombiePrefab :
                          (zombieType == 1) ? rangedZombiePrefab :
                          explodingZombiePrefab;
        }
        Debug.Log(" wavecount == " + waveCount);
        if (waveCount == 29){
            playerManager.ShowPopup("Exploding zombies have appeared!!!");}
        else if (waveCount == 9){
            playerManager.ShowPopup("Shooting zombies have appeared!!!");}
        else if (waveCount == 0){
            playerManager.ShowPopup("Mix zombies have appeared!!!");}
        

        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    }
}
