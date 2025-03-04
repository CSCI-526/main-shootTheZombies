using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public BulletSpawner bulletSpawner;
    public Bullet bulletPrefab;

    // BulletSpawner
    public float newFireRate = 5f;
    public int newBurstCount = 3;
    public int newParallelCount = 2;

    // Bullet
    public int newDamage = 20;
    public int newSplitCount = 5;

    void Update()
    {
        ModifyBulletSpawnerProperties();
        ModifyBulletProperties();
    }

    void ModifyBulletSpawnerProperties()
    {
        if (bulletSpawner != null)
        {
            bulletSpawner.fireRate = newFireRate;
            bulletSpawner.burstCount = newBurstCount;
            bulletSpawner.parallelCount = newParallelCount;
        }
    }

    void ModifyBulletProperties()
    {
        if (bulletPrefab != null)
        {
            bulletPrefab.damage = newDamage;
            bulletPrefab.splitCount = newSplitCount;
        }
    }
}

