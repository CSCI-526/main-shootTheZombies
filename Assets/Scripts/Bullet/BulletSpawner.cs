using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public float bulletSpeed = 10f; 
    public float fireRate = 1f; 
    private Zombie targetZombie; 

    public int splitCount = 2;   
    public float spreadAngle = 30f; 
    public int parallelCount = 3; 
    public float parallelSpacing = 0.5f; 
    public int burstCount = 2;
    public float burstInterval = 0.1f; 

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
            yield return new WaitForSeconds(1/fireRate); 
        }
    }

    void FireBullet(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized; 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // rotate
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle + 90));
        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        
        // 设置发射模式
        bulletScript.speed = bulletSpeed;
        bulletScript.splitCount = splitCount;
        bulletScript.spreadAngle = spreadAngle;
        bulletScript.parallelCount = parallelCount;
        bulletScript.parallelSpacing = parallelSpacing;
        bulletScript.burstCount = burstCount;
        bulletScript.burstInterval = burstInterval;

        bulletScript.Initialize(target);
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
