using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 1f;
    private Zombie targetZombie;

    public int burstCount = 1; 
    public float burstInterval = 0.1f; 
    public int parallelCount = 1; 
    public float parallelSpacing = 0.5f;

    void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            
            Zombie clickedZombie = FindZombieAtPosition(mousePosition);
            
            if (clickedZombie != null)
            {
                targetZombie = clickedZombie; 
            }
        }

        if (targetZombie == null || targetZombie.health <= 0)
        {
            targetZombie = FindNearestZombie(transform.position);
        }
    }

    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (targetZombie != null)
            {
                FireBullet(targetZombie.transform.position);
            }
            float adjustedInterval = Mathf.Pow(0.9f, fireRate - 1);
            yield return new WaitForSeconds(adjustedInterval); 
        }
    }

    void FireBullet(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized; 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        Debug.Log("Quaternion.Euler(0, 0, angle): " + Quaternion.Euler(0, 0, angle).eulerAngles);

        StartCoroutine(BurstFire(transform.position, Quaternion.Euler(0, 0, angle)));
    }

    IEnumerator BurstFire(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < burstCount; i++)
        {
            CreateParallelBullets(position, rotation);
            yield return new WaitForSeconds(burstInterval);
        }
    }

    private void CreateParallelBullets(Vector3 position, Quaternion rotation)
    {
        float offsetStart = -(parallelCount - 1) * parallelSpacing / 2f;

        for (int i = 0; i < parallelCount; i++)
        {
            Vector3 spawnPosition = position + new Vector3(offsetStart + i * parallelSpacing, 0, 0);
            CreateBullet(spawnPosition, rotation);
        }
    }

    private void CreateBullet(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetRotation(rotation);
        if (bulletScript != null)
        {
            bulletScript.Initialize(position, null);
        }

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = rotation * Vector2.up;
            rb.linearVelocity = direction * bulletSpeed;
        }
    }

    Zombie FindZombieAtPosition(Vector3 position)
    {
        float detectionRadius = 1.5f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Zombie"))
            {
                return collider.GetComponent<Zombie>();
            }
        }
        return null;
    }

    Zombie FindNearestZombie(Vector3 playerPosition)
    {
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        Zombie nearestZombie = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Zombie zombie in zombies)
        {
            float distance = Vector3.Distance(playerPosition, zombie.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestZombie = zombie;
            }
        }

        return nearestZombie;
    }
}
