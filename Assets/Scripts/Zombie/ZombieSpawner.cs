

using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("References")]
    public Player player;                    // Drag your Player GameObject here
    public GameObject meleeZombiePrefab;
    public GameObject rangedZombiePrefab;
    public GameObject explodingZombiePrefab;

    [Header("Spawn Settings")]
    public float baseSpawnInterval = 5f;     // Seconds between spawns at level 1
    public Vector2 spawnRangeX = new Vector2(-4f, 4f);
    public float spawnY = 10f;               // Y position for all zombies

    private float spawnTimer;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        // interval shrinks 10% per level, min 1 second
        float lvl      = player != null ? player.playerLevel : 1;
        float interval = Mathf.Max(1f, baseSpawnInterval / (1f + lvl * 0.1f));

        if (spawnTimer >= interval)
        {
            spawnTimer -= interval;
            if (lvl < 4) 
                SpawnSingle();
            else if (lvl >= 4 && lvl < 7)
                SpawnWave(1,2);
            else if (lvl >= 7 && lvl < 10)
                SpawnWave(1,3);
            else if (lvl >= 10 && lvl < 15)
                SpawnWave(1,4);   // multiple spawns at random Xs
            else
                SpawnWave(2, 4); // single spawn
        }
    }

    private void SpawnSingle()
    {
        var prefab = ChoosePrefab();
        Vector3 pos = RandomPosition();
        var go = Instantiate(prefab, pos, Quaternion.identity);
        ScaleStats(go.GetComponent<Zombie>());
    }

    private void SpawnWave(int minCount, int maxCount)
    {
        // choose between 2 and 4 zombies
        int count = Random.Range(1, 3);

        for (int i = 0; i < count; i++)
        {
            var prefab = ChoosePrefab();
            Vector3 pos = RandomPosition(); // fully random each time
            var go = Instantiate(prefab, pos, Quaternion.identity);
            ScaleStats(go.GetComponent<Zombie>());
        }
    }

    private GameObject ChoosePrefab()
    {
        float r   = Random.value;
        int lvl   = player != null ? player.playerLevel : 1;

        if (lvl < 1)
            return meleeZombiePrefab;
        else if (lvl < 2)
            return (r < 0.5f) ? meleeZombiePrefab : rangedZombiePrefab;
        else
            return (r < 0.4f) ? meleeZombiePrefab
                 : (r < 0.8f) ? rangedZombiePrefab
                 : explodingZombiePrefab;
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(
            Random.Range(spawnRangeX.x, spawnRangeX.y),
            spawnY,
            0f
        );
    }

    private void ScaleStats(Zombie z)
    {
        if (z == null || player == null) return;
        int lvl = player.playerLevel;

        // HP +10% per level
        float hpMult = 1f + lvl * 0.1f;
        z.hp        = Mathf.RoundToInt(z.hp * hpMult);
        z.SendMessage("UpdateHealthBar", SendMessageOptions.DontRequireReceiver);

        // Speed +5% per level
        float speedMult = 1f + lvl * 0.05f;
        if (z is MeleeZombie mz)
        {
            mz.moveSpeed *= speedMult;
        }
        else if (z is RangedZombie rz)
        {
            rz.moveSpeed     *= speedMult;
            rz.shootCooldown /= speedMult;
        }
        else if (z is ExplodingZombie ez)
        {
            ez.moveSpeed       *= speedMult;
            ez.explosionRadius *= 1f + lvl * 0.02f;
            ez.explosionDamage = Mathf.RoundToInt(ez.explosionDamage * hpMult);
        }
    }
}

