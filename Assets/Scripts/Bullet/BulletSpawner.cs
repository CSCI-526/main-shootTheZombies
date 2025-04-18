using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
    public GameObject bulletGuideLinePrefab;
    public bool showGuideInTutorial = false;
    public float maxTime = 5f;

    private Color bulletColor = Color.red;
    private float timer = 0f;
    private bool stop = false;

    void Start()
    {
        StartCoroutine(ShootCoroutine());
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            showGuideInTutorial = true;
        }
        else
        {
            showGuideInTutorial = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) bulletColor = Color.red;
        if (Input.GetKeyDown(KeyCode.W)) bulletColor = Color.green;
        if (Input.GetKeyDown(KeyCode.E)) bulletColor = Color.blue;
        if (timer < maxTime)
            timer += Time.deltaTime;
        else
            stop = true;
    }

    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Vector3 target = GetMouseWorldPosition();
            FireBullet(target);
            float adjustedInterval = Mathf.Pow(0.9f, fireRate - 1);
            yield return new WaitForSeconds(adjustedInterval);
        }
    }

    bool IsZombieVisibleOnScreen()
    {
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        Camera cam = Camera.main;

        foreach (Zombie zombie in zombies)
        {
            Vector3 viewPos = cam.WorldToViewportPoint(zombie.transform.position);
            if (viewPos.z > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
            {
                return true;
            }
        }

        return false;
    }

    void FireBullet(Vector3 target)
    {   
        if (showGuideInTutorial && bulletGuideLinePrefab != null && !stop)
        {
            GameObject guide = Instantiate(bulletGuideLinePrefab);
            BulletGuideLine guideScript = guide.GetComponent<BulletGuideLine>();
            guideScript.bulletStart = transform;
        }

        if (!IsZombieVisibleOnScreen())
            return;

        Vector3 direction = (target - transform.position).normalized; 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        // Debug.Log("Quaternion.Euler(0, 0, angle): " + Quaternion.Euler(0, 0, angle).eulerAngles);
        
        StartCoroutine(BurstFire(transform.position, Quaternion.Euler(0, 0, angle)));

        SendAccuracy.bulletsFired += 1;
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
            bulletScript.Initialize(position, bulletColor, null);
        }

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = rotation * Vector2.up;
            if ((direction.x / direction.y > 7.6/3.25 || direction.y < 0) && direction.x > 0)
            {
                direction.x = 7.6f;
                direction.y = 3.25f;
            }
            if ((direction.x / direction.y < -7.6/3.25 || direction.y <0 )&& direction.x < 0)
            {
                direction.x = -7.6f;
                direction.y = 3.25f;
            }
            direction.Normalize();
            rb.linearVelocity = direction * bulletSpeed;
        }

        SpriteRenderer sr = bullet.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = bulletColor;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    // Zombie FindZombieAtPosition(Vector3 position)
    // {
    //     float detectionRadius = 1.5f;
    //     Collider2D[] colliders = Physics2D.OverlapCircleAll(position, detectionRadius);

    //     foreach (Collider2D collider in colliders)
    //     {
    //         if (collider.CompareTag("Zombie"))
    //         {
    //             return collider.GetComponent<Zombie>();
    //         }
    //     }
    //     return null;
    // }

    // Zombie FindNearestZombie(Vector3 playerPosition)
    // {
    //     Zombie[] zombies = FindObjectsOfType<Zombie>();
    //     Zombie nearestZombie = null;
    //     float shortestDistance = Mathf.Infinity;

    //     foreach (Zombie zombie in zombies)
    //     {
    //         float distance = Vector3.Distance(playerPosition, zombie.transform.position);
    //         if (distance < shortestDistance)
    //         {
    //             shortestDistance = distance;
    //             nearestZombie = zombie;
    //         }
    //     }

    //     return nearestZombie;
    // }
}
