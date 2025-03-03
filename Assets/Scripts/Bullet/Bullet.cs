using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 30f; 
    public int damage = 30;

    public GameObject splitBulletPrefab; 
    public int splitCount = 1; 
    public float spreadAngle = 30f; 


    public int parallelCount = 1; 
    public float parallelSpacing = 0.5f; 


    public int burstCount = 1; 
    public float burstInterval = 0.1f;   
    
    private Vector3 targetPosition;  

    public void Initialize(Vector3 target)
    {
        targetPosition = target;
        Vector3 direction = (targetPosition - transform.position).normalized;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; 
        }
    }

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zombie"))
        {
            Zombie zombie = other.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
            SplitIntoSmallBullets();
            Destroy(gameObject); 
        }
    }

    private void SplitIntoSmallBullets()
    {
        if (splitCount < 2 || splitBulletPrefab == null) return;

        float startAngle = -spreadAngle / 2f;
        float angleStep = spreadAngle / (splitCount - 1);

        for (int i = 0; i < splitCount; i++)
        {
            float angleOffset = startAngle + (angleStep * i);
            Quaternion rotation = Quaternion.Euler(0, 0, angleOffset);
            CreateBullet(transform.position, rotation);
        }
    }

    public void FireBullet(Vector3 position, Quaternion rotation)
    {
        StartCoroutine(BurstFire(position, rotation));
    }

    private IEnumerator BurstFire(Vector3 position, Quaternion rotation)
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
        GameObject bullet = Instantiate(splitBulletPrefab, position, rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = rotation * Vector2.right;
            rb.linearVelocity = direction * speed;
        }
    }
}
